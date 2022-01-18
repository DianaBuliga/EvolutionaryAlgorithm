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

            Chromosome[] population = new Chromosome[populationSize];
            for (int i = 0; i < population.Length; i++)
            {
                population[i] = p.MakeChromosome();
                p.ComputeFitness(population[i]);
            }

            for (int gen = 0; gen < maxGenerations; gen++)
            {
                Chromosome[] newPopulation = new Chromosome[populationSize];

                for (int i = 1; i < populationSize; i++)
                {

                    // Se aleg 3 cromozomi diferiti alatoriu
                    int new1 = _rand.Next(populationSize);
                    int new2 = _rand.Next(populationSize);
                    int new3 = _rand.Next(populationSize);

                    while (new1 == new2 || new1 == new3 || new2 == new3)
                    {
                        new1 = _rand.Next(populationSize);
                        new2 = _rand.Next(populationSize);
                        new3 = _rand.Next(populationSize);
                    }

                    Chromosome newChromosome1 = population[new1];
                    Chromosome newChromosome2 = population[new2];
                    Chromosome newChromosome3 = population[new3];

                    Chromosome potentialChromosome = new Chromosome(newChromosome3);
                    int divisionPoint = _rand.Next(populationSize);
                    for (int gene = 0; gene < potentialChromosome.NoGenes; ++gene)
                    {
                        if(gene == divisionPoint || _rand.NextDouble() < crossoverRate)
                        {
                            potentialChromosome.Genes[gene] = newChromosome3.Genes[gene] + motivationRate * (newChromosome1.Genes[gene] - newChromosome2.Genes[gene]);
                        }
                        else
                        {
                            potentialChromosome.Genes[gene] = population[i].Genes[i];
                        }
                    }

                    p.ComputeFitness(potentialChromosome);
                    if(potentialChromosome.Fitness >= population[i].Fitness)
                    {
                        newPopulation[i] = potentialChromosome;
                    }
                    else
                    {
                        newPopulation[i] = population[i];
                    }

                    for (int k = 0; k < populationSize; k++)
                        population[k] = newPopulation[k];

                }

                for (int i = 0; i < populationSize; i++)
                    population[i] = newPopulation[i];
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
        int _minVal;
        int _maxVal;
        public Chromosome MakeChromosome()
        {
            // un cromozom are o gena (x) care poate lua valori in intervalul (minVal, maxVal)
            return new Chromosome(1, new double[] { _minVal }, new double[] { _maxVal });
        }

        public void ComputeFitness(Chromosome c)
        {
            throw new Exception("Aceasta metoda trebuie completata");

            double x = c.Genes[0];
            // c.Fitness = functia care va fi maximizata
        }
    }
}

