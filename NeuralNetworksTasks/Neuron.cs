using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworksTasks
{
    class Neuron
    {
        public List<double> weights;
        bool bias;
        /// <summary>
        /// The constructor recieves number of inputs and boolean indicating existence of the bias & initializes the weights with random numbers
        /// </summary>
        /// <param name="numberOfInputs"></NumberOfInputFeatures>
        /// <param name="b"></Bias>
        public Neuron(int numberOfInputs, bool b)
        {
            bias = b;
            weights = new List<double>();
            for (int i = 0; i < numberOfInputs; i++)
                weights.Add(GetRandomNumber(-1, 1));
            if (bias)
                weights.Add(GetRandomNumber(-1, 1));
        }

        /// <summary>
        /// The fire function acts as a summer (returns the sum of weights * input features)
        /// </summary>
        /// <param name="features"></InputFeatures>
        /// <returns></WeightsXInputFeatures>
        public double fire(List<double> features)
        {
            if (bias)
                features.Add(1);

            double value=0;

            for (int i = 0; i < features.Count; i++)
                value += features[i] * weights[i];
            return value;
        }

        double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
