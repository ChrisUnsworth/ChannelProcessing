using ChannelProcessing.common;

namespace ChannelProcessing.io
{
    public static class Writer
    {
        public static void Write(IDataSet data, Config config)
        {
            if (!string.IsNullOrWhiteSpace(config.ScalarOutFileName))
            {
                using var file = File.CreateText(config.ScalarOutFileName);
                foreach (var id in config.OutputIds.Where(i => char.IsLower(i)))
                {
                    var value = data.GetScalar(id).GetValue(data);
                    file.WriteLine($"{id}, {value}");
                }

                file.Flush();
                file.Close();
            }

            if (!string.IsNullOrWhiteSpace(config.ChannelOutFileName))
            {
                using var file = File.CreateText(config.ChannelOutFileName);
                foreach (var id in config.OutputIds.Where(i => char.IsUpper(i)))
                {
                    var values = data.GetChannel(id).GetValues(data);
                    file.WriteLine($"{id}, {string.Join(", ", values)}");
                }

                file.Flush();
                file.Close();
            }
        }
    }
}
