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
            var xt = Naml.Create<TemplateData>(t => 
                html => html.Set(
                    div => div.Set(
                        new { attribute_name = "value", stuff = "cool", number = 2 },
                        p => p.Set(string.Format("Item 1 = {0}", t.TheString)),
                        p => p.Set(new { data = "some stuff" }, "Item \" 2"),
                        ul => ul.Set(Enumerable.Range(1, t.TheNumber).Select(
                            i => (Action<Naml<TemplateData>>) 
                                (li => li.Set(string.Format("list item {0}", i))))
                        )
                    ),
                    div => div.Set((CData)"this is some cdata"),
                    script => script.Set(new { src = "http://www.something.com/test.js", type = "text/javascript" })
                )
            );

            Console.WriteLine(xt.ToString(new TemplateData { TheNumber = 5, TheString = "Hello" }));
            Console.WriteLine();
            Console.WriteLine(xt.ToString(new TemplateData { TheNumber = 2, TheString = "BLAH BLAH" }));
            Console.ReadLine();
        }
    }
}
