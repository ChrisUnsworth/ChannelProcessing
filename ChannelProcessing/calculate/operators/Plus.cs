using ChannelProcessing.calculate.common;

namespace ChannelProcessing.calculate.operators
{
    public class Plus : IBinaryOperator
    {
        public int Precedence => 2;

        public double Evaluate(double lhs, double rhs) => lhs + rhs;
    }
}
