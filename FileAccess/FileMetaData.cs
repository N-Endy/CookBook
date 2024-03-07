public class FileMetaData
{
    public string FileName { get;}
    public FileFormat Format { get;}

    public FileMetaData(string fileName, FileFormat format)
    {
        FileName = fileName;
        Format = format;
    }

    public string ToPath() => $"{FileName}.{Format.AsFileExtension()}";
}
