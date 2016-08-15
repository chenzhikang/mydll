using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;

namespace fav._6655.com
{
    public static class DES2
    {
        //#region DES加密
        //public static string EncryptString(string pToEncrypt, string key)
        //{
        //    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //    byte[] inputByteArray = Encoding.ASCII.GetBytes(pToEncrypt);


        //    des.Key = Encoding.ASCII.GetBytes(key);
        //    des.IV = Encoding.ASCII.GetBytes(key);
        //    MemoryStream ms = new MemoryStream();
        //    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

        //    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //    cs.FlushFinalBlock();

        //    StringBuilder ret = new StringBuilder();
        //    foreach (byte b in ms.ToArray())
        //    {
        //        ret.AppendFormat("{0:X2}", b);
        //    }

        //    ret.ToString();
        //    return ret.ToString();
        //}
        //#endregion

        public static string EncryptString(string decryptedString, string key, Encoding encoding)
        {
            DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();
            desProvider.Mode = CipherMode.ECB;
            desProvider.Padding = PaddingMode.Zeros;//自动补0  
            desProvider.Key = encoding.GetBytes(key);
            StringBuilder ret = new StringBuilder();
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(stream, desProvider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] data = encoding.GetBytes(decryptedString);
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    foreach (byte b in stream.ToArray())
                    {
                        ret.AppendFormat("{0:X2}", b);
                    }
                }
            }
            return ret.ToString();
        }

        public static byte[] GetEncryptBytes(string decryptedString, string key, Encoding encoding)
        {
            DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();
            desProvider.Mode = CipherMode.ECB;
            desProvider.Padding = PaddingMode.Zeros;//自动补0  
            desProvider.Key = encoding.GetBytes(key);
            StringBuilder ret = new StringBuilder();
            byte[] bytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(stream, desProvider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] data = encoding.GetBytes(decryptedString);
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    bytes = stream.ToArray();
                }
            }
            return bytes;
        }

        public static string HexToStr(string hex)
        {
            string result = string.Empty;
            for (int i = 0; i < hex.Length; i++)
            {
                if ((i % 2) == 0)
                {
                    result += (char)(Convert.ToByte(hex.Substring(i, 2), 16));
                }
            }
            return result;
        }

        public static byte[] HexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            int n = 0;
            for (int i = 0; i < hex.Length; i++)
            {
                if ((i % 2) == 0)
                {
                    bytes[n++] += Convert.ToByte(hex.Substring(i, 2), 16);
                }
            }
            return bytes;
        }

        public static string DecryptString(string encryptedString, string key, Encoding encoding)
        {
            DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();
            desProvider.Mode = CipherMode.ECB;
            desProvider.Padding = PaddingMode.Zeros;//自动补0  
            desProvider.Key = encoding.GetBytes(key);
            byte[] bytes = HexToBytes(encryptedString);

            //Convert.FromBase64String(encryptedString)
            //(byte[])encryptedString.ToCharArray();
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (CryptoStream cs = new CryptoStream(stream, desProvider.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs, encoding))
                    {
                        string resultstr = sr.ReadToEnd();
                        return resultstr.Replace("\0","");
                    }
                }
            }
        }

        //#region DES解密
        //public static string DecryptString(string pToDecrypt, string key)
        //{
        //    DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        //    byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
        //    for (int x = 0; x < pToDecrypt.Length / 2; x++)
        //    {
        //        int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
        //        inputByteArray[x] = (byte)i;
        //    }

        //    des.Key = UTF8Encoding.ASCII.GetBytes(key);
        //    des.IV = UTF8Encoding.ASCII.GetBytes(key);
        //    MemoryStream ms = new MemoryStream();
        //    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        //    cs.Write(inputByteArray, 0, inputByteArray.Length);

        //    StringBuilder ret = new StringBuilder();
        //    string aa = Encoding.GetEncoding("gb2312").GetString(ms.ToArray());
        //    cs.FlushFinalBlock();
        //    return aa;
        //}
        //#endregion
    }
}
