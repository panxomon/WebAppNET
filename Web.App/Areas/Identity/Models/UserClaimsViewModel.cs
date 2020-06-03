using System.Collections.Generic;
//using System.Web.Mvc;

namespace Web.App.Areas.Identity.Models
{
    public class UserClaimsViewModel
    {
        public UserClaimsViewModel()
        {
            ClaimGroups = new List<ClaimGroup>();

            SelectedClaims = new List<string>();
        }

        public string UserName { get; set; }

        public List<ClaimGroup> ClaimGroups { get; set; }

        public IEnumerable<string> SelectedClaims { get; set; }


        public class ClaimGroup
        {
            public ClaimGroup()
            {
                GroupClaimsCheckboxes = new List<SelectListItem>();
            }
            public string GroupName { get; set; }

            public int GroupId { get; set; }

            public List<SelectListItem> GroupClaimsCheckboxes { get; set; }
        }
    }
}