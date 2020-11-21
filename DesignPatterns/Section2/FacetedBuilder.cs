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
        /// Faceted Builder:
    /// Sometimes a single builder isn't enough. May want several builders that are responsible
    /// for building different aspects of an object.
    ///
    /// Each "facet" is a builder that builds a facet of an object.

    public class Person
    {
        //address info
        public string StreetAddress, PostCode, City;
        
        //employment info
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(PostCode)}: {PostCode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    public class PersonBuilder //facade
    {
        // reference to the object being built
        protected Person person = new Person();
        
        // want to expose PersonJobBuilder and PersonAddressBuilder
        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);
        
        //Implicit conversion operator for person. This allows for printing the person without introducing extra API
        // This may not be the best programming practice in some cases though.
        public static implicit operator Person(PersonBuilder personBuilder)
        {
            return personBuilder.person;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person; //passing in the object being built to the builder.
            //stored in the field inherited from PersonBuilder
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostCode(string postcode)
        {
            person.PostCode = postcode;
            return this;
        }

        public PersonAddressBuilder InCity(string city)
        {
            person.City = city;
            return this;
        }
    }
    
    public class FacetedBuilder
    {
        //Example Main driver function (made nonstatic to not be used with project on run, only for example storage)
        void ExampleMain(string[] args)
        {
            PersonBuilder personBuilder = new PersonBuilder();

            //Fluent interface using two sub builders.
            //Notice that the code below is using the implicit conversion from the PersonBuilder class,
            //    allowing for the WriteLine to print as a person object. 
            Person person = personBuilder
                .Lives
                .At("5000 N Garfunkle")
                .WithPostCode("80123")
                .InCity("FlavorTown")
                .Works
                .At("Blizzard")
                .AsA("Programmer")
                .Earning(150000);
            WriteLine(person);
        }
    }
}