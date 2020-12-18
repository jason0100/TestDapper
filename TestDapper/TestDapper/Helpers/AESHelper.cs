using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TestDapper.Models;

namespace TestDapper.Helpers
{
	public class AESHelper:IAESHelper
	{
        private readonly IConfiguration _config;
        public AESHelper(IConfiguration config) {
            _config = config;
        }

        public string Encrypt(string text)
        {
            string base64 = Convert.ToBase64String(Encoding.Default.GetBytes(text));
            var result = new ResultModel();

            //這裡使用Aes創建編譯物件
            using (Aes aes = System.Security.Cryptography.Aes.Create())
            {
                var CryptoKey = _config["key"];
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                aes.Key = key;//加密金鑰(32 Byte)
                aes.IV = iv;//初始向量(Initial Vector, iv) 
                //aes.Mode = CipherMode.CBC;
                //aes.Padding = PaddingMode.Zeros;

                //創建密碼編譯轉換運算
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                //使用記憶體串流來儲存結果，
                //執行加密
                byte[] cryptTextBytes = Encoding.Unicode.GetBytes(base64);

                byte[] cryptedText = encryptor.TransformFinalBlock(cryptTextBytes, 0, cryptTextBytes.Length);
                byte[] array64 = new byte[(int)Math.Ceiling(cryptedText.Length / 6.0) * 6];//新增byte array 長度為加密字串的6的倍數
                Array.Copy(cryptedText, array64, cryptedText.Length);
                result.r = true;
                result.m = Convert.ToBase64String(cryptedText);


            }
            return result.m;
        }

        public ResultModel Decrypt(string CipherText64)
        {
            var CryptoKey = _config["key"];
            var result = new ResultModel();
            try
            {
                //檢查參數
                if (CipherText64 == null || CipherText64.Length <= 0)
                    throw new ArgumentNullException("沒有要解密的內容");
                using (Aes aes = System.Security.Cryptography.Aes.Create())
                {//這裡使用Aes創建編譯物件
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                    byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));//加密金鑰用sha256產生
                    byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));//初始向量用md5產生
                    aes.Key = key; //加密金鑰(32 Byte)
                    aes.IV = iv; //初始向量(Initial Vector, iv) 
                                 //aes.Mode = CipherMode.CBC;
                                 //aes.Padding = PaddingMode.Zeros;

                    byte[] encryptTextBytes = (Convert.FromBase64String(CipherText64));//密文處理 轉成byte array

                    //創建密碼解譯轉換運算  //加密器
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    byte[] decryptedText = decryptor.TransformFinalBlock(encryptTextBytes, 0, encryptTextBytes.Length);
                    result.r = true;
                    result.d = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.Unicode.GetString(decryptedText)));
                    result.m = "Decryption Success";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                result.r = false;
                result.m = e.ToString();
            }
            return result;
        }
    }
}
