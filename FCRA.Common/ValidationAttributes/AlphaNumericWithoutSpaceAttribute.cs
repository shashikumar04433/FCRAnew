﻿using System.ComponentModel.DataAnnotations;

namespace FCRA
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class AlphaNumericWithoutSpaceAttribute : RegularExpressionAttribute
    {
        public AlphaNumericWithoutSpaceAttribute() : base(@"^[A-Za-z0-9]+$")
        {
            if (!string.IsNullOrWhiteSpace(this.ErrorMessage))
                base.ErrorMessage = this.ErrorMessage;
            else base.ErrorMessage = "Only alphanumeric [A-Z, a-z, 0-9] allowed";
        }
    }
}
