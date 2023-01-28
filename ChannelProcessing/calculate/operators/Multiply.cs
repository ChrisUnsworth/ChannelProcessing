using ChannelProcessing.calculate.common;

namespace ChannelProcessing.calculate.operators
{
    public class Multiply : IBinaryOperator
    {
        public int Precedence => 3;

        public double Evaluate(double lhs, double rhs) => lhs * rhs;
    }
}
