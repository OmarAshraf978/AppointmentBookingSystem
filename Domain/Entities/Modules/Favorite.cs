using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modules
{
    #region FavoriteModule
    public class Favorite
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;
    }
    #endregion
}
