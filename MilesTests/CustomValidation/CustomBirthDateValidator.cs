using System;
using System.ComponentModel.DataAnnotations;

namespace MilesTests.CustomValidation
{
    public class CustomBirthDateValidator : ValidationAttribute
    {
        /// <summary>
        /// validator to be used as a dataAnotation and verifies if the input value date is older than now
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>true if the date is older than now</returns>
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime < DateTime.Now;
        }

    }
}
