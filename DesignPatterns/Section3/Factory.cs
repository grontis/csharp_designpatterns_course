using System;


namespace Section3
{
    /// Factory:
    /// In terms of single responsibility principle,
    /// the construction of an object could be considered a single responsibility
    ///
    /// Instead of having a bunch of factory methods inside of a class,
    /// could just create a factory class.
    ///
    /// When making a factory for creating objects, there arises the issue of that factory class
    /// needing access to the product object's constructor. One possible solution to this would be
    /// to make the constructor public, but there are reasons you may not want to do this, because if you are
    /// using the factory pattern you probably won't want to expose the constructor.

    public class Point1
    {
        private double x, y;
        
        public Point1(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }

    public static class Point1Factory
    {
        public static Point1 NewCartesianPoint(double x, double y)
        {
            return new Point1(x, y);
        }

        public static Point1 NewPolarPoint(double rho, double theta)
        {
            return new Point1(rho*Math.Cos(theta), rho*Math.Sin(theta));
        }
    }
    
    public class Factory
    {
        void ExampleMain(string[] args)
        {
            Point1 point = Point1Factory.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
        }
    }
}