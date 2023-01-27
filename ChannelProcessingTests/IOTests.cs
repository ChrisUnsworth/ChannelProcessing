using System.Reflection;
using System.IO;

using Xunit;

using ChannelProcessing.io;
using ChannelProcessing.common;
using System.Linq;

namespace ChannelProcessingTests
{
    public class IOTests
    {
        private const string parameterFilePath = "ChannelProcessingTests.data.parameters.txt";
        private const string channelFilePath = "ChannelProcessingTests.data.channels.txt";

        private Stream GetResourceStream(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream(resourceName);
        }

        [Fact]
        public void ParameterReader_Works()
        {
            using var stream = GetResourceStream(parameterFilePath);
            var paramReader = new ParameterReader(stream);

            Assert.NotNull(paramReader);
            Assert.True(paramReader.TryGetParameterValue('m', out IScalar m));
            Assert.Equal(2.0, m.GetValue(default));
            Assert.True(paramReader.TryGetParameterValue('c', out IScalar c));
            Assert.Equal(0.5, c.GetValue(default));
        }

        [Fact]
        public void ChannelReader_Works()
        {
            using var stream = GetResourceStream(channelFilePath);
            var channelReader = new ChannelReader(stream);

            Assert.NotNull(channelReader);
            Assert.True(channelReader.TryGetChannelData('X', out IChannel X));
            var data = X.GetValues(default).ToArray();
            Assert.Equal(100, data.Length);
            Assert.Equal(0.814723686393179, data[0]);
            Assert.Equal(0.337122644398882, data[^1]);
        }
    }
}