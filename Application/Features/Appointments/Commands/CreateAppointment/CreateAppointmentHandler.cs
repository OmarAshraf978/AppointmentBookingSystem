using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Appointments.Outputs;
using AutoMapper;
using Domain.Entities.Modules;
using Domain.Interfaces;
using MediatR;
using Shared.ResultPattern;

namespace Application.Features.Appointments.Commands.CreateAppointment
{
    #region CreateAppointmentHandler
    public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, Result<CreateAppointmentResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public CreateAppointmentHandler(IUnitOfWork unitOfWork, IUserContext userContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task<Result<CreateAppointmentResponse>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = _mapper.Map<Appointment>(request);

            var service = await _unitOfWork.GetRepository<Service, int>()
                                           .GetBySpecificColumnWithIncludeAsync(x => x.Id == request.ServiceId,
                                                                                x => x.ServiceProvider);

            if (service is null)
                return Error.NotFound($"Service With Id : {request.ServiceId} Not Found");

            if (service.IsDeleted)
                return Error.NotFound($"Service With Id : {request.ServiceId} Not Available");

            if (!service.ServiceProvider.IsApproved)
                return Error.Failure("This Service Provider Is Not Approved.");

            if (service.ServiceProvider.IsDeleted)
                return Error.Failure("This Service Provider Is Not Available.");

            if (service.ServiceProvider.UserId == _userContext.UserId)
                return Error.Failure("You cannot book your own service.");

            var start = request.AppointmentDate;
            var end = start.AddMinutes(service.DurationInMinutes);

            var workingHours = await _unitOfWork.GetRepository<WorkingHour, int>()
                                                .GetAllBySpecificColumnAsync(x => x.ServiceProviderId == service.ServiceProviderId
                                                                          && x.DayOfWeek == request.AppointmentDate.DayOfWeek.ToString());
            if (!workingHours.Any())
                return Error.Failure("The Service Provider Is Not Available On This Day.");

            var appointmentStartTime = request.AppointmentDate.TimeOfDay;
            var appointmentEndTime = appointmentStartTime.Add(TimeSpan.FromMinutes(service.DurationInMinutes));

            var isWithinWorkingHours = workingHours.Any(x =>
                appointmentStartTime >= x.StartTime &&
                appointmentEndTime <= x.EndTime);

            if (!isWithinWorkingHours)
                return Error.Failure("The Appointment Time Is Outside The Service Provider's Working Hours.");

            var appointments = await _unitOfWork.GetRepository<Appointment, int>()
                                                .GetAllBySpecificColumnWithIncludeAsync(x => x.Service.ServiceProviderId == service.ServiceProviderId &&
                                                                            (x.Status == AppointmentStatus.Pending ||
                                                                             x.Status == AppointmentStatus.Approved),
                                                                             x => x.Service);

            
            var hasConflict = appointments.Any(x =>
            {
                var existingStart = x.AppointmentDate;
                var existingEnd = existingStart.AddMinutes(x.Service.DurationInMinutes);
                return start < existingEnd && end > existingStart;
            });
            if (hasConflict)
                return Error.Failure("This Appointment Time Is Already Booked");

            appointment.Status = AppointmentStatus.Pending;
            appointment.UserId = _userContext.UserId;

            await _unitOfWork.GetRepository<Appointment, int>().AddAsync(appointment);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 0)
                return Error.Failure("Something Went Wrong");

            var createdAppointment = await _unitOfWork.GetRepository<Appointment, int>()
                                                      .GetBySpecificColumnWithIncludeAsync(x => x.Id == appointment.Id,
                                                                                           x => x.Service,
                                                                                           x => x.Service.ServiceProvider);

            return _mapper.Map<CreateAppointmentResponse>(createdAppointment);

        }
    }
    #endregion
}
