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

            // NEW FUNCTIONS

            RunCompare(OriginXml, CorrectedXml, ResultDocument);


            // Old functions - delete in feature
            //CompareTaggedTokens(OriginXml, CorrectedXml, ResultDocument);

            return this.ResultDocument;

        }

        private void RunCompare(XmlDocument OriginXml, XmlDocument CorrectedXml, XmlDocument ResultDocument)
        {
            // Główny NODE w XML

            XmlNode baseNode = ResultDocument.CreateElement("diffreport");
            ResultDocument.AppendChild(baseNode);

            // Poszukanie wszystkich XDRS w XML

            XmlNodeList OriginListXDRS = OriginXml.SelectNodes("//xdrs");
            XmlNodeList CorrectedListXDRS = CorrectedXml.SelectNodes("//xdrs");



            // PRZEJŚCIE PO WSZYSTKICH XDRS 

            for (int i = 0; i < OriginListXDRS.Count; i++)
            {
                XmlNode changeNode = ResultDocument.CreateElement("changes_in_xdrs");
                baseNode.AppendChild(changeNode);
                XmlAttribute id_xdrs = ResultDocument.CreateAttribute("id");
                id_xdrs.Value = (i + 1).ToString();
                changeNode.Attributes.Append(id_xdrs);

                // Przeszukanie taggedtokens w danym xdrs
                XmlNode TagTokenNode = ResultDocument.CreateElement("taggedtokens");
                changeNode.AppendChild(TagTokenNode);
                CompareSection_taggedtokens(OriginListXDRS[i], CorrectedListXDRS[i], TagTokenNode);

                // Przeszukanie merge w danym xdrs ( w nim jest Domain i Conds)
                CompareSection_merge(OriginListXDRS[i], CorrectedListXDRS[i], TagTokenNode, changeNode);
            }

        }

        private void CompareSection_merge(XmlNode OriginXml, XmlNode CorrectedXml, XmlNode TagTokenNode, XmlNode changeNode)
        {
            CompareSection_domain(OriginXml, CorrectedXml, TagTokenNode, changeNode);
            CompareSection_conds(OriginXml, CorrectedXml, TagTokenNode, changeNode);

        }

        private void CompareSection_taggedtokens(XmlNode OriginXml, XmlNode CorrectedXml, XmlNode TagTokenNode)
        {
            XmlNodeList OriginListTAGGED = OriginXml.SelectNodes("taggedtokens");
            XmlNodeList CorrectedListTAGGGED = CorrectedXml.SelectNodes("taggedtokens");
            int error_added = 0, error_removed = 0, error_changed = 0;

            // Zmienne dla obliczania procentu błędu
            int all_nodes = 0;
            float error_added_count = 0, error_removed_count = 0, error_changed_count = 0;


            for (int l = 0; l < OriginListTAGGED.Count; l++)
            {
                XmlNodeList OriginListTAGEED_token = OriginListTAGGED[l].SelectNodes("tagtoken");
                XmlNodeList CorrectedListTAGGGED_token = CorrectedListTAGGGED[l].SelectNodes("tagtoken");

                all_nodes = all_nodes + OriginListTAGEED_token.Count;

                // Search Removed Nodes

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
                        error_removed++;
                        XmlNode changenode = ResultDocument.CreateElement("removed");
                        XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                        changenumber.Value = error_removed.ToString();
                        changenode.AppendChild(changenode.OwnerDocument.ImportNode(xn_tag_original, true));
                        TagTokenNode.AppendChild(changenode);

                        changenode.Attributes.Append(changenumber);

                        error_removed_count++;
                    }
                    else
                    {
                        //nico
                    }
                }



                // Search Added Nodes 
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
                        error_added++;
                        XmlNode changenode = ResultDocument.CreateElement("added");
                        XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                        changenumber.Value = error_added.ToString();
                        changenode.AppendChild(changenode.OwnerDocument.ImportNode(xn_tag_corrected, true));
                        TagTokenNode.AppendChild(changenode);

                        changenode.Attributes.Append(changenumber);

                        error_added_count++;
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
                                error_changed++;
                                XmlNode changenode = ResultDocument.CreateElement("changed");
                                XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                                changenumber.Value = error_changed.ToString();

                                XmlNode originalnode = ResultDocument.CreateElement("original");
                                originalnode.AppendChild(originalnode.OwnerDocument.ImportNode(xn_tag_original, true));
                                XmlNode correctednode = ResultDocument.CreateElement("corrected");
                                correctednode.AppendChild(correctednode.OwnerDocument.ImportNode(xn_tag_corrected, true));

                                changenode.AppendChild(originalnode);
                                changenode.AppendChild(correctednode);

                                TagTokenNode.AppendChild(changenode);

                                changenode.Attributes.Append(changenumber);

                                error_changed_count++;
                            }
                            break;
                        }
                    }
                }
            }

            TagTokenNode.AppendChild(AppendResultNode(error_added_count, error_removed_count, error_changed_count, all_nodes));

        }

        private void CompareSection_domain(XmlNode OriginXml, XmlNode CorrectedXml, XmlNode TagTokenNode, XmlNode changeNode)
        {
            XmlNode DomainNode = ResultDocument.CreateElement("Domain");
            changeNode.AppendChild(DomainNode);

            XmlNodeList OriginListDRS = OriginXml.SelectNodes("merge/drs/domain");
            XmlNodeList CorrectedListDRS = CorrectedXml.SelectNodes("merge/drs/domain");
            int error_added = 0, error_removed = 0, error_changed = 0;

            // Zmienne dla obliczania procentu błędu
            int all_nodes = 0;
            float error_added_count = 0, error_removed_count = 0, error_changed_count = 0;

            for (int l = 0; l < OriginListDRS.Count; l++)
            {
                XmlNodeList OriginListDRS_domain = OriginListDRS[l].SelectNodes("dr");
                XmlNodeList CorrectedListDRS_domain = CorrectedListDRS[l].SelectNodes("dr");

                all_nodes = all_nodes + CorrectedListDRS_domain.Count;

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
                        error_removed++;
                        XmlNode changenode = ResultDocument.CreateElement("removed");
                        XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                        changenumber.Value = error_removed.ToString();
                        changenode.AppendChild(changenode.OwnerDocument.ImportNode(xn_dr_original, true));
                        DomainNode.AppendChild(changenode);

                        changenode.Attributes.Append(changenumber);
                        error_removed_count++;
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
                        error_added++;
                        XmlNode changenode = ResultDocument.CreateElement("added");
                        XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                        changenumber.Value = error_added.ToString();

                        changenode.AppendChild(changenode.OwnerDocument.ImportNode(xn_dr_corrected, true));
                        DomainNode.AppendChild(changenode);

                        changenode.Attributes.Append(changenumber);

                        error_added_count++;
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
                                error_changed++;
                                XmlNode changenode = ResultDocument.CreateElement("changed");
                                XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                                changenumber.Value = error_changed.ToString();

                                XmlNode originalnode = ResultDocument.CreateElement("original");
                                originalnode.AppendChild(originalnode.OwnerDocument.ImportNode(xn_dr_original, true));
                                XmlNode correctednode = ResultDocument.CreateElement("corrected");
                                correctednode.AppendChild(correctednode.OwnerDocument.ImportNode(xn_dr_corrected, true));

                                changenode.AppendChild(originalnode);
                                changenode.AppendChild(correctednode);

                                DomainNode.AppendChild(changenode);

                                changenode.Attributes.Append(changenumber);

                                error_changed_count++;
                            }
                            break;
                        }
                    }
                }
            }

            DomainNode.AppendChild(AppendResultNode(error_added_count, error_removed_count, error_changed_count, all_nodes));
        }

        private void CompareSection_conds(XmlNode OriginXml, XmlNode CorrectedXml, XmlNode TagTokenNode, XmlNode changeNode)
        {
            XmlNode CondsNode = ResultDocument.CreateElement("Conds");
            changeNode.AppendChild(CondsNode);

            XmlNodeList OriginListCONDS = OriginXml.SelectNodes("merge/drs/conds");
            XmlNodeList CorrectedListCONDS = CorrectedXml.SelectNodes("merge/drs/conds");
            int error_added = 0, error_removed = 0, error_changed = 0;

            // Zmienne dla obliczania procentu błędu
            int all_nodes = 0;
            float error_added_count = 0, error_removed_count = 0, error_changed_count = 0;

            for (int l = 0; l < OriginListCONDS.Count; l++)
            {
                XmlNodeList OriginListCONDS_cond = OriginListCONDS[l].SelectNodes("cond");
                XmlNodeList CorrectedListCONDS_cond = CorrectedListCONDS[l].SelectNodes("cond");

                all_nodes = all_nodes + OriginListCONDS_cond.Count;

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
                        error_removed++;
                        XmlNode changenode = ResultDocument.CreateElement("removed");
                        XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                        changenumber.Value = error_removed.ToString();

                        changenode.AppendChild(changenode.OwnerDocument.ImportNode(xn_cond_original, true));
                        CondsNode.AppendChild(changenode);

                        changenode.Attributes.Append(changenumber);

                        error_removed_count++;
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
                        error_added++;
                        XmlNode changenode = ResultDocument.CreateElement("added");
                        XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                        changenumber.Value = error_added.ToString();

                        changenode.AppendChild(changenode.OwnerDocument.ImportNode(xn_cond_corrected, true));
                        CondsNode.AppendChild(changenode);

                        changenode.Attributes.Append(changenumber);

                        error_added_count++;
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
                                Boolean not_found = true;
                                foreach (XmlNode confirm in OriginListCONDS_cond)
                                {
                                    if (xn_cond_corrected.OuterXml == confirm.OuterXml)
                                    {
                                        not_found = false;
                                        break;
                                    }
                                    else
                                    {

                                    }
                                }
                                if (not_found == true)
                                {
                                    error_changed++;
                                    XmlNode changenode = ResultDocument.CreateElement("changed");
                                    XmlAttribute changenumber = ResultDocument.CreateAttribute("number");
                                    changenumber.Value = error_changed.ToString();



                                    XmlNode originalnode = ResultDocument.CreateElement("original");
                                    originalnode.AppendChild(originalnode.OwnerDocument.ImportNode(xn_cond_original, true));
                                    XmlNode correctednode = ResultDocument.CreateElement("corrected");
                                    correctednode.AppendChild(correctednode.OwnerDocument.ImportNode(xn_cond_corrected, true));
                                    changenode.Attributes.Append(changenumber);
                                    changenode.AppendChild(originalnode);
                                    changenode.AppendChild(correctednode);

                                    CondsNode.AppendChild(changenode);

                                    changenode.Attributes.Append(changenumber);

                                    error_changed_count++;
                                }
                                else
                                {

                                }
                            }
                            break;
                        }
                    }
                }
            }


            CondsNode.AppendChild(AppendResultNode(error_added_count, error_removed_count, error_changed_count, all_nodes));


        }

        // ----------------------------------------------------------------------------------------------------------------
        // STARA FUNKCJA - na zakończenei do wyrzucenia
        // ---------------------------------------------------------------------------------------------------------------

        private void CompareTaggedTokens(XmlDocument OriginXml, XmlDocument CorrectedXml, XmlDocument ResultDocument)
        {
            //  var OriginTaggedTokensNode = OriginXml.SelectSingleNode("//taggedtokens");
            //  var CorrectedTaggedTokensNode = CorrectedXml.SelectSingleNode("//taggedtokens");

            XmlNode baseNode = ResultDocument.CreateElement("DiffReport");
            ResultDocument.AppendChild(baseNode);
            XmlNode changeNode = ResultDocument.CreateElement("Changes");
            baseNode.AppendChild(changeNode);

            XmlNode TagTokenNode = ResultDocument.CreateElement("TagToken");
            changeNode.AppendChild(TagTokenNode);

            XmlNodeList OriginListTAGGED = OriginXml.SelectNodes("//taggedtokens");
            XmlNodeList CorrectedListTAGGGED = CorrectedXml.SelectNodes("//taggedtokens");
            int err_nr_1_add = 0, err_nr_1_removed = 0, err_nr_1_changed = 0;

            // Zmienne dla obliczania procentu błędu
            int base_1 = 0;
            float err_add_count = 0, err_removed_count = 0, err_changed_count = 0;


            for (int l = 0; l < OriginListTAGGED.Count; l++)
            {
                XmlNodeList OriginListTAGEED_token = OriginListTAGGED[l].SelectNodes("//tagtoken");
                XmlNodeList CorrectedListTAGGGED_token = CorrectedListTAGGGED[l].SelectNodes("//tagtoken");

                base_1 = OriginListTAGEED_token.Count;

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

                        err_removed_count++;
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

                        err_add_count++;
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

                                err_changed_count++;
                            }
                            break;
                        }
                    }
                }
            }

            TagTokenNode.AppendChild(AppendResultNode(err_add_count, err_removed_count, err_changed_count, base_1));

            err_add_count = err_removed_count = err_changed_count = 0;

            XmlNode DomainNode = ResultDocument.CreateElement("Domain");
            changeNode.AppendChild(DomainNode);

            XmlNodeList OriginListDRS = OriginXml.SelectNodes("//merge/drs/domain");
            XmlNodeList CorrectedListDRS = CorrectedXml.SelectNodes("//merge/drs/domain");
            int err_nr_2_add = 0, err_nr_2_removed = 0, err_nr_2_changed = 0;
            int base_2 = 0;

            for (int l = 0; l < OriginListDRS.Count; l++)
            {
                XmlNodeList OriginListDRS_domain = OriginListDRS[l].SelectNodes("dr");
                XmlNodeList CorrectedListDRS_domain = CorrectedListDRS[l].SelectNodes("dr");

                base_2 = base_2 + CorrectedListDRS_domain.Count;

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

            DomainNode.AppendChild(AppendResultNode(err_add_count, err_removed_count, err_changed_count, base_2));

            err_add_count = err_removed_count = err_changed_count = 0;




            XmlNode CondsNode = ResultDocument.CreateElement("Conds");
            changeNode.AppendChild(CondsNode);

            XmlNodeList OriginListCONDS = OriginXml.SelectNodes("//merge/drs/conds");
            XmlNodeList CorrectedListCONDS = CorrectedXml.SelectNodes("//merge/drs/conds");
            int err_nr_3_add = 0, err_nr_3_removed = 0, err_nr_3_changed = 0;
            int base_3 = 0;

            for (int l = 0; l < OriginListCONDS.Count; l++)
            {
                XmlNodeList OriginListCONDS_cond = OriginListCONDS[l].SelectNodes("cond");
                XmlNodeList CorrectedListCONDS_cond = CorrectedListCONDS[l].SelectNodes("cond");

                base_3 = base_3 + OriginListCONDS_cond.Count;

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
                        CondsNode.AppendChild(changenode);

                        changenode.Attributes.Append(changenumber);

                        err_removed_count++;
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
                        CondsNode.AppendChild(changenode);

                        changenode.Attributes.Append(changenumber);

                        err_add_count++;
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
                                Boolean not_found = true;
                                foreach (XmlNode confirm in OriginListCONDS_cond)
                                {
                                    if (xn_cond_corrected.OuterXml == confirm.OuterXml)
                                    {
                                        not_found = false;
                                        break;
                                    }
                                    else
                                    {

                                    }
                                }
                                if (not_found == true)
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

                                    CondsNode.AppendChild(changenode);

                                    changenode.Attributes.Append(changenumber);

                                    err_changed_count++;
                                }
                                else
                                {

                                }
                            }
                            break;
                        }
                    }
                }
            }


            CondsNode.AppendChild(AppendResultNode(err_add_count, err_removed_count, err_changed_count, base_3));

            err_add_count = err_removed_count = err_changed_count = 0;

        }

        private XmlNode CreateResultNode(string name, float count, float all_nodes)
        {
            XmlNode ChildResultNode = ResultDocument.CreateElement(name);
            XmlAttribute PercentAttr = ResultDocument.CreateAttribute("percent");
            XmlAttribute AllNodesAttr = ResultDocument.CreateAttribute("all_nodes");
            XmlAttribute WrongNodesAttr = ResultDocument.CreateAttribute("wrong_nodes");

            PercentAttr.Value = Math.Round((decimal)((count / all_nodes) * 100), 3).ToString();
            AllNodesAttr.Value = all_nodes.ToString();
            WrongNodesAttr.Value = count.ToString();

            ChildResultNode.Attributes.Append(PercentAttr);
            ChildResultNode.Attributes.Append(AllNodesAttr);
            ChildResultNode.Attributes.Append(WrongNodesAttr);

            return ChildResultNode;
        }

        private XmlNode AppendResultNode(float add_p, float remove_p, float change_p, int all_nodes)
        {
            XmlNode ResultNode = ResultDocument.CreateElement("Result");
            ResultNode.AppendChild(CreateResultNode("Added", add_p, all_nodes));
            ResultNode.AppendChild(CreateResultNode("Removed", remove_p, all_nodes));
            ResultNode.AppendChild(CreateResultNode("Changed", change_p, all_nodes));
            ResultNode.AppendChild(CreateResultNode("All", (add_p + remove_p + change_p), all_nodes));

            return ResultNode;
        }

        private string GetAttributeValue(XmlNode xNode, string attributeToFind1, string attributeToFind2)
        {
            string returnValue = "";
            XmlElement ele = xNode as XmlElement;

            if (ele.HasAttribute(attributeToFind1) == true)
                returnValue = ele.GetAttribute(attributeToFind1);
            else if (ele.HasAttribute(attributeToFind2) == true)
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
