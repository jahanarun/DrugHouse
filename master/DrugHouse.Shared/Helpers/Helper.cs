using System;

namespace DrugHouse.Shared.Helpers
{
    public static class Helper
    {
        public static Exception GetInnerMostException(Exception ex)
        {
            var innerEx = ex;
            while (innerEx.InnerException != null)
                innerEx = innerEx.InnerException;
            return innerEx;
        }
    }
}
