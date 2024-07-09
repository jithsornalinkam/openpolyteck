namespace JsonConverterApp.Core.Interfaces
{
    public interface IReadDataHandler
    {
        Task<string> ReadFileAsync(string filePath);
    }
}
