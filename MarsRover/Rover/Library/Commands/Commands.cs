using Rover.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Library
{
    public static class Commands
    {
        public static OrientationEnum Left(OrientationEnum orientationEnum)
        {
            int orientation = (int)orientationEnum;
            orientation += 1;

            if (orientation > 4)
            {
                orientation = 1;
            }

            return (OrientationEnum)orientation;
        }

        public static OrientationEnum Right(OrientationEnum orientationEnum)
        {
            int orientation = (int)orientationEnum;
            orientation -= 1;

            if (orientation < 1)
            {
                orientation = 4;
            }

            return (OrientationEnum)orientation;
        }
    }
}
