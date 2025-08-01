using System;
using EquipmentManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Equipment> Equipments => Set<Equipment>();
    public DbSet<Availability> Availabilities => Set<Availability>();
}
