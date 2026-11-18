using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lab8MVC.Models;

namespace Lab8MVC.ViewModels
{
    // View Model for the Students Index page — carries the filtered student list
    // together with everything needed to render the campus filter dropdown.
    public class StudentIndexViewModel
    {
        public IQueryable<Student> Students { get; set; }
        public string Search { get; set; }
        public IEnumerable<CampusWithCount> CampusesWithCount { get; set; }
        public string Campus { get; set; }

        // Items for the "Filter by campus" DropDownListFor, e.g. "Albany Campus (2)"
        public IEnumerable<SelectListItem> CampusFilterItems
        {
            get
            {
                var allCampuses = CampusesWithCount.Select(cc => new SelectListItem
                {
                    Value = cc.CampusName,
                    Text = cc.CampusNameWithCount
                });
                return allCampuses;
            }
        }
    }

    public class CampusWithCount
    {
        public int StudentCount { get; set; }
        public string CampusName { get; set; }
        public string CampusNameWithCount
        {
            get
            {
                return CampusName + " (" + StudentCount.ToString() + ")";
            }
        }
    }
}
