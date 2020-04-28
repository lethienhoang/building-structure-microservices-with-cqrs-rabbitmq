using Framework.Domain;

namespace App.Services.Identity.Domain
{
    public class Role : EntityBase
    {
        public string RoleName { get; private set; }

        protected Role() { }
    }
}
