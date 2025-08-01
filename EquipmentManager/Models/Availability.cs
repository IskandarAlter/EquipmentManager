using System;
using EquipmentManager.Models;
using System.Text.Json.Serialization;


namespace EquipmentManager.Models;

public class Availability
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsAvailable { get; set; }

    public int EquipmentId { get; set; }

    [JsonIgnore]
    public Equipment? Equipment { get; set; }
}
