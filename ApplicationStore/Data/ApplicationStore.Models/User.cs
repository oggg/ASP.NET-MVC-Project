using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ApplicationStore.Models
{
    public class User : IdentityUser
    {
        private ICollection<Application> applications;
        private ICollection<Rating> ratings;

        public User()
        {
            this.applications = new HashSet<Application>();
            this.ratings = new HashSet<Rating>();
        }

        public bool Developer { get; set; }

        public bool? Banned { get; set; }

        public virtual ICollection<Application> Applications { get { return this.applications; } set { this.applications = value; } }

        public virtual ICollection<Rating> Ratings { get { return this.ratings; } set { this.ratings = value; } }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //public ClaimsIdentity GenerateUserIdentity(UserManager<User> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}

        //public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        //{
        //    return Task.FromResult(this.GenerateUserIdentity(manager));
        //}
    }
}
