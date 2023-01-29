using ChannelProcessing.calculate;
using ChannelProcessing.common;

namespace ChannelProcessing.io
{
    public class DataSet : IDataSet
    {
        private readonly Dictionary<char, IScalar> _scalars;
        private readonly Dictionary<char, IChannel> _channels;

        public DataSet() 
        {
            _scalars = new();
            _channels = new();
        }

        public DataSet(Config config) : this()
        {
            config.ScalarFileNames.ForEach(AddScalarFile);
            config.ChannelFileNames.ForEach(AddChannelFile);
            config.ScalarExpressions.ForEach(s => _scalars[s.id] = ExpressionParser.ParseScalarExpression(s.expression));
            config.ChannelExpressions.ForEach(c => _channels[c.id] = ExpressionParser.ParseChannelExpression(c.expression));
        }

        public IChannel GetChannel(char id) => _channels[id];

        public IScalar GetScalar(char id) => _scalars[id];

        private void AddScalarFile(string fileName)
        {
            var reader = new ParameterReader(fileName);
            reader.GetAll().ForEach(k => _scalars[k.Key] = k.Value);
        }

        private void AddChannelFile(string fileName)
        {
            var reader = new ChannelReader(fileName);
            reader.GetAll().ForEach(k => _channels[k.Key] = k.Value);
        }
    }
}