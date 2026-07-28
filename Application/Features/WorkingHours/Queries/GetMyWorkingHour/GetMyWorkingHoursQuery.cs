using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.WorkingHours.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.WorkingHours.Queries.GetMyWorkingHour
{
    public class GetMyWorkingHoursQuery : IRequest<Result<IEnumerable<GetMyWorkingHourResponse>>>
    {
    }
}
