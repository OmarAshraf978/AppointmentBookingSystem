using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modules
{
    #region AppointmentModule
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public string UserId { get; set; } = default!;
        public int ServiceId { get; set; }
        public Service Service { get; set; } = default!;
    }
    #endregion
}
