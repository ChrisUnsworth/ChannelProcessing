using ChannelProcessing.calculate.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelProcessing.calculate.operators
{
    public class OperatorParser
    {
        private static IBinaryOperator? GetFunction(char value) =>
            value switch
            {
                '+' => new Plus(),
                '-' => new Minus(),
                '/' => new Divide(),
                '*' => new Multiply(),
                _ => null
            };

        public static bool TryParse(BacktrackableStringReader reader, out IBinaryOperator result)
        {
            result = GetFunction(reader.Peek());
            if (result != null)
            {
                reader.Next();
                reader.Commit();
                return true;
            }

            reader.Backtrack();
            return false;
        }

    }
}
