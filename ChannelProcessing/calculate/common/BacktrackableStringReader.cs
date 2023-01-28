namespace ChannelProcessing.calculate.common
{
    public class BacktrackableStringReader
    {
        private readonly string _data;
        private int _current = 0;
        private int _lastCommit = 0;

        public BacktrackableStringReader(string data) => _data = data;

        public BacktrackableStringReader Backtrack()
        {
            _current = _lastCommit;
            return this;
        }
        public BacktrackableStringReader Commit()
        {
            _lastCommit = _current;
            return this;
        }

        public bool HasNext() => _data.Length > _current;
        public char Next() => _data[_current++];
        public char Peek() => _data[_current];
        public IEnumerable<char> Remainder() => _data[_current..];

        public BacktrackableStringReader TrimLeft()
        {
            while (HasNext() && char.IsWhiteSpace(Peek()))
            {
                Next();
            }
            return this;
        }
    }
}
