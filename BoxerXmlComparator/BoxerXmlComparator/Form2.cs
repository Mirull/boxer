using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace BoxerXmlComparator
{
    public partial class Form2 : Form
    {
        private String localXMLFileName = "localXML.xml";

        public Form2(XmlDocument XML)
        {
            InitializeComponent();

            XML.Save(localXMLFileName);
           
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(localXMLFileName);
            webBrowser1.Url = new Uri(xmlDoc.BaseURI);
             
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (File.Exists(localXMLFileName))
                File.Delete(localXMLFileName);

            base.Dispose(disposing);
        }     

    }
}
