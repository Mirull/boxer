namespace BoxerXmlComparator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoadXmlOriginal = new System.Windows.Forms.Button();
            this.LoadXMLCorrected = new System.Windows.Forms.Button();
            this.Compare = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.Show = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.NameFileOriginal = new System.Windows.Forms.Label();
            this.NameFileCorrected = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadXmlOriginal
            // 
            this.LoadXmlOriginal.Location = new System.Drawing.Point(12, 32);
            this.LoadXmlOriginal.Name = "LoadXmlOriginal";
            this.LoadXmlOriginal.Size = new System.Drawing.Size(244, 34);
            this.LoadXmlOriginal.TabIndex = 0;
            this.LoadXmlOriginal.Text = "Wczytaj oryginalny XML";
            this.LoadXmlOriginal.UseVisualStyleBackColor = true;
            this.LoadXmlOriginal.Click += new System.EventHandler(this.LoadXmlOriginal_Click);
            // 
            // LoadXMLCorrected
            // 
            this.LoadXMLCorrected.Location = new System.Drawing.Point(12, 105);
            this.LoadXMLCorrected.Name = "LoadXMLCorrected";
            this.LoadXMLCorrected.Size = new System.Drawing.Size(244, 34);
            this.LoadXMLCorrected.TabIndex = 1;
            this.LoadXMLCorrected.Text = "Wczytaj poprawiony XML";
            this.LoadXMLCorrected.UseVisualStyleBackColor = true;
            this.LoadXMLCorrected.Click += new System.EventHandler(this.LoadXMLCorrected_Click);
            // 
            // Compare
            // 
            this.Compare.Location = new System.Drawing.Point(12, 145);
            this.Compare.Name = "Compare";
            this.Compare.Size = new System.Drawing.Size(244, 34);
            this.Compare.TabIndex = 2;
            this.Compare.Text = "Porównaj";
            this.Compare.UseVisualStyleBackColor = true;
            this.Compare.Click += new System.EventHandler(this.Compare_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(12, 185);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(119, 34);
            this.Save.TabIndex = 3;
            this.Save.Text = "Zapisz wynik (XML)";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Show
            // 
            this.Show.Location = new System.Drawing.Point(137, 185);
            this.Show.Name = "Show";
            this.Show.Size = new System.Drawing.Size(119, 34);
            this.Show.TabIndex = 4;
            this.Show.Text = "Wyświetl";
            this.Show.UseVisualStyleBackColor = true;
            this.Show.Click += new System.EventHandler(this.Show_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "OpenXML";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 227);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(264, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusBar
            // 
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(0, 17);
            // 
            // NameFileOriginal
            // 
            this.NameFileOriginal.AutoSize = true;
            this.NameFileOriginal.Location = new System.Drawing.Point(12, 9);
            this.NameFileOriginal.Name = "NameFileOriginal";
            this.NameFileOriginal.Size = new System.Drawing.Size(97, 13);
            this.NameFileOriginal.TabIndex = 6;
            this.NameFileOriginal.Text = "(Nie wybrano pliku)";
            // 
            // NameFileCorrected
            // 
            this.NameFileCorrected.AutoSize = true;
            this.NameFileCorrected.Location = new System.Drawing.Point(12, 79);
            this.NameFileCorrected.Name = "NameFileCorrected";
            this.NameFileCorrected.Size = new System.Drawing.Size(97, 13);
            this.NameFileCorrected.TabIndex = 7;
            this.NameFileCorrected.Text = "(Nie wybrano pliku)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 249);
            this.Controls.Add(this.NameFileCorrected);
            this.Controls.Add(this.NameFileOriginal);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.Show);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Compare);
            this.Controls.Add(this.LoadXMLCorrected);
            this.Controls.Add(this.LoadXmlOriginal);
            this.Name = "Form1";
            this.Text = "BoxerXMLComparator";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadXmlOriginal;
        private System.Windows.Forms.Button LoadXMLCorrected;
        private System.Windows.Forms.Button Compare;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Show;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusBar;
        private System.Windows.Forms.Label NameFileOriginal;
        private System.Windows.Forms.Label NameFileCorrected;
    }
}

