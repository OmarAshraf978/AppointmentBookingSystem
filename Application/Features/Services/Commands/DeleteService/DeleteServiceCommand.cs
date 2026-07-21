using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.Features.Services.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Services.Commands.DeleteService
{
    #region DeleteServiceCommand
    public class DeleteServiceCommand : IRequest<Result<DeleteServiceResponse>>
    {
        public int Id { get; set; }
        public DeleteServiceCommand(int id)
        {
            Id = id;
        }
    }
    #endregion
}
