using ChannelProcessing.calculate.common;
using ChannelProcessing.common;

namespace ChannelProcessing.calculate.operands
{
    public class ChannelVariable : IExpression, IChannel
    {
        private readonly char _id;

        public ChannelVariable(char id) => _id = id;

        public bool IsScalar => false;

        public bool IsChannel => true;

        public IChannel AsChannel() => this;

        public IScalar AsScalar() => throw new NotSupportedException();

        public IEnumerable<double> GetValues(IDataSet data) => data.GetChannel(_id).GetValues(data);
    }
}