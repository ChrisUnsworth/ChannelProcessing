// See https://aka.ms/new-console-template for more information

using ChannelProcessing.io;

string configFileName;

if (args.Length == 0)
{
    configFileName = "default_config.json";
}
else
{
    configFileName = args[0];
}

Console.WriteLine($"Reading config from: {configFileName}");
var config = new ConfigReader(configFileName).GetConfig();
Console.WriteLine($"Creating Data set");
var dataSet = new DataSet(config);
Console.WriteLine($"Writing Results");
Writer.Write(dataSet, config);
Console.WriteLine($"Done");

