using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace EncrypteR
{
    class Crypt
    {
        public void Encrypt()
        {
            using (var DES = new DESCryptoServiceProvider())
            {
                DES.Key = Encoding.UTF8.GetBytes(pass);
                DES.Mode = CipherMode.CBC;
                DES.Padding = PaddingMode.PKCS7;
                DES.IV = Encoding.UTF8.GetBytes(pass);
                using (var file = File.Open("encryptfile.txt", FileMode.Open, FileAccess.ReadWrite))
                {
                    FileStream mem = File.Open(path, FileMode.Open, FileAccess.ReadWrite);
                    CryptoStream cryptoStream = new CryptoStream(file, DES.CreateEncryptor(),
                        CryptoStreamMode.Write);
                    mem.CopyTo(cryptoStream);
                    mem.Flush();
                    cryptoStream.FlushFinalBlock();
                    Console.WriteLine("Encrypted succesfully. Your File is encryptedfile.txt");
                }
            }
        }

        public void Decipher()//расшифровать
        {
            byte[] encrypted = File.ReadAllBytes(path);
            using (var DES = new DESCryptoServiceProvider())
            {
                DES.IV = Encoding.UTF8.GetBytes(pass);
                DES.Key = Encoding.UTF8.GetBytes(pass);
                DES.Mode = CipherMode.CBC;
                DES.Padding = PaddingMode.PKCS7;


                using (var memStream = new MemoryStream())
                {
                    CryptoStream cryptoStream = new CryptoStream(memStream, DES.CreateDecryptor(),
                        CryptoStreamMode.Write);

                    cryptoStream.Write(encrypted, 0, encrypted.Length);
                    cryptoStream.FlushFinalBlock();
                    File.WriteAllBytes(path, memStream.ToArray());
                    Console.WriteLine("Decrypted succesfully " + path);
                }
            }

        }

        public string path; 
        public string pass; 
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 - зашифровать | 2 - расшифровать");
            string numb = Console.ReadLine();
            Console.WriteLine("Введите путь");
            Crypt fbi = new Crypt();
            fbi.path = Console.ReadLine();
            Console.WriteLine("Введите пароль из 8 символов");
            fbi.pass = Console.ReadLine();
            switch (numb)
            {
                case "1":
                    fbi.Encrypt();
                    break;
                case "2":
                    fbi.Decipher();
                    break;
            }
        }


    }

}

