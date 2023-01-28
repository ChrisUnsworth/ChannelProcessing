using ChannelProcessing.calculate.common;
using ChannelProcessing.common;

namespace ChannelProcessing.calculate.operators
{
    public class BinaryExpression : IExpression, IChannel, IScalar
    {
        private readonly IExpression _lhs;
        private readonly IExpression _rhs;
        private readonly IBinaryOperator _binaryOperator;

        public BinaryExpression(IExpression lhs, IBinaryOperator binaryOperator, IExpression rhs)
        {
            _lhs = lhs;
            _rhs = rhs;
            _binaryOperator = binaryOperator;
        }

        public bool IsScalar => _lhs.IsScalar && _rhs.IsScalar;

        public bool IsChannel => !IsScalar;

        public IChannel AsChannel() => this;

        public IScalar AsScalar() => this;

        IEnumerable<double> IChannel.GetValues(IDataSet data) => Calculate(data);

        double IScalar.GetValue(IDataSet data) => Calculate(data).First();

        public IEnumerable<double> Calculate(IDataSet data) =>
            (_lhs.IsScalar, _rhs.IsScalar) switch
            {
                (true, true) => CalculateScalarScalar(data),
                (false, true) => CalculateChannelScalar(data),
                (true, false) => CalculateScalarChannel(data),
                (false, false) => CalculateChannelChannel(data),
            };

        private IEnumerable<double> CalculateChannelChannel(IDataSet data)
        {
            var lhc = _lhs.AsChannel().GetValues(data).GetEnumerator();
            var rhc = _rhs.AsChannel().GetValues(data).GetEnumerator();

            while (lhc.MoveNext() && rhc.MoveNext())
            {
                yield return _binaryOperator.Evaluate(lhc.Current, rhc.Current);
            }
        }

        private IEnumerable<double> CalculateScalarChannel(IDataSet data)
        {
            var lhv = _lhs.AsScalar().GetValue(data);
            foreach (var rhv in _rhs.AsChannel().GetValues(data))
            {
                yield return _binaryOperator.Evaluate(lhv, rhv);
            }
        }

        private IEnumerable<double> CalculateChannelScalar(IDataSet data)
        {
            var rhv = _rhs.AsScalar().GetValue(data);
            foreach (var lhv in _lhs.AsChannel().GetValues(data))
            {
                yield return _binaryOperator.Evaluate(lhv, rhv);
            }
        }

        private IEnumerable<double> CalculateScalarScalar(IDataSet data)
        {
            yield return _binaryOperator.Evaluate(_lhs.AsScalar().GetValue(data), _rhs.AsScalar().GetValue(data));
        }
    }
}
