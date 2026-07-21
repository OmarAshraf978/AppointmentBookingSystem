using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reviews.Outputs
{
    public class GetReviewsForServiceResponse
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
    }
}
