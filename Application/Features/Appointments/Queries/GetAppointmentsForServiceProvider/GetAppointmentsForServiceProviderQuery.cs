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
    public class GetAppointmentsForServiceProviderQuery : IRequest<Result<IEnumerable<GetAppointmentsForServiceProviderResponse>>>
    {
    }
}
