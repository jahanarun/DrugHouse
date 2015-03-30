/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using Dexuse.Security;

namespace DrugHouse.Model
{
    public class Decoder
    {
        public static string Decrypt(string cipherText)
        {
            return Cryptography.Decrypt(cipherText, "ijklmnop", "qrstuv", "SHA1", 1, "12mnoprstberyipb", 256);
        }
    }
}
