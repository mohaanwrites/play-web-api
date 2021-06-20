using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using play_web_api.Model;
using play_web_api.Services;

namespace play_web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Patient>> ReadAllPatients() => PatientService.ReadAllPatients().ToList();

        [HttpGet("{patientId}")]
        public ActionResult<Patient> ReadPatientById(int patientId)
        {
            var patient = PatientService.ReadByPatientId(patientId);
            if (patient is null)
                return NotFound();

            return patient;
        }

        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            PatientService.CreatePatient(patient);
            return CreatedAtAction(nameof(Create), new { id = patient.PatientId }, patient);
        }

        [HttpPut("{patientId}")]
        public IActionResult Update(int patientId, Patient patient)
        {
            if (patientId != patient.PatientId)
                return BadRequest();

            var existingPatient = PatientService.ReadByPatientId(patientId);
            if (existingPatient is null)
                return NotFound();

            PatientService.Update(patient);

            return NoContent();
        }

        [HttpDelete("{patientId}")]
        public IActionResult Delete(int patientId)
        {
            var existingPatient = PatientService.ReadByPatientId(patientId);
            if (existingPatient is null)
                return NotFound();

            PatientService.Delete(existingPatient);
            return NoContent();
        }
    }
}
