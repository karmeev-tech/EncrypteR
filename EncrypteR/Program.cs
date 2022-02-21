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
        //пароль из 8 символов
        public void Encrypt()//зашифровать
        {
            using (var DES = new DESCryptoServiceProvider())
            {
                DES.Key = Encoding.UTF8.GetBytes(pass);
                DES.Mode = CipherMode.CBC;
                DES.Padding = PaddingMode.PKCS7;
                DES.IV = Encoding.UTF8.GetBytes(pass);


                using (var file = File.Open(path, FileMode.Open))
                {
                    MemoryStream mem = new MemoryStream();
                    CryptoStream cryptoStream = new CryptoStream(mem, DES.CreateEncryptor(),
                        CryptoStreamMode.Write);

                    file.CopyTo(cryptoStream);
                    cryptoStream.FlushFinalBlock();

                    File.WriteAllBytes(path + "crypt", mem.ToArray());
                    Console.WriteLine("Encrypted succesfully " + path);
                }
            }
            //есть using (var memStream = ...)/ Нужно уйти 
            //У класса Stream есть метод Copy To. Если правильно создать потоки, то все методы будут сводить к использованию Copy To Stream.CopyTo(file)
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

        public string path; //сюда записываем путь к файлу, который расшифровываем или шифруем;
        public string pass; //сюда пароль к файлу;
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
            fbi.path = "C:\\Users\\karmeev-technology\\Documents\\work\\encrypter\\EncrypteR\\bin\\Debug\\C1793.JPG"; //нужна чисто для теста, чтоб время не тратить, работаем с картинкой в качестве примера
            Console.WriteLine("Введите пароль из 8 символов");
            fbi.pass = Console.ReadLine();
            fbi.pass = "12345678";//также чисто для теста, потом все это дело из релиза уберём
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
