using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SyntaxHighlighter;
using System.Threading;

namespace Web_Builder
{
    public partial class Web_Builder : Form
    {
        public Web_Builder()
        {
            InitializeComponent();
        }

        public LinkedList<string> editingFiles = new LinkedList<string>();

        private void Web_Builder_Load(object sender, EventArgs e)
        {
            populateFiles("c:/users/" + this.Text.Substring(8));
            
            Thread.Sleep(1000);
            string path = global::Web_Builder.Form1.path + @"\index.html";
            editingFiles.AddLast(path);
            /*
            StreamReader reader = new StreamReader(path);
            string text_to_load = reader.ReadToEnd();
            reader.Close();
             * */
            richTextBox1.Text = "";
        }

        int spaces = 0;
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && (Control.ModifierKeys & Keys.Shift) != 0)
            {
                try
                {
                    spaces = 0;
                    foreach (char thisChar in richTextBox1.Lines[richTextBox1.GetLineFromCharIndex(richTextBox1.GetFirstCharIndexOfCurrentLine())])
                    {
                        if (thisChar == ' ')
                            spaces++;
                        else
                            break;
                    }
                }
                catch { }
            }

            webBrowser1.Url = new Uri("file:///" + global::Web_Builder.Form1.path + "/index.html");
            //global::Web_Builder.Form1.path + "/" + richTextBox1.Parent.Text)
            using (StreamWriter sw = new StreamWriter(editingFiles.ElementAt<string>(tabControl1.SelectedIndex)))
            {
                //MessageBox.Show(global::Web_Builder.Form1.path + "/" + richTextBox1.Parent.Text);
                sw.WriteLine(tabControl1.SelectedTab.GetChildAtPoint(new Point(25, 25)).Text);
                sw.Flush();
                sw.Close();
            }
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                for (int i = 0; i < spaces + 2; i++)
                {
                    SendKeys.Send(" ");
                }
            }
        }

        private void populateFiles(string root)
        {
            foreach (string thisFile in Directory.GetFiles(root + "/css"))
            {
                tabControl1.TabPages.Add(thisFile.Substring(global::Web_Builder.Form1.path.Length));
                editingFiles.AddLast(thisFile);
                RichTextBox rtf = new RichTextBox();
                rtf.Parent = tabControl1.TabPages[tabControl1.TabPages.Count - 1];
                rtf.Dock = System.Windows.Forms.DockStyle.Fill;
                rtf.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                rtf.Location = new System.Drawing.Point(3, 3);
                rtf.Name = "rtf";
                rtf.Size = new System.Drawing.Size(666, 211);
                rtf.TabIndex = 0;
                rtf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
                rtf.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyUp);
                StreamReader sr = new StreamReader(thisFile);
                rtf.Text = sr.ReadToEnd();
                sr.Close();
            }

            foreach (string thisFile in Directory.GetFiles(root + "/js"))
            {
                tabControl1.TabPages.Add(thisFile.Substring(global::Web_Builder.Form1.path.Length));
                editingFiles.AddLast(thisFile);
                SyntaxRichTextBox rtf = new SyntaxRichTextBox();
                rtf.Parent = tabControl1.TabPages[tabControl1.TabPages.Count - 1];
                rtf.Dock = System.Windows.Forms.DockStyle.Fill;
                rtf.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                rtf.Location = new System.Drawing.Point(3, 3);
                rtf.Name = "rtf";
                rtf.Size = new System.Drawing.Size(666, 211);
                rtf.TabIndex = 0;
                rtf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
                rtf.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyUp);
                StreamReader sr = new StreamReader(thisFile);
                rtf.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        public SyntaxRichTextBox rtf;

        private void addFile(string extension)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = global::Web_Builder.Form1.path;
            sfd.Filter = extension;

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                editingFiles.AddLast(sfd.FileName);
                File.Create(sfd.FileName);
                tabControl1.TabPages.Add(sfd.FileName.Substring(global::Web_Builder.Form1.path.Length));
                rtf = new SyntaxRichTextBox();
                rtf.Settings.Comment = "<!--";
                rtf.Settings.CommentColor = Color.Green;
                rtf.Settings.IntegerColor = Color.Blue;
                rtf.Settings.StringColor = Color.Red;
                rtf.Settings.EnableComments = true;
                rtf.Settings.EnableIntegers = true;
                rtf.Settings.EnableStrings = true;
                rtf.Settings.Keywords.Add("DOCTYPE");
                rtf.Settings.Keywords.Add("html");
                rtf.Settings.Keywords.Add("title");
                rtf.Settings.Keywords.Add("style");
                rtf.Settings.Keywords.Add("script");
                rtf.Settings.Keywords.Add("link");
                rtf.Settings.Keywords.Add("body");
                rtf.Settings.Keywords.Add("head");
                rtf.Settings.Keywords.Add("center");
                rtf.Settings.Keywords.Add("h1");
                rtf.Settings.Keywords.Add("h2");
                rtf.Settings.Keywords.Add("h3");
                rtf.Settings.Keywords.Add("h4");
                rtf.Settings.Keywords.Add("h5");
                rtf.Settings.Keywords.Add("h6");
                rtf.Settings.Keywords.Add("h7");
                rtf.Settings.Keywords.Add("p");
                rtf.Settings.Keywords.Add("form");
                rtf.Settings.Keywords.Add("a");
                rtf.Settings.KeywordColor = Color.Blue;
                rtf.ProcessAllLines();
                rtf.Parent = tabControl1.TabPages[tabControl1.TabPages.Count - 1];
                rtf.Dock = System.Windows.Forms.DockStyle.Fill;
                rtf.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                rtf.Location = new System.Drawing.Point(3, 3);
                rtf.Name = "rtf";
                rtf.Size = new System.Drawing.Size(666, 211);
                rtf.TabIndex = 0;
                rtf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
                rtf.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyUp);
                rtf.TextChanged += new System.EventHandler(this.syntaxRichTextBox1_TextChanged);
            }
        }

        private void hTMLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addFile("HTML Files|*.html");
        }

        private void cSSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addFile("CSS Files|*.css");
        }

        private void javascriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addFile("Javascript Files|*.js");
        }

        private void cSSToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            addFile("CSS Files|*.css");
        }

        private void otherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addFile("ALL Files|*.*");
        }

        private void changeFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.Font = fd.Font;
            }
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.BackColor = cd.Color;
            }
        }

        private void textColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.ForeColor = cd.Color;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void syntaxRichTextBox1_TextChanged(object sender, EventArgs e)
        {
            rtf.ProcessAllLines();
        }
    }
}
