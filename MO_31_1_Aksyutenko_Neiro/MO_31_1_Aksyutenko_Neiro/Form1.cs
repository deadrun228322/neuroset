﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MO_31_1_Aksyutenko_Neiro
{
    public partial class Form1 : Form
    {
        private double[] inputPixels;
        //private NumericUpDown actualNumberChoice;
        //private Network network;

        public Form1()
        {
            InitializeComponent();
            inputPixels = new double[15];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void ChangeStateOnClick(object sender, EventArgs e)
        {
            
            if (((Button)sender).BackColor == Color.White)
            {
                ((Button)sender).BackColor = Color.Black;
                inputPixels[((Button)sender).TabIndex] = 1d;
            }

            
            else
            {
                ((Button)sender).BackColor = Color.White;
                inputPixels[((Button)sender).TabIndex] = 0d;
            }
        }

        private string PrepareNumberInfo()
        {
            string buffer = numericUpDown1.Value.ToString();

            for (int i = 0; i < inputPixels.Length; i++)
            {
                buffer += " " + inputPixels[i].ToString();
            }
            buffer += "\n";
            return buffer;
        }

        private void SaveTrainOnClick(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "train.txt";
            string buffer = PrepareNumberInfo();
            File.AppendAllText(path, buffer);
        }

        private void SaveTestOnClick(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "test.txt";
            string buffer = PrepareNumberInfo();
            File.AppendAllText(path, buffer);
        }

        
    }
}
