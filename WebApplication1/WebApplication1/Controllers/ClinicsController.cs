using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ProjectPatientsClinics.Controllers
{
	/// <summary>
	/// Контроллер обработки запросов к объектам Clinics
	/// </summary>
	[ApiController]
    [Route("api/[controller]")]
    public class ClinicsController : Controller
    {
        private readonly ApplicationContext _context;

        public ClinicsController(ApplicationContext context)
        {
            _context = context;
        }

		/// <summary>
		/// Получение списка Clinics
		/// </summary>
		/// <returns></returns>
		// GET: Clinics
		[HttpGet]
        public async Task<IActionResult> Index()
        {
            var clinics = await _context.clinics.ToListAsync();
            if (clinics == null)
                return NoContent();
            return View(clinics);
        }

		/// <summary>
		/// Получение клиники по id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        // GET: Clinics/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinic = await _context.clinics
                .FirstOrDefaultAsync(m => m.ID == id);
            if (clinic == null)
            {
                return NotFound();
            }

            return View(clinic);
        }

		/// <summary>
		/// Добавление новой клиники
		/// </summary>
		/// <param name="clinic">Наименование клиники</param>
		/// <returns></returns>
        // POST: Clinics/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([Bind("Name")] Clinic clinic)
        {
            try
            {
                _context.clinics.Add(clinic);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
				//встроенная в sql валидация на уникальность поля Name
				if (ex.InnerException.Message.Contains("UNIQUEName"))
                 return BadRequest("The object already exists");
                throw ex;
            }
            return CreatedAtAction(nameof(Details), new { id = clinic.ID }, clinic);
        }
		#region Неиспользуемый код
		//// GET: Clinics/Edit/5
		//public async Task<IActionResult> Edit(int? id)
		//{
		//    if (id == null)
		//    {
		//        return NotFound();
		//    }

		//    var clinic = await _context.clinics.FindAsync(id);
		//    if (clinic == null)
		//    {
		//        return NotFound();
		//    }
		//    return View(clinic);
		//}

		//// POST: Clinics/Edit/5
		//// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		//// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Edit(int id, [Bind("Name,ID")] Clinic clinic)
		//{
		//    if (id != clinic.ID)
		//    {
		//        return NotFound();
		//    }

		//    if (ModelState.IsValid)
		//    {
		//        try
		//        {
		//            _context.Update(clinic);
		//            await _context.SaveChangesAsync();
		//        }
		//        catch (DbUpdateConcurrencyException)
		//        {
		//            if (!ClinicExists(clinic.ID))
		//            {
		//                return NotFound();
		//            }
		//            else
		//            {
		//                throw;
		//            }
		//        }
		//        return RedirectToAction(nameof(Index));
		//    }
		//    return View(clinic);
		//}

		//// GET: Clinics/Delete/5
		//public async Task<IActionResult> Delete(int? id)
		//{
		//    if (id == null)
		//    {
		//        return NotFound();
		//    }

		//    var clinic = await _context.clinics
		//        .FirstOrDefaultAsync(m => m.ID == id);
		//    if (clinic == null)
		//    {
		//        return NotFound();
		//    }

		//    return View(clinic);
		//}

		//// POST: Clinics/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> DeleteConfirmed(int id)
		//{
		//    var clinic = await _context.clinics.FindAsync(id);
		//    _context.clinics.Remove(clinic);
		//    await _context.SaveChangesAsync();
		//    return RedirectToAction(nameof(Index));
		//}

		//private bool ClinicExists(int id)
		//{
		//    return _context.clinics.Any(e => e.ID == id);
		//}
		#endregion Неиспользуемый код
	}
}
