using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Console;

namespace SOLIDPrinciples
{
    /// <summary>
    /// Open-Closed Principle:
    /// classes should be open for extension, but closed for modification.
    /// This can be implemented with an enterprise pattern: specification pattern (not a gang of four pattern)
    /// This principle may be followed through the use of interfaces that can be extended.
    /// Interface methods MUST be implemented, but interfaces can be expanded upon in design past these methods.
    /// </summary>

    public enum Color
    {
        Red, Green, Blue, Grey
    }

    public enum Size
    {
        Small, Medium, Large
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }

            Name = name;
            Color = color;
            Size = size;
        }
    }

    //Implements the specification pattern. Defines whether or not a product fits a certain criteria.
    //This interface can be extended to any number of varying specifications, and they must implement the IsSatisfied method
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    //Filters items of type T according to the specification. 
    //The filter method takes a parameter of a class that implements the ISpecification interface as a filter criteria
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification);
    }

    //Implementation of the ISpecification interface to check for color specification of Products
    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product product)
        {
            return product.Color == color;
        }
    }
    
    //Implementation of the ISpecification interface to check for size specification of Products
    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product product)
        {
            return product.Size == size;
        }
    }
    
    //Implementation of the ISpecification interface to check for name specification of Products
    public class NameSpecification : ISpecification<Product>
    {
        private string name;

        public NameSpecification(string name)
        {
            this.name = name;
        }

        public bool IsSatisfied(Product product)
        {
            return product.Name == name;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first;
            this.second = second;
        }
        
        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    //An implementation of the IFilter interface to filter Products.
    //Products will be filtered by the parameter of an implemented ISpecification for Products.
    public class ProductFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specification)
        {
            foreach (var item in items)
            {
                if (specification.IsSatisfied(item))
                {
                    yield return item;
                }
            }
        }
    }
    
    public class OpenClosed
    {
        //Example Main driver function (made nonstatic to not be used with project on run, only for example storage)
        void ExampleMain(string[] args)
        {
            Product apple = new Product("apple", Color.Red, Size.Small);
            Product melon = new Product("melon", Color.Green, Size.Medium);
            Product car = new Product("car", Color.Blue, Size.Large);

            Product[] products = {apple, melon, car};
            
            ProductFilter productFilter = new ProductFilter();
            WriteLine("Filtering products for large sized and blue objects:");
            foreach (var product in productFilter.Filter(
                products, 
                new AndSpecification<Product>(new ColorSpecification(Color.Blue),new SizeSpecification(Size.Large))))
            {
                WriteLine(product.Name);
            }
        }
    }
}