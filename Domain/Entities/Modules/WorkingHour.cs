using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modules
{
    #region WorkingHourModule
    public class WorkingHour
    {
        public int Id { get; set; }
        public string DayOfWeek { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int ServiceProviderId { get; set; }
        public ServiceProvider ServiceProvider { get; set; } = null!;
    }
    #endregion
}
