namespace NewPDR.Data.Migrations
{
    using NewPDR.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    //public sealed class Configuration : DbMigrationsConfiguration<NewPDR.Data.NewPDRDataContext>
    //{
    //    public Configuration()
    //    {
    //        AutomaticMigrationsEnabled = false;
    //    }

    //    protected override void Seed(NewPDR.Data.NewPDRDataContext context)
    //    {
    ////        context.UserTypes.AddOrUpdate(x => x.ID,
    ////    new UserType() { Name = "Head Office", ID = 1 },
    ////    new UserType() { Name = "UK Field", ID = 2 },
    ////    new UserType() { Name = "ROI Field", ID = 3 }
    ////    );

    ////        context.PDRStatuses.AddOrUpdate(x => x.ID,
    ////new PDRStatus() { Name = "Not Started", ID = 1 },
    ////new PDRStatus() { Name = "Started", ID = 2 },
    ////new PDRStatus() { Name = "Completed", ID = 3 }

    ////);


    ////        context.RatingTypes.AddOrUpdate(x => x.ID,
    ////    new RatingType() { Name = "Objective", ID = 1 },
    ////    new RatingType() { Name = "Success", ID = 2 },
    ////    new RatingType() { Name = "Overall Performance", ID = 3 }
    ////    );


    ////        context.HRProPersonnelRecords.AddOrUpdate(x => x.EmailId,
    ////    new HRProPersonnelRecord() { EmailId = "saneej@gmail.com", ForeName = "Saneej", Surname = "Cherakadavath", ManagerEmailId = "jplace@gmail.com", LineManagerEmailId = "jkelly@gmail.com", JobTitle = "Developer", Department = "HO Business Change & Development (HC)", Division = "HC" },
    ////    new HRProPersonnelRecord() { EmailId = "tausif@gmail.com", ForeName = "Tausif", Surname = "Ilyas", ManagerEmailId = "jplace@gmail.com", LineManagerEmailId = "jkelly@gmail.com", JobTitle = "Developer", Department = "HO Business Change & Development (HC)", Division = "HC" },
    ////    new HRProPersonnelRecord() { EmailId = "jkelly@gmail.com", ForeName = "Jamie", Surname = "Kelly", ManagerEmailId = "", LineManagerEmailId = "jplace@gmail.com", JobTitle = "Dev Manager", Department = "HO Business Change & Development (HC)", Division = "HC" },
    ////    new HRProPersonnelRecord() { EmailId = "jplace@gmail.com", ForeName = "Jamie", Surname = "Place", ManagerEmailId = "", LineManagerEmailId = "someone@gmail.com", JobTitle = "Manager", Department = "HO Business Change & Development (HC)", Division = "HC" }
    ////    );


    //        context.ObjectiveTypes.AddOrUpdate(x => x.ID,

    //             new ObjectiveType() { Name = "Objective 1", Index = 0, UserTypeId = 1 },
    //             new ObjectiveType() { Name = "Objective 2", Index = 1, UserTypeId = 1 },
    //             new ObjectiveType() { Name = "Objective 3", Index = 2, UserTypeId = 1 },
    //              new ObjectiveType() { Name = "Objective_1", Index = 0, UserTypeId = 2 },
    //             new ObjectiveType() { Name = "Objective_2", Index = 1, UserTypeId = 2 },
    //             new ObjectiveType() { Name = "Objective_3", Index = 2, UserTypeId = 2 },
    //                new ObjectiveType() { Name = "Objective_4", Index = 3, UserTypeId = 3 },
    //             new ObjectiveType() { Name = "Objective_1", Index = 0, UserTypeId = 3 },
    //             new ObjectiveType() { Name = "Objective_2", Index = 1, UserTypeId = 3 }
    //         );

    //  //      context.SuccessFactorTypes.AddOrUpdate(x => x.ID,
    //  //  new SuccessFactorType() { Name = "HO Success factor 1", Index = 0, UserTypeId = 1 },
    //  //  new SuccessFactorType() { Name = "HO Success factor 2", Index = 1, UserTypeId = 1 },
    //  //  new SuccessFactorType() { Name = "HO Success factor 3", Index = 2, UserTypeId = 1 },
    //  //   new SuccessFactorType() { Name = "UKField_Success factor_1", Index = 0, UserTypeId = 2 },
    //  //  new SuccessFactorType() { Name = "UKField_Success factor_2", Index = 1, UserTypeId = 2 },
    //  //  new SuccessFactorType() { Name = "UKField_Success factor_3", Index = 2, UserTypeId = 2 },
    //  //     new SuccessFactorType() { Name = "UKField_Success factor_4", Index = 3, UserTypeId = 3 },
    //  //  new SuccessFactorType() { Name = "ROIField_Success factor_1", Index = 0, UserTypeId = 3 },
    //  //  new SuccessFactorType() { Name = "ROIField_Success factor_2", Index = 1, UserTypeId = 3 }
    //  //  );



    //  //      context.Ratings.AddOrUpdate(x => x.ID,
    //  //  new Rating() { Name = "Rating to be calculated", RatingTypeId = 1, UserTypeId = 1, ScoreIndex = 0 },
    //  //  new Rating() { Name = "Step it up", RatingTypeId = 1, UserTypeId = 1, ScoreIndex = 1 },
    //  //  new Rating() { Name = "Get on the right track", RatingTypeId = 1, UserTypeId = 1, ScoreIndex = 2 },
    //  //  new Rating() { Name = "Great Job", RatingTypeId = 1, UserTypeId = 1, ScoreIndex = 3 },
    //  //   new Rating() { Name = "Star Performer", RatingTypeId = 1, UserTypeId = 1, ScoreIndex = 4 },

    //  //new Rating() { Name = "Rating to be calculated", RatingTypeId = 1, UserTypeId = 2, ScoreIndex = 0 },
    //  //  new Rating() { Name = "Step it up", RatingTypeId = 1, UserTypeId = 2, ScoreIndex = 1 },
    //  //  new Rating() { Name = "Get on the right track", RatingTypeId = 1, UserTypeId = 2, ScoreIndex = 2 },
    //  //  new Rating() { Name = "Great Job", RatingTypeId = 1, UserTypeId = 2, ScoreIndex = 3 },
    //  //   new Rating() { Name = "Star Performer", RatingTypeId = 1, UserTypeId = 2, ScoreIndex = 4 },

    //  //   new Rating() { Name = "UKField_Success factor_4", RatingTypeId = 2, UserTypeId = 1 },
    //  //new Rating() { Name = "ROIField_Success factor_1", RatingTypeId = 2, UserTypeId = 1 },
    //  //new Rating() { Name = "ROIField_Success factor_2", RatingTypeId = 2, UserTypeId = 1 }
    //  // );



    //  //      context.DevelopmentCategorys.AddOrUpdate(x => x.ID,
    //  //  new DevelopmentCategory() { Description = "LMS", UserTypeId = 1 },
    //  //  new DevelopmentCategory() { Description = "Management", UserTypeId = 1 },
    //  //  new DevelopmentCategory() { Description = "Soft skills", UserTypeId = 1 },
    //  //   new DevelopmentCategory() { Description = "Others", UserTypeId = 1 },
    //  //  new DevelopmentCategory() { Description = "UKField_Success LMS", UserTypeId = 2 },
    //  //  new DevelopmentCategory() { Description = "UKField_Success Management", UserTypeId = 2 },
    //  //     new DevelopmentCategory() { Description = "UKField_Success Soft skills", UserTypeId = 2 },
    //  //  new DevelopmentCategory() { Description = "ROIField_Success LMS", UserTypeId = 3 },
    //  //  new DevelopmentCategory() { Description = "ROIField_Success Soft skills", UserTypeId = 3 }
    //  //  );


    //    }
    //}
}
