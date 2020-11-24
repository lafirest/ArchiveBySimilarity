using abs.src;
using System;
using System.Collections.Generic;
using System.IO;

namespace abs
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var inputDir = args[0];
                var pattern = args[1];
                var threshold = double.Parse(args[2]);
                var output = args[3];
                Process(inputDir, pattern, threshold, output);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(@"Useage : abs inputDir searchPattern threshold outputDir
                                    threshold : [0, 1]");
            }
        }

        private static void Process(string dir,
                                    string pattern,
                                    double threshold,
                                    string output)
        {
            var files = Directory.GetFiles(dir, pattern, SearchOption.AllDirectories);
            var nodes = new List<FileNode>(files.Length);

            for (var i = 0; i < files.Length; ++i)
            {
                var node = new FileNode(i, files[i]);
                nodes.Add(node);
            }

            for (var i = 0; i < nodes.Count; ++i)
            {
                var node = nodes[i];
                if (node.MergeBy != -1)
                    continue;

                for (var j = i + 1; j < nodes.Count; ++j)
                    node.Compare(nodes[j], threshold);
            }

            if (!Directory.Exists(output))
                Directory.CreateDirectory(output);

            foreach (var node in nodes)
                node.Output(output);
        }
    }
}