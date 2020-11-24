using System.IO;

namespace abs.src
{
    public class FileNode
    {
        public readonly int Id;
        public readonly string Name;
        public readonly string Content;
        public int MergeBy { get; private set; }

        public FileNode(int id, string path)
        {
            Id = id;
            Name = Path.GetFileName(path);
            Content = File.ReadAllText(path);
            MergeBy = -1;
        }

        public void Compare(FileNode other, double threshold)
        {
            if (other.MergeBy != -1)
                return;

            if (DiceCoefficient.Calc(Content, other.Content) >= threshold)
                other.MergeBy = Id;
        }

        public void Output(string path)
        {
            var outDir = path;
            if (MergeBy == -1)
                outDir = $"{outDir}/{Id}/";
            else
                outDir = $"{outDir}/{MergeBy}";

            if (!Directory.Exists(outDir))
                Directory.CreateDirectory(outDir);

            var fullPath = $"{outDir}/{Name}";

            File.WriteAllText(fullPath, Content);
        }
    }
}