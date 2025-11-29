using FileSignatures;

namespace ECommerceAPI.Services.Files;

public class TextDocument : FileFormat
{
    public TextDocument() : this("text/plain", "txt")
    {}

    public TextDocument(string mediaType, string extension) : base([], mediaType, extension)
    {}
    

    public override bool IsMatch(Stream stream)
    {
        var nul = Convert.ToByte('\0');
        
        // Git's approach is to check the first 8000 bytes for a nul
        // https://stackoverflow.com/questions/57030698/do-i-really-need-to-specify-all-binary-files-in-gitattributes
        for (int i = 0; i < 8000 && i < stream.Length - 1; i++)
            if (stream.ReadByte() == nul)
                return false;
        
        return true;
    }
}