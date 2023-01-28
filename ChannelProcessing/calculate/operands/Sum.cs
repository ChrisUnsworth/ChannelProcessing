namespace ChannelProcessing.calculate.operands
{
    public class Sum : IAggregationFunction
    {
        public double Aggregate(IEnumerable<double> values) => values.Sum();
    }
}