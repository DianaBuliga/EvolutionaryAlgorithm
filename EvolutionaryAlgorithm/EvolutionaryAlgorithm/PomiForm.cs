using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Equation;

namespace EvolutionaryAlgorithm
{
    public partial class Pomi : Form
    {
        private int _populationSize; // capacitate livada
        private int _maxPopulation; // capacitate livada
        private double  _lowAmount; // numarul de kilograme cu care va scadea productia
                                    // pentru fiecare pom plantat
        private double _amount;
        private double _crossoverRate;
        private double _motivationRate;
        private double _price; // pretul/kg 
        private int _maxGeneration; // numarul de pomi initial

        public Pomi()
        {
            InitializeComponent();
        }


        private void Pomi_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int pop;
            Int32.TryParse(textBox1.Text, out pop);
            _maxPopulation = pop;

            Int32.TryParse(textBox4.Text, out pop);
            _maxGeneration = pop;

            Int32.TryParse(textBox2.Text, out pop);
            _populationSize = pop;

            double par;
            Double.TryParse(textBox3.Text, out par);
            _lowAmount = pop;

            Double.TryParse(textBox7.Text, out par);
            _amount = pop;

            Double.TryParse(textBox8.Text, out par);
            _price = pop;

            Double.TryParse(textBox5.Text, out par);
            _crossoverRate = pop;

            Double.TryParse(textBox6.Text, out par);
            _motivationRate = pop;

            var x = new Equation.Equation(_price, _amount, _lowAmount);
            var newE = new EvolutionaryAlgorithm();

            String sol = newE.Solve(x, _populationSize, _maxGeneration, _crossoverRate, _motivationRate, _maxPopulation);

            richTextBox1.Text = sol;
        }
    }
}
