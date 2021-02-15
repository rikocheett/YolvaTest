using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.Web;
using System.Text.Json;


namespace GeoTest.Classes
{
    class GeoOSM : GeoAbstractSrevice
    {
        protected int CoordRate { get; set;}

        public GeoOSM(string address, string fileName, int coordRate)
            : base("OSM", "XML", "https://nominatim.openstreetmap.org/search.php?q=", address, fileName) 
        {
            CoordRate = coordRate;
        }

        public override void DoRequest() 
        {
            if (CheckParams()) {
                XmlDocument doc = new XmlDocument();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BuildQuerry());
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36";
                doc.Load(request.GetResponse().GetResponseStream());

                XmlElement xRoot = doc.DocumentElement;

                foreach (XmlNode xnode in xRoot) 
                {
                    if (xnode.Attributes.Count > 0)
                    {
                        XmlNode attr = xnode.Attributes.GetNamedItem("geotext");
                        if (attr != null)
                        {
                            attr.Value = EditPolygon(attr.Value);
                        }

                    }
                }
                doc.Save(FileName + ".xml");
            }
        }

        protected override bool CheckParams()
        {
            return true;
        }

        protected string BuildQuerry() 
        {
            return BaseUrl + HttpUtility.UrlEncode(Address) + "&polygon_text=1&format=xml";
        }

        protected string EditPolygon(string coord)
        {
            char[] arrCoord = coord.ToCharArray();
            int len = arrCoord.Length;
            char[] editArr = new char[len];
            int num = 0;
            for (int i = 0; i < arrCoord.Length; i++)
            {
                if (Char.IsDigit(arrCoord[i]) || arrCoord[i] == '.' || arrCoord[i] == ',' || arrCoord[i] == ' ')
                {
                    editArr[num] = arrCoord[i];
                    num++;
                }
            }
            string editStr = new string(editArr);
            string[] editCoordArr = editStr.Split(",");

            string start = coord.Substring(0, coord.IndexOf("("));
            if (start == "POLYGON") { 
                start += "((";
                for (int i = 0; i < editCoordArr.Length; i++)
                {
                    if (i % CoordRate == 0)
                    {
                        start += editCoordArr[i] + ",";
                    }
                }
                start += "))";
                return start;
            }         
            else return coord;
        }
    }
}
