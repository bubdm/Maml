# Maml - a Microsoft .Net XML Templating Engine
Maml is an easy way to build XML/HTML (xml compliant) documents.  Insipred by Jaml (http://edspencer.github.io/jaml/),
Maml provides, hopefully, an easy way to build XML templates in Microsoft .NET applications.

## Quick Example

This console application:
```
using System;
using System.Linq;
using Maml;

namespace Maml.TestConsole
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
            var xt = new Maml<TemplateData>(t => 
                html => html.Set(
                    div => div.Set(
                        new { attribute_name = "value", stuff = "cool", number = 2 },
                        p => p.Set(string.Format("Item 1 = {0}", t.TheString)),
                        p => p.Set(new { data = "some stuff" }, "Item \" 2"),
                        ul => ul.Set(Enumerable.Range(1, t.TheNumber).Select(
                            i => (Action<Maml<TemplateData>>) 
                                (li => li.Set(string.Format("list item {0}", i))))
                        )
                    ),
                    div => div.Set((CData)"this is some cdata"),
                    script => script.Set(new 
                    { 
                        src = "http://www.something.com/test.js", type = "text/javascript" 
                    })
                )
            );

            Console.WriteLine(
                xt.ToString(
                    new TemplateData { TheNumber = 5, TheString = "Hello" }
                )
            );
            Console.WriteLine();
            Console.WriteLine(
                xt.ToString(
                    new TemplateData { TheNumber = 2, TheString = "BLAH BLAH" }
                )
            );
            Console.ReadLine();
        }
    }
}
```

Produces this output:
```
<html>
        <div attribute-name="value" stuff="cool" number="2">
                <p>Item 1 = Hello</p>
                <p data="some stuff">Item &quot; 2</p>
                <ul>
                        <li>list item 1</li>
                        <li>list item 2</li>
                        <li>list item 3</li>
                        <li>list item 4</li>
                        <li>list item 5</li>
                </ul>
        </div>
        <div><![CDATA[this is some cdata]]></div>
        <script src="http://www.test.com/test.js" type="text/javascript"></script>
</html>

<html><div attribute-name="value" stuff="cool" number="2"><p>Item 1 = BLAH BLAH</p><p data="some stuff">Item &quot; 2</p><ul><li>list item 1</li><li>list item 2</li></ul></div><div><![CDATA[this is some cdata]]></div><script src="http://www.test.com/test.js" type="text/javascript"></script></html>
```

## How it works
### Creating the document and root node
Use `new Maml<T>(Action<Maml<T>> node)` to generate a document with a root node.  The action delegate is used 
to build the contents of the root node.  Using anonymous delegates as node builders, the parameter name of the function is
is used as a node name.

Use `new Maml<T>(Func<T, Action<Maml<T>>> node)>` to generate a document with a root node that is derived from a template 
data object.  The Func will be called when a `ToString(T source)` is called.  The function will then generate
an `Action<Maml<T>>` delegate to generate content.

The generic type `T` determines the .Net type that can be used as a template data source.

### Generating node contents
The `Action<Maml<T>>` delegate lets you call methods on a `Maml<T>` builder instance:

* The `Set()` method can be used to generate child nodes, text content or a CDATA node with or without attributes.
* The `Self()` method can be used to generate a self closing node with out without attributes.

#### The `Set()` method
Used to generate node contents:  

* (Optional) The first parameter, which is optional, is the node's attributes. It can either be:
 * Any old CLR object - property names and values determine the node's attribute names and associated values
 * Any object implementing `IDictionary<string, string>` - key names and values determine the node's 
   attribute names and associated values.
* (Optional) One of:
 * One or more `Action<Maml<T>>` node builders representing the child nodes - the parameter name of each node
   builder delegate drives the child node name.  *NOTE*: This can be a variable number of arguments or a single
   `IEnumerable<Action<Maml<T>>>`
 * A string representing the text contents of the node.
 * A string casted to type of `Maml.CData` to represent a CData content node.

In addition, any parameter can be replaced by a `Func<T, TargetType>` delegate to have template data of type `T`
drive the generation of attributes, child nodes, text and CDATA elements.  The different `Func<,>` types are:

* `Func<T, object>` is a template data function to generate attribute object.
* `Func<T, IDictionary<string, string>>` is a template data function to generate attribute string dictionary.
* `Func<T, IEnumerable<Action<Maml<T>>>` is a template data function to generate child nodes.
* `Func<T, string>` is a template data function to generate node text.
* `Func<T, Maml.CData>` is a template data function to generate node CDATA.

#### The `Self()` method
Used to generate a self-closing node.  There is one optional parameter to provide attributes to the node, similar
to how `Set()` accepts attributes.

[*NOTE*: the `Set()` and `Self()` methods facilitate argument types by having a crap-ton of overloads covering all the
cases :)]

### Generating XML document string
Use the `ToString(T source, bool indent = false)` method to generate an XML string.  Provide an instance of type `T` as the argument
to drive delegate-driven content and attribute generation.  The `indent` argument determines if indentation and line break are added
(defaults to no indenting).  Indentation always uses tabs for indentions.

[*NOTE*: `Maml<T>.ToString()` will throw a NotImplementedException.  You _must_ provide a template data object
parameter.]

### Using the non-generic Maml class
The non-generic `Maml` class is used to generate XML documents without template data.  Internally, `Maml` is
a sub-class of `Maml<object>`, all of the methods are available.  However, providing delegate-driven content
and attribute functions will always have a `null` passed into them when XML generation is done.

Use `Maml.ToString()` and `Maml.ToString(bool indent)` to generate XML content (yes, this one will _not_ throw an exception).