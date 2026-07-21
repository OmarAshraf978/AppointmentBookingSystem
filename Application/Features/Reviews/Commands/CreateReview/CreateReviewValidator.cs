using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewValidator()
        {
            RuleFor(x => x.Rating).NotEmpty().InclusiveBetween(1, 5);
            RuleFor(x => x.Comment).NotEmpty().MaximumLength(200);
            RuleFor(x => x.ServiceId).NotEmpty().GreaterThan(0);
        }
    }
}
