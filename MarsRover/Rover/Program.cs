using Rover.Library;
using Rover.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rover
{
    static class Program
    {
        private static string[] _directionCommands = new[] { "left", "right" };
        private static OrientationEnum _orientation;
        private static int _position;
        private static int _y = 1;
        private static int _x = 1;
        private static readonly int _minXCoordinate = 1;
        private static readonly int _minYCoordinate = 0;
        private static readonly int _maxXCoordinate = 100;
        private static readonly int _maxYCoordinate = 99;

        static void Main(string[] args)
        {
            _position = 1;
            _orientation = OrientationEnum.South;
            bool result = false;

            while (!result)
            {
                for (int i = 0; i < 5; i++)
                {
                    var input = Console.ReadLine();
                    if (String.IsNullOrEmpty(input))
                    {
                        break;
                    }
                    int integer;

                    if (Int32.TryParse(input, out integer))
                    {
                        result = Move(integer);

                        if (result)
                        {
                            break;
                        }
                    }
                    else if (_directionCommands.Contains(input.ToLower()))
                    {
                        InvokeDirectionCommands(input);
                    }
                }
                Console.WriteLine($"Current position: {_position} {_orientation}");

                if (result)
                {
                    Console.WriteLine("Edge of area reached. Please press any key to exit.");
                    Console.ReadKey();
                }
            }
        }

        static void InvokeDirectionCommands(string methodName)
        {
            var type = Assembly.GetExecutingAssembly()
                                .GetTypes()
                                .Single(w => w.Name == nameof(Commands));

            methodName = methodName.First().ToString().ToUpper() + methodName.Substring(1);

            var method = type.GetMethod(methodName);
            try
            {
                _orientation = (OrientationEnum)method.Invoke(type, new object[] { _orientation });
            }
            catch (Exception)
            {
                Console.WriteLine($"Unable to invoke command {methodName}");
            }
        }

        static bool Move(int metres)
        {
            bool result = false;
            switch (_orientation)
            {
                case OrientationEnum.North:
                    _y -= metres;

                    if (_y <= _minYCoordinate)
                    {
                        _y = _minYCoordinate;
                        result = true;
                    }
                    break;
                case OrientationEnum.South:
                    _y += metres;

                    if (_y >= _maxYCoordinate)
                    {
                        _y = _maxYCoordinate;
                        result = true;
                    }
                    break;
                case OrientationEnum.East:
                    _x += metres;

                    if (_x > _maxXCoordinate)
                    {
                        _x = _maxXCoordinate;
                        result = true;
                    }
                    break;
                case OrientationEnum.West:
                    _x -= metres;

                    if (_x < _minXCoordinate)
                    {
                        _x = _minXCoordinate;
                        result = true;
                    }
                    break;
            }

            _position = (_y * 100) + _x;
            return result;
        }
    }
}
