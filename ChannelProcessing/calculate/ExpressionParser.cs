using ChannelProcessing.calculate.common;
using ChannelProcessing.calculate.operands;
using ChannelProcessing.calculate.operators;
using ChannelProcessing.common;

namespace ChannelProcessing.calculate
{
    public static class ExpressionParser
    {
        public static IScalar ParseScalarExpression(string expression) => Parse(expression).AsScalar();

        public static IChannel ParseChannelExpression(string expression) => Parse(expression).AsChannel();

        public static IExpression Parse(string stringExpresion)
        {
            if (TryParse(new BacktrackableStringReader(stringExpresion), out IExpression expression))
            {
                return expression;
            }

            throw new FormatException("Unable to parse expression");
        }

        public static bool TryParse(BacktrackableStringReader stringExpresion, out IExpression expression) => 
            TryParseUntil(stringExpresion, r => false, out expression);

        public static bool TryParseUntil(
            BacktrackableStringReader stringExpresion,
            Func<BacktrackableStringReader, bool> until,
            out IExpression expression)
        {
            var operands = new Stack<IExpression>();
            var operators = new Stack<IBinaryOperator>();
            var operandParsers = new IOperandParser[]
            {
                new DoubleParser(),
                new AggregationFunctionParser(),
                new BracketParser(),
                new VariableParser()
            };

            expression = null;
            bool lookingForOperand = true;

            while (stringExpresion.TrimLeft().Commit().HasNext() &&
                   until(stringExpresion) == false)
            {
                if (lookingForOperand)
                {
                    IExpression operand = null;
                    if (operandParsers.Any(p => p.TryParse(stringExpresion, out operand)))
                    {
                        operands.Push(operand);
                        lookingForOperand = false;
                        continue;
                    }
                }

                if (OperatorParser.TryParse(stringExpresion, out IBinaryOperator binaryOperator))
                {
                    lookingForOperand = true;
                    if (operators.Any() == false || operators.Peek().Precedence <= binaryOperator.Precedence)
                    {
                        operators.Push(binaryOperator);
                        continue;
                    }

                    while (operators.Any() && operators.Peek().Precedence > binaryOperator.Precedence)
                    {
                        var right = operands.Pop();
                        var left = operands.Pop();
                        var operation = operators.Pop();
                        operands.Push(new BinaryExpression(left, operation, right));
                    }

                    operators.Push(binaryOperator);
                    continue;
                }

                return false;
            }

            foreach (var operation in operators)
            {
                if (operands.Count < 2) { return false; }
                var right = operands.Pop();
                var left = operands.Pop();
                operands.Push(new BinaryExpression(left, operation, right));
            }

            if (operands.Count != 1)
            {
                return false;
            }

            expression = operands.First();
            return expression != null;
        }
    }
}
