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
            int i = 0,j = 0;
            foreach(XmlNode tagTokenNode in OriginTaggedTokensNode)
            {
                    foreach(XmlNode node in tagTokenNode.FirstChild)
                    {
                       
                        var correctedNode = CorrectedTaggedTokensNode.ChildNodes[i].FirstChild.ChildNodes[j];
                        if (node.OuterXml != correctedNode.OuterXml)
                        {
                            

                            XmlNode originalnode = ResultDocument.CreateElement("original");
                            XmlAttribute attribute = ResultDocument.CreateAttribute("line");
                            originalnode.InnerText = node.ParentNode.ParentNode.Name + " " + node.ParentNode.ParentNode.Attributes[0].Name + " "  + node.ParentNode.ParentNode.Attributes[0].Value + " " + node.OuterXml;
                            rootNode.AppendChild(originalnode);

                            XmlNode correctednode = ResultDocument.CreateElement("corrected");
                            correctednode.InnerText = correctedNode.ParentNode.ParentNode.Name + " " + correctedNode.ParentNode.ParentNode.Attributes[0].Name + " " + correctedNode.ParentNode.ParentNode.Attributes[0].Value + " " + correctedNode.OuterXml;
                            rootNode.AppendChild(correctednode);

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
    }
}
