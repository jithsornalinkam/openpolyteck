namespace JsonConverterApp
{
    public class ConverterAppHelper
    {
       
        public static string URI = "https://raw.githubusercontent.com/openpolytechnic/dotnet-developer-evaluation/main/xml-api/";

        public static string GetPath(params string[] values)
        {
            return Path.Combine(string.Join("\\", values));
        }
    }
}
