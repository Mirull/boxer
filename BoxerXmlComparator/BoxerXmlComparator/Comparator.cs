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

            XmlNode baseNode = ResultDocument.CreateElement("DiffReport");
            ResultDocument.AppendChild(baseNode);
            XmlNode changeNode = ResultDocument.CreateElement("changes");
            baseNode.AppendChild(changeNode);
            XmlNode TagTokenNode = ResultDocument.CreateElement("TagToken");
            changeNode.AppendChild(TagTokenNode);
            int i = 0, j = 0, err_nr_1 = 0;

            foreach(XmlNode tagTokenNode in OriginTaggedTokensNode)
            {
                    foreach(XmlNode node in tagTokenNode.FirstChild)
                    {
                        var correctedNode = CorrectedTaggedTokensNode.ChildNodes[i].FirstChild.ChildNodes[j];
                        if (node.OuterXml != correctedNode.OuterXml)
                        {
                            err_nr_1++;
                            XmlNode changenode = ResultDocument.CreateElement("change");
                            XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                            changenumber.Value = err_nr_1.ToString();
                            changenode.Attributes.Append(changenumber);
                            TagTokenNode.AppendChild(changenode);

                            XmlNode originalnode = ResultDocument.CreateElement("original");
                            originalnode.InnerText = node.ParentNode.ParentNode.Name + " " + node.ParentNode.ParentNode.Attributes[0].Name + " " + node.ParentNode.ParentNode.Attributes[0].Value;

                            XmlNode OriginalNodeTag0 = ResultDocument.CreateElement("tag");
                            XmlAttribute OriginalTag0 = ResultDocument.CreateAttribute("type");

                            OriginalTag0.Value = node.ParentNode.ChildNodes[0].Attributes[0].Value;
                            OriginalNodeTag0.InnerText = node.ParentNode.ChildNodes[0].InnerText;
                            OriginalNodeTag0.Attributes.Append(OriginalTag0);
                            originalnode.AppendChild(OriginalNodeTag0);

                            XmlNode OriginalNodeTag1 = ResultDocument.CreateElement("tag");
                            XmlAttribute OriginalTag1 = ResultDocument.CreateAttribute("type");

                            OriginalTag1.Value = node.ParentNode.ChildNodes[1].Attributes[0].Value;
                            OriginalNodeTag1.InnerText = node.ParentNode.ChildNodes[1].InnerText;
                            OriginalNodeTag1.Attributes.Append(OriginalTag1);
                            originalnode.AppendChild(OriginalNodeTag1);

                            XmlNode OriginalNodeTag2 = ResultDocument.CreateElement("tag");
                            XmlAttribute OriginalTag2 = ResultDocument.CreateAttribute("type");

                            OriginalTag2.Value = node.ParentNode.ChildNodes[2].Attributes[0].Value;
                            OriginalNodeTag2.InnerText = node.ParentNode.ChildNodes[2].InnerText;
                            OriginalNodeTag2.Attributes.Append(OriginalTag2);
                            originalnode.AppendChild(OriginalNodeTag2);

                            XmlNode OriginalNodeTag3 = ResultDocument.CreateElement("tag");
                            XmlAttribute OriginalTag3 = ResultDocument.CreateAttribute("type");

                            OriginalTag3.Value = node.ParentNode.ChildNodes[3].Attributes[0].Value;
                            OriginalNodeTag3.InnerText = node.ParentNode.ChildNodes[3].InnerText;
                            OriginalNodeTag3.Attributes.Append(OriginalTag3);
                            originalnode.AppendChild(OriginalNodeTag3);
                            /*
                            XmlAttribute nodename = ResultDocument.CreateAttribute("NodeName");
                            nodename.Value = node.ParentNode.ParentNode.Name;

                            XmlAttribute attribute = ResultDocument.CreateAttribute("AttributeName");
                            attribute.Value = node.ParentNode.ParentNode.Attributes[0].Name;

                            XmlAttribute attributevalue = ResultDocument.CreateAttribute("AttributeValue");
                            attributevalue.Value = node.ParentNode.ParentNode.Attributes[0].Value;

                            XmlAttribute outernode = ResultDocument.CreateAttribute("OuterXml");
                            outernode.Value = node.OuterXml.ToString(); //tu cos kodowanie sie jebie. Obczaj ktos.

                            originalnode.Attributes.Append(nodename);
                            originalnode.Attributes.Append(attribute);
                            originalnode.Attributes.Append(attributevalue);
                            originalnode.Attributes.Append(outernode);
                            
                            */

                            
                            changenode.AppendChild(originalnode);

                            XmlNode correctednode = ResultDocument.CreateElement("corrected");
                            correctednode.InnerText = correctedNode.ParentNode.ParentNode.Name + " " + correctedNode.ParentNode.ParentNode.Attributes[0].Name + " " + correctedNode.ParentNode.ParentNode.Attributes[0].Value;

                            XmlNode CorrectedNodeTag0 = ResultDocument.CreateElement("tag");
                            XmlAttribute CorrectedTag0 = ResultDocument.CreateAttribute("type");

                            CorrectedTag0.Value = correctedNode.ParentNode.ChildNodes[0].Attributes[0].Value;
                            CorrectedNodeTag0.InnerText = correctedNode.ParentNode.ChildNodes[0].InnerText;
                            CorrectedNodeTag0.Attributes.Append(CorrectedTag0);
                            correctednode.AppendChild(CorrectedNodeTag0);

                            XmlNode CorrectedNodeTag1 = ResultDocument.CreateElement("tag");
                            XmlAttribute CorrectedTag1 = ResultDocument.CreateAttribute("type");

                            CorrectedTag1.Value = correctedNode.ParentNode.ChildNodes[1].Attributes[0].Value;
                            CorrectedNodeTag1.InnerText = correctedNode.ParentNode.ChildNodes[1].InnerText;
                            CorrectedNodeTag1.Attributes.Append(CorrectedTag1);
                            correctednode.AppendChild(CorrectedNodeTag1);

                            XmlNode CorrectedNodeTag2 = ResultDocument.CreateElement("tag");
                            XmlAttribute CorrectedTag2 = ResultDocument.CreateAttribute("type");

                            CorrectedTag2.Value = correctedNode.ParentNode.ChildNodes[2].Attributes[0].Value;
                            CorrectedNodeTag2.InnerText = correctedNode.ParentNode.ChildNodes[2].InnerText;
                            CorrectedNodeTag2.Attributes.Append(CorrectedTag2);
                            correctednode.AppendChild(CorrectedNodeTag2);

                            XmlNode CorrectedNodeTag3 = ResultDocument.CreateElement("tag");
                            XmlAttribute CorrectedTag3 = ResultDocument.CreateAttribute("type");

                            CorrectedTag3.Value = correctedNode.ParentNode.ChildNodes[3].Attributes[0].Value;
                            CorrectedNodeTag3.InnerText = correctedNode.ParentNode.ChildNodes[3].InnerText;
                            CorrectedNodeTag3.Attributes.Append(CorrectedTag3);
                            correctednode.AppendChild(CorrectedNodeTag3);
                            /*
                            nodename = ResultDocument.CreateAttribute("NodeName"); //to inaczej sie nazwie
                            nodename.Value = correctedNode.ParentNode.ParentNode.Name;

                            attribute = ResultDocument.CreateAttribute("AttributeName"); //to inaczej sie nazwie
                            attribute.Value = correctedNode.ParentNode.ParentNode.Attributes[0].Name;

                            attributevalue = ResultDocument.CreateAttribute("AttributeValue"); //to inaczej sie nazwie
                            attributevalue.Value = correctedNode.ParentNode.ParentNode.Attributes[0].Value;

                            correctednode.Attributes.Append(nodename);
                            correctednode.Attributes.Append(attribute);
                            correctednode.Attributes.Append(attributevalue);
                            correctednode.Attributes.Append(outernode);
                            */
                            
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
                            //MessageBox.Show("RIGHT: <" + node.ParentNode.ParentNode.Name + " " + node.ParentNode.ParentNode.Attributes[0].Name + "=" + node.ParentNode.ParentNode.Attributes[0].Value + ">" + node.OuterXml + "\nLEFT: <" + correctedNode.ParentNode.ParentNode.Name + " " + correctedNode.ParentNode.ParentNode.Attributes[0].Name + "=" + correctedNode.ParentNode.ParentNode.Attributes[0].Value + ">" + correctedNode.OuterXml);
                        }
                        j++;
                    }
                i++;
                j = 0;
            }


            XmlNode MergeNode = ResultDocument.CreateElement("Merge");
            changeNode.AppendChild(MergeNode);
            //koncept, zobaczymy.

            XmlNodeList OriginListDRS = OriginXml.SelectNodes("//merge/drs");
            XmlNodeList CorrectedListDRS = CorrectedXml.SelectNodes("//merge/drs");
            int m = 0, k = 0, err_nr_2 = 0; 




            
            for(int l = 0; l < OriginListDRS.Count; l++)
            {
                XmlNodeList OriginListDRS_domain = OriginListDRS[l].SelectNodes("//domain/dr");
                XmlNodeList CorrectedListDRS_domain = CorrectedListDRS[l].SelectNodes("//domain/dr");


                //szukanie usunięń
                foreach (XmlNode xn_dr_original in OriginListDRS_domain)
                {
                    Boolean not_found = true;
                    foreach (XmlNode xn_dr_corrected in CorrectedListDRS_domain)
                    {
                        if (xn_dr_corrected.Attributes[1].Value == xn_dr_original.Attributes[1].Value)
                        {
                            not_found = false;
                            break;
                        }
                    }
                    if (not_found == true) //nie znaleziono pasującego
                    {
                        err_nr_2++;
                        XmlNode changenode = ResultDocument.CreateElement("remove");
                        XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                        changenumber.Value = err_nr_2.ToString();
                        changenode.Attributes.Append(changenumber);
                        changenode.InnerText = xn_dr_original.Name + " " + xn_dr_original.Attributes[0].Name + " " + xn_dr_original.Attributes[0].Value + " " + xn_dr_original.Attributes[1].Name + " " + xn_dr_original.Attributes[1].Value;
                        MergeNode.AppendChild(changenode);
                        
                    }
                    else
                    {
                        //nico
                    }
                }
                foreach (XmlNode xn_dr_corrected in CorrectedListDRS_domain)
                {
                    Boolean not_found = true;
                    foreach (XmlNode xn_dr_original in OriginListDRS_domain)
                    {
                        if (xn_dr_corrected.Attributes[1].Value == xn_dr_original.Attributes[1].Value)
                        {
                            not_found = false;
                            break;
                        }
                    }
                    if (not_found == true) //nie znaleziono pasującego
                    {
                        err_nr_2++;
                        XmlNode changenode = ResultDocument.CreateElement("added");
                        XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                        changenumber.Value = err_nr_2.ToString();
                        changenode.Attributes.Append(changenumber);
                        changenode.InnerText = xn_dr_corrected.Name + " " + xn_dr_corrected.Attributes[0].Name + " " + xn_dr_corrected.Attributes[0].Value + " " + xn_dr_corrected.Attributes[1].Name + " " + xn_dr_corrected.Attributes[1].Value;
                        MergeNode.AppendChild(changenode);

                    }
                    else
                    {
                        //nico
                    }
                }
            }
            
            XmlNode errorpercentage = ResultDocument.CreateElement("statistics");
            XmlAttribute wrongtag = ResultDocument.CreateAttribute("changed");
            float percWT = ((float)err_nr_1 / (float)i) * 100;
            wrongtag.Value = Convert.ToString(percWT) + "%";

            //XmlAttribute removed = ResultDocument.CreateAttribute("removed");
            //float percR = (((float)i - (float)CorrectedTaggedTokensNode.ChildNodes.Count) * 100);
            //removed.Value = Convert.ToString(percR) + "%";
            errorpercentage.Attributes.Append(wrongtag);
            //errorpercentage.Attributes.Append(removed);
            baseNode.AppendChild(errorpercentage);

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
