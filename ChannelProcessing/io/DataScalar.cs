using ChannelProcessing.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelProcessing.io
{
    public readonly struct DataScalar : IScalar
    {
        private readonly char _id;
        private readonly double _value;

        public DataScalar(char id, double value) => (_id, _value) = (id, value);

        public char Id => _id;

        public double Value => _value;

        public double GetValue(IDataSet _) => _value;
    }
}
