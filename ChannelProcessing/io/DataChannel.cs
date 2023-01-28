using ChannelProcessing.common;

namespace ChannelProcessing.io
{
    public readonly struct DataChannel : IChannel
    {
        private readonly double[] _data;

        public DataChannel(double[] data) => _data = data;

        public IEnumerable<double> GetValues(IDataSet data) => _data;
    }
}
