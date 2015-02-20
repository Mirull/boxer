using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace BoxerXmlComparator
{
    class Comparator
    {
        private double percentError;
        private XmlDocument ResultDocument;

        public XmlDocument Compare(XmlDocument OriginXml, XmlDocument CorrectedXml)
        {
            ResultDocument = new XmlDocument();
            CompareTaggedTokens(OriginXml, CorrectedXml, ResultDocument);
            //for (int i = 0; i < OriginXml.ChildNodes.Count; i++)
            //{
            //    CompareSection(OriginXml.ChildNodes[i], CorrectedXml.ChildNodes[i]);
            //}

            return this.ResultDocument;
        
        }
        
        private void CompareTaggedTokens(XmlDocument OriginXml, XmlDocument CorrectedXml, XmlDocument ResultDocument)
        {
            var OriginTaggedTokensNode = OriginXml.SelectSingleNode("//taggedtokens");
            var CorrectedTaggedTokensNode = CorrectedXml.SelectSingleNode("//taggedtokens");
            XmlNode rootNode = ResultDocument.CreateElement("changes");
            ResultDocument.AppendChild(rootNode);
            int i = 0, j = 0, err_nr = 0;
            foreach(XmlNode tagTokenNode in OriginTaggedTokensNode)
            {
                    foreach(XmlNode node in tagTokenNode.FirstChild)
                    {
                       
                        var correctedNode = CorrectedTaggedTokensNode.ChildNodes[i].FirstChild.ChildNodes[j];
                        if (node.OuterXml != correctedNode.OuterXml)
                        {
                            err_nr = err_nr + 1;
                            XmlNode changenode = ResultDocument.CreateElement("change");
                            XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                            changenumber.Value = err_nr.ToString();
                            changenode.Attributes.Append(changenumber);
                            rootNode.AppendChild(changenode);


                            XmlNode originalnode = ResultDocument.CreateElement("original");
                            XmlAttribute attribute = ResultDocument.CreateAttribute("line");
                            originalnode.InnerText = node.ParentNode.ParentNode.Name + " " + node.ParentNode.ParentNode.Attributes[0].Name + " "  + node.ParentNode.ParentNode.Attributes[0].Value + " " + node.OuterXml;
                            changenode.AppendChild(originalnode);

                            XmlNode correctednode = ResultDocument.CreateElement("corrected");
                            correctednode.InnerText = correctedNode.ParentNode.ParentNode.Name + " " + correctedNode.ParentNode.ParentNode.Attributes[0].Name + " " + correctedNode.ParentNode.ParentNode.Attributes[0].Value + " " + correctedNode.OuterXml;
                            changenode.AppendChild(correctednode);


                            XmlNode pathnode = ResultDocument.CreateElement("path");
                            pathnode.InnerText = GetXPathToNode(correctedNode) + " " + pathnode.OuterXml;
                            changenode.AppendChild(pathnode);

                            /*
                            XmlNode originalnode = ResultDocument.CreateElement("original");
                            XmlAttribute attribute = ResultDocument.CreateAttribute("line");
                            originalnode.InnerText = node.ParentNode.ParentNode.Name + " " + node.ParentNode.ParentNode.Attributes[0].Name + " "  + node.ParentNode.ParentNode.Attributes[0].Value + " " + node.OuterXml;
                            rootNode.AppendChild(originalnode);

                            XmlNode correctednode = ResultDocument.CreateElement("corrected");
                            correctednode.InnerText = correctedNode.ParentNode.ParentNode.Name + " " + correctedNode.ParentNode.ParentNode.Attributes[0].Name + " " + correctedNode.ParentNode.ParentNode.Attributes[0].Value + " " + correctedNode.OuterXml;
                            rootNode.AppendChild(correctednode);


                            XmlNode pathnode = ResultDocument.CreateElement("path");
                            pathnode.InnerText = GetXPathToNode(correctedNode) + " " + pathnode.OuterXml;
                            rootNode.AppendChild(pathnode);

                            */
                            MessageBox.Show("RIGHT: <" + node.ParentNode.ParentNode.Name + " " + node.ParentNode.ParentNode.Attributes[0].Name + "=" + node.ParentNode.ParentNode.Attributes[0].Value + ">" + node.OuterXml + "\nLEFT: <" + correctedNode.ParentNode.ParentNode.Name + " " + correctedNode.ParentNode.ParentNode.Attributes[0].Name + "=" + correctedNode.ParentNode.ParentNode.Attributes[0].Value + ">" + correctedNode.OuterXml);
                        }
                        j++;
                    }
                i++;
                j = 0;
            }
            ResultDocument.Save("test-doc.xml");
        }

        private void CompareSection(XmlNode sectionOriginal, XmlNode sectionCorrected)
        {
            //if(sectionOriginal != sectionCorrected)
            //{
            //    MessageBox.Show(sectionOriginal.ToString() + " | " + sectionCorrected.ToString());
            //}
        }

        static int GetNodePosition(XmlNode child)
        {
            for (int i = 0; i < child.ParentNode.ChildNodes.Count; i++)
            {
                if (child.ParentNode.ChildNodes[i] == child)
                {
                    // tricksy XPath, not starting its positions at 0 like a normal language
                    return i + 1;
                }
            }
            throw new InvalidOperationException("Child node somehow not found in its parent's ChildNodes property.");
        }

        static string GetXPathToNode(XmlNode node)
        {
            if (node.NodeType == XmlNodeType.Attribute)
            {
                // attributes have an OwnerElement, not a ParentNode; also they have
                // to be matched by name, not found by position
                return String.Format(
                    "{0}/@{1}",
                    GetXPathToNode(((XmlAttribute)node).OwnerElement),
                    node.Name
                    );
            }
            if (node.ParentNode == null)
            {
                // the only node with no parent is the root node, which has no path
                return "";
            }
            // the path to a node is the path to its parent, plus "/node()[n]", where 
            // n is its position among its siblings.
            return String.Format(
                "{0}/node()[{1}]",
                GetXPathToNode(node.ParentNode),
                GetNodePosition(node)
                );
        }
    }
}
