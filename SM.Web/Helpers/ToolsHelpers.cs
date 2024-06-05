using System.Text.RegularExpressions;

namespace SM.Web.Helpers
{
    public static class ToolsHelpers
    {
        /// <summary>
        /// Remove mask
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public static string RemoveMaskCNPJ(string cnpj)
        {
            string cnpjClear = Regex.Replace(cnpj, "[./-]", "");
            return cnpjClear;
        }
        /// <summary>
        /// Uplods to files
        /// </summary>
        /// <param name="file"></param>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public static string UploadFile(IFormFile file, string subFolder)
        {
            string extension = Path.GetExtension(file.FileName);
            if (extension != ".png" || extension == ".bmp")
                throw new ApplicationException("Arquivo não é válido! Formato do arquivo deve ser: .png ou .bmp");

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
