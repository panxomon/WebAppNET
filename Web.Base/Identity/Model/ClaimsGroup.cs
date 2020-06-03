using System;
using System.Collections.Generic;

namespace Web.Base.Identity.Model
{
    public class ClaimsGroup
    {
        public ClaimsGroup()
        {
            Claims = new List<string>();
        }

        public string GroupName { get; set; }

        public int GroupId { get; set; }

        public Type ControllerType { get; set; }

        public List<string> Claims { get; set; }

        //public List<Claim> GetAllClaims()
        //{
        //    return Claims.Select(c => new Claim(GroupId.ToString(), c)).ToList();
        //}
    }
}
