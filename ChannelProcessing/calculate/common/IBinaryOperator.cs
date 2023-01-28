namespace ChannelProcessing.calculate.common
{
    public interface IBinaryOperator
    {
        int Precedence { get; }

        double Evaluate(double lhs, double rhs);
    }
}
