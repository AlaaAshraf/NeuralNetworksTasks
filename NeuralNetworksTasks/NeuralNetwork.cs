using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworksTasks
{
    class NeuralNetwork
    {
        //List containing all neurons in the network
        List<List<Neuron>> neurons;
        //IrisData data;
        List<List<double>> class1Data, class2Data;
        bool bias;
        /// <summary>
        /// Builds the Neural network, takes 3 parameters: 1- List containing number of nuerons per layer
        ///                                                2- Number of Input Features
        ///                                                3- Boolean indicating the existence of bias
        /// </summary>
        /// <param name="NetworkStructure"></ListOfNumberOfNeuronsPerLayer>
        /// <param name="numberOfInputFeatures"></NumberOfInputFeatures>
        /// <param name="bias"></BiasBoolean>
        public NeuralNetwork(List<int> NetworkStructure, int numberOfInputFeatures, bool b)
        {
            bias = b;
            neurons = new List<List<Neuron>>();
            for (int i = 0; i < NetworkStructure.Count; i++)
            {
                List<Neuron> layer = new List<Neuron>();
                for (int j = 0; j < NetworkStructure[i]; j++)
                {
                    Neuron n;
                    if (i == 0)
                        n = new Neuron(numberOfInputFeatures, bias);
                    else n = new Neuron(NetworkStructure[i - 1], bias);
                    layer.Add(n);
                }
                neurons.Add(layer);
            }
        }

        /// <summary>
        /// Loads the data set partially (i.e. the intended classes and features are passed to the function)
        /// </summary>
        /// <param name="classes"></ListOfBooleansOfIntendedClasses>
        /// <param name="features"></ListOfBooleansOfIntendedFeatures>
        public void LoadData(List<bool> classes, List<bool> features)
        {
            IrisData data = new IrisData();
            for (int i = 0; i < 50; i++)
            {
                List<double> features1 = new List<double>(), features2 = new List<double>();

                for (int j = 0; j < 4; j++)
                {
                    if (classes[0] && classes[1])
                    {
                        if (features[j])
                        {
                            features1.Add(data.setosa[i][j]);
                            features2.Add(data.versicolor[i][j]);
                        }
                    }
                    else if (classes[1] && classes[2])
                    {
                        if (features[j])
                        {
                            features1.Add(data.versicolor[i][j]);
                            features2.Add(data.virginica[i][j]);
                        }
                    }
                    else
                    {
                        if (features[j])
                        {
                            features1.Add(data.setosa[i][j]);
                            features2.Add(data.virginica[i][j]);
                        }
                    }
                }
                class1Data.Add(features1);
                class2Data.Add(features2);
            }
        }

        public void LMS_Train(int epochs, double eta, double mse_threshold, List<bool> classes, List<bool> features)
        {
            LoadData(classes, features);
            int feature_num = 0;
            for(int i=0; i<features.Count; i++)
            {
                if (features[i])
                    feature_num++;
            }
            if (bias)
                feature_num++;
            for (int i=0; i<epochs; i++)
            {
                double[] error = new double[60];
                int k = 0;
                for (int j=0; j<30; j++)
                {
                    error[k] = (1) - neurons[0][0].fire(class1Data[j]);
                    
                    if (bias)
                        feature_num++;
                    for(int l=0; l<feature_num; l++)
                    {
                        neurons[0][0].weights[l] += eta * class1Data[j][l];
                    }
                    k++;

                    error[k] = (-1) - neurons[0][0].fire(class2Data[j]);
                    for (int l = 0; l < feature_num; l++)
                    {
                        neurons[0][0].weights[l] += eta * class2Data[j][l];
                    }
                    k++;
                }
                double mse = 0;
                for(int j=0; j<60; j++)
                {
                    mse +=error[j] * error[j];
                }
                mse /= 2 * 60;
                if (mse < mse_threshold)
                    break;
            }
        }

        public int LMS_Test(List<double> sample, List<bool>features)
        {
            List<double> features_val = new List<double>();
            for(int i =0; i<features.Count; i++)
            {
                if (features[i])
                    features_val.Add(sample[i]);
            }
            double sum = neurons[0][0].fire(features_val);
            int classified;
            if (sum >= 0)
                classified = 1;
            else classified = -1;

            return classified;
        }
        
    }
}
