using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Favorites.Outputs
{
    #region GetMyFavoritesResponse
    public class GetMyFavoritesResponse
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
    }
    #endregion
}
