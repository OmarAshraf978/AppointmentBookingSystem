using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserContext
    {
        public string UserId { get; }
        public string Role {  get; }
    }
}
