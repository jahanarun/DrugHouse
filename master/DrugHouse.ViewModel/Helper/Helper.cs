/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace DrugHouse.ViewModel.Helper
{
    public static class  Helper
    {
        public static T GetAttribute<T>(Type type)
    where T : Attribute
        {
            IList attributes = type.GetCustomAttributes(typeof(T), true);
            if (attributes.Count == 0)
            {
                Debug.Fail(string.Format("{0} attribute missing for {1}", typeof(T).Name, type.FullName));
            }
            else if (attributes.Count > 1)
            {
                Debug.Fail(string.Format("Multiple {0} attributes  for {1}", typeof(T).Name, type.FullName));
            }
            else
            {
                return (T)attributes[0];
            }
            return null;
        }


        public static Guid StringToGUID(string value)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            return new Guid(data);
        }
    }
}
