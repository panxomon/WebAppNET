using System;

namespace Web.Base.Identity.Permisos
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public  abstract class ClaimsGroupAttribute : Attribute
    {
        public Enum Resource { get; private set; }

        public ClaimsGroupAttribute(Enum resource)
        {
            Resource = resource;
        }

        public virtual string GetGroupId()
        {
            return Convert.ToInt32(Resource).ToString();
        }
    }
}