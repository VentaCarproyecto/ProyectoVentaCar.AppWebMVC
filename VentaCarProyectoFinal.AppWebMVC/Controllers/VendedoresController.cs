using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VentaCarProyectoFinal.AppWebMVC.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace VentaCarProyectoFinal.AppWebMVC.Controllers
{
    [Authorize(AuthenticationSchemes = "VendedorCookie")]
    public class VendedoresController : Controller
    {
        private readonly VentacarProyectContext _context;

        public VendedoresController(VentacarProyectContext context)
        {
            _context = context;
        }

        // GET: Vendedores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vendedores.ToListAsync());
        }

        // GET: Vendedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedore = await _context.Vendedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendedore == null)
            {
                return NotFound();
            }

            return View(vendedore);
        }

        // GET: Vendedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Telefono,Direccion,Email,Dui,Password,Role")] Vendedore vendedore)
        {
            if (ModelState.IsValid)
            {
                vendedore.Password = CalcularHashMD5(vendedore.Password);
                _context.Add(vendedore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendedore);
        }

        private string CalcularHashMD5(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public async Task<IActionResult> CerrarSession()
        {
            // Hola mundo
            await HttpContext.SignOutAsync("VendedorCookie");
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Vendedore vendedore, string returnUrl = null)
        {
            vendedore.Password = CalcularHashMD5(vendedore.Password);
            var usuarioAuth = await _context.
                Vendedores.
                FirstOrDefaultAsync(s => s.Email == vendedore.Email && s.Password == vendedore.Password);
            if (usuarioAuth != null && usuarioAuth.Id > 0 && usuarioAuth.Email == vendedore.Email)
            {
                var claims = new[] {
                    new Claim(ClaimTypes.Name, usuarioAuth.Email),
                    new Claim("Id", usuarioAuth.Id.ToString()),
                     new Claim("Nombre", usuarioAuth.Nombre),
                     new Claim(ClaimTypes.Role, usuarioAuth.Role)
                    };
                //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                //return RedirectToAction("Index", "Vendedores");
                var identity = new ClaimsIdentity(claims, "VendedorCookie");
                await HttpContext.SignInAsync("VendedorCookie", new ClaimsPrincipal(identity));
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "El email o contraseña estan incorrectos");
                return View();
            }
        }

        // GET: Vendedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedore = await _context.Vendedores.FindAsync(id);
            if (vendedore == null)
            {
                return NotFound();
            }
            return View(vendedore);
        }

        // POST: Vendedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Telefono,Direccion,Email,Dui")] Vendedore vendedore)
        {
            if (id != vendedore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendedore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendedoreExists(vendedore.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vendedore);
        }

        // GET: Vendedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendedore = await _context.Vendedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendedore == null)
            {
                return NotFound();
            }

            return View(vendedore);
        }

        // POST: Vendedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendedore = await _context.Vendedores.FindAsync(id);
            if (vendedore != null)
            {
                _context.Vendedores.Remove(vendedore);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendedoreExists(int id)
        {
            return _context.Vendedores.Any(e => e.Id == id);
        }

        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro([Bind("Email,Nombre,Apellido,Telefono,Dui,Direccion,Password")] Vendedore vendedore)
        {
            if (ModelState.IsValid)
            {
                // Asignar el rol por defecto "CLIENTE"
                vendedore.Role = "VENDEDOR";

                //  Encriptar la contraseña antes de guardarla
                vendedore.Password = CalcularHashMD5(vendedore.Password);

                //  Guardar el cliente en la base de datos
                _context.Vendedores.Add(vendedore);
                await _context.SaveChangesAsync();

                //  Redirigir al Login después del registro exitoso
                return RedirectToAction("Login", "Vendedores");
            }

            //  Si hay errores, recargar la vista con los datos ingresados
            return View(vendedore);
        }
    }
}
