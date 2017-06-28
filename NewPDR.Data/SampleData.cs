using NewPDR.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPDR.Data
{


    public class SampleData : DropCreateDatabaseAlways<NewPDRDataContext>
    {
        protected override void Seed(NewPDRDataContext context)
        {
            PhaseOne(context);
            PhaseTwo(context);
        }

        private static void PhaseTwo(NewPDRDataContext context)
        {

            new List<ObjectiveType>
            {
                new ObjectiveType() { Name = "Objective 1", Index = 0, UserTypeId = 1 },
                new ObjectiveType() { Name = "Objective 2", Index = 1, UserTypeId = 1 },
                new ObjectiveType() { Name = "Objective 3", Index = 2, UserTypeId = 1 },
                 new ObjectiveType() { Name = "Objective Field 1", Index = 0, UserTypeId = 2 },
                new ObjectiveType() { Name = "Objective Field 2 ", Index = 1, UserTypeId = 2 },
                new ObjectiveType() { Name = "Objective Field 3", Index = 2, UserTypeId = 2 },
                   new ObjectiveType() { Name = "Objective Field 4", Index = 3, UserTypeId = 2 },
                new ObjectiveType() { Name = "Objective_2", Index = 0, UserTypeId = 3 },
                new ObjectiveType() { Name = "Objective_3", Index = 1, UserTypeId = 3 }
            }.ForEach(m => context.ObjectiveTypes.Add(m));


            new List<SuccessFactorType>
            {
                new SuccessFactorType() { Name = "Looking Through the eyes of the customer", Index = 0, UserTypeId = 1 },
                new SuccessFactorType() { Name = "Working Together", Index = 1, UserTypeId = 1 },
                new SuccessFactorType() { Name = "Taking Accountability", Index = 2, UserTypeId = 1 },

                 new SuccessFactorType() { Name = "Looking Through the eyes of the customer", Index = 0, UserTypeId = 2 },
                new SuccessFactorType() { Name = "Working Together", Index = 1, UserTypeId = 2 },
                new SuccessFactorType() { Name = "Taking Accountability", Index = 2, UserTypeId = 2 },

                   new SuccessFactorType() { Name = "ROIField_Success factor 0", Index = 3, UserTypeId = 3 },
                new SuccessFactorType() { Name = "ROIField_Success factor_1", Index = 0, UserTypeId = 3 },
                new SuccessFactorType() { Name = "ROIField_Success factor_2", Index = 1, UserTypeId = 3 }
            }.ForEach(m => context.SuccessFactorTypes.Add(m));


            new List<DevelopmentCategory>
            {
                new DevelopmentCategory() { Description = "LMS", UserTypeId = 1 },
                new DevelopmentCategory() { Description = "Management", UserTypeId = 1 },
                new DevelopmentCategory() { Description = "Soft skills", UserTypeId = 1 },
                 new DevelopmentCategory() { Description = "Others", UserTypeId = 1 },
                new DevelopmentCategory() { Description = "UKField_Success LMS", UserTypeId = 2 },
                new DevelopmentCategory() { Description = "UKField_Success Management", UserTypeId = 2 },
                   new DevelopmentCategory() { Description = "UKField_Success Soft skills", UserTypeId = 2 },
                new DevelopmentCategory() { Description = "ROIField_Success LMS", UserTypeId = 3 },
                new DevelopmentCategory() { Description = "ROIField_Success Soft skills", UserTypeId = 3 }
            }.ForEach(m => context.DevelopmentCategorys.Add(m));


            new List<Rating>
            {
                new Rating() { Name = "Rating to be calculated", RatingTypeId = 1, UserTypeId = 1, ScoreIndex = 0 },
                new Rating() { Name = "Step it up", RatingTypeId = 1, UserTypeId = 1, ScoreIndex = 1 },
                new Rating() { Name = "Get on the right track", RatingTypeId = 1, UserTypeId = 1, ScoreIndex = 2 },
                new Rating() { Name = "Great Job", RatingTypeId = 1, UserTypeId = 1, ScoreIndex = 3 },
                 new Rating() { Name = "Star Performer", RatingTypeId = 1, UserTypeId = 1, ScoreIndex = 4 },

              new Rating() { Name = "Rating to be calculated", RatingTypeId = 2, UserTypeId = 2, ScoreIndex = 0 },
                new Rating() { Name = "Step it up", RatingTypeId = 2, UserTypeId = 2, ScoreIndex = 1 },
                new Rating() { Name = "Get on the right track", RatingTypeId = 2, UserTypeId = 2, ScoreIndex = 2 },
                new Rating() { Name = "Great Job", RatingTypeId = 2, UserTypeId = 2, ScoreIndex = 3 },
                 new Rating() { Name = "Star Performer", RatingTypeId = 2, UserTypeId = 2, ScoreIndex = 4 },

                  new Rating() { Name = "Rating to be calculated", RatingTypeId = 3, UserTypeId = 2, ScoreIndex = 0 },
                new Rating() { Name = "Step it up", RatingTypeId = 3, UserTypeId = 2, ScoreIndex = 1 },
                new Rating() { Name = "Get on the right track", RatingTypeId = 3, UserTypeId = 2, ScoreIndex = 2 },
                new Rating() { Name = "Great Job", RatingTypeId = 3, UserTypeId = 2, ScoreIndex = 3 },
                 new Rating() { Name = "Star Performer", RatingTypeId = 3, UserTypeId = 2, ScoreIndex = 4 }
            }.ForEach(m => context.Ratings.Add(m));



            context.Commit();
        }

        private static void PhaseOne(NewPDRDataContext context)
        {
            // User Types
            new List<UserType>
            {
                new UserType { ID=1, Name = "Head Office"},
                new UserType { ID=2, Name = "UK Field"},
                new UserType { ID=3, Name = "ROI Field"}

            }.ForEach(m => context.UserTypes.Add(m));

            // Ratings
            new List<RatingType>
            {
                 new RatingType() { ID=1, Name = "Objective" },
                    new RatingType() {ID=2, Name = "Success" },
                    new RatingType() { ID=3, Name = "Overall Performance" }

            }.ForEach(m => context.RatingTypes.Add(m));


            new List<PDRStatus>
            {
                new PDRStatus() { ID=1, Name = "Not Started" },
                new PDRStatus() { ID=2, Name = "In Progress" },
                new PDRStatus() { ID=3, Name = "Completed" }

            }.ForEach(m => context.PDRStatuses.Add(m));

            // HR Pro Data
            new List<HRProPersonnelRecord>
            {
                new HRProPersonnelRecord() { EmailId = "saneej@gmail.com", ForeName = "Saneej", Surname = "Cherakadavath", ManagerEmailId = "jplace@gmail.com", LineManagerEmailId = "jkelly@gmail.com", JobTitle = "Developer", Department = "HO Business Change & Development (HC)", Division = "HC" },
                new HRProPersonnelRecord() { EmailId = "tausif@gmail.com", ForeName = "Tausif", Surname = "Ilyas", ManagerEmailId = "jplace@gmail.com", LineManagerEmailId = "jkelly@gmail.com", JobTitle = "Developer", Department = "HO Business Change & Development (HC)", Division = "HC" },
                new HRProPersonnelRecord() { EmailId = "jkelly@gmail.com", ForeName = "Jamie", Surname = "Kelly", ManagerEmailId = "", LineManagerEmailId = "jplace@gmail.com", JobTitle = "Dev Manager", Department = "HO Business Change & Development (HC)", Division = "HC" },
                new HRProPersonnelRecord() { EmailId = "jplace@gmail.com", ForeName = "Jamie", Surname = "Place", ManagerEmailId = "", LineManagerEmailId = "someone@gmail.com", JobTitle = "Manager", Department = "HO Business Change & Development (HC)", Division = "HC" }
            }.ForEach(m => context.HRProPersonnelRecords.Add(m));

            context.Commit();
        }

    }
}
