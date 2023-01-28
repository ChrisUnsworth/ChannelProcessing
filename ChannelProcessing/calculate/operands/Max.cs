namespace ChannelProcessing.calculate.operands
{
    internal class Max : IAggregationFunction
    {
        public double Aggregate(IEnumerable<double> values) => values.Max();
    }
}