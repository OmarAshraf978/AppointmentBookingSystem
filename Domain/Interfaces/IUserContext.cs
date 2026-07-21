using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    #region IUserContext
    public interface IUserContext
    {
        public string UserId { get; }
        public string Role {  get; }
    }
    #endregion
}
