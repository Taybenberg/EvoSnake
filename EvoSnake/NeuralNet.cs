namespace EvoSnake
{
    public class NeuralNet
    {
        private double[][][] layers;

        public NeuralNet()
        {
            layers = new double[Settings.hiddenLayersSize.Length + 1][][];

            for (int i = 0; i < Settings.hiddenLayersSize.Length + 1; ++i)
            {
                layers[i] = new double[Settings.getLayerSize(i + 1)][];

                for (int j = 0; j < Settings.getLayerSize(i + 1); ++j)
                {
                    layers[i][j] = new double[Settings.getLayerSize(i)];

                    for (int z = 0; z < Settings.getLayerSize(i); ++z)
                        layers[i][j][z] = Settings.random();
                }
            }
        }

        public NeuralNet(NeuralNet parent)
        {
            layers = new double[Settings.hiddenLayersSize.Length + 1][][];

            for (int i = 0; i < Settings.hiddenLayersSize.Length + 1; ++i)
            {
                layers[i] = new double[Settings.getLayerSize(i + 1)][];

                for (int j = 0; j < Settings.getLayerSize(i + 1); ++j)
                {
                    layers[i][j] = new double[Settings.getLayerSize(i)];

                    for (int z = 0; z < Settings.getLayerSize(i); ++z)
                        layers[i][j][z] = parent.layers[i][j][z];
                }
            }
        }

        public NeuralNet(NeuralNet parent1, NeuralNet parent2)
        {
            int
                a = Settings.R.Next(0, Settings.hiddenLayersSize.Length + 1),
                b = Settings.R.Next(0, Settings.getLayerSize(a + 1)),
                c = Settings.R.Next(0, Settings.getLayerSize(a));

            layers = new double[Settings.hiddenLayersSize.Length + 1][][];

            for (int i = 0; i < Settings.hiddenLayersSize.Length + 1; ++i)
            {
                layers[i] = new double[Settings.getLayerSize(i + 1)][];

                for (int j = 0; j < Settings.getLayerSize(i + 1); ++j)
                {
                    layers[i][j] = new double[Settings.getLayerSize(i)];

                    for (int z = 0; z < Settings.getLayerSize(i); ++z)
                    {
                        if (i < a && j < b && z < c)
                            layers[i][j][z] = parent1.layers[i][j][z];
                        else
                            layers[i][j][z] = parent2.layers[i][j][z];
                    }
                }
            }
        }

        ~NeuralNet()
        {
            for (int i = 0; i < Settings.hiddenLayersSize.Length + 1; ++i)
            {
                for (int j = 0; j < Settings.getLayerSize(i + 1); ++j)
                    layers[i][j] = null;

                layers[i] = null;
            }

            layers = null;
        }

        public void mutate()
        {
            for (int i = 0; i < Settings.hiddenLayersSize.Length + 1; ++i)
                for (int j = 0; j < Settings.getLayerSize(i + 1); ++j)
                    for (int z = 0; z < Settings.getLayerSize(i); ++z)
                        if (Settings.R.NextDouble() <= Settings.mutationRate)
                            layers[i][j][z] += Settings.mutationFunction();
        }

        public double[] output(double[] input)
        {
            double[] outLayer, inLayer = new double[Settings.getLayerSize(0)];
            double sum;

            input.CopyTo(inLayer, 0);

            for (int i = 0; i < Settings.hiddenLayersSize.Length + 1; ++i)
            {
                outLayer = new double[Settings.getLayerSize(i + 1)];

                for (int j = 0; j < Settings.getLayerSize(i + 1); ++j)
                {
                    sum = Settings.bias;

                    for (int z = 0; z < Settings.getLayerSize(i); ++z)
                        sum += inLayer[z] * layers[i][j][z];

                    outLayer[j] = Settings.activationFunction(sum);
                }

                inLayer = new double[Settings.getLayerSize(i + 1)];

                outLayer.CopyTo(inLayer, 0);
            }

            return inLayer;
        }
    }
}