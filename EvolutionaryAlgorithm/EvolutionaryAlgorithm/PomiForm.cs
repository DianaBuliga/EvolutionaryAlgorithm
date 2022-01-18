using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvolutionaryAlgorithm
{
    public partial class Pomi : Form
    {
        private int _populationSize; // capacitate livada
        private int _noGenes; // numarul de tipuri de pomi care vor fi in livada
        private double  _motivationRate; // factorul de amplificare = numarul de kilograme cu care va scadea productia
                                         // pentru fiecare pom plantat
        private double crossoverRate;
        private int _minValues; // numarul minim de productie pentru pomi
        private int _maxValues; // numarul maxim de productie pentru pomi
        private double price; // pretul/kg 
        private int _nrThrees; // numarul de pomi initial

        public Pomi()
        {
            InitializeComponent();
        }

        private void Pomi_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
