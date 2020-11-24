using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq.Extensions;
using NUnit.Framework;

namespace Section5
{
    /// Singleton Pattern: A component which is instantiated once only. 
    /// Singleton pattern is a controversial design pattern.
    ///
    /// The motivation for using the singleton pattern comes from the idea that for some components it only
    /// makes sense to have one in the system. (Database repositories, factories). Situations where the
    /// constructor call is expensive. Want to provide every single consumer with the same instance and prevent
    /// clients from creating other instances.
    ///
    /// When using singleton, want to prevent anyone from instantiating more than one singleton object.
    /// One step is making the singleton class's constructor private. The next step is typically to have
    /// a static field that exposes the singleton object. 
    ///
    /// Lazy construction:
    /// Lazy constructor passes in a lambda function for the initialization. Allows for only creating a singleton when
    /// the instance is accessed.
    ///
    /// Why is singleton a bad idea?:
    /// Testability issues induced by dependencies.
    ///
    /// One remedy to the woes of singletons is known as dependency injection. 

    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private static int instanceCount = 0;
        public static int Count => instanceCount;

        private SingletonDatabase()
        {
            instanceCount++;
            Console.WriteLine("Initializing database.");

            capitals = File.ReadAllLines("capitals.txt")
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                );
        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }
        
        //Singleton instance using the Lazy construction approach.
        private static Lazy<SingletonDatabase> instance = 
            new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        public static SingletonDatabase Instance => instance.Value;
    }

    [TestFixture]
    public class SingletonTest
    {
        [Test]
        public void IsSingletonTest()
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.That(db, Is.SameAs(db2)); //check that both instances reference the same object.
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            SingletonDatabase db = SingletonDatabase.Instance;

            Console.WriteLine(db.GetPopulation("Tokyo"));
        }
    }
}