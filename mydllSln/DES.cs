using System;
using System.IO;
using System.Text;

namespace mydll
{
    public enum TDesMode
    {
        dmEncry,
        dmDecry,
    }

    public static class DES
    {
        #region 定义常量
        public static byte[][] subKey;
        // 初始值置IP
        public static byte[] BitIP = { 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3, 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7, 56, 48, 40, 32, 24, 16, 8, 0, 58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4, 62, 54, 46, 38, 30, 22, 14, 6 };
        // 逆初始置IP-1
        public static byte[] BitCP = { 39, 7, 47, 15, 55, 23, 63, 31, 38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29, 36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27, 34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25, 32, 0, 40, 8, 48, 16, 56, 24 };
        // 位选择函数E
        public static int[] BitExp = { 31, 0, 1, 2, 3, 4, 3, 4, 5, 6, 7, 8, 7, 8, 9, 10, 11, 12, 11, 12, 13, 14, 15, 16, 15, 16, 17, 18, 19, 20, 19, 20, 21, 22, 23, 24, 23, 24, 25, 26, 27, 28, 27, 28, 29, 30, 31, 0 };
        // 置换函数P
        public static byte[] BitPM = { 15, 6, 19, 20, 28, 11, 27, 16, 0, 14, 22, 25, 4, 17, 30, 9, 1, 7, 23, 13, 31, 26, 2, 8, 18, 12, 29, 5, 21, 10, 3, 24 };
        // S盒
        public static byte[][] sBox = new byte[][] { new byte[] {
                14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7,
                0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8,
                4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0,
                15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13
             }, new byte[] {
                15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10,
                3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5,
                0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15,
                13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9
             }, new byte[] {
                10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8,
                13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1,
                13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7,
                1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12
             }, new byte[] {
                7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15,
                13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9,
                10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4,
                3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14
             }, new byte[] {
                2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9,
                14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6,
                4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14,
                11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3
             }, new byte[] {
                12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11,
                10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8,
                9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6,
                4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13
             }, new byte[] {
                4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1,
                13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6,
                1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2,
                6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12
             }, new byte[] {
                13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7,
                1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2,
                7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8,
                2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11
             } };
        // 选择置换PC-1
        public static byte[] BitPMC1 = { 56, 48, 40, 32, 24, 16, 8, 0, 57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18, 10, 2, 59, 51, 43, 35, 62, 54, 46, 38, 30, 22, 14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 60, 52, 44, 36, 28, 20, 12, 4, 27, 19, 11, 3 };
        // 选择置换PC-2
        public static byte[] BitPMC2 = { 13, 16, 10, 23, 0, 4, 2, 27, 14, 5, 20, 9, 22, 18, 11, 3, 25, 7, 15, 6, 26, 19, 12, 1, 40, 51, 30, 36, 46, 54, 29, 39, 50, 44, 32, 47, 43, 48, 38, 55, 33, 52, 45, 41, 49, 35, 28, 31 };
        #endregion
        private static object obj = new object();
        static DES()
        {
            subKey = new byte[0x10][];
            for (int i = 0; i < 0x10; i++)
            {
                subKey[i] = new byte[6];
            }
        }

        private static void InitPermutation(byte[] inData)
        {
            byte[] newData = new byte[8];

            FillChar(newData, 8, 0);
            for (byte i = 0; i <= 63; i++)
            {
                if ((inData[BitIP[i] >> 3] & (1 << (7 - (BitIP[i] & 0x07)))) != 0)
                {
                    // 填充字符
                    newData[i >> 3] = (byte)(newData[i >> 3] | (1 << (7 - (i & 0x07))));
                }
            }
            for (byte i = 0; i <= 7; i++)
            {
                inData[i] = newData[i];
            }
        }

        private static void FillChar(byte[] bytes, int count, byte b)
        {
            for (int i = 0; i < count; i++)
            {
                bytes[i] = b;
            }
        }

        private static void ConversePermutation(byte[] inData)
        {
            byte[] newData = new byte[8];
            int i = 0;
            // Unsupported function or procedure: 'FillChar'
            FillChar(newData, 8, 0);
            for (i = 0; i <= 63; i++)
            {
                if ((inData[BitCP[i] >> 3] & (1 << (7 - (BitCP[i] & 0x07)))) != 0)
                {
                    newData[i >> 3] = (byte)(newData[i >> 3] | (1 << (7 - (i & 0x07))));
                }
            }
            for (i = 0; i <= 7; i++)
            {
                inData[i] = newData[i];
            }
        }

        private static void Expand(byte[] inData, byte[] outData)
        {
            int i = 0;
            // Unsupported function or procedure: 'FillChar'
            FillChar(outData, 6, 0);
            for (i = 0; i <= 47; i++)
            {
                if ((inData[BitExp[i] >> 3] & (1 << (7 - (BitExp[i] & 0x07)))) != 0)
                {
                    outData[i >> 3] = (byte)(outData[i >> 3] | (1 << (7 - (i & 0x07))));
                }
            }
        }

        private static void Permutation(byte[] inData)
        {
            byte[] newData = new byte[4];
            int i = 0;
            // Unsupported function or procedure: 'FillChar'
            FillChar(newData, 4, 0);
            for (i = 0; i <= 31; i++)
            {
                if ((inData[BitPM[i] >> 3] & (1 << (7 - (BitPM[i] & 0x07)))) != 0)
                {
                    newData[i >> 3] = (byte)(newData[i >> 3] | (1 << (7 - (i & 0x07))));
                }
            }
            for (i = 0; i <= 3; i++)
            {
                inData[i] = newData[i];
            }
        }

        private static byte SI(byte s, byte inByte)
        {
            byte c = (byte)((((uint)(inByte & 0x20)) | (((uint)(inByte & 30)) >> 1)) | (((uint)(inByte & 1)) << 4));
            return (byte)(sBox[(int)s][(int)c] & 15);

            //byte result = 0;
            //byte c = 0;
            //c = (byte)((inByte & 0x20) | ((inByte & 0x1e) >> 1) | ((inByte & 0x01) << 4));
            //result = (sBox[s][c] & 0x0f);
            //return result;
        }

        private static void PermutationChoose1(byte[] inData, byte[] outData)
        {
            int i = 0;
            // Unsupported function or procedure: 'FillChar'
            FillChar(outData, 7, 0);
            for (i = 0; i <= 55; i++)
            {
                if ((inData[BitPMC1[i] >> 3] & (1 << (7 - (BitPMC1[i] & 0x07)))) != 0)
                {
                    outData[i >> 3] = (byte)(outData[i >> 3] | (1 << (7 - (i & 0x07))));
                }
            }
        }

        private static void PermutationChoose2(byte[] inData, byte[] outData)
        {
            int i = 0;
            // Unsupported function or procedure: 'FillChar'
            FillChar(outData, 6, 0);
            for (i = 0; i <= 47; i++)
            {
                if ((inData[BitPMC2[i] >> 3] & (1 << (7 - (BitPMC2[i] & 0x07)))) != 0)
                {
                    outData[i >> 3] = (byte)(outData[i >> 3] | (1 << (7 - (i & 0x07))));
                }
            }
        }

        private static void CycleMove(byte[] inData, byte bitMove)
        {
            int i = 0;
            for (i = 0; i <= bitMove - 1; i++)
            {
                inData[0] = (byte)((inData[0] << 1) | (inData[1] >> 7));
                inData[1] = (byte)((inData[1] << 1) | (inData[2] >> 7));
                inData[2] = (byte)((inData[2] << 1) | (inData[3] >> 7));
                inData[3] = (byte)((inData[3] << 1) | ((inData[0] & 0x10) >> 4));
                inData[0] = (byte)((inData[0] & 0x0f));
            }
        }

        private static void MakeKey(byte[] inKey, ref byte[][] outKey)
        {
            byte[] bitDisplace = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

            byte[] outData56 = new byte[7];
            byte[] key28l = new byte[4];
            byte[] key28r = new byte[4];
            byte[] key56o = new byte[7];
            int i = 0;

            PermutationChoose1(inKey, outData56);
            key28l[0] = (byte)(outData56[0] >> 4);
            key28l[1] = (byte)((outData56[0] << 4) | (outData56[1] >> 4));
            key28l[2] = (byte)((outData56[1] << 4) | (outData56[2] >> 4));
            key28l[3] = (byte)((outData56[2] << 4) | (outData56[3] >> 4));
            key28r[0] = (byte)(outData56[3] & 0x0f);
            key28r[1] = outData56[4];
            key28r[2] = outData56[5];
            key28r[3] = outData56[6];
            for (i = 0; i <= 15; i++)
            {
                CycleMove(key28l, bitDisplace[i]);
                CycleMove(key28r, bitDisplace[i]);
                key56o[0] = (byte)((key28l[0] << 4) | (key28l[1] >> 4));
                key56o[1] = (byte)((key28l[1] << 4) | (key28l[2] >> 4));
                key56o[2] = (byte)((key28l[2] << 4) | (key28l[3] >> 4));
                key56o[3] = (byte)((key28l[3] << 4) | key28r[0]);
                key56o[4] = key28r[1];
                key56o[5] = key28r[2];
                key56o[6] = key28r[3];
                PermutationChoose2(key56o, outKey[i]);
            }
        }

        private static void Encry(byte[] inData, byte[] subKey, byte[] outData)
        {
            byte[] outBuf = new byte[6];
            byte[] buf = new byte[8];
            int i = 0;
            Expand(inData, outBuf);
            for (i = 0; i <= 5; i++)
            {
                outBuf[i] = (byte)(outBuf[i] ^ subKey[i]);
            }
            buf[0] = (byte)(outBuf[0] >> 2);
            buf[1] = (byte)(((outBuf[0] & 0x03) << 4) | (outBuf[1] >> 4));
            buf[2] = (byte)(((outBuf[1] & 0x0f) << 2) | (outBuf[2] >> 6));
            buf[3] = (byte)(outBuf[2] & 0x3f);
            buf[4] = (byte)(outBuf[3] >> 2);
            buf[5] = (byte)(((outBuf[3] & 0x03) << 4) | (outBuf[4] >> 4));
            buf[6] = (byte)(((outBuf[4] & 0x0f) << 2) | (outBuf[5] >> 6));
            buf[7] = (byte)(outBuf[5] & 0x3f);
            for (i = 0; i <= 7; i++)
            {
                buf[i] = SI((byte)i, buf[i]);
            }
            for (i = 0; i <= 3; i++)
            {
                outBuf[i] = (byte)((buf[i * 2] << 4) | buf[i * 2 + 1]);
            }
            Permutation(outBuf);
            for (i = 0; i <= 3; i++)
            {
                outData[i] = outBuf[i];
            }
        }

        private static void DesData(TDesMode desMode, byte[] inData, ref byte[] outData)
        {
            // inData, outData 都为8Bytes，否则出错
            int i = 0;
            int j = 0;
            byte[] temp = new byte[4];
            byte[] buf = new byte[4];
            for (i = 0; i <= 7; i++)
            {
                outData[i] = inData[i];
            }
            InitPermutation(outData);
            if (desMode == TDesMode.dmEncry)
            {
                for (i = 0; i <= 15; i++)
                {
                    for (j = 0; j <= 3; j++)
                    {
                        temp[j] = outData[j];
                    }
                    for (j = 0; j <= 3; j++)
                    {
                        // temp = Ln
                        outData[j] = outData[j + 4];
                    }
                    // Ln+1 = Rn
                    Encry(outData, subKey[i], buf);
                    for (j = 0; j <= 3; j++)
                    {
                        // Rn ==Kn==> buf
                        outData[j + 4] = (byte)(temp[j] ^ buf[j]);
                    }
                    // Rn+1 = Ln^buf
                }
                for (j = 0; j <= 3; j++)
                {
                    temp[j] = outData[j + 4];
                }
                for (j = 0; j <= 3; j++)
                {
                    outData[j + 4] = outData[j];
                }
                for (j = 0; j <= 3; j++)
                {
                    outData[j] = temp[j];
                }
            }
            else
            {
                if (desMode == TDesMode.dmDecry)
                {
                    for (i = 15; i >= 0; i--)
                    {
                        for (j = 0; j <= 3; j++)
                        {
                            temp[j] = outData[j];
                        }
                        for (j = 0; j <= 3; j++)
                        {
                            outData[j] = outData[j + 4];
                        }
                        Encry(outData, subKey[i], buf);
                        for (j = 0; j <= 3; j++)
                        {
                            outData[j + 4] = (byte)(temp[j] ^ buf[j]);
                        }
                    }
                    for (j = 0; j <= 3; j++)
                    {
                        temp[j] = outData[j + 4];
                    }
                    for (j = 0; j <= 3; j++)
                    {
                        outData[j + 4] = outData[j];
                    }
                    for (j = 0; j <= 3; j++)
                    {
                        outData[j] = temp[j];
                    }
                }
            }
            ConversePermutation(outData);
        }

        #region 加密的字符串
        /// <summary>
        /// 加密的字符串
        /// </summary>
        /// <param name="Str">要加密的字符串</param>
        /// <param name="Key">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryStr(string Str, string Key)
        {
            byte[] StrByte = new byte[8];
            byte[] OutByte = new byte[8];
            byte[] KeyByte = new byte[8];

            if ((((Str != null) ? Str.Length : 0) > 0) && (Str[((Str != null) ? Str.Length : 0) - 1] == '\0'))
            {
                throw new Exception("Error: the last char is NULL char.");
            }
            if (((Key != null) ? Key.Length : 0) < 8)
            {
                while (((Key != null) ? Key.Length : 0) < 8)
                {
                    Key = Key + "\0";
                }
            }
            while ((((Str != null) ? Str.Length : 0) % 8) != 0)
            {
                Str = Str + "\0";
            }
            int J = 0;
            do
            {
                KeyByte[J] = (byte)Key[(J + 1) - 1];
                J++;
            }
            while (J != 8);
            MakeKey(KeyByte, ref subKey);
            string StrResult = "";
            int num = (((Str != null) ? Str.Length : 0) / 8) - 1;
            for (int i = 0; i <= num; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //StrByte[j] = (byte)Str[(((I << 3) + j) + 1) - 1];
                    StrByte[j] = (byte)Str[j + i * 8];
                }
                DesData(TDesMode.dmEncry, StrByte, ref OutByte);

                for (int j = 0; j < 8; j++)
                {
                    StrResult = StrResult + (char)(OutByte[j]);
                    //StrResult = StrResult + System.@WStrFromWChar((char)OutByte[J]);
                }
            }
            return StrToHex(StrResult);
        }
        #endregion

        #region 解密字符串
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="Str">要解密的字符串</param>
        /// <param name="Key">密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryStr(string Str, string Key)
        {
            lock (obj)
            {


                byte[] StrByte = new byte[8];
                byte[] OutByte = new byte[8];
                byte[] KeyByte = new byte[8];

                // Undeclared identifier: 'HexToStr'
                Str = HexToStr(Str);
                if (Key.Length < 8)
                {
                    while (Key.Length < 8)
                    {
                        Key = Key + (char)(0);
                    }
                }
                for (int j = 0; j < 8; j++)
                {
                    KeyByte[j] = (byte)(Key[j]);
                }
                MakeKey(KeyByte, ref subKey);
                string StrResult = string.Empty;

                for (int i = 0; i < Str.Length / 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        StrByte[j] = (byte)(Str[i * 8 + j]);
                    }

                    DesData(TDesMode.dmDecry, StrByte, ref OutByte);

                    for (int j = 0; j < 8; j++)
                    {
                        StrResult = StrResult + (char)(OutByte[j]);
                    }
                }
                while ((StrResult.Length > 0) && ((int)(StrResult[StrResult.Length - 1]) == 0))
                {
                    StrResult = StrResult.Remove(StrResult.Length - 1, 1);
                }

                return StrResult;
            }
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="Str">要解密的字符串</param>
        /// <param name="Key">密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryStrFromEnCoding(string Str, string Key, Encoding encoding)
        {
            byte[] StrByte = new byte[8];
            byte[] OutByte = new byte[8];
            byte[] KeyByte = new byte[8];

            // Undeclared identifier: 'HexToStr'
            Str = HexToStr(Str);
            if (Key.Length < 8)
            {
                while (Key.Length < 8)
                {
                    Key = Key + (char)(0);
                }
            }
            for (int j = 0; j < 8; j++)
            {
                KeyByte[j] = (byte)(Key[j]);
            }
            MakeKey(KeyByte, ref subKey);
            string StrResult = string.Empty;
            byte[] bsResult = new byte[Str.Length];

            for (int i = 0; i < Str.Length / 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    StrByte[j] = (byte)(Str[i * 8 + j]);
                }

                DesData(TDesMode.dmDecry, StrByte, ref OutByte);

                for (int j = 0; j < 8; j++)
                {
                    bsResult[8 * i + j] = OutByte[j];
                }
                //StrResult += encoding.GetString(OutByte);
                //for (int j = 0; j < 8; j++)
                // {
                //     //StrResult = StrResult + (char)(OutByte[j]);
                // }
            }
            StrResult = encoding.GetString(bsResult);
            while ((StrResult.Length > 0) && ((int)(StrResult[StrResult.Length - 1]) == 0))
            {
                StrResult = StrResult.Remove(StrResult.Length - 1, 1);
            }

            return StrResult;
        }
        #endregion

        private static string EncryStrHex(string Str, string Key)
        {
            string result = null;
            string StrResult = null;
            string TempResult = null;
            string Temp = null;
            int I = 0;
            TempResult = EncryStr(Str, Key);
            StrResult = "";
            for (I = 0; I <= TempResult.Length - 1; I++)
            {
                // Unsupported function or procedure: 'Format'
                Temp = string.Format("{0:X2}", new int[] { (int)(TempResult[I + 1]) });
                if (Temp.Length == 1)
                {
                    Temp = '0' + Temp;
                }
                StrResult = StrResult + Temp;
            }
            result = StrResult;
            return result;
        }

        private static int HexToInt(string Hex)
        {
            int result = 0;
            int I = 0;
            int Res = 0;
            char ch = (char)0;
            Res = 0;
            for (I = 0; I <= Hex.Length - 1; I++)
            {
                ch = Hex[I + 1];
                if ((ch >= '0') && (ch <= '9'))
                {
                    Res = Res * 16 + (int)(ch) - (int)('0');
                }
                else
                {
                    if ((ch >= 'A') && (ch <= 'F'))
                    {
                        Res = Res * 16 + (int)(ch) - (int)('A') + 10;
                    }
                    else
                    {
                        if ((ch >= 'a') && (ch <= 'f'))
                        {
                            Res = Res * 16 + (int)(ch) - (int)('a') + 10;
                        }
                        else
                        {
                            throw new Exception("Error: not a Hex String");
                        }
                    }
                }
            }
            result = Res;
            return result;
        }

        private static string DecryStrHex(string StrHex, string Key)
        {
            string result = null;
            string Str = null;
            string Temp = null;
            int I = 0;
            Str = "";
            for (I = 0; I <= StrHex.Length / 2 - 1; I++)
            {
                Temp = StrHex.Substring(I * 2 + 1 - 1, 2);
                Str = Str + (char)(HexToInt(Temp));
            }
            result = DecryStr(Str, Key);
            return result;
        }

        private static string StrToHex(string str)
        {
            StringBuilder text = new StringBuilder();

            foreach (char b in str.ToCharArray())
            {
                text.AppendFormat("{0:X2}", (int)b);
            }
            text.ToString();
            return text.ToString();
        }

        private static string HexToStr(string hex)
        {
            StringBuilder strReturn = new StringBuilder();
            for (int i = 0; i < hex.Length; i++)
            {
                if ((i % 2) == 0)
                {
                    strReturn.Append((char)(Convert.ToByte(hex.Substring(i, 2), 16)));
                }
            }
            return strReturn.ToString();
        }

    } // end DES

}
