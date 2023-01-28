namespace ChannelProcessing.calculate.operands
{
    public class Mean : IAggregationFunction
    {
        public double Aggregate(IEnumerable<double> values) => values.Average();
    }
}