using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.Features.Services.Outputs;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Services.Commands.UpdateService
{
    public class UpdateServiceCommand : IRequest<Result<UpdateServiceResponse>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string ServiceName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
