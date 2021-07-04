using System;
using System.IO;

namespace Kpo4162_nmv.Lib
{
    public static class LogUtility
    {
        /// <summary>
        /// Запись ошибки в лог
        /// </summary>
        /// <param name="message"></param>
        public static void ErrorLog(string message)
        {
            var errorMessage = $"{DateTime.Now:dd.MM.yyyy HH:ss} | {message}\n";
            File.AppendAllText("error.log", errorMessage);
        }

        /// <summary>
        /// Запись ошибки в лог по тексту исключения
        /// </summary>
        /// <param name="exception"></param>
        public static void ErrorLog(Exception exception)
        {
            var errorMessage = $"{DateTime.Now:dd.MM.yyyy HH:ss} | {exception.Message}\n";
            File.AppendAllText("error.log", errorMessage);
        }
    }
}
