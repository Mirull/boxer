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
          //  var OriginTaggedTokensNode = OriginXml.SelectSingleNode("//taggedtokens");
          //  var CorrectedTaggedTokensNode = CorrectedXml.SelectSingleNode("//taggedtokens");

            XmlNode baseNode = ResultDocument.CreateElement("DiffReport");
            ResultDocument.AppendChild(baseNode);
            XmlNode changeNode = ResultDocument.CreateElement("changes");
            baseNode.AppendChild(changeNode);
            XmlNode TagTokenNode = ResultDocument.CreateElement("TagToken");
            changeNode.AppendChild(TagTokenNode);       

            XmlNodeList OriginListTAGGED = OriginXml.SelectNodes("//taggedtokens");
            XmlNodeList CorrectedListTAGGGED = CorrectedXml.SelectNodes("//taggedtokens");
            int err_nr_1_add = 0, err_nr_1_removed = 0, err_nr_1_changed = 0;

            for (int l = 0; l < OriginListTAGGED.Count; l++)
            {
                XmlNodeList OriginListTAGEED_token = OriginListTAGGED[l].SelectNodes("//tagtoken");
                XmlNodeList CorrectedListTAGGGED_token = CorrectedListTAGGGED[l].SelectNodes("//tagtoken");


                //szukanie usunięń
                foreach (XmlNode xn_tag_original in OriginListTAGEED_token)
                {
                    Boolean not_found = true;
                    foreach (XmlNode xn_tag_corrected in CorrectedListTAGGGED_token)
                    {
                        if (xn_tag_original.Attributes[0].Value == xn_tag_corrected.Attributes[0].Value)
                        {
                            not_found = false;
                            break;
                        }
                    }
                    if (not_found == true) //nie znaleziono pasującego
                    {
                        err_nr_1_removed++;
                        XmlNode changenode = ResultDocument.CreateElement("removed");
                        XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                        changenumber.Value = err_nr_1_removed.ToString();                    
                        changenode.AppendChild(changenode.OwnerDocument.ImportNode(xn_tag_original, true));
                        TagTokenNode.AppendChild(changenode);

                        changenode.Attributes.Append(changenumber);

                    }
                    else
                    {
                        //nico
                    }
                }

                //szukanie dodań
                foreach (XmlNode xn_tag_corrected in CorrectedListTAGGGED_token)
                {
                    Boolean not_found = true;
                    foreach (XmlNode xn_tag_original in OriginListTAGEED_token)
                    {
                        if (xn_tag_corrected.Attributes[0].Value == xn_tag_original.Attributes[0].Value)
                        {
                            not_found = false;
                            break;
                        }
                    }
                    if (not_found == true) //nie znaleziono pasującego
                    {
                        err_nr_1_add++;
                        XmlNode changenode = ResultDocument.CreateElement("added");
                        XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                        changenumber.Value = err_nr_1_add.ToString();
                        changenode.AppendChild(changenode.OwnerDocument.ImportNode(xn_tag_corrected, true));
                        TagTokenNode.AppendChild(changenode);

                        changenode.Attributes.Append(changenumber);
                    }
                    else
                    {
                        //nico
                    }
                }

                //szukanie zmian
                foreach (XmlNode xn_tag_original in OriginListTAGEED_token)
                {
                    foreach (XmlNode xn_tag_corrected in CorrectedListTAGGGED_token)
                    {
                        if (xn_tag_corrected.Attributes[0].Value == xn_tag_original.Attributes[0].Value)
                        {
                            if (xn_tag_original.OuterXml != xn_tag_corrected.OuterXml)
                            {
                                err_nr_1_changed++;
                                XmlNode changenode = ResultDocument.CreateElement("changed");
                                XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                                changenumber.Value = err_nr_1_changed.ToString();

                                XmlNode originalnode = ResultDocument.CreateElement("original");
                                originalnode.AppendChild(originalnode.OwnerDocument.ImportNode(xn_tag_original, true));
                                XmlNode correctednode = ResultDocument.CreateElement("corrected");
                                correctednode.AppendChild(correctednode.OwnerDocument.ImportNode(xn_tag_corrected, true));

                                changenode.AppendChild(originalnode);
                                changenode.AppendChild(correctednode);

                                TagTokenNode.AppendChild(changenode);

                                changenode.Attributes.Append(changenumber);
                            }
                            break;
                        }
                    }
                }
            }

            XmlNode DomainNode = ResultDocument.CreateElement("Domain");
            changeNode.AppendChild(DomainNode);

            XmlNodeList OriginListDRS = OriginXml.SelectNodes("//merge/drs");
            XmlNodeList CorrectedListDRS = CorrectedXml.SelectNodes("//merge/drs");
            int err_nr_2_add = 0, err_nr_2_removed = 0, err_nr_2_changed = 0;




            
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
                        err_nr_2_removed++;
                        XmlNode changenode = ResultDocument.CreateElement("removed");
                        XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                        changenumber.Value = err_nr_2_removed.ToString();
                        changenode.AppendChild(changenode.OwnerDocument.ImportNode(xn_dr_original, true));
                        DomainNode.AppendChild(changenode);

                        changenode.Attributes.Append(changenumber);
                    }
                    else
                    {
                        //nico
                    }
                }

                //szukanie dodań
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
                        err_nr_2_add++;
                        XmlNode changenode = ResultDocument.CreateElement("added");
                        XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                        changenumber.Value = err_nr_2_add.ToString();

                        changenode.AppendChild(changenode.OwnerDocument.ImportNode(xn_dr_corrected, true));
                        DomainNode.AppendChild(changenode);

                        changenode.Attributes.Append(changenumber);
                    }
                    else
                    {
                        //nico
                    }
                }

                //szukanie zmian
                foreach (XmlNode xn_dr_original in OriginListDRS_domain)
                {
                    foreach (XmlNode xn_dr_corrected in CorrectedListDRS_domain)
                    {
                        if (xn_dr_corrected.Attributes[1].Value == xn_dr_original.Attributes[1].Value)
                        {
                            if (xn_dr_original.OuterXml != xn_dr_corrected.OuterXml)
                            {
                                err_nr_2_changed++;
                                XmlNode changenode = ResultDocument.CreateElement("changed");
                                XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                                changenumber.Value = err_nr_2_changed.ToString();

                                XmlNode originalnode = ResultDocument.CreateElement("original");
                                originalnode.AppendChild(originalnode.OwnerDocument.ImportNode(xn_dr_original, true));
                                XmlNode correctednode = ResultDocument.CreateElement("corrected");
                                correctednode.AppendChild(correctednode.OwnerDocument.ImportNode(xn_dr_corrected, true));

                                changenode.AppendChild(originalnode);
                                changenode.AppendChild(correctednode);

                                DomainNode.AppendChild(changenode);

                                changenode.Attributes.Append(changenumber);
                            }
                            break;
                        }
                    }
                }
            }
            


            XmlNode CondsNode = ResultDocument.CreateElement("Conds");
            changeNode.AppendChild(DomainNode);
            //koncept, zobaczymy.

            XmlNodeList OriginListCONDS = OriginXml.SelectNodes("//merge/drs");
            XmlNodeList CorrectedListCONDS = CorrectedXml.SelectNodes("//merge/drs");
            int err_nr_3_add = 0, err_nr_3_removed = 0, err_nr_3_changed = 0;

            for(int l = 0; l < OriginListDRS.Count; l++)
            {
                XmlNodeList OriginListCONDS_cond = OriginListCONDS[l].SelectNodes("//conds/cond");
                XmlNodeList CorrectedListCONDS_cond = CorrectedListCONDS[l].SelectNodes("//conds/cond");


                //szukanie usunięń
                foreach (XmlNode xn_cond_original in OriginListCONDS_cond)
                {
                    Boolean not_found = true;
                    foreach (XmlNode xn_cond_corrected in CorrectedListCONDS_cond)
                    {

                        if ((GetAttributeValue(xn_cond_corrected.FirstChild, "arg1", "arg")) == (GetAttributeValue(xn_cond_original.FirstChild, "arg1", "arg")) && xn_cond_corrected.FirstChild.Name == xn_cond_original.FirstChild.Name)
                        {
                            not_found = false;
                            break;
                        }
                    }
                    if (not_found == true) //nie znaleziono pasującego
                    {
                        err_nr_3_removed++;
                        XmlNode changenode = ResultDocument.CreateElement("removed");
                        XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                        changenumber.Value = err_nr_3_removed.ToString();

                        changenode.AppendChild(changenode.OwnerDocument.ImportNode(xn_cond_original, true));
                        DomainNode.AppendChild(changenode);

                        changenode.Attributes.Append(changenumber);
                    }
                    else
                    {
                        //nico
                    }
                }

                //szukanie dodań
                foreach (XmlNode xn_cond_corrected in CorrectedListCONDS_cond)
                {
                    Boolean not_found = true;
                    foreach (XmlNode xn_cond_original in OriginListCONDS_cond)
                    {
                        if ((GetAttributeValue(xn_cond_corrected.FirstChild, "arg1", "arg")) == (GetAttributeValue(xn_cond_original.FirstChild, "arg1", "arg")) && xn_cond_corrected.FirstChild.Name == xn_cond_original.FirstChild.Name)
                        {
                            not_found = false;
                            break;
                        }
                    }
                    if (not_found == true) //nie znaleziono pasującego
                    {
                        err_nr_3_add++;
                        XmlNode changenode = ResultDocument.CreateElement("added");
                        XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                        changenumber.Value = err_nr_3_add.ToString();

                        changenode.AppendChild(changenode.OwnerDocument.ImportNode(xn_cond_corrected, true));
                        DomainNode.AppendChild(changenode);

                        changenode.Attributes.Append(changenumber);

                    }
                    else
                    {
                        //nico
                    }
                }

                //szukanie zmian
                foreach (XmlNode xn_cond_original in OriginListCONDS_cond)
                {
                    foreach (XmlNode xn_cond_corrected in CorrectedListCONDS_cond)
                    {
                        if ((GetAttributeValue(xn_cond_corrected.FirstChild, "arg1", "arg")) == (GetAttributeValue(xn_cond_original.FirstChild, "arg1", "arg")) && xn_cond_corrected.FirstChild.Name == xn_cond_original.FirstChild.Name)
                        {
                            if (xn_cond_original.OuterXml != xn_cond_corrected.OuterXml)
                            {
                                err_nr_3_changed++;
                                XmlNode changenode = ResultDocument.CreateElement("changed");
                                XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                                changenumber.Value = err_nr_3_changed.ToString();



                                XmlNode originalnode = ResultDocument.CreateElement("original");
                                originalnode.AppendChild(originalnode.OwnerDocument.ImportNode(xn_cond_original, true));
                                XmlNode correctednode = ResultDocument.CreateElement("corrected");
                                correctednode.AppendChild(correctednode.OwnerDocument.ImportNode(xn_cond_corrected, true));
                                changenode.Attributes.Append(changenumber);
                                changenode.AppendChild(originalnode);
                                changenode.AppendChild(correctednode);

                                DomainNode.AppendChild(changenode);

                                changenode.Attributes.Append(changenumber);
                            }
                            break;
                        }
                    }
                }
            }


            
          //  XmlNode errorpercentage = ResultDocument.CreateElement("statistics");
          //  XmlAttribute wrongtag = ResultDocument.CreateAttribute("changed");
          //  float percWT = ((float)err_nr_1 / (float)i) * 100;
         //   wrongtag.Value = Convert.ToString(percWT) + "%";

            //XmlAttribute removed = ResultDocument.CreateAttribute("removed");
            //float percR = (((float)i - (float)CorrectedTaggedTokensNode.ChildNodes.Count) * 100);
            //removed.Value = Convert.ToString(percR) + "%";
            //errorpercentage.Attributes.Append(wrongtag);
            //errorpercentage.Attributes.Append(removed);
            //baseNode.AppendChild(errorpercentage);

            ResultDocument.Save("test-doc.xml");
        }

        private string GetAttributeValue(XmlNode xNode, string attributeToFind1, string attributeToFind2)
        {
            string returnValue = "";
            XmlElement ele = xNode as XmlElement;

            if (ele.HasAttribute(attributeToFind1) == true)
                returnValue = ele.GetAttribute(attributeToFind1);
            else if (ele.HasAttribute(attributeToFind2) == true )
                returnValue = ele.GetAttribute(attributeToFind2);
            else
                returnValue = ele.Name;

            return returnValue;
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
