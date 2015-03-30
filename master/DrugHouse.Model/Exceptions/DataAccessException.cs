using System;

namespace DrugHouse.Model.Exceptions
{
    //<summary>The exception that is thrown when an error occurs accessing data.</summary>
    // <remarks>
    //    Use to hide DataProvider specific Exceptions from calling assemblies.
    // </remarks>

    public sealed class DataAccessException : ApplicationException
    {

        /// <summary>Initializes a new instance of the DataAccessException class with a message.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public DataAccessException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the DataAccessException class with a message and a innerException.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
        public DataAccessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
