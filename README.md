# Naml - a .NET XML Templating Engine
Naml is an easy way to build XML/HTML (xml compliant) documents.  Insipred by Jaml (http://edspencer.github.io/jaml/),
Naml provides, hopefully, an easy way to build XML templates in .NET.

## Quick Example

```
using System;
using System.Linq;
using Naml;

namespace Naml.TestConsole
{
    class Program
    {
        private class TemplateData
        {
            public int TheNumber { get; set; }
            public String TheString { get; set; }
        }

        static void Main(string[] args)
        {
            var xt = Naml.Create<TemplateData>(
                html => html.Set(
                    div => div.Set(
                        new { attribute_name = "value", stuff = "cool", number = 2 },
                        p => p.Set(t => string.Format("Item 1 = {0}", t.TheString)),
                        p => p.Set(new { data = "some stuff" }, "Item \" 2"),
                        ul => ul.Set(t => Enumerable.Range(1, t.TheNumber).Select(
                            i => (Action<Naml<TemplateData>>) (li => li.Set(string.Format("list item {0}", i))))
                        )
                    ),
                    div => div.Set((CData)"this is some cdata"),
                    script => script.Set(new { src = "http://www.something.com/test.js", type = "text/javascript" })
                )
            );

            Console.WriteLine(xt.ToString(new TemplateData { TheNumber = 5, TheString = "Hello" }));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(xt.ToString(new TemplateData { TheNumber = 2, TheString = "BLAH BLAH" }));
            Console.ReadLine();
        }
    }
}
```

## How it works
### Creating the document and root node
Use `Naml.Create<T>(Action<Naml<T>> node)` to generate a document with a root node.  The action delegate is used 
to build the contents of the root node.  Using lambda functions as node builders, the parameter name of the function is
used a node name.

The generic type `T` determines the .Net type that can be used as a template data source.

### Generating node contents
The `Action<Naml<T>>` delegate lets you call methods on a `<Naml<T>>` builder instance:

* The `Set()` method can be used to generate child nodes, text content or a CDATA node with or without attributes.
* The `Self()` method can be used to generate a self closing node with out without attributes.

#### The `Set()` method
Used to generate node contents:  

* (Optional) The first parameter, which is optional, is the node's attributes. It can either be:
 * Any old CLR object - property names and values determine the node's attribute names and associated values
 * Any object implementing `IDictionary<string, string>` - key names and values determine the node's 
   attribute names and associated values.
* (Optional) One of:
 * One or more `Action<Naml<T>>` node builders representing the child nodes - the parameter name of each node
   builder delegate drives the child node name.
 * A string representing a text contents of the node.
 * A string casted to type of `Naml.CData` to represent a CData content node.

In addition, any parameter can be replaced by a `Func<T, TargetType>` delegate to have template data of type `T`
drive the generation of attributes, child nodes, text and CDATA elements.  The different `Func<,>` types are:

* `Func<T, object>` is a template data function to generate attribute object.
* `Func<T, IDictionary<string, string>>` is a template data function to generate attribute string dictionary.
* `Func<T, IEnumerable<Action<Naml<T>>>` is a template data function to generate child nodes.
* `Func<T, string>` is a template data function to generate node text.
* `Func<T, Naml.CData>` is a template data function to generate node CDATA.

#### The `Self()` method
Used to generate a self-closing node.  There is one optional parameter to provide attributes to the node, similar
to how `Set()` accepts attributes.

[*NOTE*: the `Set()` and `Self()` methods facilitate argument types by having a crap-ton of overloads covering all the
cases :)]

### Generating XML document string
Use the `ToString(T source)` method to generate an XML string.  Provide an instance of type `T` as the argument
to drive delegate-driven content and attribute generation.

[*NOTE*: `Naml<T>.ToString() will throw a NotImplementedException.  You _must_ provide a template data object
parameter.]

### Using the non-generic Naml class
The non-generic `Naml` class is used to generate XML documents without template data.  Internally, `Naml` is
a sub-class of `Naml<object>`, all of the methods are available.  However, providing delegate-driven content
and attribute functions will always have a `null` passed into them when XML generation is done.

Use `Naml.ToString()` to generate XML content (yes, this one will _not_ thrown an exception).