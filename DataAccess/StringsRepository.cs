public abstract class StringsRepository : IStringsRepository
{

    public List<string> Read(string filepath)
    {
        if (File.Exists(filepath))
        {
            var fileContents = File.ReadAllText(filepath);
            return TextToStrings(fileContents);
        }
        return new List<string>();
    }

    protected abstract List<string> TextToStrings(string fileContents);

    public void Write(string filepath, List<string> strings)
    {
        File.WriteAllText(filepath, StringsToText(strings));
    }

    protected abstract string StringsToText(List<string> strings);
}
