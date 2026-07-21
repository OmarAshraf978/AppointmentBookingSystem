using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modules
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; } 
        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; } = null!;
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
