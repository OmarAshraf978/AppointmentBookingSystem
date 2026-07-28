using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.WorkingHours.Outputs
{
    public class GetWorkingHoursByServiceProviderIdResponse
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; } = null!;
    }
}
