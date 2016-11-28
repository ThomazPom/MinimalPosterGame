using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if(mesImages.Count>0)
            imgBox.Image = mesImages.ElementAt(index=(index+1)%(mesImages.Count-1));
        }

        List<Image> mesImages = new List<Image>();
       int index = 0;
        string filepath;
        private void buttonDir_Click(object sender, EventArgs e)
        {
            mesImages = new List<Image>();
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {

                    imgBox.Image = Properties.Resources._01_progress;
                    filepath = folderDialog.SelectedPath;

                    Thread t = new Thread(new ThreadStart(DoWork));
                    t.Start();


                 }
            }
        }
        private void DoWork()
        {

            DirectoryInfo d = new DirectoryInfo(filepath);

            foreach (var file in d.GetFiles("*.jpg"))
            {
                mesImages.Add(Image.FromFile(file.FullName));
            }

            imgBox.Image = mesImages.ElementAt(index);
        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            if(mesImages.Count>0)
            imgBox.Image = mesImages.ElementAt(index = (index - 1)>0?index-1:0);
        }

        private void checkBoxHide_CheckedChanged(object sender, EventArgs e)
        {
            panelTitleHide.Visible = checkBoxHide.Checked;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            int height = (int)(Height * 0.17);
            int top = (int)(Height * 0.07);
            panelTitleHide.Size = new Size( Width, height);
            panelTitleHide.Location = new Point(0, top);
        }

        Random r = new Random();
        private void buttonRandom_Click(object sender, EventArgs e)
        {

            if (mesImages.Count > 0)
                imgBox.Image = mesImages.ElementAt(r.Next(0,mesImages.Count-1));
        }
    }
}
