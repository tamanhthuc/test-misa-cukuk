using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Attribute
{
    public class MaxDateTodayAttribute: ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
           DateTime? dateOfBirth = (DateTime?)value;

            return dateOfBirth == null || dateOfBirth <= DateTime.Today;
        }
    }
}
