using System;
using System.IO;
using System.Text;

namespace Kpo4162_nmv.Lib.source.Log
{
    /// <summary>
    /// Класс для логгирования ошибок
    /// </summary>
    public static class ErrorLogger
    {
        /// <summary>
        /// Логгирование ошибки
        /// </summary>
        /// <param name="errorText">Текст ошибки</param>
        public static void LogError(string errorText)
        {
            var message = $"CAUGHT ERROR | {DateTime.Now:dd.MM.yyyy HH:ss} | {errorText}\n";
            writeLogToFile(message);
        }

        /// <summary>
        /// Запись лога в файл
        /// </summary>
        /// <param name="message">Текст лога</param>
        private static void writeLogToFile(string message)
        {
            using (var stream = new FileStream("errorLogger.txt", FileMode.Append))
            {
                var stringContent = Encoding.UTF8.GetBytes(message);
                stream.Write(stringContent, 0, stringContent.Length);
            }
        }
    }
}
