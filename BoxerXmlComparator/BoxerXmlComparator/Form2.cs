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

namespace BoxerXmlComparator
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            //Form1 frm1 = new Form1();
            //webBrowser1.Url = new Uri(frm1.ResultXml.BaseURI);

            string fileName = "test-doc.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            webBrowser1.Url = new Uri(xmlDoc.BaseURI);
        }
    }
}
