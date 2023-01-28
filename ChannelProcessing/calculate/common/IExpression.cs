using ChannelProcessing.common;

namespace ChannelProcessing.calculate.common
{
    public interface IExpression
    {
        bool IsScalar { get; }

        bool IsChannel { get; }

        IScalar AsScalar();

        IChannel AsChannel();
    }
}
