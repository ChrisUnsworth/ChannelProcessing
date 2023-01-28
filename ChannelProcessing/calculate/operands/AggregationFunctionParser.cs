using ChannelProcessing.calculate.common;

namespace ChannelProcessing.calculate.operands
{
    public class AggregationFunctionParser : IOperandParser
    {
        public bool TryParse(BacktrackableStringReader reader, out IExpression result)
        {
            result = null;
            if (char.IsLetter(reader.Peek()) == false) { return false; }

            var name = new List<char>();
            while (reader.HasNext() && char.IsLetter(reader.Peek()))
            {
                name.Add(reader.Next());
            }

            if (reader.HasNext() &&
                reader.Next() == '(' &&
                TryGetFunction(new string(name.ToArray()), out var function))
            {
                var args = new List<IExpression>();
                do
                {
                    if (ExpressionParser.TryParseUntil(reader, r => r.Peek() == ',' || r.Peek() == ')', out IExpression expression))
                    {
                        args.Add(expression);
                    }
                    else
                    {
                        throw new FormatException("Invalid syntax in function.");
                    }
                } while (reader.HasNext() && reader.Next() != ')');

                result = new AggregationFunction(args.First().AsChannel(), function);
                return true;
            }

            reader.Backtrack();
            return false;
        }
        private static bool TryGetFunction(string value, out IAggregationFunction? function)
        {
            function = value.ToLowerInvariant() switch
            {
                "mean" => new Mean(),
                "sum" => new Sum(),
                "min" => new Min(),
                "max" => new Max(),
                _ => null
            };

            return function != null;
        }
    }
}
