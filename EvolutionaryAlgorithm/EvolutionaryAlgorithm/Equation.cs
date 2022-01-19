using EvolutionaryAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equation
{
    public class Equation : IOptimizationProblem
    {

        private double _price;
        private double _amount;
        private double _lowAmount;

        public Equation(double price, double amount, double lowAmount)
        {
            _price = price;
            _amount = amount;
            _lowAmount = lowAmount;
        }

        public Chromosome MakeChromosome()
        {
            return new Chromosome(3, _price, _amount, _lowAmount);
        }

        public void ComputeFitness(Chromosome c)
        {
            double x1 = c.Genes[0]; // pret
            double x2 = c.Genes[1]; // productie
            double x3 = c.Genes[2]; // cat de mult scade productia pentru fiecare copac plantat ulterior

            c.Fitness = 0.65 * x1 + 0.45 * x2 - 0.1 * x3; //valoare pentru fiecare copac
        }
    }
}
