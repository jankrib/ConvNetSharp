using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepRL.Helpers;

namespace DeepRL.Model
{
    public class Eye
    {
        public double Angle { get; set; }

        public double MaxRange { get; set; } = 85;

        public double SensedProximity { get; set; } = 85;

        public HitType HitType { get; set; }

        public Eye(double angle)
        {
            Angle = angle;
        }
    }
}
