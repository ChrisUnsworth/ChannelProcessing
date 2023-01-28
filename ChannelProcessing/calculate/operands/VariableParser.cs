using ChannelProcessing.calculate.common;

namespace ChannelProcessing.calculate.operands
{
    public class VariableParser : IOperandParser
    {
        public bool TryParse(BacktrackableStringReader reader, out IExpression result)
        {
            if (char.IsLetter(reader.Peek()) == false)
            {
                result = null;
                return false;
            }

            var id = reader.Next();

            result = char.IsLower(id)
                ? new ScalarVariable(id)
                : new ChannelVariable(id);
            reader.Commit();
            return true;
        }
    }
}
