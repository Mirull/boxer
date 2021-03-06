﻿using System;
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
        public XmlDocument CorrectedXml, OriginXml, ResultXml;

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadXmlOriginal_Click(object sender, EventArgs e)
        {
            OriginXml = LoadXmlDocument(this.NameFileOriginal);
            StatusBar.Text = "Wczytano plik 1";
        }

        private void LoadXMLCorrected_Click(object sender, EventArgs e)
        {
            CorrectedXml = LoadXmlDocument(this.NameFileCorrected);
            StatusBar.Text = "Wczytano plik 2";
        }

        private XmlDocument LoadXmlDocument(Label lab)
        {
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String path = openFileDialog1.FileName;
                lab.Text = path;
                var xml1 = new XmlDocument();
                xml1.XmlResolver = null;
                xml1.Load(path);
                return xml1;
            }
            else
                return null;
        }

        private void SaveXmlDocument()
        {
            XmlDocument saveDocument = this.ResultXml;
            //saveDocument.Load("test-doc.xml");

            saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.CreatePrompt = true;
            saveFileDialog1.FileName = null;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveDocument.Save(saveFileDialog1.FileName);
            }
            else
            {
                StatusBar.Text = "Błąd zapisu";
            }
        }

        private void Compare_Click(object sender, EventArgs e)
        {
            if (!CheckXml())
                StatusBar.Text = "Brak XML";
            else
            {
                Comparator Comp = new Comparator();
                ResultXml = Comp.Compare(OriginXml, CorrectedXml);
            }
            StatusBar.Text = "Ukończono porównywanie!";
        }

        private bool CheckXml()
        {
            return (CorrectedXml != null && OriginXml != null);
        }

        private void Show_Click(object sender, EventArgs e)
        {
            if (this.ResultXml == null)
                MessageBox.Show("Brak pliku wynikowego.");
            else
            {
                Form2 frm2 = new Form2(this.ResultXml);
                frm2.Show();
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            SaveXmlDocument();
        }
    }
}
