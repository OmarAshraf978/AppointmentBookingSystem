using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Appointments.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Appointments.Queries.GetAppointmentById
{
    #region GetAppointmentByIdQuery
    public class GetAppointmentByIdQuery : IRequest<Result<GetAppointmentByIdResponse>>
    {
        public int Id { get; set; }
        public GetAppointmentByIdQuery(int id)
        {
            Id = id;
        }
    }
    #endregion
}
