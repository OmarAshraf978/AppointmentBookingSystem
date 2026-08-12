using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Appointments.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Appointments.Queries.GetAppointmentsForServiceProvider
{
    #region GetAppointmentsForServiceProviderQuery
    public class GetAppointmentsForServiceProviderQuery : IRequest<Result<IEnumerable<GetAppointmentsForServiceProviderResponse>>>
    {
    }
    #endregion
}
