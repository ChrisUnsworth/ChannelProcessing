namespace ChannelProcessing.common
{
    public interface IChannel
    {
        char Id { get; }

        IEnumerable<double> GetValues(IDataSet data);
    }
}
