using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using static System.Console;

namespace Section2
{
    /// Fluent Builder inheritance with recursive generics:
    /// The fluent builder approach works well until you want to inherit from it.
    ///
    /// Once a fluent builder class is inherited from, the ability to chain properly in the fluent style
    /// becomes broken. this is because when the parent class's method is called, it returns a parent class,
    /// which has no knowledge of the inheriting class's methods.
    ///
    /// This is remedied by using recursive generics. The return type in methods needs to reference a generic
    /// type parameter for the class. 
    /// 

    public class Person
    {
        public string Name;
        public string Position;

        //Exposing an objects builder. 
        public class Builder : PersonJobBuilder<Builder>
        {
            
        }
        
        public static Builder New => new Builder(); //whenever a new object is created, create new builder

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person(); //Protected, not private, because of using inheritance

        public Person Build()
        {
            return person;
        }
    }

    public class PersonInfoBuilder<SELF> 
        : PersonBuilder
        where SELF : PersonInfoBuilder<SELF>
    //Restrict SELF, to avoid any 
    {
        
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF) this; 
        }
    }

    public class PersonJobBuilder<SELF> 
        : PersonInfoBuilder<PersonJobBuilder<SELF>>
        where SELF : PersonJobBuilder<SELF>
    //Further using generic to allow for fluent builder with inheritance
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            return (SELF) this;
        }
    }

    
    public class RecursiveGenerics
    {
        //Example Main driver function (made nonstatic to not be used with project on run, only for example storage)
        void ExampleMain(string[] args)
        {
            //Creating a person object using its exposed builder property. This allows for the fluent method calls
            //    while using inheritance. Note that the Called method is used from PersonInfoBuilder, and then the
            //    WorksAsA method is used from PersonJobBuilder.
            //    Finally, the Build method, from the abstract class, is called to return the built Person object. 
            Person grant = Person.New.Called("Grant").WorksAsA("Programmer").Build();

            Console.WriteLine(grant);
        }
    }
}