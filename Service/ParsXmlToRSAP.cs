using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelSite.Service
{
    public static class ParsXmlToRSAP
    {


        public static RSAParameters GetPubKeyFromXmlString(string xmlString)
        {
            RSAParameters result2 = new RSAParameters();
            result2.Modulus = getRSAKeyEle("Modulus", xmlString);
            result2.Exponent = getRSAKeyEle("Exponent", xmlString);
            return result2;
        }
        public static RSAParameters GetPrivKeyFromXmlString(string xmlString)
        {
            RSAParameters result = new RSAParameters();
            result.D = getRSAKeyEle("D", xmlString);
            result.DP = getRSAKeyEle("DP", xmlString);
            result.DQ = getRSAKeyEle("DQ", xmlString);
            result.Exponent = getRSAKeyEle("Exponent", xmlString);
            result.InverseQ = getRSAKeyEle("InverseQ", xmlString);
            result.Modulus = getRSAKeyEle("Modulus", xmlString);
            result.P = getRSAKeyEle("P", xmlString);
            result.Q = getRSAKeyEle("Q", xmlString);
            return result;
        }

        private static byte[] getRSAKeyEle(string keyName, string xmlString)
        {
            Regex r = new Regex("<" + keyName + @">[\w+=/]*</" + keyName + ">");
            string s = r.Match(xmlString).Value;
            return Convert.FromBase64String(s.Substring(keyName.Length + 2, s.Length - 2 * keyName.Length - 5));
        }

    }
}
