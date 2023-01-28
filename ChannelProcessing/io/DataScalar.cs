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
        private readonly double _value;

        public DataScalar(double value) => _value = value;

        public double GetValue(IDataSet _) => _value;
    }
}
