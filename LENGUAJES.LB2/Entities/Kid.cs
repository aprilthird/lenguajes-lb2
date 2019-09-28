using System;
using System.Collections.Generic;
using System.Text;

namespace LENGUAJES.LB2.Entities
{
    public class Kid : Person
    {
        public Kid(int weight = Helpers.ConstantHelpers.AVERAGE_KID_WEIGHT) : base(weight)
        {
        }
    }
}
