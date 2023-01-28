namespace ChannelProcessing.calculate.common
{
    public interface IOperandParser
    {
        bool TryParse(BacktrackableStringReader reader, out IExpression result);
    }
}
