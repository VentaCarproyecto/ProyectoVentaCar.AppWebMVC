using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VentaCarProyectoFinal.AppWebMVC.Models;

namespace VentaCarProyectoFinal.AppWebMVC.Controllers
{
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
                _context.Add(vendedore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendedore);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Telefono,Direccion,Email,Dui,Password,Role")] Vendedore vendedore)
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
    }
}
