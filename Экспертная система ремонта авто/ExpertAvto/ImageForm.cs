﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpertAvto
{
    public partial class ImageForm : Form
    {
        public ImageForm(string filename)
        {
            InitializeComponent();

            pictureBox1.Image = new Bitmap(filename);
        }

        private void ImageForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
