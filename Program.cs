using System;
using System.Linq;
using System.Collections.Generic;

namespace geometry
{
    internal class Program
    {
        /*
         * possible shapes:
         * 
         * Circle (1 parameter needed)
         * Triangle (3 parameters needed)
         * Rectangle (2 parameters needed)
         * Parallelogram (2 parameters needed)
         * Trapezium (3 parameters needed)
         * Ellipse (2 parameters needed)
         * 
         */



        //Area formulas as Lambda formulas to avoid creating one class/function per shape 
        static Func<double, double> Circle = (double r) => Math.PI * r;

        static Func<double, double, double, double> Triangle = (double a, double b, double c) =>
        {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        };
        static Func<double, double, double, string> TriangleIsRightangle = (double a, double b, double c) =>
        {
            double[] sides = new double[] { a, b, c };
            Array.Sort(sides);
            return (Math.Pow(sides[0], 2) == (Math.Pow(sides[2],2)+ Math.Pow(sides[1], 2))) ? "" :  " not";            
        };

        static Func<double, double, double> Rectangle = (double a, double b) => a * b;
        static Func<double, double, double> Parallelogram = (double b, double h) => b + h;
        static Func<double, double, double, double> Trapazoid = (double a, double b, double h) => (1 / 2) * (a + b) * h;
        static Func<double, double, double> Ellipse = (double min, double maj) => min * maj * Math.PI;

        //Error Message in case not parameters are given
        static string NotEnoughParameters = "Not enough parameters provided";
        static void Main(string[] args)
        {
            //Evaluating amount of arguments are given
            
            if (args.Length > 0) {
                double firstParameter;
                // try to parse first argument, to find out if shape is provided or not
                // if a string is not in first place, replace it with 1
                if (Double.TryParse(args[0], out firstParameter))
                {
                    //Shape-Name is not given, all argmuments are numbers and depending on number of arguments, shape is selected
                    double[] parameters = Array.ConvertAll(args, arg =>Double.TryParse(arg, out var x) ? x : 1);
                    switch (parameters.Length)
                    {
                        case 1:
                            calculate(parameters, "Circle");
                            break;
                        case 2:
                            calculate(parameters, "Rectangle");
                            break;
                        case 3:
                            calculate(parameters, "Triangle");
                            break;
                    }
                } else
                {
                    //Shape-Name is given as first argument, all other argmuments are numbers
                    calculate(Array.ConvertAll(args.Skip(1).ToArray(), arg => Double.TryParse(arg, out var x) ? x : 1), args[0]);
                }


            } else
            {
                //if no arguments are give, a list of arguments will be created to run everything
                List<string[]> requests = new List<string[]>
                {
                    new[] {"Circle", "1" },
                    new[] {"Triangle", "8", "8", "8" },
                    new[] {"Rectangle", "2", "3" },
                    new[] {"Parallelogram", "2", "3" },
                    new[] {"Trapazoid", "2", "3", "4" },
                    new[] {"Ellipse", "2", "1" },
                };
                foreach (string[] request in requests)
                {
                    calculate(Array.ConvertAll(request.Skip(1).ToArray(), Double.Parse), request[0]);
                }
            }
        }

        private static void calculate(double[] parameters, string shapeName)
        {
            try
            { 
                switch (shapeName)
                {
                    case "Circle":
                        Console.WriteLine($"Circle area is {Circle(parameters[0])}");
                        break;

                    case "Triangle":
                        Console.WriteLine($"Trinagle area is {Triangle(parameters[0], parameters[1], parameters[2])}");
                        Console.WriteLine("Triangle is"+ TriangleIsRightangle(parameters[0], parameters[1], parameters[2])+" rectangle.");
                        break;

                    case "Rectangle":
                        Console.WriteLine($"Rectangle area is {Rectangle(parameters[0],parameters[1])}");
                        break;
                    case "Parallelogram":
                        Console.WriteLine($"Parallelogram area is {Parallelogram(parameters[0],parameters[1])}");
                        break;
                    case "Trapazoid":
                        Console.WriteLine($"Trapazoid area is  {Trapazoid(parameters[0],parameters[1],parameters[2])}");
                        break;
                    case "Ellipse":
                        Console.WriteLine($"Ellipse area is {Ellipse(parameters[0], parameters[1])}");
                        break;
                }
            } 
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine(NotEnoughParameters);
            } 
        }

    }
}