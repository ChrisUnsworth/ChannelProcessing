using ChannelProcessing.calculate.common;
using ChannelProcessing.common;

namespace ChannelProcessing.calculate.operands
{
    public class BracketOperand : IExpression
    {
        private readonly IExpression _expression;

        public BracketOperand(IExpression expression) => _expression = expression;

        public bool IsScalar => _expression.IsScalar;

        public bool IsChannel => _expression.IsChannel;

        public IChannel AsChannel() => _expression.AsChannel();

        public IScalar AsScalar() => _expression.AsScalar();
    }
}
