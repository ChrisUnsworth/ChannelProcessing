namespace ChannelProcessing.calculate.common
{
    public interface IExpressionParser
    {
        bool TryParse(BacktrackableStringReader stringExpression, out IExpression expression);
        bool TryParseUntil(BacktrackableStringReader stringExpression, Func<BacktrackableStringReader, bool> until, out IExpression expression);
    }
}
