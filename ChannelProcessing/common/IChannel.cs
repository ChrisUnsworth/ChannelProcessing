namespace ChannelProcessing.common
{
    public interface IChannel
    {
        IEnumerable<double> GetValues(IDataSet data);
    }
}
