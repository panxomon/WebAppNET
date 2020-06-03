using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Web.Base.Extensores;
using Web.Base.Identity.Model;
using Web.Base.Identity.Permisos;

namespace Web.Base.Identity.Core
{
    public class ClaimedActionsProvider
    {
        public IEnumerable<ClaimsGroup> GetClaimGroups()
        {
            var carpetaDll = ConfigurationManager.AppSettings["AssemblyNameREST"];
            var listaClaims = new List<ClaimsGroup>();

            var allAssemblies = (from dll in Directory.GetFiles(carpetaDll, "*.dll") let dlls = dll.Split('\\').Last() where dlls.ToLower().Contains("rest") select Assembly.LoadFile(dll)).ToList();

            foreach (var type in allAssemblies)
            {
                var listado = type.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(ApiController))).Where(t => t.Namespace != null && !t.Name.Contains("rest")).Where(c => c.IsDefined(typeof(ClaimsGroupAttribute)))
                                .Select(c => new ClaimsGroup()
                                {
                                    GroupName = GetGroupName(c),
                                    GroupId = GetGroupId(c),
                                    ControllerType = c,
                                    Claims = GetActionClaims(c),
                                }).ToList();

                listaClaims.AddRange(listado);
              
            }

            return listaClaims;
        }

        private string GetGroupName(Type controllerType)
        {
            var result = controllerType.GetCustomAttribute<ClaimsGroupAttribute>().Resource;

            return result.GetDisplayName();
        }

        private int GetGroupId(Type controllerType)
        {
            var claimsGroupAttribute = controllerType.GetCustomAttribute<ClaimsGroupAttribute>();

            var result = Convert.ToInt32(claimsGroupAttribute.Resource);// TODO Posible crush solo al convertir

            return result;
        }

        private List<string> GetActionClaims(Type controllerType)
        {
            var result = controllerType.GetMethods()
                .Where(m => m.IsDefined(typeof(ClaimsActionAttribute)))
                .SelectMany(m => m.GetCustomAttributes<ClaimsActionAttribute>())
                .Select(a => a.Claim)
                .Distinct()
                .Select(a => a.ToString())
                .ToList();

            var ret = controllerType.GetConstructors(BindingFlags.Instance | BindingFlags.Public)
                .Where(m => m.IsDefined(typeof(ClaimsActionAttribute)))
                .SelectMany(m => m.GetCustomAttributes<ClaimsActionAttribute>())
                .Select(a => a.Claim)
                .Distinct()
                .Select(a => a.ToString())
                .ToList();

            result.AddRange(ret);
                
            return result;
        }
    }
}