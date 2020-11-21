using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;


namespace Section2
{
    /// Builder design Pattern:
    /// Some objects can be created in a single line simple constructor call.
    /// Other objects may require many arguments or pieces for construction.
    /// Rather than having an unproductive and complicated constructor with many arguments,
    /// the builder pattern can be used to construct piece by piece.
    /// 
    /// In the example below, the builder pattern is used to construct a tree
    /// of HTML elements. This simplifies and somewhat abstracts the process of creating that tree.
    /// For example, the user of the builder does not have to encode any chevron brackets (<>) or indents
    ///
    /// 
    /// Fluent Builder:
    /// In the example below, when a child is added using the builder, the method returns a HtmlBuilder.
    /// This allows for chaining of add operations.
    
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> elements = new List<HtmlElement>();
        private const int indentSize = 2;

        public HtmlElement()
        {
            
        }

        public HtmlElement(string name, string text)
        {
            Name = name;
            Text = text;
        }

        private string ToStringImpl(int indent)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string i = new string(' ', indentSize * indent); // how many spaces to indent
            stringBuilder.AppendLine($"{i}<{Name}>");
            
            if (!string.IsNullOrWhiteSpace(Text))
            {
                stringBuilder.Append(new string(' ', indentSize * (indent + 1)));
                stringBuilder.AppendLine($"{Text}");
            }
            
            foreach (var element in elements)
            {
                stringBuilder.Append(element.ToStringImpl(indent + 1));
            }

            stringBuilder.AppendLine($"{i}</{Name}>");

            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class HtmlBuilder
    {
        private readonly string rootName;
        private HtmlElement root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = rootName;
        }

        // Note that this method's return type is HtmlBuilder, this allows for chaining of function calls
        public HtmlBuilder AddChild(string childName, string childText)
        {
            HtmlElement element = new HtmlElement(childName, childText);
            root.elements.Add(element);

            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement{Name = rootName};
        }
    }
    
    public class Builder
    {
        //Example Main driver function (made nonstatic to not be used with project on run, only for example storage)
        void ExampleMain(string[] args)
        {
            HtmlBuilder builder = new HtmlBuilder("ul");
            
            //Note the use of function chaining below using the builder (Fluent Interface)
            builder.AddChild("li", "hello").AddChild("li", "world");
            
            WriteLine(builder.ToString());
        }
    }
}