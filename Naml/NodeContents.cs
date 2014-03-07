using System;
using System.Collections.Generic;
using XB = Naml;

namespace Naml
{
    internal enum NodeType
    {
        Nodes = 0,
        Text = 1,
        CData = 2,
        NodesFunc = 3,
        TextFunc = 4,
        CDataFunc = 5,
        SelfClosed = 6,
    }
    internal enum AttributesType
    {
        Dictionary = 0,
        Object = 1,
        DictionaryFunc = 2,
        ObjectFunc = 3,
    }
    internal class NodeContents<T>
    {
        public NodeType NodeType { get; private set; }
        private string nodeText = null;
        private CData nodeCdata = null;
        private IEnumerable<Action<Naml<T>>> children;
        private Func<T, string> nodeTextFunc = null;
        private Func<T, CData> nodeCdataFunc = null;
        private Func<T, IEnumerable<Action<Naml<T>>>> childrenFunc;
        private bool selfClose = false;

        public AttributesType AttributesType { get; private set; }
        public IDictionary<string, string> nodeAttributes;
        public object nodeAttributesObject;
        public Func<T, IDictionary<string, string>> nodeAttributesFunc;
        public Func<T, object> nodeAttributesObjectFunc;

        public IDictionary<string, string> NodeAttributes
        {
            get { return nodeAttributes; }
            set
            {
                AttributesType = XB.AttributesType.Dictionary;
                nodeAttributes = value;
            }
        }
        public object NodeAttributesObject
        {
            get { return nodeAttributesObject; }
            set
            {
                AttributesType = XB.AttributesType.Object;
                nodeAttributesObject = value;
            }
        }
        public Func<T, IDictionary<string, string>> NodeAttributesFunc
        {
            get { return nodeAttributesFunc; }
            set
            {
                AttributesType = XB.AttributesType.DictionaryFunc;
                nodeAttributesFunc = value;
            }
        }
        public Func<T, object> NodeAttributesObjectFunc
        {
            get { return nodeAttributesObjectFunc; }
            set
            {
                AttributesType = XB.AttributesType.ObjectFunc;
                nodeAttributesObjectFunc = value;
            }
        }
        public IEnumerable<Action<Naml<T>>> Children
        {
            get { return children; }
            set
            {
                NodeType = XB.NodeType.Nodes;
                children = value;
                SelfClose = false;
            }
        }
        public Func<T, IEnumerable<Action<Naml<T>>>> ChildrenFunc
        {
            get { return childrenFunc; }
            set
            {
                NodeType = XB.NodeType.NodesFunc;
                childrenFunc = value;
                SelfClose = false;
            }
        }
        public string NodeText
        {
            get { return nodeText; }
            set
            {
                NodeType = XB.NodeType.Text;
                nodeText = value;
                SelfClose = false;
            }
        }
        public Func<T, string> NodeTextFunc
        {
            get { return nodeTextFunc; }
            set
            {
                NodeType = XB.NodeType.TextFunc;
                nodeTextFunc = value;
                SelfClose = false;
            }
        }
        public CData NodeCdata
        {
            get { return nodeCdata; }
            set
            {
                NodeType = XB.NodeType.CData;
                nodeCdata = value;
                SelfClose = false;
            }
        }
        public Func<T, CData> NodeCdataFunc
        {
            get { return nodeCdataFunc; }
            set
            {
                NodeType = XB.NodeType.CDataFunc;
                nodeCdataFunc = value;
                SelfClose = false;
            }
        }
        public bool SelfClose
        {
            get { return selfClose; }
            set
            {
                selfClose = value;
                if (selfClose)
                {
                    NodeType = XB.NodeType.SelfClosed;
                }
            }
        }

        public IDictionary<string, string> GetAttributes(T obj)
        {
            switch (AttributesType)
            {
                case XB.AttributesType.Dictionary:
                    return NodeAttributes;
                case XB.AttributesType.DictionaryFunc:
                    return NodeAttributesFunc(obj);
                case XB.AttributesType.Object:
                    return NodeAttributesObject.Safe(x => x.ToStringDictionary());
                case XB.AttributesType.ObjectFunc:
                    return nodeAttributesObjectFunc(obj).Safe(x => x.ToStringDictionary());
                default:
                    return null;
            }
        }
        public string GetStringValue(T obj)
        {
            switch (NodeType)
            {
                case XB.NodeType.Text:
                    return System.Security.SecurityElement.Escape(NodeText);
                case XB.NodeType.TextFunc:
                    return System.Security.SecurityElement.Escape(NodeTextFunc(obj));
                case XB.NodeType.CData:
                    return string.Format("<![CDATA[{0}]]>", ((string) NodeCdata).Safe(x => x.Replace("]]>", "___")));
                case XB.NodeType.CDataFunc:
                    return string.Format("<![CDATA[{0}]]>", ((string) NodeCdataFunc(obj)).Safe(x => x.Replace("]]>", "___")));
                default:
                    return null;
            }
        }
        public IEnumerable<Action<Naml<T>>> GetChildren(T obj)
        {
            switch (NodeType)
            {
                case XB.NodeType.Nodes:
                    return Children;
                case XB.NodeType.NodesFunc:
                    return ChildrenFunc(obj);
                default:
                    return null;
            }
        }
    }
}
