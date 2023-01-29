using ChannelProcessing.calculate.common;
using ChannelProcessing.common;

namespace ChannelProcessing.calculate.operands
{
    public class AggregationFunction : IExpression, IScalar
    {
        private readonly IChannel _channel;
        private readonly IAggregationFunction _function;

        public AggregationFunction(IChannel channel, IAggregationFunction function)
        {
            _channel = channel;
            _function = function;
        }

        public bool IsScalar => true;

        public bool IsChannel => false;

        public IChannel AsChannel() => throw new NotSupportedException();

        public IScalar AsScalar() => this;

        public double GetValue(IDataSet data) => _function.Aggregate(_channel.GetValues(data));
    }
}
