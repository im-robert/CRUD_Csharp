using Microsoft.AspNetCore.Mvc;
using AppCRUD.Data;
using AppCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace AppCRUD.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly AppDBContext _appDBContext;

        public EmpleadoController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        [HttpGet]
        public  async Task<IActionResult> Lista()
        {
            List<Empleado> lista = await _appDBContext.Empleados.ToListAsync();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo(Empleado empleado)
        {
            await _appDBContext.Empleados.AddAsync(empleado);
            await _appDBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {
            Empleado empleado = await _appDBContext.Empleados.FirstAsync(e => e.IdEmpleado == Id);
            return View(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Empleado empleado)
        {
            _appDBContext.Empleados.Update(empleado);
             await _appDBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int Id)
        {
            Empleado empleado = await _appDBContext.Empleados.FirstAsync(e => e.IdEmpleado == Id);
            
            _appDBContext.Empleados.Remove(empleado);
            await _appDBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}
