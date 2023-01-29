using ChannelProcessing.common;

namespace ChannelProcessing.io
{
    public readonly struct DataScalar : IScalar
    {
        private readonly double _value;

        public DataScalar(double value) => _value = value;

        public double GetValue(IDataSet _) => _value;
    }
}
