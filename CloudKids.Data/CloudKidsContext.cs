using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudKids.Domain.Entities;

namespace CloudKids.Data
{
    public class CloudKidsContext : DbContext
    {

        public DbSet<Profil> Profils { get; set; }
        public DbSet<Profil_Jardin> ProfilJardins { get; set; }
        public DbSet<Profil_Parent> ProfilParents { get; set; }
        public DbSet<Profil_Directeur> ProfilDirecteurs { get; set; }
        public DbSet<Profil_Administrateur> ProfilAdministrateurs { get; set; }
        public DbSet<Profil_Medecin> ProfilMedecins { get; set; }
        public DbSet<Profil_ResponsableGarderie> ProfilResponsableGarderies { get; set; }
        public DbSet<Profil_Visiteur> ProfilVisiteurs { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
        public DbSet<Appreciation> Appreciations { get; set; }
        public DbSet<Enfant> Enfants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<CommandHistory> CommandHistories { get; set; }
        public DbSet<Carte_Paiement> CartePaiements { get; set; }
        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<RDV> Rdvs { get; set; }
        public DbSet<Reclamation> Reclamations { get; set; }
        public DbSet<Reputation> Reputations { get; set; }
        public DbSet<Reunion> Reunions { get; set; }
        public DbSet<Survey> Surveys { get; set; }
       public DbSet<Question> Questions { get; set; } 
        public DbSet<Response> Responses { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Streaming> Streamings { get; set; }
        public DbSet<Visite_Virtuelle> VisiteVirtuelles { get; set; }


        public CloudKidsContext() : base("name=CloudKidsConnectionString")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.ProfilDirecteur)
                .WithMany(d => (ICollection<Product>) d.Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RDV>()
                .HasRequired(r => r.Parent)
                .WithMany(d => (ICollection<RDV>) d.Rdv)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reclamation>()
                .HasRequired(r => r.ProfilParent)
                .WithMany(d => (ICollection<Reclamation>)d.Reclamations)
                .WillCascadeOnDelete(false);

        //  modelBuilder.Entity<Survey>()
          //      .HasRequired(r => r.ProfilParent)
            //    .WithMany(d => (ICollection<Survey>) d.Surveys)
              //  .WillCascadeOnDelete(false);

            modelBuilder.Entity<Answer>()
                       .HasRequired(x => x.Question)
                       .WithMany()
                       .HasForeignKey(x => x.QuestionId)
                       .WillCascadeOnDelete(false);

         
        }
    }
}
