/**************************************************************************
 *                                                                        *
 *  Copyright:   (c) 2016-2020, Florin Leon                               *
 *  E-mail:      florin.leon@academic.tuiasi.ro                           *
 *  Website:     http://florinleon.byethost24.com/lab_ia.html             *
 *  Description: Evolutionary Algorithms                                  *
 *               (Artificial Intelligence lab 8)                          *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using System;

namespace EvolutionaryAlgorithm
{
    /// <summary>
    /// Clasa care reprezinta operatia de selectie
    /// </summary>
    public class Selection
    {

        public static Chromosome GetBest(Chromosome[] population)
        {
            Chromosome best = population[0];

            for(int i=0; i<population.Length; ++i)
            {
                if (best.Fitness > population[i].Fitness)
                    best = population[i];
            }

            return best;

        }
    }

    //==================================================================================

    /// <summary>
    /// Clasa care implementeaza algoritmul de evolutie diferentiala
    /// </summary>
    public class EvolutionaryAlgorithm
    {
        private static Random _rand = new Random();
        /// <summary>
        /// Metoda de optimizare care gaseste solutia problemei
        /// </summary>
        public Chromosome Solve(IOptimizationProblem p, int populationSize, int maxGenerations, double crossoverRate, double motivationRate)
        {

            Chromosome[] population = new Chromosome[maxGenerations];
            for (int i = 0; i < populationSize; i++)
            {
                population[i] = p.MakeChromosome();
                p.ComputeFitness(population[i]);
            }

            //gen = nr de copaci care se afla in loivada la momentul t
            for (int gen = populationSize; gen < maxGenerations; gen++)
            {
                Chromosome[] newPopulation = new Chromosome[gen+1];

                int new1 = _rand.Next(gen);
                int new2 = _rand.Next(gen);
                int new3 = _rand.Next(gen);

                while (new1 == new2 || new1 == new3 || new2 == new3)
                {
                    new1 = _rand.Next(gen);
                    new2 = _rand.Next(gen);
                    new3 = _rand.Next(gen);
                }

                Chromosome newChromosome1 = population[new1];
                Chromosome newChromosome2 = population[new2];
                Chromosome newChromosome3 = population[new3];

                Chromosome potentialChromosome = new Chromosome(newChromosome3);
                int divisionPoint = _rand.Next(0, gen);
                //gene = gena
                for (int j = 0; j < potentialChromosome.NoGenes; ++j)
                {
                    if (j == divisionPoint || _rand.NextDouble() < crossoverRate)
                    {
                        potentialChromosome.Genes[j] = newChromosome3.Genes[j] + motivationRate * (newChromosome1.Genes[j] - newChromosome2.Genes[j]);
                    }
                    else
                    {
                        potentialChromosome.Genes[j] = population[gen].Genes[j];
                    }
                }

                p.ComputeFitness(potentialChromosome);
                int randCromosome = _rand.Next(gen);
                for(int k=0; k<gen; ++k)
                {
                    newPopulation[k] = population[k];
                }

                if (potentialChromosome.Fitness >= population[randCromosome].Fitness)
                {
                    newPopulation[gen] = potentialChromosome;
                }
                else
                {
                    newPopulation[gen] = population[randCromosome];
                }

                population[gen+1] = newPopulation[gen+1];
            }

            return Selection.GetBest(population);
        }
    }

    //==================================================================================

    /// <summary>
    /// Clasa care reprezinta rezolvarea ecuatiei
    /// </summary>
    public class Equation : IOptimizationProblem
    {

        private double _price;
        private int _amount;
        private double _lowAmount;

        public Equation(double price, int amount, double lowAmount)
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

            c.Fitness = 0.4 * x1 + 0.4 * x2 + 0.2 * x3; //valoare pentru fiecare copac

        }
    }
}

