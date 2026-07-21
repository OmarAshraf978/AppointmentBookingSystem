using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ServiceProviders.Outputs
{
    #region GetAllServiceProvidersResponse
    public class GetAllServiceProvidersResponse
    {
        public int Id { get; set; }
        public string BusinessName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<ServiceResponse> AllServices { get; set; } = [];
    }
    #endregion
}
