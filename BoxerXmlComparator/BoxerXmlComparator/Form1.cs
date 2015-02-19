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
    public partial class Form1 : Form
    {
        public XmlDocument CorrectedXml, OriginXml,ResultXml;

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadXmlOriginal_Click(object sender, EventArgs e)
        {
            OriginXml = LoadXmlDocument();
            StatusBar.Text = "Wczytano plik 1";
        }

        private void LoadXMLCorrected_Click(object sender, EventArgs e)
        {
            CorrectedXml = LoadXmlDocument();
            StatusBar.Text = "Wczytano plik 2";
        }

        private XmlDocument LoadXmlDocument()
        {
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String path = openFileDialog1.FileName;
                var xml1 = new XmlDocument();
                xml1.XmlResolver = null;
                xml1.Load(path);
                return xml1;
            }
            else
                return null;            
        }

        private void Compare_Click(object sender, EventArgs e)
        {
            if (!CheckXml())
                MessageBox.Show("Brak XML");
            else
            {
                Comparator Comp = new Comparator();
                Comp.Compare(OriginXml, CorrectedXml);
            }
            StatusBar.Text = "Ukończono porównywanie!";
        }

        private bool CheckXml()
        {
            return (CorrectedXml != null && OriginXml != null);
        }

        private void Show_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
        }
    }
}
