using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Userspace.Api.Resources;

namespace Userspace.Api.Validators
{
    public class SaveLinkResourceValidator : AbstractValidator<SaveLinkResource>
    {
        public SaveLinkResourceValidator()
        {

        }
    }
}
