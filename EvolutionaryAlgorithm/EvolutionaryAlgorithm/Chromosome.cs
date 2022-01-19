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
    /// Clasa care reprezinta un individ din populatie
    /// </summary>
    public class Chromosome
    {
        public int NoGenes { get; set; } // numarul de gene ale individului

        public double[] Genes { get; set; } // valorile genelor

        public double price { get; set; } // pretul 

        public double amount { get; set; } // cantitate

        public double lowAmount { get; set; } // nr de kilograme care scad dupa plantare

        public double Fitness { get; set; } // valoarea functiei de adaptare a individului

        private static Random _rand = new Random();

       public Chromosome(int noGenes, double price, double amount, double lowAmount)
        {
            NoGenes = noGenes;
            Genes = new double[noGenes];
            Genes[0] = _rand.NextDouble() * price;
            Genes[1] = _rand.NextDouble() * amount; 
            Genes[2] = _rand.NextDouble() * lowAmount; 
        }

        public Chromosome(Chromosome c) // constructor de copiere
        {
            NoGenes = c.NoGenes;
            Fitness = c.Fitness;

            Genes = new double[c.NoGenes];
            price = c.price;
            amount = c.amount;
            lowAmount = c.lowAmount;

        }
    }
}
