using System;

namespace EquipmentManager.Models;

public class Equipment
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string? ImagePath { get; set; }

    public List<Availability> Availability { get; set; } = new();
}
