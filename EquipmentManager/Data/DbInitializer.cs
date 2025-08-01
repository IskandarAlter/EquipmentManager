using EquipmentManager.Models;

namespace EquipmentManager.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (context.Equipments.Any()) return; // Já tem dados

        var equipments = new List<Equipment>
        {
            new Equipment
            {
                Name = "Sony A7 III",
                Model = "ILCE-7M3",
                Availability = new List<Availability>
                {
                    new Availability { Date = DateTime.Today, IsAvailable = true },
                    new Availability { Date = DateTime.Today.AddDays(1), IsAvailable = false }
                }
            },
            new Equipment
            {
                Name = "Epson Projector",
                Model = "EB-X41",
                Availability = new List<Availability>
                {
                    new Availability { Date = DateTime.Today.AddDays(2), IsAvailable = true }
                }
            },
            new Equipment
            {
                Name = "Microfone Rode NT1",
                Model = "NT1-5th Gen",
                Availability = new List<Availability>()
            }
        };

        context.Equipments.AddRange(equipments);
        context.SaveChanges();
    }
}
