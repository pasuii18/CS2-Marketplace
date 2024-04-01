namespace CSMarketApp.Infrastructure.Business.Algorithms
{
    public class FileValidation
    {
        private static readonly string[] ImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

        public static bool IsImageFile(string fileName)
        {
            string extension = Path.GetExtension(fileName)?.ToLowerInvariant();
            return !string.IsNullOrEmpty(extension) && Array.Exists(ImageExtensions, e => e.Equals(extension));
        }
    }
}
