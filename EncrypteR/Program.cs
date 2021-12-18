using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EncrypteR
{

    class Crypt
    {
        public static void Encrypt(string path, string key)//зашифровать
        {
            byte[] file = File.ReadAllBytes(path);
            using (var DES = new DESCryptoServiceProvider())
            {
                DES.Key = Encoding.UTF8.GetBytes(key);
                DES.Mode = CipherMode.CBC;
                DES.Padding = PaddingMode.PKCS7;
                DES.IV = Encoding.UTF8.GetBytes(key);


                using (var memStream = new MemoryStream())
                {
                    CryptoStream cryptoStream = new CryptoStream(memStream, DES.CreateEncryptor(),
                        CryptoStreamMode.Write);

                    cryptoStream.Write(file, 0, file.Length);
                    cryptoStream.FlushFinalBlock();
                    File.WriteAllBytes(path, memStream.ToArray());
                    Console.WriteLine("Encrypted succesfully " + path);
                }
            }
        }

        public static void Decipher(string path, string key)//расшифровать
        {

        }

        public string path; //сюда записываем путь к файлу, который расшифровываем или шифруем;
        public string pass; //сюда пароль к файлу;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 - зашифровать | 2 - расшифровать");
            string numb = Console.ReadLine();
            Crypt fbi = new Crypt();
            switch (numb)
            {
                case "1":
                    Console.WriteLine("Введите путь");
                    fbi.path = Console.ReadLine();
                    Console.WriteLine("Введите пароль");
                    fbi.pass = Console.ReadLine();
                    Crypt.Encrypt(fbi.path,fbi.pass);
                    break;
                case "2":
                    break;
                   
            }
        }


    }
}
