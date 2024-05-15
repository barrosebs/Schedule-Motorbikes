using System.Text.RegularExpressions;

namespace SM.Web.Helpers
{
    public static class ToolsHelpers
    {
        public static string RemoveMaskCNPJ(string cnpj)
        {
            string cnpjClear = Regex.Replace(cnpj, "[./-]", "");
            return cnpjClear;
        }
        public static string UploadFile(IFormFile file, string subFolder)
        {
            var folderUploads = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", $"CNH_{subFolder}");

            if (!Directory.Exists(folderUploads))
            {
                Directory.CreateDirectory(folderUploads);
            }

            var pathFile = Path.Combine(folderUploads, file.FileName);

            using (var stream = new FileStream(pathFile, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }
            return pathFile;
        }
    }
}
