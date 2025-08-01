using EquipmentManager.Data;
using EquipmentManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AvailabilityController : ControllerBase
{
    private readonly AppDbContext _context;

    public AvailabilityController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/availability/{equipmentId}
    [HttpGet("{equipmentId}")]
    public async Task<ActionResult<IEnumerable<Availability>>> GetByEquipment(int equipmentId)
    {
        return await _context.Availabilities
            .Where(a => a.EquipmentId == equipmentId)
            .ToListAsync();
    }

    // POST: api/availability
    [HttpPost]
    public async Task<ActionResult<Availability>> Create(Availability availability)
    {
        _context.Availabilities.Add(availability);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetByEquipment), new { equipmentId = availability.EquipmentId }, availability);
    }

    // PUT: api/availability/id$
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Availability updated)
    {
        if (id != updated.Id)
            return BadRequest();

        var existing = await _context.Availabilities.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.Date = updated.Date;
        existing.IsAvailable = updated.IsAvailable;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/availability/id$
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var availability = await _context.Availabilities.FindAsync(id);
        if (availability == null)
            return NotFound();

        _context.Availabilities.Remove(availability);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
