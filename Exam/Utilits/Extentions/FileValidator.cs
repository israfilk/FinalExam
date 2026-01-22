namespace Exam.Utilits.Extentions
{

    public static class FileValidator
    {

        public static bool CheckFileType(this IFormFile file, string type)
        {
            if (file.ContentType.Contains(type))
            {
                return true;
            }



            return false;
        }

        public static async Task<string> CreateFileAsync(this IFormFile file, params string[] roots)
        {
            string filename = string.Concat(Guid.NewGuid().ToString(), file.FileName);

            string path = string.Empty;

            for (int i = 0; i < roots.Length; i++)
            {
                path = Path.Combine(path, roots[i]);
            }

            path = Path.Combine(path, filename);

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filename;
        }


    }
}
