namespace ChannelProcessing.common
{
    public interface IScalar
    {
        char Id { get; }

        double GetValue(IDataSet data);
    }
}
