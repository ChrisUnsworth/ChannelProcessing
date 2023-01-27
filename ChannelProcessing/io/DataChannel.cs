using System.Collections;

using ChannelProcessing.common;

namespace ChannelProcessing.io
{
    public readonly struct DataChannel : IChannel
    {
        private readonly char _id;
        private readonly double[] _data;

        public DataChannel(char id, double[] data) => (_id, _data) = (id, data);

        public char Id => _id;

        public IEnumerable<double> GetValues(IDataSet data) => _data;
    }
}
