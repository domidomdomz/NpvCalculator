using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpvCalculator.Core.Common
{
    public class ValidationError
    {
        public required string PropertyName { get; set; }
        public required string ErrorMessage { get; set; }
    }
}
