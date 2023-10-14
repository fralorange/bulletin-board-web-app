using Microsoft.AspNetCore.Http;

namespace BulletinBoard.Application.AppServices.FileProcessing.Helpers
{
    /// <summary>
    /// Хелпер для конвертации файла в массив байтов.
    /// </summary>
    public static class FileToBytesHelper
    {
        /// <summary>
        /// Обработчик файла.
        /// </summary>
        /// <param name="formFile">Файл.</param>
        /// <returns>Массив байтов.</returns>
        public async static Task<byte[]> ProcessAsync(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
