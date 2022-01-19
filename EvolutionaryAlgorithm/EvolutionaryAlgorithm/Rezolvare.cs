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

        public static Chromosome GetBest(Chromosome[] population, int populationSize)
        {
            Chromosome best = population[0];

            for(int i=0; i<populationSize; ++i)
            {
                if (best.Fitness < population[i].Fitness)
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
        private Random _rand = new Random();
        /// <summary>
        /// Metoda de optimizare care gaseste solutia problemei
        /// </summary>
        public String Solve(IOptimizationProblem p, int populationSize, int maxGenerations, double crossoverRate, double motivationRate, int maxPopulation)
        {
            double sumMax = 0;
            String sume ="";
            Chromosome[] population = new Chromosome[maxPopulation];
            for (int i = 0; i < populationSize; i++)
            {
                population[i] = p.MakeChromosome();
                p.ComputeFitness(population[i]);
                Console.WriteLine(population[i].Genes[1]);
            }

            //
            for (int gen = 0; gen < maxGenerations && populationSize<maxPopulation; gen++)
            {
                double sum = 0;

                // se aleg random 3 chromosomi dinpopulatie
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

                // se creaza noul cromozom prin incrucisarea celor 3 cromozomi alesi aleatoriu
                Chromosome potentialChromosome = new Chromosome(newChromosome3);
                int divisionPoint = _rand.Next(0, gen);

                for (int j = 0; j < potentialChromosome.NoGenes; ++j)
                {
                    if (j == divisionPoint || _rand.NextDouble() < crossoverRate)
                    {
                        potentialChromosome.Genes[j] = newChromosome3.Genes[j] + motivationRate * (newChromosome1.Genes[j] - newChromosome2.Genes[j]);
                    }
                    else
                    {
                        potentialChromosome.Genes[j] = newChromosome1.Genes[j];
                    }
                }


                p.ComputeFitness(potentialChromosome);

                // se alege random un cromozome din vechea populatie pentru a compara fitness-ul
                int randCromosome = _rand.Next(populationSize);
               
                // Se recalculeaza cantitatea 
                Chromosome[] newPopulation = recalculateGenes(population, populationSize);

                //se salveaza in vechea populatie, noua populatie cu cantitaea schimbata
                for(int i = 0; i < populationSize; ++i)
                {
                    population[i] = newPopulation[i];
                }

                // se verifica fitness-ul noului individ
                // daca noul individ are fitness-ul mai mare atunci se adauga in populatie
                // daca nu, se adauga inidividul ales random
                if (potentialChromosome.Fitness >= population[randCromosome].Fitness)
                {
                    population[populationSize] = potentialChromosome;
                }
                else
                {
                    population[populationSize] = population[randCromosome];
                }

                // se calculeaza venitul produs de noua populatie
                for (int j = 0; j < gen; j++)
                {
                    sum += population[j].Genes[0] * population[j].Genes[1];
                }
                
                // daca venitul este mai mare decat cel maxim se salveaza noul venit maxim si se mareste populatia cu un individ
                // daca venitul este mai mic, atunci se inchide bucla si se salveaza numarul de pomi 
                if(sum >= sumMax)
                {
                    sumMax = sum;
                    populationSize++;
                    sume = sume + "\n " + sum; 
                }
                else
                {
                   
                    sume = sume + "\n " + sum;
                    break;
                }
            }

            return populationSize+"\n "+sume;
        }


        //functie care ia toti cromozomii si le scade cromozomul
        public Chromosome[] recalculateGenes(Chromosome[] chromosomeList, int popSize)
        {
            Chromosome[] newChr = new Chromosome[popSize];
            for(int i=0; i< popSize; ++i)
            {
                newChr[i] = chromosomeList[i];
                Console.WriteLine(newChr[i].Genes[1]);
            }

            return newChr;
        }

    }


}

