using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MO_31_1_Aksyutenko_Neiro.Neuronet
{
    enum MemoryMode
    {
        GET,
        SET,
        INIT
    }

    enum NeuronType
    {
        HIDDEN,
        OUTPUT
    }

    enum NetworkMode
    {
        TRAIN,      // learning mode
        TEST,       // testing mode
        DEMO        // recognising mode
    }
}