using ChannelProcessing.calculate;
using ChannelProcessing.common;
using ChannelProcessing.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace ChannelProcessingTests
{
    public class ExpressionTests
    {
        private class TestDataSet : IDataSet
        {
            private readonly List<(char id, IScalar scalar)> _scalars;
            private readonly List<(char id, IChannel channel)> _channels;

            internal TestDataSet(
                List<(char id, double scalar)> scalars,
                List<(char id, double[] channel)> channels)
            {
                _scalars = scalars.Select(s => (s.id, (IScalar)new DataScalar(s.scalar))).ToList();
                _channels = channels.Select(c => (c.id, (IChannel)new DataChannel(c.channel))).ToList();
            }

            public IChannel GetChannel(char id) => _channels.First(c => c.id == id).channel;

            public IScalar GetScalar(char id) => _scalars.First(s => s.id == id).scalar;
        }

        [Fact]
        public void Plus()
        {
            var data = new TestDataSet(new() { }, new() { });
            var scalar = ExpressionParser.ParseScalarExpression("1 + 1");
            Assert.Equal(2, scalar.GetValue(data));
        }

        [Fact]
        public void Minus()
        {
            var data = new TestDataSet(new() { }, new() { });
            var scalar = ExpressionParser.ParseScalarExpression("2 - 1");
            Assert.Equal(1, scalar.GetValue(data));
        }

        [Fact]
        public void Divide()
        {
            var data = new TestDataSet(new() { }, new() { });
            var scalar = ExpressionParser.ParseScalarExpression("4 / 2");
            Assert.Equal(2, scalar.GetValue(data));
        }

        [Fact]
        public void Multiply()
        {
            var data = new TestDataSet(new() { }, new() { });
            var scalar = ExpressionParser.ParseScalarExpression("4 * 2");
            Assert.Equal(8, scalar.GetValue(data));
        }

        [Fact]
        public void Precedence()
        {
            var data = new TestDataSet(new() { }, new() { });
            var scalar = ExpressionParser.ParseScalarExpression("4 + 1 * 2");
            Assert.Equal(6, scalar.GetValue(data));
        }

        [Fact]
        public void Brackets()
        {
            var data = new TestDataSet(new() { }, new() { });
            var scalar = ExpressionParser.ParseScalarExpression("(4 + 1) * 2");
            Assert.Equal(10, scalar.GetValue(data));
        }

        [Fact]
        public void PlusScalar()
        {
            var data = new TestDataSet(new() { ('x', 2) }, new() { });
            var scalar = ExpressionParser.ParseScalarExpression("1 + x");
            Assert.Equal(3, scalar.GetValue(data));
        }

        [Fact]
        public void ScalarPlusScalar()
        {
            var data = new TestDataSet(new() { ('x', 2), ('y', 3) }, new() { });
            var scalar = ExpressionParser.ParseScalarExpression("y + x");
            Assert.Equal(5, scalar.GetValue(data));
        }

        [Fact]
        public void PlusChannel()
        {
            var data = new TestDataSet(new() { }, new() { ('X', new double[] { 1, 2, 3 }) });
            var channel = ExpressionParser.ParseChannelExpression("1 + X");
            Assert.Equal(new double[] { 2, 3, 4 }, channel.GetValues(data).ToArray());
        }

        [Fact]
        public void ScalarPlusChannel()
        {
            var data = new TestDataSet(new() { ('x', 2) }, new() { ('X', new double[] { 1, 2, 3 }) });
            var channel = ExpressionParser.ParseChannelExpression("x + X");
            Assert.Equal(new double[] { 3, 4, 5 }, channel.GetValues(data).ToArray());
        }

        [Fact]
        public void ChannelPlusChannel()
        {
            var data = new TestDataSet(new() { }, new() { ('X', new double[] { 1, 2, 3 }), ('Y', new double[] { 3, 4, 5 }) });
            var channel = ExpressionParser.ParseChannelExpression("Y + X");
            Assert.Equal(new double[] { 4, 6, 8 }, channel.GetValues(data).ToArray());
        }

        [Fact]
        public void Max()
        {
            var data = new TestDataSet(new() { }, new() { ('X', new double[] { 1, 2, 3 }), ('Y', new double[] { 3, 4, 5 }) });
            var scalar = ExpressionParser.ParseScalarExpression("MAX(Y + X)");
            Assert.Equal(8, scalar.GetValue(data));
        }

        [Fact]
        public void Min()
        {
            var data = new TestDataSet(new() { }, new() { ('X', new double[] { 1, 2, 3 }), ('Y', new double[] { 3, 4, 5 }) });
            var scalar = ExpressionParser.ParseScalarExpression("min(Y + X)");
            Assert.Equal(4, scalar.GetValue(data));
        }

        [Fact]
        public void Mean()
        {
            var data = new TestDataSet(new() { }, new() { ('X', new double[] { 1, 2, 3 }), ('Y', new double[] { 3, 4, 5 }) });
            var scalar = ExpressionParser.ParseScalarExpression("Mean(Y + X)");
            Assert.Equal(6, scalar.GetValue(data));
        }

        [Fact]
        public void Sum()
        {
            var data = new TestDataSet(new() { }, new() { ('X', new double[] { 1, 2, 3 }), ('Y', new double[] { 3, 4, 5 }) });
            var scalar = ExpressionParser.ParseScalarExpression("Sum(Y + X)");
            Assert.Equal(18, scalar.GetValue(data));
        }

    }
}
