namespace ChannelProcessing.common
{
    public interface IDataSet
    {
        IChannel GetChannel(char id);

        IScalar GetScalar(char id);
    }
}
