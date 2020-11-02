using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Console;

namespace SOLIDPrinciples
{
    /// <summary>
    /// Liskov Substitution Principle:
    /// This principle works such that you should always be able to upcast to the base type for inheritance
    /// without any changes to behavior.
    /// If there is an override in child class, it should be indicated as such.
    /// The override is necessary to avoid unexpected behavior/bugs when typing subclass as base class.
    /// </summary>

    //Standard rectangle class with virtual properties which allow for override of those properties.
    
    public class Rectangle
    {
        //The virtual keyword allows for it to be overridden by any class that inherits it.
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {
            
        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    //Square class inherits from rectangle.
    //Overrides base class properties such that the conditions of a square (W=H) are satisfied.
    public class Square : Rectangle
    {
        public override int Height
        {
            set { base.Width = base.Height = value; }
        }

        public override int Width
        {
            set { base.Width = base.Height = value; }
        }
    }
    
    public class LiskovSolution
    {
        static public int Area(Rectangle rectangle) => rectangle.Width * rectangle.Height;

        //Example Main driver function (nonstatic, not be used with project on run, only for example purposes)
        void ExampleMain(string[] args)
        {
            Rectangle rectangle = new Rectangle(10, 10);
            WriteLine($"{rectangle} has area {Area(rectangle)}");
            
            Rectangle square = new Square();
            square.Width = 5;
            WriteLine($"{square} has area {Area(square)}");
        }
    }
}