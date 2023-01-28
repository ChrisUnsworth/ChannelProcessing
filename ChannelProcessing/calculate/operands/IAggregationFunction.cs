namespace ChannelProcessing.calculate.operands
{
    public interface IAggregationFunction
    {
        double Aggregate(IEnumerable<double> values);
    }
}
