using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Xml
{
    public static class XmlDriver
    {
        public static XmlDocument Open(string filename)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                return doc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool Save(XmlDocument document, string filename)
        {
            try
            {
                document.Save(filename);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static XmlNodeList2 Search(XmlNodeList nodes, string childname)
        {
            XmlNodeList2 nodes2 = new XmlNodeList2();

            if (nodes == null)
                return nodes2;

            foreach (XmlNode child in nodes)
            {
                if (child.Name.Equals(childname))
                    nodes2.Add(child);
            }
            return nodes2;
        }

        public static XmlNodeList2 Search(XmlNodeList2 nodes, string childname)
        {
            XmlNodeList2 nodes2 = new XmlNodeList2();
            if (nodes == null)
                return nodes2;

            foreach (XmlNode node in nodes)
            {
                XmlNodeList childs = node.ChildNodes;
                foreach (XmlNode child in childs)
                {
                    if (child.Name.Equals(childname))
                        nodes2.Add(child);
                }
            }
            return nodes2;
        }

        public static XmlNodeList2 Search(XmlDocument document, string childname)
        {
            return Search(document.ChildNodes, childname);
        }

        public static XmlNodeList2 Search(XmlNode parent, string childname)
        {
            return Search(parent.ChildNodes, childname);
        }

        public static XmlNodeList2 OpenAndSearch(string filename, string childname, out XmlDocument document)
        {
            XmlNodeList2 nodes2 = new XmlNodeList2();
            document = Open(filename);
            if (document == null)
                return nodes2;
            return Search(document, childname);
        }

        public static XmlNode AppendChild(XmlDocument document, XmlNode parentNode, string name)
        {
            try
            {
                XmlElement ele = document.CreateElement(name, document.DocumentElement.NamespaceURI) as XmlElement;
                parentNode.AppendChild(ele);
                return ele;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool RemoveNode(XmlNode node)
        {
            try
            {
                node.ParentNode.RemoveChild(node);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool RemoveChilds(XmlNode node)
        {
            try
            {
                node.RemoveAll();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    public class XmlNodeList2 : List<XmlNode>
    {
        public XmlNodeList2 Search(string name)
        {
            return XmlDriver.Search(this, name);
        }
    }
}
