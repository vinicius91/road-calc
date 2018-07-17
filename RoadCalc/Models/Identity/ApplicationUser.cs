using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RoadCalc.Context;

namespace RoadCalc.Models.Identity
{
    [Table("aspnetusers")]
    public class ApplicationUser : IdentityUser<int, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this,
                DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }

        public string GetNomeCompleto()
        {
            var stringBuilder = new StringBuilder();
            //Get the first name string
            stringBuilder.Append(Claims.FirstOrDefault(x => x.ClaimType == "Nome") != null
                ? Claims.FirstOrDefault(x => x.ClaimType == "Nome").ClaimValue
                : "Nome não encontrado");

            stringBuilder.Append(" ");
            //Get the last name string
            stringBuilder.Append(Claims.FirstOrDefault(x => x.ClaimType == "Sobrenome") != null
                ? Claims.FirstOrDefault(x => x.ClaimType == "Sobrenome").ClaimValue
                : "Sobrenome não encontrado");

            return stringBuilder.ToString();
        }

    }

    [Table("aspnetuserroles")]
    public class UserRoleIntPk : IdentityUserRole<int>
    {
    }
    [Table("aspnetuserclaims")]
    public class UserClaimIntPk : IdentityUserClaim<int>
    {
    }
    [Table("aspnetuserlogins")]
    public class UserLoginIntPk : IdentityUserLogin<int>
    {
    }

    public class RoleIntPk : IdentityRole<int, UserRoleIntPk>
    {
        public RoleIntPk() { }
        public RoleIntPk(string name) { Name = name; }
    }

    public class UserStoreIntPk : UserStore<ApplicationUser, RoleIntPk, int,
        UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public UserStoreIntPk(EstradasContext context)
            : base(context)
        {
        }
    }

    public class RoleStoreIntPk : RoleStore<RoleIntPk, int, UserRoleIntPk>
    {
        public RoleStoreIntPk(EstradasContext context)
            : base(context)
        {
        }
    }
}