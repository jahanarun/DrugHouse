using System;

namespace DrugHouse.Model.Exceptions
{
    class DeleteConstraintException  : Exception
    {
        public DeleteConstraintException(string msg, Exception innerException)
            : base("Cannot delete item(s) which are in-use by other Patient records.", innerException)
        {
        }
    }
}
