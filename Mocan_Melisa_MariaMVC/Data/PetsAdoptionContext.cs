using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mocan_Melisa_MariaMVC.Models;

namespace Mocan_Melisa_MariaMVC.Data
{
    public class PetsAdoptionContext : DbContext
    {
        public PetsAdoptionContext (DbContextOptions<PetsAdoptionContext> options)
            : base(options)
        {
        }

        public DbSet<Mocan_Melisa_MariaMVC.Models.AdoptionRequest> AdoptionRequest { get; set; } = default!;
        public DbSet<Mocan_Melisa_MariaMVC.Models.Breed> Breed { get; set; } = default!;
        public DbSet<Mocan_Melisa_MariaMVC.Models.Donation> Donation { get; set; } = default!;
        public DbSet<Mocan_Melisa_MariaMVC.Models.Pet> Pet { get; set; } = default!;
        public DbSet<Mocan_Melisa_MariaMVC.Models.User> User { get; set; } = default!;
    }
}
