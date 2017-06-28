using FluentValidation;
using NewPDR.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewPDR.Web.Validators
{
    public class PDRDetailsViewModelValidator : AbstractValidator<PDRDetailsViewModel>
    {
        public PDRDetailsViewModelValidator()
        {
            When(x => x.Objectives.Any(y => y.FullYearRatings.Any(l => string.Equals(l.Value, "0"))), () =>
            {
                RuleFor(x => x.SelectedObjectiveOverallRatingId)
                             .NotEmpty()
                            .WithMessage("cannot be empty.");
            });

        }

    }
}