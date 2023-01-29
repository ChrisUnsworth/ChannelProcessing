using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelProcessing.io
{
    public class Config
    {
        public List<string> ScalarFileNames { get; } = new();
        public List<string> ChannelFileNames { get; } = new();
        public List<(char id, string expression)> ScalarExpressions { get; } = new();
        public List<(char id, string expression)> ChannelExpressions { get; } = new();
        public string? ScalarOutFileName { get; set; }
        public string? ChannelOutFileName { get; set; }
        public List<char> OutputIds { get; } = new();
    }
}
