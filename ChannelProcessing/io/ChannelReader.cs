using ChannelProcessing.common;

namespace ChannelProcessing.io
{
    public class ChannelReader
    {
        private readonly Dictionary<char, IChannel> _channels;

        public ChannelReader(Stream stream) : this(new StreamReader(stream)) { }

        public ChannelReader(string fileName) : this(File.OpenText(fileName)) { }

        public ChannelReader(StreamReader streamReader) =>
            _channels = streamReader.ReadAllData().ToDictionary(d => d.id, d => (IChannel)new DataChannel(d.data));

        public bool TryGetChannelData(char id, out IChannel value) => _channels.TryGetValue(id, out value);
    }
}
