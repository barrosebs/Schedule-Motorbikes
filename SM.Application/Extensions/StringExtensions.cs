namespace SM.Application.Extensions
{
    public static class StringExtensions
    {
        public static string FirstWord(this string texto)
        {
            return texto.Trim().Substring(0, texto.IndexOf(" "));
        }
    }
}