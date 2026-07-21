using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Modules;

namespace Application.Features.Appointments.Outputs
{
    #region GetAppointmentByIdResponse
    public class GetAppointmentByIdResponse
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
        public string BusinessName { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
    #endregion
}
