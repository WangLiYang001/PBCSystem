using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
namespace Utils
{
    /// <summary>
    /// AES加密解密
    /// </summary>
    public class HAES
    {

        static readonly string KEY = "iceworld!@#12345";

        /// <summary>
        /// 一次处理的明文字节数
        /// </summary>
        static readonly int EncryptSize = 100*1024;
        /// <summary>
        /// 一次处理的密文字节数
        /// </summary>
        static readonly int DecryptSize = 100*1024+16;



        #region 加密
        #region 加密字符串
        /// <summary>
        /// AES 加密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="EncryptString">待加密密文</param>
        /// <param name="EncryptKey">加密密钥</param>
        public static string AESEncrypt(string EncryptString, string EncryptKey)
        {
            return Convert.ToBase64String(AESEncrypt(Encoding.Default.GetBytes(EncryptString), EncryptKey));
        }
        #endregion

        #region 加密字节数组
        /// <summary>
        /// AES 加密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="EncryptString">待加密密文</param>
        /// <param name="EncryptKey">加密密钥</param>
        public static byte[] AESEncrypt(byte[] EncryptByte, string EncryptKey)
        {
            if (EncryptByte.Length == 0) { throw (new Exception("明文不得为空")); }
            if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("密钥不得为空")); }
            byte[] m_strEncrypt;
            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            byte[] m_salt = Convert.FromBase64String("gsf4jvkyhye5/d7k8OrLgM==");
            Rijndael m_AESProvider = Rijndael.Create();
            try
            {
                MemoryStream m_stream = new MemoryStream();
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(EncryptKey, m_salt);
                ICryptoTransform transform = m_AESProvider.CreateEncryptor(pdb.GetBytes(32), m_btIV);
                CryptoStream m_csstream = new CryptoStream(m_stream, transform, CryptoStreamMode.Write);
                m_csstream.Write(EncryptByte, 0, EncryptByte.Length);
                m_csstream.FlushFinalBlock();
                m_strEncrypt = m_stream.ToArray();
                m_stream.Close(); m_stream.Dispose();
                m_csstream.Close(); m_csstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_AESProvider.Clear(); }
            return m_strEncrypt;
        }
        #endregion
        #endregion

        #region 解密
        #region 解密字符串
        /// <summary>
        /// AES 解密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="DecryptString">待解密密文</param>
        /// <param name="DecryptKey">解密密钥</param>
        public static string AESDecrypt(string DecryptString, string DecryptKey)
        {
            return Convert.ToBase64String(AESDecrypt(Encoding.Default.GetBytes(DecryptString), DecryptKey));
        }
        #endregion

        #region 解密字节数组
        /// <summary>
        /// AES 解密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
        /// </summary>
        /// <param name="DecryptString">待解密密文</param>
        /// <param name="DecryptKey">解密密钥</param>
        public static byte[] AESDecrypt(byte[] DecryptByte, string DecryptKey)
        {
            if (DecryptByte.Length == 0) { throw (new Exception("密文不得为空")); }
            if (string.IsNullOrEmpty(DecryptKey)) { throw (new Exception("密钥不得为空")); }
            byte[] m_strDecrypt;
            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");
            byte[] m_salt = Convert.FromBase64String("gsf4jvkyhye5/d7k8OrLgM==");
            Rijndael m_AESProvider = Rijndael.Create();
            try
            {
                MemoryStream m_stream = new MemoryStream();
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(DecryptKey, m_salt);
                ICryptoTransform transform = m_AESProvider.CreateDecryptor(pdb.GetBytes(32), m_btIV);
                CryptoStream m_csstream = new CryptoStream(m_stream, transform, CryptoStreamMode.Write);
                m_csstream.Write(DecryptByte, 0, DecryptByte.Length);
                m_csstream.FlushFinalBlock();
                m_strDecrypt = m_stream.ToArray();
                m_stream.Close(); m_stream.Dispose();
                m_csstream.Close(); m_csstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_AESProvider.Clear(); }
            return m_strDecrypt;
        }
        #endregion
        #endregion

        public static string  EncryptAESFileToString(string filePath ,bool bigFile = false)
        {
            byte[] bytes = EncryptAESFileToBytes(filePath, bigFile);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        public static void EncryptAESFile(string filePath, string outFilePath, bool bigFile = false)
        {
            byte[] texts = EncryptAESFileToBytes(filePath, bigFile);

            using (FileStream fs = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                if (fs != null)
                {

                    fs.Write(texts, 0, texts.Length);
                    fs.Flush();
                    fs.Dispose();
                }
            }
        }

        public static byte[] EncryptAESFileToBytes(string filePath, bool bigFile = false)
        {

            if (File.Exists(filePath) == false)
            {
                Debug.Log(filePath + "文件不存在");
                return new byte[] { };
            }

            byte[] texts = new byte[] { };
            MemoryStream ms = new MemoryStream();

            if (bigFile)
            {
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {

                        if (fs.Length > 0)
                        {

                            while (true)
                            {
                                byte[] bytsize = new byte[EncryptSize];
                                //返回实际读取到的字节
                                int r = fs.Read(bytsize, 0, bytsize.Length);

                                //当字节位0的时候 证明已经读取结束
                                if (r == 0)
                                {
                                    break;
                                }
                                //加密
                                byte[] result = AESEncrypt(bytsize, KEY);
                                ms.Write(result, 0, result.Length);
                            }

                            fs.Close();
                            fs.Dispose();
                        }
                    }

                    texts = ms.ToArray();

                    return texts;
                }
                catch (Exception e)
                {
                    Debug.LogError("Message:" + e.Message);
                }
            }
            else
            {
                texts = File.ReadAllBytes(filePath);

                texts = AESEncrypt(texts, KEY);

                return texts;
            }

            return new byte[] { };
        }


        public static string DecryptAESFileToString(string filePath, bool bigFile = false)
        {
            byte[] bytes = DecryptAESFileToBytes(filePath, bigFile);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        public static void DecryptAESFile(string filePath, string outFilePath,bool bigFile = false)
        {
            byte[] texts = DecryptAESFileToBytes(filePath, bigFile);
            using (FileStream fs = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                if (fs != null)
                {
                    fs.Write(texts, 0, texts.Length);
                    fs.Flush();
                    fs.Dispose();
                }
            }
        }
    

    public static byte[] DecryptAESFileToBytes(string filePath, bool bigFile = false)
    {

        if (File.Exists(filePath) == false)
        {
            Debug.Log(filePath + "文件不存在");
            return new byte[] { };
        }

        byte[] texts = new byte[] { };
        MemoryStream ms = new MemoryStream();
        if (bigFile)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fs.Length > 0)
                    {
                        while (true)
                        {
                            byte[] bytsize = new byte[DecryptSize];
                            //返回实际读取到的字节
                            int r = fs.Read(bytsize, 0, bytsize.Length);
                            Debug.Log("bytsize:" + bytsize.Length);
                            //当字节位0的时候 证明已经读取结束
                            if (r == 0)
                            {
                                break;
                            }

                            //加密
                            byte[] result = AESDecrypt(bytsize, KEY);
                            Debug.Log("result:" + result.Length);
                            ms.Write(result, 0, result.Length);
                        }

                        fs.Close();
                        fs.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Message:" + e.Message);
            }

            texts = ms.ToArray();
            return texts;
        }
        else
        {
            texts = File.ReadAllBytes(filePath);
            texts = AESDecrypt(texts, KEY);
            return texts;
        }

        return new byte[] { };
    }

}
}


