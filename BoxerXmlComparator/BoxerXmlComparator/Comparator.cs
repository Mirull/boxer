using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            
            for (int i = 0; i < OriginXml.ChildNodes.Count; i++)
            {
                CompareSection(OriginXml.ChildNodes[i], CorrectedXml.ChildNodes[i]);
            }

            return this.ResultDocument;
        
        }

        private void CompareSection(XmlNode sectionOriginal, XmlNode sectionCorrected)
        {
            
        }
    }
}
