using NewPDR.Model;
using NewPDR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NewPDR.Web.Extensions
{
    public static class SelectListExtensions
    {

        public static IEnumerable<SelectListItem> ToSelectListItems(
            this IEnumerable<Rating> ratings, int selectedId)
        {
            return

                ratings.OrderBy(rating => rating.ScoreIndex)
                      .Select(metric =>
                          new SelectListItem
                          {
                              Selected = (metric.ID == selectedId),
                              Text = metric.Name,
                              Value = metric.ID.ToString()
                          });
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(
           this IEnumerable<DevelopmentCategory> developmentCategories, int selectedId)
        {
            return

                developmentCategories
                      .Select(metric =>
                          new SelectListItem
                          {
                              Selected = (metric.ID == selectedId),
                              Text = metric.Description,
                              Value = metric.ID.ToString()
                          });
        }

        public static Tuple<string, string> ToCurrentMidYearPDRStatuses(this PDReview MyPDR)
        {

            if (MyPDR.MidYearStatus == 3)
            {
                return new Tuple<string, string>("Mid year is completed", "label-success");

            }
            else if (MyPDR.MidYearStatus == 2)
            {
                return new Tuple<string, string>("Mid year is In Progress", "label-warning");

            }
            else
            {
                return new Tuple<string, string>("Mid year is not started", "label-danger");

            }

        }

        public static Tuple<string ,string> ToCurrentFullYearPDRStatuses(this PDReview MyPDR)
        {

            if (MyPDR.FullYearStatus == 3)
            {
                return new Tuple<string, string>("Full year is completed", "label-success");

            }
            else if (MyPDR.FullYearStatus == 2)
            {
                return new Tuple<string, string>("Full year is In Progress", "label-warning");

            }
            else
            {
                return new Tuple<string, string>("Full year is not started", "label-danger");

            }
        }

    }
}