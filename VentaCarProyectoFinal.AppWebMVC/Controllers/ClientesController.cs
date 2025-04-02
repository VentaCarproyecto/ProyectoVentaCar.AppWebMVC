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
    public class ClientesController : Controller
    {
        private readonly VentacarProyectContext _context;

        public ClientesController(VentacarProyectContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index(string searchUserName)
        {
            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchUserName))
            {
                query = query.Where(u => u.Nombre.Contains(searchUserName));
            }

            return View(await query.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Telefono,Direccion,Correo,Dui,Password")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Password = CalcularHashMD5(cliente.Password);
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
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
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl; // Guardamos returnUrl para pasarlo a la vista
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Cliente cliente, string returnUrl = null)
        {
            cliente.Password = CalcularHashMD5(cliente.Password); // Cifrar la contraseña

            var usuarioAuth = await _context.Clientes
                .FirstOrDefaultAsync(s => s.Correo == cliente.Correo && s.Password == cliente.Password);

            if (usuarioAuth != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuarioAuth.Correo),
            new Claim("Id", usuarioAuth.Id.ToString()),
            new Claim("Nombre", usuarioAuth.Nombre),
            new Claim(ClaimTypes.Role, usuarioAuth.Role)
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Verificar `returnUrl` antes de redirigir
                Console.WriteLine($"ReturnUrl recibido: {returnUrl}");

                // Si `returnUrl` es la misma página de login, ignórala
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl) && !returnUrl.Contains("Login"))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home"); // Si no hay returnUrl válido, va al Home
            }

            ModelState.AddModelError("", "El email o contraseña son incorrectos.");
            return View();
        }



        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Telefono,Direccion,Correo,Dui,Password")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }


        [Authorize]
        public IActionResult FormularioPago()
        {
            var usuarioId = User.FindFirst("Id")?.Value;
            var nombreUsuario = User.FindFirst("Nombre")?.Value;
            var rolUsuario = User.FindFirst(ClaimTypes.Role)?.Value; // Obtener el rol

            if (rolUsuario == "VENDEDOR")
            {
                ViewBag.Mensaje = "Los vendedores no pueden acceder al formulario de pago.";
                return View("Mensaje");
            }

            ViewBag.UsuarioId = usuarioId;
            ViewBag.NombreUsuario = nombreUsuario;
            return View();
        }
        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro([Bind("Correo,Nombre,Apellido,Telefono,Direccion,Password, Dui")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                // Asignar el rol por defecto "CLIENTE"
                cliente.Role = "CLIENTE";

                //  Encriptar la contraseña antes de guardarla
                cliente.Password = CalcularHashMD5(cliente.Password);

                //  Guardar el cliente en la base de datos
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                //  Redirigir al Login después del registro exitoso
                return RedirectToAction("Login", "Clientes");
            }

            //  Si hay errores, recargar la vista con los datos ingresados
            return View(cliente);
        }
    }
}
