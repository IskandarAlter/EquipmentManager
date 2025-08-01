using EquipmentManager.Data;
using EquipmentManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipmentController : ControllerBase
{
	private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public EquipmentController(AppDbContext context, IWebHostEnvironment env) 
    { 
        _context = context;
        _env = env;
    }

    // GET: api/equipment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipments()
    {
        return await _context.Equipments
            .Include(e => e.Availability)
            .ToListAsync();
    }

    // GET: api/equipment/id$
    [HttpGet("{id}")]
    public async Task<ActionResult<Equipment>> GetEquipment(int id)
    {
        var equipment = await _context.Equipments
            .Include(e => e.Availability)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (equipment == null)
            return NotFound();

        return equipment;
    }

    // POST: api/equipment
    [HttpPost]
    public async Task<ActionResult<Equipment>> AddEquipment([FromBody] Equipment equipment)
    {
        _context.Equipments.Add(equipment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEquipment), new { id = equipment.Id }, equipment);
    }

    // PUT: api/equipment/id$
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEquipment(int id, [FromBody] Equipment equipment)
    {
        if (id != equipment.Id)
            return BadRequest();

        _context.Entry(equipment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Equipments.Any(e => e.Id == id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // DELETE: api/equipment/id$
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEquipment(int id)
    {
        var equipment = await _context.Equipments.FindAsync(id);
        if (equipment == null)
            return NotFound();

        _context.Equipments.Remove(equipment);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // POST: api/equipment/upload-image/id$
    [HttpPost("upload-image/{id}")]
    public async Task<IActionResult> UploadImage(int id, IFormFile file)
    {
        var equipment = await _context.Equipments.FindAsync(id);
        if (equipment == null)
            return NotFound();

        if (file == null || file.Length == 0)
            return BadRequest("Invalid File.");

        var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "images");
        Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        equipment.ImagePath = $"images/{fileName}";
        await _context.SaveChangesAsync();

        return Ok(new { equipment.Id, equipment.ImagePath });
    }
}
