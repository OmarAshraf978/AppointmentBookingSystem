using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.IdentityModule;

namespace Application.Interfaces
{
    public interface IJwtProvider
    {
        public Task<String> GenerateTokenAsync(ApplicationUser applicationUser);
    }
}
