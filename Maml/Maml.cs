using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace Maml
{   
    /// <summary>
    /// Maml XML builder class.  Can also produce templated output.
    /// </summary>
    /// <typeparam name="T">Type of template Maml instance will use for templated output.</typeparam>
    public class Maml<T>
    {
        private Action<Maml<T>> builder = null;
        private Func<T, Action<Maml<T>>> builderFunc = null;
        private NodeContents<T> contents = new NodeContents<T>();

        /// <summary>
        /// Create instance of Maml{T} with the root node builder.
        /// </summary>
        /// <param name="node">Action{Maml{T}} node builder delegate.  Name of parameter in delegate
        /// determines the name of the node.</param>
        public Maml(Action<Maml<T>> node)
        {
            builder = node;
        }
        /// <summary>
        /// Create instance of Maml{T} with the root node builder.
        /// </summary>
        /// <param name="node">Func{T, Action{Maml{T}}} delegate for generating a node builder from template 
        /// data of type T.  The parameter for the Action{Maml{T}} delegate determines the node name
        public Maml(Func<T, Action<Maml<T>>> node)
        {
            builderFunc = node;
        }

        /// <summary>
        /// Create instance of Maml{T} with the root node builder.
        /// </summary>
        /// <param name="node">Action{Maml{T}} function to product content.  Name of function parameters 
        /// determines the name of the node.</param>
        /// <returns>Instance of Maml{T} object representing a root node.</returns>
        public static Maml<T> Create(Action<Maml<T>> node)
        {
            return new Maml<T>(node);
        }
        public static Maml<T> Create(Func<T, Action<Maml<T>>> node)
        {
            return new Maml<T>(node);
        }

        /// <summary>
        /// Set attributes and child nodes of current node.
        /// </summary>
        /// <param name="attributes">Any object where property names and values are used to determine attribute 
        /// names and values</param>
        /// <param name="nodes">One of more node builder functions representing the child nodes.</param>
        public void Set(object attributes, params Action<Maml<T>>[] nodes)
        {
            contents.NodeAttributesObject = attributes;
            contents.Children = nodes;
        }
        /// <summary>
        /// Set attributes and child nodes of current node.
        /// </summary>
        /// <param name="attributes">Any object where property names and values are used to determine attribute 
        /// names and values</param>
        /// <param name="nodes">One of more node builder functions representing the child nodes.</param>
        public void Set(object attributes, IEnumerable<Action<Maml<T>>> nodes)
        {
            contents.NodeAttributesObject = attributes;
            contents.Children = nodes;
        }        /// <summary>
        /// Set attributes and child nodes of current node.
        /// </summary>
        /// <param name="attributes">Any object where the property names and values are used to determine attribute 
        /// names and values</param>
        /// <param name="nodes">A function that takes a parameter of type T and returns an enumeration of
        /// node builder functions representing the child nodes.</param>
        public void Set(object attributes, Func<T, IEnumerable<Action<Maml<T>>>> nodes)
        {
            contents.NodeAttributesObject = attributes;
            contents.ChildrenFunc = nodes;
        }
        /// <summary>
        /// Set attributes and child nodes of current node.
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns any object wherer
        /// the property names and values are used to determine attribute names and values</param>
        /// <param name="nodes">One of more node builder functions representing the child nodes.</param>
        public void Set(Func<T, object> attributes, params Action<Maml<T>>[] nodes)
        {
            contents.nodeAttributesObjectFunc = attributes;
            contents.Children = nodes;
        }
        /// <summary>
        /// Set attributes and child nodes of current node.
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns any object wherer
        /// the property names and values are used to determine attribute names and values</param>
        /// <param name="nodes">One of more node builder functions representing the child nodes.</param>
        public void Set(Func<T, object> attributes, IEnumerable<Action<Maml<T>>> nodes)
        {
            contents.nodeAttributesObjectFunc = attributes;
            contents.Children = nodes;
        }
        /// <summary>
        /// Set attributes and child nodes of current node.
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns any object wherer
        /// the property names and values are used to determine attribute names and values</param>
        /// <param name="nodes">A function that takes a parameter of type T and returns an enumeration of
        /// node builder functions representing the child nodes.</param>
        public void Set(Func<T, object> attributes, Func<T, IEnumerable<Action<Maml<T>>>> nodes)
        {
            contents.nodeAttributesObjectFunc = attributes;
            contents.ChildrenFunc = nodes;
        }
        /// <summary>
        /// Set attributes and child nodes of current node.
        /// </summary>
        /// <param name="attributes">A string/string dictionary where keys and values are used to determine 
        /// attribute names and values</param>
        /// <param name="nodes">One of more node builder functions representing the child nodes.</param>
        public void Set(IDictionary<string, string> attributes, params Action<Maml<T>>[] nodes)
        {
            contents.NodeAttributes = attributes;
            contents.Children = nodes;
        }
        /// <summary>
        /// Set attributes and child nodes of current node.
        /// </summary>
        /// <param name="attributes">A string/string dictionary where keys and values are used to determine 
        /// attribute names and values</param>
        /// <param name="nodes">One of more node builder functions representing the child nodes.</param>
        public void Set(IDictionary<string, string> attributes, IEnumerable<Action<Maml<T>>> nodes)
        {
            contents.NodeAttributes = attributes;
            contents.Children = nodes;
        }
        /// <summary>
        /// Set attributes and child nodes of current node.
        /// </summary>
        /// <param name="attributes">A string/string dictionary where keys and values are used to determine 
        /// attribute names and values</param>
        /// <param name="nodes">A function that takes a parameter of type T and returns an enumeration of
        /// node builder functions representing the child nodes.</param>
        public void Set(IDictionary<string, string> attributes, Func<T, IEnumerable<Action<Maml<T>>>> nodes)
        {
            contents.NodeAttributes = attributes;
            contents.ChildrenFunc = nodes;
        }
        /// <summary>
        /// Set attributes and child nodes of current node.
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns a string/string 
        /// dictionary where keys and values are used to determine attribute names and values</param>
        /// <param name="nodes">One of more node builder functions representing the child nodes.</param>        
        public void Set(Func<T, IDictionary<string, string>> attributes, params Action<Maml<T>>[] nodes)
        {
            contents.NodeAttributesFunc = attributes;
            contents.Children = nodes;
        }
        /// <summary>
        /// Set attributes and child nodes of current node.
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns a string/string 
        /// dictionary where keys and values are used to determine attribute names and values</param>
        /// <param name="nodes">One of more node builder functions representing the child nodes.</param>        
        public void Set(Func<T, IDictionary<string, string>> attributes, IEnumerable<Action<Maml<T>>> nodes)
        {
            contents.NodeAttributesFunc = attributes;
            contents.Children = nodes;
        }
        /// <summary>
        /// Set attributes and child nodes of current node.
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns a string/string 
        /// dictionary where keys and values are used to determine attribute names and values</param>
        /// <param name="nodes">A function that takes a parameter of type T and returns an enumeration of
        /// node builder functions representing the child nodes.</param>      
        public void Set(Func<T, IDictionary<string, string>> attributes, Func<T, IEnumerable<Action<Maml<T>>>> nodes)
        {
            contents.NodeAttributesFunc = attributes;
            contents.ChildrenFunc = nodes;
        }
        /// <summary>
        /// Set child nodes of current node with no attributes.
        /// </summary>
        /// <param name="nodes">One of more node builder functions representing the child nodes.</param>        
        public void Set(params Action<Maml<T>>[] nodes)
        {
            contents.NodeAttributesObject = null;
            contents.Children = nodes;
        }        /// <summary>
        /// Set child nodes of current node with no attributes.
        /// </summary>
        /// <param name="nodes">One of more node builder functions representing the child nodes.</param>        
        public void Set(IEnumerable<Action<Maml<T>>> nodes)
        {
            contents.NodeAttributesObject = null;
            contents.Children = nodes;
        }
        /// <summary>
        /// Set child nodes of current node with no attributes.
        /// </summary>
        /// <param name="nodes">A function that takes a parameter of type T and returns an enumeration of
        /// node builder functions representing the child nodes.</param>              
        public void Set(Func<T, IEnumerable<Action<Maml<T>>>> nodes)
        {
            contents.NodeAttributesObject = null;
            contents.ChildrenFunc = nodes;
        }

        /// <summary>
        /// Set attributes and text of current node.
        /// </summary>
        /// <param name="attributes">Any object where property names and values are used to determine attribute 
        /// names and values</param>
        /// <param name="text">Text contained in the node.</param>
        public void Set(object attributes, string text)
        {
            contents.NodeAttributesObject = attributes;
            contents.NodeText = text;
        }
        /// <summary>
        /// Set attributes and text of current node.
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns any object wherer
        /// the property names and values are used to determine attribute names and values</param>
        /// <param name="text">Text contained in the node.</param>
        public void Set(Func<T, object> attributes, string text)
        {
            contents.NodeAttributesObjectFunc = attributes;
            contents.NodeText = text;
        }
        /// <summary>
        /// Set attributes and text of current node.
        /// </summary>
        /// <param name="attributes">Any object where property names and values are used to determine attribute 
        /// names and values</param>
        /// <param name="text">A function that takes a parameter of type T a returns the text contained 
        /// in the node.</param>
        public void Set(object attributes, Func<T, string> text)
        {
            contents.NodeAttributesObject = attributes;
            contents.NodeTextFunc = text;
        }
        /// <summary>
        /// Set attributes and text of current node.
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns any object wherer
        /// the property names and values are used to determine attribute names and values</param>
        /// <param name="text">A function that takes a parameter of type T a returns the text contained 
        /// in the node.</param>
        public void Set(Func<T, object> attributes, Func<T, string> text)
        {
            contents.NodeAttributesObjectFunc = attributes;
            contents.NodeTextFunc = text;
        }
        /// <summary>
        /// Set attributes and text of current node.
        /// </summary>
        /// <param name="attributes">A string/string dictionary where keys and values are used to determine 
        /// attribute names and values</param>
        /// <param name="text">Text contained in the node.</param>
        public void Set(IDictionary<string, string> attributes, string text)
        {
            contents.NodeAttributes = attributes;
            contents.NodeText = text;
        }
        /// <summary>
        /// Set attributes and text of current node.
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns a string/string 
        /// dictionary where keys and values are used to determine attribute names and values</param>
        /// <param name="text">Text contained in the node.</param>
        public void Set(Func<T, IDictionary<string, string>> attributes, string text)
        {
            contents.NodeAttributesFunc = attributes;
            contents.NodeText = text;
        }
        /// <summary>
        /// Set attributes and text of current node.
        /// </summary>
        /// <param name="attributes">A string/string dictionary where keys and values are used to determine 
        /// attribute names and values</param>
        /// <param name="text">A function that takes a parameter of type T a returns the text contained 
        /// in the node.</param>
        public void Set(IDictionary<string, string> attributes, Func<T, string> text)
        {
            contents.NodeAttributes = attributes;
            contents.NodeTextFunc = text;
        }
        /// <summary>
        /// Set attributes and text of current node.
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns a string/string 
        /// dictionary where keys and values are used to determine attribute names and values</param>
        /// <param name="text">A function that takes a parameter of type T a returns the text contained 
        /// in the node.</param>
        public void Set(Func<T, IDictionary<string, string>> attributes, Func<T, string> text)
        {
            contents.NodeAttributesFunc = attributes;
            contents.NodeTextFunc = text;
        }
        /// <summary>
        /// Set text of current node with no attributes.
        /// </summary>
        /// <param name="text">Text contained in the node.</param
        public void Set(string text)
        {
            contents.NodeAttributesObject = null;
            contents.NodeText = text;
        }
        /// <summary>
        /// Set text of current node with no attributes.
        /// </summary>
        /// <param name="text">A function that takes a parameter of type T a returns the text contained 
        /// in the node.</param>
        public void Set(Func<T, string> text)
        {
            contents.NodeAttributesObject = null;
            contents.NodeTextFunc = text;
        }

        /// <summary>
        /// Set attributes and CDATA of current node.
        /// </summary>
        /// <param name="attributes">Any object where property names and values are used to determine attribute 
        /// names and values</param>
        /// <param name="cdata">CData text contained in the node.</param>
        public void Set(object attributes, CData cdata)
        {
            contents.NodeAttributesObject = attributes;
            contents.NodeCdata = cdata;
        }
        /// <summary>
        /// Set attributes and CDATA of current node.
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns any object wherer
        /// the property names and values are used to determine attribute names and values</param>
        /// <param name="cdata">CData text contained in the node.</param>
        public void Set(Func<T, object> attributes, CData cdata)
        {
            contents.NodeAttributesObjectFunc = attributes;
            contents.NodeCdata = cdata;
        }
        /// <summary>
        /// Set attributes and CDATA of current node.
        /// </summary>
        /// <param name="attributes">Any object where property names and values are used to determine attribute 
        /// names and values</param>
        /// <param name="cdata">A function that takes a parameter of type T a returns the CDATA text contained 
        /// in the node.</param>
        public void Set(object attributes, Func<T, CData> cdata)
        {
            contents.NodeAttributesObject = attributes;
            contents.NodeCdataFunc = cdata;
        }
        /// <summary>
        /// Set attributes and CDATA of current node.
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns any object wherer
        /// the property names and values are used to determine attribute names and values</param>
        /// <param name="cdata">A function that takes a parameter of type T a returns the CDATA text contained 
        /// in the node.</param>
        public void Set(Func<T, object> attributes, Func<T, CData> cdata)
        {
            contents.NodeAttributesObjectFunc = attributes;
            contents.NodeCdataFunc = cdata;
        }
        /// <summary>
        /// Set attributes and CDATA of current node.
        /// </summary>
        /// <param name="attributes">A string/string dictionary where keys and values are used to determine 
        /// attribute names and values</param>
        /// <param name="cdata">CDATA text contained in the node.</param>
        public void Set(IDictionary<string, string> attributes, CData cdata)
        {
            contents.NodeAttributes = attributes;
            contents.NodeCdata = cdata;
        }
        /// <summary>
        /// Set attributes and CDATA of current node.
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns a string/string 
        /// dictionary where keys and values are used to determine attribute names and values</param>
        /// <param name="cdata">CDATA text contained in the node.</param>
        public void Set(Func<T, IDictionary<string, string>> attributes, CData cdata)
        {
            contents.NodeAttributesFunc = attributes;
            contents.NodeCdata = cdata;
        }
        /// <summary>
        /// Set attributes and CDATA of current node.
        /// </summary>
        /// <param name="attributes">A string/string dictionary where keys and values are used to determine 
        /// attribute names and values</param>
        /// <param name="cdata">A function that takes a parameter of type T a returns the CDATA text contained 
        /// in the node.</param>
        public void Set(IDictionary<string, string> attributes, Func<T, CData> cdata)
        {
            contents.NodeAttributes = attributes;
            contents.NodeCdataFunc = cdata;
        }
        /// <summary>
        /// Set attributes and CDATA of current node.
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns a string/string 
        /// dictionary where keys and values are used to determine attribute names and values</param>
        /// <param name="cdata">A function that takes a parameter of type T a returns the CDATA text contained 
        /// in the node.</param>
        public void Set(Func<T, IDictionary<string, string>> attributes, Func<T, CData> cdata)
        {
            contents.NodeAttributesFunc = attributes;
            contents.NodeCdataFunc = cdata;
        }
        /// <summary>
        /// Set CDATA of current node with no attributes.
        /// </summary>
        /// <param name="cdata">CDATA text contained in the node.</param>
        public void Set(CData cdata)
        {
            contents.NodeAttributesObject = null;
            contents.NodeCdata = cdata;
        }
        /// <summary>
        /// Set CDATA of current node with no attributes.
        /// </summary>
        /// <param name="cdata">A function that takes a parameter of type T a returns the CDATA text contained 
        /// in the node.</param>
        public void Set(Func<T, CData> cdata)
        {
            contents.NodeAttributesObject = null;
            contents.NodeCdataFunc = cdata;
        }

        /// <summary>
        /// Set node as self-closing (no content) with attributes
        /// </summary>
        /// <param name="attributes">Any object where property names and values are used to determine attribute 
        /// names and values</param>
        public void Self(object attributes)
        {
            contents.NodeAttributesObject = attributes;
            contents.SelfClose = true;
        }
        /// <summary>
        /// Set node as self-closing (no content) with attributes
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns any object wherer
        /// the property names and values are used to determine attribute names and values</param>
        public void Self(Func<T, object> attributes)
        {
            contents.NodeAttributesObjectFunc = attributes;
            contents.SelfClose = true;
        }
        /// <summary>
        /// Set node as self-closing (no content) with attributes
        /// </summary>
        /// <param name="attributes">A string/string dictionary where keys and values are used to determine 
        /// attribute names and values</param>
        public void Self(IDictionary<string, string> attributes)
        {
            contents.NodeAttributes = attributes;
            contents.SelfClose = true;
        }
        /// <summary>
        /// Set node as self-closing (no content) with attributes
        /// </summary>
        /// <param name="attributes">A function that takes a parameter of type T and returns a string/string 
        /// dictionary where keys and values are used to determine attribute names and values</param>
        public void Self(Func<T, IDictionary<string, string>> attributes)
        {
            contents.NodeAttributesFunc = attributes;
            contents.SelfClose = true;
        }
        /// <summary>
        /// Set node as sel-closing with no content and no attributes
        /// </summary>
        public void Self()
        {
            contents.NodeAttributesObject = null;
            contents.SelfClose = true;
        }

        protected void RenderNode(StringBuilder sb, T obj)
        {
            Action<Maml<T>> actualBuilder = builder ?? builderFunc(obj);
            string nodeName = XmlConvert.EncodeName(actualBuilder.Method.GetParameters()[0].Name);
            actualBuilder(this);

            sb.AppendFormat("<{0}", nodeName);
            var atts = contents.GetAttributes(obj);
            if (atts != null)
            {
                foreach (var att in atts)
                {
                    sb.AppendFormat(@" {0}=""{1}""",
                        XmlConvert.EncodeName(att.Key.Replace('_', '-')), 
                        System.Security.SecurityElement.Escape(att.Value));
                }
            }
            if (contents.SelfClose)
            {
                sb.Append("/>");
            }
            else
            {
                sb.Append(">");
                var text = contents.GetStringValue(obj);
                if (text != null)
                {
                    sb.Append(text);
                }
                else
                {
                    var children = contents.GetChildren(obj);
                    foreach (var expr in children)
                    {
                        Maml<T>.Create(expr).RenderNode(sb, obj);
                    }
                }
                sb.AppendFormat("</{0}>", nodeName);
            }
        }
        /// <summary>
        /// Returns a string that represents the current object.  In Maml{T}, this will ALWAYS throw a
        /// InvalidOperationException.  You must use ToString(T source) to generate XML strings with 
        /// instances of this object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            throw new InvalidOperationException("Must use ToString(source)");
        }
        /// <summary>
        /// Render Maml template to an XML string using a provided template data object of type T.
        /// </summary>
        /// <param name="source">Instance of object of type T to drive function-based attributes,
        /// Text, CDATA and child nodes.</param>
        /// <returns>XML string</returns>
        public string ToString(T source)
        {
            StringBuilder xml = new StringBuilder();
            RenderNode(xml, source);
            return xml.ToString();
        }
    }

    /// <summary>
    /// Non-generic version of a Maml template, design for non-function-based XML generation.
    /// </summary>
    /// <remarks>
    /// This basically is a sub-class of Maml{object}.  All of the function-based Set and Self methods
    /// are avaiable, but will always contain a null object when those functions are run.
    /// </remarks>
    public class Maml : Maml<object>
    {
        /// <summary>
        /// Create instance of Maml with the root node builder.
        /// </summary>
        /// <param name="node">Action{Maml} node builder delegate.  Name of parameter in delegate
        /// determines the name of the node.</param>
        public Maml(Action<Maml> node) : base((Action<Maml<object>>) node)
        {
        }

        /// <summary>
        /// Create a non-generic instance of Maml.
        /// </summary>
        /// <param name="node">Action{Maml} function to product content.  Name of function parameters 
        /// determines the name of the node.</param>
        /// <returns>Instance of Maml object representing a root node.</returns>
        public static Maml Create(Action<Maml> node)
        {
            return new Maml(node);
        }
        /// <summary>
        /// Render Maml template to an XML string.
        /// </summary>
        /// <remarks>
        /// No template data object is required, the template data object provided to all function-driven attribute,
        /// text, CDATA, and node builders will be null.
        /// </remarks>
        /// <returns>XML string</returns>
        public override string ToString()
        {
            return base.ToString(null);
        }
    }
}
