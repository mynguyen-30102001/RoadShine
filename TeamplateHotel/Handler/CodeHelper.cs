using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TeamplateHotel.Handler
{
    public class CodeHelper
    {
        public static double GetExrate()
        {
            var vnd = "21150";
            try
            {
                var load = XDocument.Load(@"http://www.vietcombank.com.vn/ExchangeRates/ExrateXML.aspx");
                var xElement = load.Element("ExrateList");
                if (xElement != null)
                {
                    var usds = xElement.Elements("Exrate");
                    foreach (var element in usds.Where(element => element.Attribute("CurrencyCode").Value == "USD"))
                    {
                        vnd = element.Attribute("Sell").Value;
                    }
                }
            }
            catch (Exception)
            {
                vnd = "21150";
            }
            return double.Parse(vnd);
        }

        public static string RandomString(int numberChar = 10)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < numberChar; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(36);
                if (temp != -1 && temp == t)
                {
                    return RandomString(numberChar);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
    }
}