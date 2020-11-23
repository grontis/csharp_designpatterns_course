using System;
using System.Threading.Tasks;

namespace Section3
{
    /// Asynchronous factory method:
    /// Asynchronous methods cannot be executed in constructors.
    /// Compiler will not allow for use of await within constructor
    ///
    /// A factory method allows for the asynchronous method to be called.
    /// Factory methods prohibit use of the constructor by making private,
    /// and allows for creating an object that uses async logic.

    public class Foo
    {
        //Constructor made private to follow factory pattern. 
        private Foo()
        {
            
        }

        private async Task<Foo> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        public static Task<Foo> CreateAsync()
        {
            var result = new Foo(); //call constructor to create object
            return result.InitAsync(); //call asynchronous method and return resulting object
        }
    }
    public class AsyncFactoryMethod
    {
        public static async Task ExampleMain(string[] args)
        {
            Foo foo = await Foo.CreateAsync(); //asynchronous "construction" of object using factory method
        }
    }
}