using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectPatientsClinics;

namespace ProjectPatientsClinics.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : Controller
    {
        private readonly ApplicationContext _context;

        public PatientsController(ApplicationContext context)
        {
            _context = context;
        }

		/// <summary>
		/// Получение списка пациентов
		/// </summary>
		/// <returns></returns>
        // GET: Patients
        [HttpGet]
        public async Task<IActionResult> Index()
        {
			var patients = await _context.patients.ToListAsync();
			if (patients == null)
				return NoContent();
			LoadClinic(patients);
			return View(patients);
        }
		/// <summary>
		/// Загрузка объектов поля "Clinic"
		/// </summary>
		/// <param name="patients"></param>
		private void LoadClinic(List<Patient> patients)
		{
			patients.ForEach(i => _context.Entry(i).Reference(i => i.Clinic).Load());
		}
		/// <summary>
		/// Получение информации о клиенту по id
		/// </summary>
		/// <param name="id">Id пациента</param>
		/// <returns></returns>
		// GET: Patients/Details/5
        [HttpGet("{id}")]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var patient = await _context.patients
				.FirstOrDefaultAsync(m => m.ID == id);
			if (patient == null)
			{
				return NotFound();
			}

			return View(patient);
		}

		/// <summary>
		/// Добавление нового пациента
		/// </summary>
		/// <param name="patient"></param>
		/// <param name="clinic"></param>
		/// <returns></returns>
		// POST: Patients/Create
		[HttpPost("Create/{clinic}")]
        public async Task<IActionResult> Create([Bind("City,Name")] Patient patient,int? clinic)
        {
            if (ModelState.IsValid)
            {
				if (clinic != null)
				{
					patient.Clinic = await _context.clinics.FirstOrDefaultAsync(i => i.ID == clinic);
				}
				patient.City = CityTrimer.Trim(patient.City);
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }
		/// <summary>
		/// Получение списка пациентов по выбранной клинике
		/// </summary>
		/// <param name="clinic">ID Клиники</param>
		/// <param name="page">Номер страницы (начинаются с 0)</param>
		/// <param name="capacity">Колчества записей на странице</param>
		/// <returns></returns>
		[HttpGet("GetByClinic/Clinic={clinic};Page={page};PageCapacity={capacity}")]
		public async Task<IActionResult> GetByClinic(int? clinic,int? page,int? capacity)
		{
			if (clinic == null)
				return NotFound();
			List<Patient> patients;
			if (page == null || capacity == null)
			{
				patients = await _context.patients.Where(i => i.clinicID == clinic).ToListAsync();
			}
			else
			{
				patients = await _context.patients.Where(i => i.clinicID == clinic).Page<Patient>(capacity.Value, page.Value).ToListAsync();
			}
			LoadClinic(patients);
			return View(patients);
		}
		#region Неиспользуемый код
		//// GET: Patients/Edit/5
		//public async Task<IActionResult> Edit(int? id)
		//{
		//    if (id == null)
		//    {
		//        return NotFound();
		//    }

		//    var patient = await _context.patients.FindAsync(id);
		//    if (patient == null)
		//    {
		//        return NotFound();
		//    }
		//    return View(patient);
		//}

		//// POST: Patients/Edit/5
		//// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		//// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Edit(int id, [Bind("City,ID,Name")] Patient patient)
		//{
		//    if (id != patient.ID)
		//    {
		//        return NotFound();
		//    }

		//    if (ModelState.IsValid)
		//    {
		//        try
		//        {
		//            _context.Update(patient);
		//            await _context.SaveChangesAsync();
		//        }
		//        catch (DbUpdateConcurrencyException)
		//        {
		//            if (!PatientExists(patient.ID))
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
		//    return View(patient);
		//}

		//// GET: Patients/Delete/5
		//public async Task<IActionResult> Delete(int? id)
		//{
		//	if (id == null)
		//	{
		//		return NotFound();
		//	}

		//	var patient = await _context.patients
		//		.FirstOrDefaultAsync(m => m.ID == id);
		//	if (patient == null)
		//	{
		//		return NotFound();
		//	}

		//	return View(patient);
		//}

		//// POST: Patients/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> DeleteConfirmed(int id)
		//{
		//	var patient = await _context.patients.FindAsync(id);
		//	_context.patients.Remove(patient);
		//	await _context.SaveChangesAsync();
		//	return RedirectToAction(nameof(Index));
		//}

		//private bool PatientExists(int id)
		//      {
		//          return _context.patients.Any(e => e.ID == id);
		//      }
		#endregion Неиспользуемый код
	}
}
