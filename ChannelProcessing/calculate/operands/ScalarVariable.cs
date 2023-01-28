using ChannelProcessing.calculate.common;
using ChannelProcessing.common;

namespace ChannelProcessing.calculate.operands
{
    public class ScalarVariable : IExpression, IScalar
    {
        private readonly char _id;

        public ScalarVariable(char id) => _id = id;

        public bool IsScalar => true;

        public bool IsChannel => false;

        public IChannel AsChannel() => throw new NotSupportedException();

        public IScalar AsScalar() => this;

        public double GetValue(IDataSet data) => data.GetScalar(_id).GetValue(data);
    }
}