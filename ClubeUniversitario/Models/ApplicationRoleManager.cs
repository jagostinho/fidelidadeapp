using ClubeUniversitario.Helpers;
using Microsoft.AspNet.Identity.Owin;
using MySql.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ClubeUniversitario.Models
{
    public class ApplicationRoleManager : Microsoft.AspNet.Identity.RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(Microsoft.AspNet.Identity.IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, Microsoft.Owin.IOwinContext context)
        {
            var appRoleManager = new ApplicationRoleManager(new MySqlRoleStore<IdentityRole>("DBConnection"));
            
            return appRoleManager;
        }
    }
}