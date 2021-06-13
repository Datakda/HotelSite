using HotelSite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HotelSite.Service
{
    public class RSAservice
    {
        private RSACryptoServiceProvider cps = new RSACryptoServiceProvider(2048);
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;
        public  RSAservice()
        {
            _privateKey = cps.ExportParameters(true);
            _publicKey = cps.ExportParameters(false);
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _privateKey);

            using (FileStream privateKey = new FileStream("privateKey.xml", FileMode.Create))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(sw.ToString());
                // запись массива байтов в файл
                privateKey.Write(array, 0, array.Length);

            }

              
            Keys key = new Keys(GetPublicKey());
            string json = JsonSerializer.Serialize<Keys>(key);
            var res = PostJsonAsync("https://localhost:44391/api/rsa/", json);

            res.Wait();

            var result = res.Result;
            using (FileStream publicKey = new FileStream("publicKey.xml", FileMode.Create))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(result);
                // запись массива байтов в файл
                publicKey.Write(array, 0, array.Length);

            }

        }

        public RSAservice(RSAParameters _publicKey, RSAParameters _privatKey)
        {
            this._publicKey = _publicKey;
            this._privateKey = _privatKey;

        }

        public string GetPublicKey()
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _publicKey);
            return sw.ToString();
        }

        public string Encrypt(string plainText, RSAParameters _publicKey)
        {
            cps = new RSACryptoServiceProvider();
            cps.ImportParameters(_publicKey);
            var data = Encoding.Unicode.GetBytes(plainText);
            var cypher = cps.Encrypt(data, false);
            return Convert.ToBase64String(cypher);

        }

        public string Decrypt(string cypherText)
        {
            var dataBytes = Convert.FromBase64String(cypherText);
            cps.ImportParameters(_privateKey);
            var plainText = cps.Decrypt(dataBytes, false);
            return Encoding.Unicode.GetString(plainText);
        }


       


        private async Task<string> PostJsonAsync(string url, string publicKey)
        {
            HttpClient client = new HttpClient();
            using HttpContent content = new StringContent(publicKey, Encoding.UTF8, "application/json");
            using var response = await client.PostAsync(url, content);
            return await response.Content.ReadAsStringAsync();
        }

    }
}
