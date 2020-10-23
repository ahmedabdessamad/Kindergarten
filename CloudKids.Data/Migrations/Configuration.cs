using CloudKids.Domain.Entities;

namespace CloudKids.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CloudKids.Data.CloudKidsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CloudKids.Data.CloudKidsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.ProfilParents.AddOrUpdate(
              p => p.Username,
              new Profil_Parent {
                  Username = "Andrew Peters",
                  Adresse = new Adresse
                  {
                      CodePostal = 2000,
                      Street = "Street",
                      Ville = "Ville"
                  } ,
                  Email = "email@gmail.com",
                  Gender = Gender.Homme,
                  Name = "Andrew",
                  Phone = "23568877",
                  Password = "Pwd",
                  ConfirmPassword = "Pwd",
                  Statut = Statut.Enabled,
                  Calendar = DateTime.Now
              }
              //new Profil_Parent { Username = "Brice Lambson" },
              //new Profil_Parent { Username = "Rowan Miller" }
            );

        }
    }
}
