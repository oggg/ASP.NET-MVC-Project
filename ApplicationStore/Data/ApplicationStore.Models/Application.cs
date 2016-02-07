using System.Collections.Generic;

namespace ApplicationStore.Models
{
    public class Application
    {
        private ICollection<User> installedBy;

        public Application()
        {
            this.installedBy = new HashSet<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public virtual ICollection<User> InstalledBy { get { return this.installedBy; } set { this.installedBy = value; } }

        // TODO: add rating
    }
}