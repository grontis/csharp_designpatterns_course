using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static System.Console;

namespace SOLIDPrinciples
{
    /// Dependency Inversion Principle:
    /// High level parts of the system should not depend on lower level parts of the system directly.
    /// There should be some form of abstraction.
    ///
    /// Dont want to access and rely on lower level classes and components in such a way that the higher level
    /// class or component is too dependent on the lower level class implementation and data specifics.
    ///
    /// An interface can be used to abstract the ways in which the data for the lower level class is accessed.
    ///
    /// In the example classes below, the Relationships class represents a low level class,
    /// and the logic within the Main() method in the Program class is a high level class.
    /// The IRelationshipBrowser interface serves as a way to abstract the access to the low level class.
    ///
    /// This removes the strong dependency of high level objects to low level objects. If this is implemented properly,
    /// a low level class's implementation may change without causing dependency issues with the higher level class.
    ///

    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name;
        
    }

    public interface IRelationshipsBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }
    
    
    //low level parts of the system
    public class Relationships : IRelationshipsBrowser
    {
        public List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations.Where(
                x => x.Item1.Name == name &&
                     x.Item2 == Relationship.Parent
            ).Select(relation => relation.Item3);
        }
    }
    
    
    class DependencyInversion
    {
        public DependencyInversion(IRelationshipsBrowser relationshipsBrowser)
        {
            foreach (var relation in relationshipsBrowser.FindAllChildrenOf("John"))
            {
                WriteLine($"John has a child called {relation.Name}");
            }
        }
        
        //Example Main driver function (nonstatic, not be used with project on run, only for example purposes)
        void ExampleMain(string[] args)
        {
            var parent = new Person {Name = "John"};
            var child1 = new Person {Name = "Jason"};
            var child2 = new Person {Name = "Mary"};
            
            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            relationships.FindAllChildrenOf("John");
        }
    }
    
}