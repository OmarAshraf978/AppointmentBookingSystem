using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Authentication.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Authentication.Queries.GetCurrentUser
{
    #region GetCurrentUserQuery
    public class GetCurrentUserQuery : IRequest<Result<GetCurrentUserResponse>>
    {
    }
    #endregion
}
