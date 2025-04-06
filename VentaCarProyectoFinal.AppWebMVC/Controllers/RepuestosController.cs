using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VentaCarProyectoFinal.AppWebMVC.Models; 
using Microsoft.AspNetCore.Hosting;

namespace VentaCarProyectoFinal.AppWebMVC.Controllers
{
    public class RepuestosController : Controller
    {
        private readonly VentacarProyectContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RepuestosController(VentacarProyectContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Repuestos
        // El index lo use para mostrar la lista de repuestos activos
        public async Task<IActionResult> Index(Repuesto repuesto, int topRegistro = 10)
        {

            var proVentacarProyectContext = _context.Repuestos
                .Include(r => r.IdDepartamentoNavigation)
                .Include(r => r.IdVendedorNavigation)
               .Where(r => r.Actividad == 1);
            return View(await proVentacarProyectContext.ToListAsync());
        }

        // GET: Repuestos/Details/5
        // Son los detalles para mostrar un repuesto y Consulta a la base de datos para obtener el repuesto con el ID especificado
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuestos
                .Include(r => r.IdDepartamentoNavigation)
                .Include(r => r.IdVendedorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (repuesto == null)
            {
                return NotFound();
            }

            return View(repuesto);
        }


        // Este metodo se creo para la vista Detallescliente)
        public async Task<IActionResult> Detallescliente(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuestos
                .Include(r => r.IdDepartamentoNavigation)
                .Include(r => r.IdVendedorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (repuesto == null)
            {
                return NotFound();
            }

            return View(repuesto);
        }




        // Este método es para guardar una imagen en el servidor
        public async Task<string> GuardarImage(IFormFile? file, string url = "")
        {
            string urlImage = url;
            if (file != null && file.Length > 0)
            {
                string nameFile = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes", nameFile);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                urlImage = "/imagenes/" + nameFile;
            }
            return urlImage;
        }

        // GET: Repuestos/Create
        // Acción para mostrar el formulario de creación de un nuevo repuesto
        public IActionResult Create()
        {
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Departamento1");
            ViewData["IdVendedor"] = new SelectList(_context.Vendedores, "Id", "Nombre");
            return View();
        }

        // POST: Repuestos/Create
        // Acción para recibir los datos del formulario de creación y guardar el nuevo repuesto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreRepuesto,IdVendedor,IdDepartamento,ImgProducto,Compatiblilidad,DescripcionR,Proveniencia,EstadoRp,Precio,FechaRp,Disponibilidad,Actividad,ComentarioR")] Repuesto repuesto, IFormFile? file = null)
        {
            if (ModelState.IsValid)
            {
                repuesto.ImgProducto = await GuardarImage(file);
                _context.Add(repuesto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Departamento1", repuesto.IdDepartamento);
            ViewData["IdVendedor"] = new SelectList(_context.Vendedores, "Id", "Nombre", repuesto.IdVendedor);
            return View(repuesto);
        }

        // GET: Repuestos/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuestos.FindAsync(id);
            if (repuesto == null)
            {
                return NotFound();
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Departamento1", repuesto.IdDepartamento);
            ViewData["IdVendedor"] = new SelectList(_context.Vendedores, "Id", "Nombre", repuesto.IdVendedor);
            return View(repuesto);
        }

        // POST: Repuestos/Edit/5
        // Acción para recibir los datos del formulario de edición y actualizar el repuesto existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreRepuesto,IdVendedor,IdDepartamento,ImgProducto,Compatiblilidad,DescripcionR,Proveniencia,EstadoRp,Precio,FechaRp,Disponibilidad,Actividad,ComentarioR")] Repuesto repuesto, IFormFile? file = null)
        {
            if (id != repuesto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repuesto.ImgProducto = await GuardarImage(file);
                    _context.Update(repuesto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepuestoExists(repuesto.Id))
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
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Departamento1", repuesto.IdDepartamento);
            ViewData["IdVendedor"] = new SelectList(_context.Vendedores, "Id", "Nombre", repuesto.IdVendedor);
            return View(repuesto);
        }

        // GET: Repuestos/Delete/5
        // Acción para mostrar la confirmación de eliminación de un repuesto
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuestos
                .Include(r => r.IdDepartamentoNavigation)
                .Include(r => r.IdVendedorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (repuesto == null)
            {
                return NotFound();
            }

            return View(repuesto);
        }

        // POST: Repuestos/Delete/5
        // Acción para realizar la eliminación del repuesto confirmada por el usuario
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repuesto = await _context.Repuestos.FindAsync(id);
            if (repuesto != null)
            {
                _context.Repuestos.Remove(repuesto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método privado para verificar si un repuesto con un ID específico existe en la base de datos
        private bool RepuestoExists(int id)
        {
            return _context.Repuestos.Any(e => e.Id == id);
        }

        // Acción para mostrar la vista de publicLista
        public async Task<IActionResult> PublicLista(Repuesto repuesto, int topRegistro = 10)
        {

            var repuestos = await _context.Repuestos
                .Include(r => r.IdDepartamentoNavigation)
                .Include(r => r.IdVendedorNavigation)
                .Where(r => r.Actividad == 1)
                .ToListAsync();
            return View(repuestos);
        }


        // Acción para mostrar una lista completa de todos los repuestos (activos e inactivos)
        public IActionResult ListaCompleta()
        {
            var repuestos = _context.Repuestos.ToList(); // Muestra activos e inactivos
            return View(repuestos);
        }

        // Acción para cambiar el estado de un repuesto a inactivo 
        public async Task<IActionResult> Desactivar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuestos.FindAsync(id);
            if (repuesto == null)
            {
                return NotFound();
            }

            // Me cambia el estado a inactivo 
            repuesto.Actividad = 0;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Me Redirigi a la lista principal
        }

        // GET: Repuestos/Inactivos
        // Acción para mostrar la lista de repuestos inactivos
        public async Task<IActionResult> Inactivos()
        {
            var repuestosInactivos = await _context.Repuestos
                .Include(r => r.IdDepartamentoNavigation)
                .Include(r => r.IdVendedorNavigation)
                .Where(r => r.Actividad == 0) // Sirve solo para filtrar los inactivos los inactivos
                .ToListAsync();

            return View(repuestosInactivos);
        }

        // GET: Repuestos/Activar/5
        // Acción para mostrar la lista de repuestos Activos
        public async Task<IActionResult> Activar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuestos.FindAsync(id);
            if (repuesto == null)
            {
                return NotFound();
            }

            // Sirve para cambiar el estado a activo (1)
            repuesto.Actividad = 1;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Me redirige a la lista principal
        }
    }
}
