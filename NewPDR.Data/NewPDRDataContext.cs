using Microsoft.AspNet.Identity.EntityFramework;
using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Data
{
    public class NewPDRDataContext : IdentityDbContext<ApplicationUser>
    {
        public NewPDRDataContext()
            : base("NewPDRDataContext")
        {

        }

        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<PDReview> PDReviews { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<ObjectiveType> ObjectiveTypes { get; set; }
        public DbSet<PDRStatus> PDRStatuses { get; set; }

        public DbSet<SuccessFactorType> SuccessFactorTypes { get; set; }
        public DbSet<SuccessFactor> SuccessFactors { get; set; }

        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RatingType> RatingTypes { get; set; }

        public DbSet<ToDoList>ToDoLists  { get; set; }

        public DbSet<Activity>Activities { get; set; }

        public DbSet<HRProPersonnelRecord> HRProPersonnelRecords { get; set; }

        public DbSet<PersonalDevelopmentPlan> PersonalDevelopmentPlans { get; set; }
        public DbSet<DevelopmentCategory> DevelopmentCategorys { get; set; }


        

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
