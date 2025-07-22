using AppointmentBookingSystem.Data;
using AppointmentBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppointmentBookingSystem.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAppointments()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var appointments = _context.Appointments
                .Where(a => a.UserId == userId)
                .ToList();

            return Ok(appointments);
        }

        [HttpPost]
        public IActionResult Book(Appointment appointment)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            appointment.UserId = userId;
            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return Ok(appointment);
        }
    }
}
