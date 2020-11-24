using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Section4
{
    /// <summary>
    /// Prototype design pattern:
    /// An existing object (partially or fully constructed) has a copy made, in which variations are made upon.
    ///
    /// Deep copy: copying not just the object, but also all of the objects references.
    ///
    /// ICloneable is not a great solution for copying because it does not specify if Clone() is a deep or shallow copy.
    /// Also the Clone() method from the ICloneable interface returns an Object type, which may require type casting when
    /// the client is using class Clone() implementation.
    ///
    /// Copy constructors. Copy constructors construct and object using another object of the same type as a parameter.
    /// Copy constructors are a C++ thing, and may not be as recognized in the C# world.
    ///
    /// An explicit deep copy interface can be created for copying objects. But there is still an issue such that the deep copy
    /// must be implemented for each of the objects in the hierarchy. This can be remedied by copying through serialization.
    ///
    /// Serialization is how the prototype pattern is done. An extensionmethods class can be used to implement the DeepCopy()
    /// method. For the serialization to work correctly, any class that will be serialized must be tagged
    /// [Serializable] (root class & members).
    ///
    /// One example below uses a binary formatter to serialize the object and deserialize. One caveat with this approach is that
    /// every object (root and member) must be tagged with [Serializable]
    ///
    /// Another example below uses XML formatter to serialize and deserialize object. This does NOT require that classes be
    /// tagged with [Serializable]. A caveat with this approach though, is that classes can only be serialized if they have
    /// a parameterless constructor.
    ///
    /// NOTE: different methods/forms of serialization will require different specifications be met, which can be seen in the comparison
    /// between the binary vs xml serialization methods. 
    /// 
    /// </summary>

    //Extension methods class that includes DeepCopy methods using serialization. 
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            //serialize, and then deserialize the object
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, self); //serialize the object self, to the stream

            stream.Seek(0, SeekOrigin.Begin); // return to the beginning of the stream
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T) copy; //cast copy to T and return
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T) s.Deserialize(ms);
            }
        }
    }

    public class Person
    {
        public string[] Names;
        public Address Address;

        public Person()
        {
            
        }

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        //Copy Constructor
        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(Address);
        }
        
        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ",Names)}, {nameof(Address)}: {Address}";
        }
    }
    
    public class Address
    {
        public string StreetName;
        public int HouseNumber;

        public Address()
        {
            
        }
        
        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public Address(Address other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Person john = new Person(new []{"John", "smith"},
                new Address("Estes St", 5000)
                );

            Person jane = john.DeepCopyXml();
            jane.Names[0] = "Jane";

            jane.Address.HouseNumber = 555;
            
            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}