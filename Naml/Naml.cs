using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Naml
{   
    /// <summary>
    /// Naml XML builder class.  Can also produce templated output.
    /// </summary>
    /// <typeparam name="T">Type of template Naml instance will use for templated output.</typeparam>
    public class Naml<T>
    {
        protected string nodeName = null;
        private NodeContents<T> contents = new NodeContents<T>();

        protected Naml(string node)
        {
            nodeName = node;
        }

        /// <summary>
        /// Create instance of Naml{T} with the root node builder.
        /// </summary>
        /// <param name="node">Action{Naml{T}} function to product content.  Name of function parameters 
        /// determines the name of the node.</param>
        /// <returns>Instance of Naml{T} object representing a root node.</returns>
        public static Naml<T> Create(Action<Naml<T>> node)
        {
            string name = node.Method.GetParameters()[0].Name;
            Naml<T> newNode = new Naml<T>(name);
            node(newNode);
            return newNode;
        }

        /// <summary>
        /// Set attributes and child nodes of current node.
        /// </summary>
        /// <param name="attributes">Any object where property names and values are used to determine attribute 
        /// names and values</param>
        /// <param name="nodes">One of more node builder functions representing the child nodes.</param>
        public void Set(object attributes, params Action<Naml<T>>[] nodes)
        {
            contents.NodeAttributesObject = attributes;
            contents.Children = nodes;
        }
        /// <summary>
        /// Set attributes and child nodes of current node.
        /// </summary>
        /// <param name="attributes">Any object where the property names and values are used to determine attribute 
        /// names and values</param>
        /// <param name="nodes">A function that takes a parameter of type T and returns an enumeration of
        /// node builder functions representing the child nodes.</param>
        public void Set(object attributes, Func<T, IEnumerable<Action<Naml<T>>>> nodes)
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
        public void Set(Func<T, object> attributes, params Action<Naml<T>>[] nodes)
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
        public void Set(Func<T, object> attributes, Func<T, IEnumerable<Action<Naml<T>>>> nodes)
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
        public void Set(IDictionary<string, string> attributes, params Action<Naml<T>>[] nodes)
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
        public void Set(IDictionary<string, string> attributes, Func<T, IEnumerable<Action<Naml<T>>>> nodes)
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
        public void Set(Func<T, IDictionary<string, string>> attributes, params Action<Naml<T>>[] nodes)
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
        public void Set(Func<T, IDictionary<string, string>> attributes, Func<T, IEnumerable<Action<Naml<T>>>> nodes)
        {
            contents.NodeAttributesFunc = attributes;
            contents.ChildrenFunc = nodes;
        }
        /// <summary>
        /// Set child nodes of current node with no attributes.
        /// </summary>
        /// <param name="nodes">One of more node builder functions representing the child nodes.</param>        
        public void Set(params Action<Naml<T>>[] nodes)
        {
            contents.NodeAttributesObject = null;
            contents.Children = nodes;
        }
        /// <summary>
        /// Set child nodes of current node with no attributes.
        /// </summary>
        /// <param name="nodes">A function that takes a parameter of type T and returns an enumeration of
        /// node builder functions representing the child nodes.</param>              
        public void Set(Func<T, IEnumerable<Action<Naml<T>>>> nodes)
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

        protected void RenderNode(StringBuilder builder, T obj)
        {
            builder.AppendFormat("<{0}", XmlConvert.EncodeName(nodeName));
            var atts = contents.GetAttributes(obj);
            if (atts != null)
            {
                foreach (var att in atts)
                {
                    builder.AppendFormat(" {0}=\"{1}\"",
                        XmlConvert.EncodeName(att.Key.Replace('_', '-')), 
                        System.Security.SecurityElement.Escape(att.Value));
                }
            }
            if (contents.SelfClose)
            {
                builder.Append("/>");
            }
            else
            {
                builder.Append(">");
                var text = contents.GetStringValue(obj);
                var children = contents.GetChildren(obj);
                if (text != null)
                {
                    builder.Append(text);
                }
                else
                {
                    foreach (var expr in children)
                    {
                        Naml<T>.Create(expr).RenderNode(builder, obj);
                    }
                }
                builder.AppendFormat("</{0}>", XmlConvert.EncodeName(nodeName));
            }
        }
        /// <summary>
        /// Returns a string that represents the current object.  In Naml{T}, this will ALWAYS throw a
        /// InvalidOperationException.  You must use ToString(T source) to generate XML strings with 
        /// instances of this object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            throw new InvalidOperationException("Must use ToString(source)");
        }
        /// <summary>
        /// Render Naml template to an XML string using a provided template data object of type T.
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
    /// Non-generic version of a Naml template, design for non-function-based XML generation.
    /// </summary>
    /// <remarks>
    /// This basically is a sub-class of Naml{object}.  All of the function-based Set and Self methods
    /// are avaiable, but will always contain a null object when those functions are run.
    /// </remarks>
    public class Naml : Naml<object>
    {
        protected Naml(string node) : base(node)
        {
        }
        /// <summary>
        /// Another way to generate a Naml{T} instance by using a generic method type parameter.
        /// </summary>
        /// <typeparam name="T">Type of template Naml instance will use for templated output.</typeparam>
        /// <param name="node">Action{Naml{T}} function to product content.  Name of function parameters 
        /// determines the name of the node.</param>
        /// <returns>Instance of Naml{T} object representing a root node.</returns>
        public static Naml<T> Create<T>(Action<Naml<T>> node)
        {
            return Naml<T>.Create(node);
        }
        /// <summary>
        /// Create a non-generic instance of Naml.
        /// </summary>
        /// <param name="node">Action{Naml} function to product content.  Name of function parameters 
        /// determines the name of the node.</param>
        /// <returns>Instance of Naml object representing a root node.</returns>
        public static Naml Create(Action<Naml> node)
        {
            string name = node.Method.GetParameters()[0].Name;
            Naml newNode = new Naml(name);
            node(newNode);
            return newNode;
        }
        /// <summary>
        /// Render Naml template to an XML string.
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
