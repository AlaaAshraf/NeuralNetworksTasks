using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworksTasks
{
    class IrisData
    {
        public List<List<double>> setosa, versicolor, virginica;
        /// <summary>
        /// Loads the complete dataset and stores it in a list of list of doubles for each class and normalizes it
        /// </summary>
        public IrisData()
        {
            setosa = new List<List<double>>();
            versicolor = new List<List<double>>();
            virginica = new List<List<double>>();
            double[] featuresMax = new double[4];
            double[] featuresMin = new double[4];
            for (int i = 0; i < 4; i++)
            {
                featuresMax[i] = double.MinValue;
                featuresMin[i] = double.MaxValue;
            }
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("Iris Data.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] tokens = line.Split(',');
                if (tokens[0] == "X1")
                    continue;
                List<double> features = new List<double>();
                for (int i = 0; i < 4; i++)
                    features.Add(Convert.ToDouble(tokens[i]));
                if (tokens[4] == "Iris-versicolor")
                    versicolor.Add(features);
                else if (tokens[4] == "Iris-virginica")
                    virginica.Add(features);
                else setosa.Add(features);
                for(int i=0; i<4; i++)
                {
                    if (features[i] > featuresMax[i])
                        featuresMax[i] = features[i];
                    if (features[i] < featuresMin[i])
                        featuresMin[i] = features[i];
                }
            }
            file.Close();

            //Feature scaling between -1,1
            for(int i=0; i<50; i++)
            {
                for(int j=0; j<4; j++)
                {
                    double x = setosa[i][j];
                    x = -1 + ((x - featuresMin[j]) * 2) / (featuresMax[j] - featuresMin[j]);
                    setosa[i][j] = x;
                    x = versicolor[i][j];
                    x = -1 + ((x - featuresMin[j]) * 2) / (featuresMax[j] - featuresMin[j]);
                    versicolor[i][j] = x;
                    x = virginica[i][j];
                    x = -1 + ((x - featuresMin[j]) * 2) / (featuresMax[j] - featuresMin[j]);
                    virginica[i][j] = x;
                }
            }

        }
    }
}
