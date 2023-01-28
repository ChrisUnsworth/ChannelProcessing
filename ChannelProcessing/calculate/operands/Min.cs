namespace ChannelProcessing.calculate.operands
{
    public class Min : IAggregationFunction
    {
        public double Aggregate(IEnumerable<double> values) => values.Min();
    }
}