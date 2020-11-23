using System;

namespace Section3
{
    /// Factories:
    /// Sometimes object creation logic becomes too convoluted.
    /// Constructor is limiting because it isnt descriptive.
    ///
    /// Another type of object creation (non piecewise, unlike Builder pattern)
    /// Object creation can be outsources to another function (factory method), or a hierarchy of factories.
    ///
    /// Cannot overload constructors with the same parameter types, which leads to complex and interesting solutions
    /// when only using constructors (not ideal). This is where the factory pattern can help simplify this kind of problem.
    ///
    /// Factory methods are used in replacement of the standard new() keyword for the constructor
    /// Below is an example of a class that has factory methods for creating different types of Point (cartesian or polar)
    /// 

    
    //NOTE: ReSharper provides a refactor tool for refactoring a constructor into a factory method
    // This will create a factory method based on the constructor, and will make the constructor private. 
    // The factory method invokes the constructor
    
    public class Point
    {
        private double x, y;
        
        //Factory Method: Name of factory method is not directly tied to the name of class
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        //Factory method
        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho*Math.Cos(theta), rho*Math.Sin(theta));
        }

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }
    
    public class FactoryMethods
    {
        void ExampleMain(string[] args)
        {
            Point point = Point.NewCartesianPoint(0, 10);
            Point polarPoint = Point.NewPolarPoint(45, 45);
            Console.WriteLine(point);
            Console.WriteLine(polarPoint);
        }
    }
}