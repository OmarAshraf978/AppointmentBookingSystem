using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Modules;

namespace Application.Features.Appointments.Outputs
{
    #region GetMyAppointmentsResponse
    public class GetMyAppointmentsResponse
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public string ServiceName { get; set; } = null!;
        public string ServiceProviderName { get; set; } = null!;
    }
    #endregion
}
