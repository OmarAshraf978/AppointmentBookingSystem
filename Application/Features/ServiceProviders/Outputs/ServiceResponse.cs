using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ServiceProviders.Outputs
{
    #region ServiceResponse
    public class ServiceResponse
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public int DurationInMinutes { get; set; }
        public bool IsDeleted { get; set; }
    }
    #endregion
}
