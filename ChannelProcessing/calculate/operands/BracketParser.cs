using ChannelProcessing.calculate.common;

namespace ChannelProcessing.calculate.operands
{
    public class BracketParser : IOperandParser
    {
        public bool TryParse(BacktrackableStringReader reader, out IExpression result)
        {
            if (reader.Peek() != '(')
            {
                result = null;
                return false;
            }

            reader.Next();

            if (ExpressionParser.TryParseUntil(reader, r => r.Peek() == ')', out IExpression expression) &&
                reader.HasNext() &&
                reader.Next() == ')')
            {
                reader.Commit();
                result = new BracketOperand(expression);
                return true;
            }

            throw new FormatException("Unable to parse bracket expression");
        }
    }
}
