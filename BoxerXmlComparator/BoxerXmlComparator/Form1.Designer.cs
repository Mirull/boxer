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
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadXmlOriginal
            // 
            this.LoadXmlOriginal.Location = new System.Drawing.Point(12, 12);
            this.LoadXmlOriginal.Name = "LoadXmlOriginal";
            this.LoadXmlOriginal.Size = new System.Drawing.Size(119, 34);
            this.LoadXmlOriginal.TabIndex = 0;
            this.LoadXmlOriginal.Text = "Wczytaj 1";
            this.LoadXmlOriginal.UseVisualStyleBackColor = true;
            this.LoadXmlOriginal.Click += new System.EventHandler(this.LoadXmlOriginal_Click);
            // 
            // LoadXMLCorrected
            // 
            this.LoadXMLCorrected.Location = new System.Drawing.Point(161, 12);
            this.LoadXMLCorrected.Name = "LoadXMLCorrected";
            this.LoadXMLCorrected.Size = new System.Drawing.Size(119, 34);
            this.LoadXMLCorrected.TabIndex = 1;
            this.LoadXMLCorrected.Text = "Wczytaj 2";
            this.LoadXMLCorrected.UseVisualStyleBackColor = true;
            this.LoadXMLCorrected.Click += new System.EventHandler(this.LoadXMLCorrected_Click);
            // 
            // Compare
            // 
            this.Compare.Location = new System.Drawing.Point(88, 94);
            this.Compare.Name = "Compare";
            this.Compare.Size = new System.Drawing.Size(119, 34);
            this.Compare.TabIndex = 2;
            this.Compare.Text = "Porównaj";
            this.Compare.UseVisualStyleBackColor = true;
            this.Compare.Click += new System.EventHandler(this.Compare_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(12, 179);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(119, 34);
            this.Save.TabIndex = 3;
            this.Save.Text = "Zapisz";
            this.Save.UseVisualStyleBackColor = true;
            // 
            // Show
            // 
            this.Show.Location = new System.Drawing.Point(161, 179);
            this.Show.Name = "Show";
            this.Show.Size = new System.Drawing.Size(119, 34);
            this.Show.TabIndex = 4;
            this.Show.Text = "Wyświetl";
            this.Show.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "OpenXML";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 225);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(303, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusBar
            // 
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 247);
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
    }
}

