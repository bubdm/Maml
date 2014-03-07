using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Naml
{    
    public class Naml<T>
    {
        protected string nodeName = null;
        private NodeContents<T> contents = new NodeContents<T>();

        protected Naml(string node)
        {
            nodeName = node;
        }
        public static Naml<T> Create(Action<Naml<T>> node)
        {
            string name = node.Method.GetParameters()[0].Name;
            Naml<T> newNode = new Naml<T>(name);
            node(newNode);
            return newNode;
        }

        public void Set(object attributes, params Action<Naml<T>>[] nodes)
        {
            contents.NodeAttributesObject = attributes;
            contents.Children = nodes;
        }
        public void Set(object attributes, Func<T, IEnumerable<Action<Naml<T>>>> nodes)
        {
            contents.NodeAttributesObject = attributes;
            contents.ChildrenFunc = nodes;
        }
        public void Set(Func<T, object> attributes, params Action<Naml<T>>[] nodes)
        {
            contents.nodeAttributesObjectFunc = attributes;
            contents.Children = nodes;
        }
        public void Set(Func<T, object> attributes, Func<T, IEnumerable<Action<Naml<T>>>> nodes)
        {
            contents.nodeAttributesObjectFunc = attributes;
            contents.ChildrenFunc = nodes;
        }
        public void Set(IDictionary<string, string> attributes, params Action<Naml<T>>[] nodes)
        {
            contents.NodeAttributes = attributes;
            contents.Children = nodes;
        }
        public void Set(IDictionary<string, string> attributes, Func<T, IEnumerable<Action<Naml<T>>>> nodes)
        {
            contents.NodeAttributes = attributes;
            contents.ChildrenFunc = nodes;
        }
        public void Set(Func<T, IDictionary<string, string>> attributes, params Action<Naml<T>>[] nodes)
        {
            contents.NodeAttributesFunc = attributes;
            contents.Children = nodes;
        }
        public void Set(Func<T, IDictionary<string, string>> attributes, Func<T, IEnumerable<Action<Naml<T>>>> nodes)
        {
            contents.NodeAttributesFunc = attributes;
            contents.ChildrenFunc = nodes;
        }
        public void Set(params Action<Naml<T>>[] nodes)
        {
            contents.NodeAttributesObject = null;
            contents.Children = nodes;
        }
        public void Set(Func<T, IEnumerable<Action<Naml<T>>>> nodes)
        {
            contents.NodeAttributesObject = null;
            contents.ChildrenFunc = nodes;
        }

        public void Set(object attributes, string text)
        {
            contents.NodeAttributesObject = attributes;
            contents.NodeText = text;
        }
        public void Set(Func<T, object> attributes, string text)
        {
            contents.NodeAttributesObjectFunc = attributes;
            contents.NodeText = text;
        }
        public void Set(object attributes, Func<T, string> text)
        {
            contents.NodeAttributesObject = attributes;
            contents.NodeTextFunc = text;
        }
        public void Set(Func<T, object> attributes, Func<T, string> text)
        {
            contents.NodeAttributesObjectFunc = attributes;
            contents.NodeTextFunc = text;
        }
        public void Set(IDictionary<string, string> attributes, string text)
        {
            contents.NodeAttributes = attributes;
            contents.NodeText = text;
        }
        public void Set(Func<T, IDictionary<string, string>> attributes, string text)
        {
            contents.NodeAttributesFunc = attributes;
            contents.NodeText = text;
        }
        public void Set(IDictionary<string, string> attributes, Func<T, string> text)
        {
            contents.NodeAttributes = attributes;
            contents.NodeTextFunc = text;
        }
        public void Set(Func<T, IDictionary<string, string>> attributes, Func<T, string> text)
        {
            contents.NodeAttributesFunc = attributes;
            contents.NodeTextFunc = text;
        }
        public void Set(string text)
        {
            contents.NodeAttributesObject = null;
            contents.NodeText = text;
        }
        public void Set(Func<T, string> text)
        {
            contents.NodeAttributesObject = null;
            contents.NodeTextFunc = text;
        }

        public void Set(object attributes, CData cdata)
        {
            contents.NodeAttributesObject = attributes;
            contents.NodeCdata = cdata;
        }
        public void Set(Func<T, object> attributes, CData cdata)
        {
            contents.NodeAttributesObjectFunc = attributes;
            contents.NodeCdata = cdata;
        }
        public void Set(object attributes, Func<T, CData> cdata)
        {
            contents.NodeAttributesObject = attributes;
            contents.NodeCdataFunc = cdata;
        }
        public void Set(Func<T, object> attributes, Func<T, CData> cdata)
        {
            contents.NodeAttributesObjectFunc = attributes;
            contents.NodeCdataFunc = cdata;
        }
        public void Set(IDictionary<string, string> attributes, CData cdata)
        {
            contents.NodeAttributes = attributes;
            contents.NodeCdata = cdata;
        }
        public void Set(Func<T, IDictionary<string, string>> attributes, CData cdata)
        {
            contents.NodeAttributesFunc = attributes;
            contents.NodeCdata = cdata;
        }
        public void Set(IDictionary<string, string> attributes, Func<T, CData> cdata)
        {
            contents.NodeAttributes = attributes;
            contents.NodeCdataFunc = cdata;
        }
        public void Set(Func<T, IDictionary<string, string>> attributes, Func<T, CData> cdata)
        {
            contents.NodeAttributesFunc = attributes;
            contents.NodeCdataFunc = cdata;
        }
        public void Set(CData cdata)
        {
            contents.NodeAttributesObject = null;
            contents.NodeCdata = cdata;
        }
        public void Set(Func<T, CData> cdata)
        {
            contents.NodeAttributesObject = null;
            contents.NodeCdataFunc = cdata;
        }

        public void Self(object attributes)
        {
            contents.NodeAttributesObject = attributes;
            contents.SelfClose = true;
        }
        public void Self(Func<T, object> attributes)
        {
            contents.NodeAttributesObjectFunc = attributes;
            contents.SelfClose = true;
        }
        public void Self(IDictionary<string, string> attributes)
        {
            contents.NodeAttributes = attributes;
            contents.SelfClose = true;
        }
        public void Self(Func<T, IDictionary<string, string>> attributes)
        {
            contents.NodeAttributesFunc = attributes;
            contents.SelfClose = true;
        }
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
        public override string ToString()
        {
            throw new InvalidOperationException("Must use ToString(source)");
        }
        public string ToString(T source)
        {
            StringBuilder xml = new StringBuilder();
            RenderNode(xml, source);
            return xml.ToString();
        }
    }

    public class Naml : Naml<object>
    {
        protected Naml(string node) : base(node)
        {
        }
        public static Naml<T> Create<T>(Action<Naml<T>> node)
        {
            return Naml<T>.Create(node);
        }
        public static Naml Create(Action<Naml> node)
        {
            string name = node.Method.GetParameters()[0].Name;
            Naml newNode = new Naml(name);
            node(newNode);
            return newNode;
        }
        public override string ToString()
        {
            return base.ToString(null);
        }
    }
}
