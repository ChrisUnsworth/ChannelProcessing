using ChannelProcessing.calculate.common;
using ChannelProcessing.common;

namespace ChannelProcessing.calculate.operands
{
    public class DoubleOperand : IExpression, IScalar
    {
        private readonly double _value;

        public DoubleOperand(double value) => _value = value;

        public bool IsScalar => true;

        public bool IsChannel => false;

        public IChannel AsChannel() => throw new NotSupportedException();

        public IScalar AsScalar() => this;

        public double GetValue(IDataSet data) => _value;
    }
}
