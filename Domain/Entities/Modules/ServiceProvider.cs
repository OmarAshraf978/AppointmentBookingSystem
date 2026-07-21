using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modules
{
    #region ServiceProviderModule
    public class ServiceProvider
    {
        public int Id { get; set; }
        public string BusinessName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string UserId { get; set; } = default!;
        public bool IsApproved { get; set; }
        public ICollection<Service> Services { get; set; } = new List<Service>();
        public bool IsDeleted { get; set; }
    }
    #endregion
}
