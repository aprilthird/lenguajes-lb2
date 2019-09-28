using System;
using System.Collections.Generic;
using System.Text;

namespace LENGUAJES.LB2.Entities
{
    public class Adult : Person
    {
        public Adult(int weight = Helpers.ConstantHelpers.AVERAGE_ADULT_WEIGHT) : base(weight)
        {
        }
    }
}
