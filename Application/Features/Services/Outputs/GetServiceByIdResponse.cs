using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Services.Outputs
{
    #region GetServiceByIdResponse
    public class GetServiceByIdResponse
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public int DurationInMinutes { get; set; }
    }
    #endregion
}
