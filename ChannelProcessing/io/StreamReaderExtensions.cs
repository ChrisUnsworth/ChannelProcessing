namespace ChannelProcessing.io
{
    public static class StreamReaderExtensions
    {
        public static IEnumerable<(char id, double[] data)> ReadAllData(this StreamReader reader)
        {
            var line = reader.ReadLine();
            while (line != null)
            {
                yield return ParseLine(line);
                line = reader.ReadLine();
            }
        }

        private static (char id, double[]) ParseLine(string line) =>
            (line[0], line[3..].Split(',').Select(s => double.Parse(s.Trim())).ToArray());
    }
}
