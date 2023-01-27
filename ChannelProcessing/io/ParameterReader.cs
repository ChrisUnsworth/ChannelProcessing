using ChannelProcessing.common;

namespace ChannelProcessing.io
{
    public class ParameterReader
    {
        private readonly Dictionary<char, IScalar> _parameters;

        public ParameterReader(Stream stream) : this(new StreamReader(stream)) { }

        public ParameterReader(string fileName) : this(File.OpenText(fileName)) { }

        public ParameterReader(StreamReader streamReader) => 
            _parameters = streamReader.ReadAllData().ToDictionary(d => d.id, d => (IScalar)new DataScalar(d.id, d.data[0]));

        public bool TryGetParameterValue(char id, out IScalar value) => _parameters.TryGetValue(id, out value);
    }
}
