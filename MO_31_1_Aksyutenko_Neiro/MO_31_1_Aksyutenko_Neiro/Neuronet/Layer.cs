﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MO_31_1_Aksyutenko_Neiro.Neuronet
{
    abstract class Layer
    {
        protected string layerName;
        string weightPath;
        string weightFile;
        protected int size;                             // Count of neurons on this layer
        protected int prevSize;                         // Count of neurons on previous layer
        protected const double learningRate = 0.060;    // How fast neurons will be learning
        protected const double momentum = 0.050d;       // Inertion moment
        protected double[,] latestWeights;              // 2-dim. array of weights calculated on previous iteration
        protected Neuron[] neurons;

        /* Properties */
        public Neuron[] Neurons { get => neurons; set => neurons = value; }

        /* Setting neuron data througth activating */
        public double[] Data
        {
            set
            {
                for (int i = 0; i < size; i++)
                {
                    Neurons[i].Activator(value);
                }
            }
        }

        /* Methods */
        protected Layer(int size, int prevSize, NeuronType type, string layerName)
        {
            this.size = size;
            this.prevSize = prevSize;
            Neurons = new Neuron[size];
            this.layerName = layerName;
            weightPath = AppDomain.CurrentDomain.BaseDirectory + "memory\\";
            weightFile = weightPath + layerName + "_memory.csv";

            double[,] weights;

            if (File.Exists(weightFile))
            {
                weights = InitializeWeights(MemoryMode.GET, weightFile);
            }
            else
            {
                Directory.CreateDirectory(weightPath);
                weights = InitializeWeights(MemoryMode.INIT, weightFile);
            }

            latestWeights = new double[size, prevSize + 1];
            for (int i = 0; i < size; i++)
            {
                double[] tempWeights = new double[size + 1];
                for (int j = 0; j < prevSize + 1; j++)
                {
                    tempWeights[j] = weights[i, j];
                }
                Neurons[i] = new Neuron(tempWeights, type);
            }
        }

        public double[,] InitializeWeights(MemoryMode memoryMode, string path)
        {
            char[] delim = new char[] { ';', ' ' };
            string buffer;
            string[] tempStrWeights;
            double[,] weights = new double[size, prevSize + 1];

            switch (memoryMode)
            {
                case MemoryMode.GET:
                    tempStrWeights = File.ReadAllLines(path);
                    string[] memoryElement;
                    for (int i = 0; i < size; i++)
                    {
                        memoryElement = tempStrWeights[i].Split(delim);
                        for (int j = 0; j < prevSize + 1; j++)
                        {
                            weights[i, j] = double.Parse(memoryElement[j].Replace(',', '.'),
                                System.Globalization.CultureInfo.InvariantCulture);
                        }
                    }
                    break;

                case MemoryMode.SET:
                    tempStrWeights = new string[size];

                    if (!File.Exists(path))
                    {
                        MessageBox.Show("Weight file not found!");
                    }

                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < prevSize + 1; j++)
                        {
                            tempStrWeights[i] += weights[i, j].ToString() + "; ";
                        }
                    }

                    File.WriteAllLines(path, tempStrWeights);
                    break;

                case MemoryMode.INIT:
                    tempStrWeights = new string[size];
                    Random random = new Random();

                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < prevSize + 1; j++)
                        {
                            double randomNumber = random.NextDouble();
                            weights[i, j] = randomNumber * 0.98 + 0.01;
                            tempStrWeights[i] += weights[i, j].ToString() + "; ";
                        }
                    }

                    File.WriteAllLines(path, tempStrWeights);
                    break;
            }

            return weights;
        }
    }
}
