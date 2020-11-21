using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using static System.Console;

namespace FunctionalBuilder
{
    /// Functional Builder:
    /// Builder pattern using a functional approach.
    
    public class Person
    {
        public string Name, Position;
    }

    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf> //restrict typing
        where TSubject : new() //restrictions so that there is a constructor for subject
    {
        //List of actions to mutate a Person object.
        private readonly List<Func<TSubject, TSubject>> actions = new List<Func<TSubject, TSubject>>();
        
        public TSelf Do(Action<TSubject> action)
            => AddAction(action);

        public TSubject Build()
            => actions.Aggregate(new TSubject(), (p, f) => f(p)); 
        //compact a list into a single application
        //p is a person, and f is a function, and the function is called upon with person as a parameter

        private TSelf AddAction(Action<TSubject> action)
        {
            actions.Add(product =>
            {
                action(product); //take an action on the product
                return product; //return the product
            });
            return (TSelf) this; //make method fluent, need to cast to TSelf
        }
    }

    //Sealed class, cannot be inherited from. Can only extend using extension methods. 
    public sealed class PersonBuilder
        : FunctionalBuilder<Person, PersonBuilder>
    {
        public PersonBuilder Called(string name)
            => Do(p => p.Name = name);
    }

    public static class PersonBuilderExtensions
    {
        // this method below is an extension method upon the PersonBuilder class
        public static PersonBuilder WorksAs
            (this PersonBuilder builder, string position)
            => builder.Do(p => p.Position = position);
    }

    
    public class FunctionalBuilder
    {
        //Example Main driver function (made nonstatic to not be used with project on run, only for example storage)
        void ExampleMain(string[] args)
        {
            var person = new PersonBuilder()
                .Called("Grant")
                .WorksAs("Developer")
                .Build();
        }
    }
}