using ChannelProcessing.calculate.common;

namespace ChannelProcessing.calculate.operands
{
    public class DoubleParser : IOperandParser
    {
        public bool TryParse(BacktrackableStringReader reader, out IExpression result)
        {
            result = null;
            if (reader.Peek() != '-' && char.IsDigit(reader.Peek()) == false)
            {
                return false;
            }

            var chars = new List<char>
            {
                reader.Next()
            };

            while (reader.HasNext() &&
                (char.IsDigit(reader.Peek()) || reader.Peek() == '.'))
            {
                chars.Add(reader.Next());
            }

            if (double.TryParse(new string(chars.ToArray()), out var value))
            {
                result = new DoubleOperand(value);
                reader.Commit();
                return true;
            }

            reader.Backtrack();
            result = null;
            return false;
        }
    }
}
