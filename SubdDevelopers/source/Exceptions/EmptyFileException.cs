using System;

namespace SubdDevelopers.source.Exceptions
{
    /// <summary>
    /// Исключение, возникающее если файл пустой
    /// </summary>
    public class EmptyFileException : Exception
    {
        public EmptyFileException(string message) : base(message) { }
    }
}
