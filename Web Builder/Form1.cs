using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Web_Builder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string path { get; set; }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::Web_Builder.Properties.Resources.add_blue_Clicked;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::Web_Builder.Properties.Resources.add_blue;
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            /*
            foreach (var file in directoryInfo.GetFiles())
                directoryNode.Nodes.Add(new TreeNode(file.Name));
             */
            return directoryNode;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ListDirectory(treeView1, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                timer1.Start();
                pictureBox3.Image = global::Web_Builder.Properties.Resources.xig67ak5T;
                pictureBox3.Visible = true;
            }
        }

        int _timeIndex = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_timeIndex >= 200)
            {
                path = "C:/Users/" + Environment.UserName + "/" + treeView1.SelectedNode.FullPath.ToString();
                startMain(new Web_Builder());
                this.Hide();
                timer1.Stop();
            }
            _timeIndex++;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog sfd = new FolderBrowserDialog();
            sfd.Description = "Please select a folder for the project to be initilized in";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = sfd.SelectedPath;
                createProject(global::Web_Builder.Form1.path);
            }
            startMain(new Web_Builder());
            this.Hide();
        }

        private void createProject(string PATH)
        {
            Directory.CreateDirectory(PATH + "\\css");
            Directory.CreateDirectory(PATH + "\\js");
            Directory.CreateDirectory(PATH + "\\font");
//            File.Create(PATH + "\\index.html");
            using (StreamWriter readme = new StreamWriter(PATH + "\\README.txt"))
            {
                readme.Write("CREATED WITH \'WEB BUILDER\' V[1.0]\n(c)2017\n\nPLEASE LAUNCH \'index.html\' TO RUN");
                readme.Close();
            }
        }

        private void startMain(Form form)
        {
            string text = Environment.UserName.ToString() + " - " + global::Web_Builder.Form1.path.Substring("c:/users/".Length);
            form.Text = text;
            form.Show();
        }
    }
}
