using System.Text.Json;
using System.Collections;

namespace ChannelProcessing.io
{
    public class ConfigReader : IEnumerable<ConfigItem>
    {
        private readonly List<ConfigItem> _channels;

        public ConfigReader(Stream stream) : this(new StreamReader(stream)) { }

        public ConfigReader(string fileName) : this(File.OpenText(fileName)) { }

        public ConfigReader(StreamReader streamReader)
        {
            string json = streamReader.ReadToEnd();
            _channels = JsonSerializer.Deserialize<List<ConfigItem>>(json) ?? new();
        }

        public IEnumerator<ConfigItem> GetEnumerator() => _channels.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Config GetConfig()
        {
            var config = new Config();
            foreach (var item in _channels) 
            { 
                switch (item.itemtype?.ToLowerInvariant())
                {
                    case "scalar_in_file":
                        config.ScalarFileNames.Add(item.filename);
                        break;
                    case "channel_in_file":
                        config.ChannelFileNames.Add(item.filename);
                        break;
                    case "expression":
                        if (item.datatype.ToLowerInvariant() == "scalar")
                        {
                            config.ScalarExpressions.Add((item.id.Value, item.expression));
                        }
                        else
                        {
                            config.ChannelExpressions.Add((item.id.Value, item.expression));
                        }

                        break;
                    case "scalar_out_file":
                        config.ScalarOutFileName = item.filename;
                        break;
                    case "channel_out_file":
                        config.ChannelOutFileName = item.filename;
                        break;
                    case "output":
                        config.OutputIds.AddRange(item.ids);
                        break;
                }
            }

            return config;
        }
    }
}
