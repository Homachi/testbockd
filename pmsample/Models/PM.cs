using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Json;
using System.Text;
using System.Threading.Tasks;

namespace pmsample.Models
{
    public class PM
    {
        private string _Station;
        /// <summary>
        /// 측정소
        /// </summary>
        public string Station
        {
            get { return _Station; }
            set { _Station = value; }
        }

        private string _DataTime;
        /// <summary>
        /// 측정일
        /// </summary>
        public string DataTime
        {
            get { return _DataTime; }
            set { _DataTime = value; }
        }

        private string _So2Value;
        /// <summary>
        /// 아황산가스
        /// </summary>
        public string So2Value
        {
            get { return _So2Value; }
            set { _So2Value = value; }
        }

        private string _CoValue;
        /// <summary>
        /// 일산화탄소 농도
        /// </summary>
        public string CoValue
        {
            get { return _CoValue; }
            set { _CoValue = value; }
        }

        private string _O3Value;
        /// <summary>
        /// 오존 농도
        /// </summary>
        public string O3Value
        {
            get { return _O3Value; }
            set { _O3Value = value; }
        }

        private string _No2Value;
        /// <summary>
        /// 이산화질소 농도
        /// </summary>
        public string No2Value
        {
            get { return _No2Value; }
            set { _No2Value = value; }
        }

        private string _PM10Value;
        /// <summary>
        /// PM10 농도
        /// </summary>
        public string PM10Value
        {
            get { return _PM10Value; }
            set { _PM10Value = value; }
        }

        private string _PM25Value;
        /// <summary>
        /// PM2.5 농도
        /// </summary>
        public string PM25Value
        {
            get { return _PM25Value; }
            set { _PM25Value = value; }
        }

        public PM()
        {
            _Station = _DataTime = _So2Value = _CoValue = _O3Value = _No2Value = _PM10Value = _PM25Value = string.Empty;
        }

        public PM(string station)
        {
            GetPMData(station);
        }

        public string GetPMData(string station)
        {
            string url = string.Format("http://openapi.airkorea.or.kr/openapi/services/rest/ArpltnInforInqireSvc/getMsrstnAcctoRltmMesureDnsty?stationName={0}&dataTerm=daily&pageNo=1&numOfRows=10&ServiceKey=m8h98wz065Z5VAAKtsag6u8ReNvXVLBtsuXWlL6j91tyVVpNdh3rJRiiX7WhjLskFCL3AZ4E6MFNXJXjaCUTnQ%3D%3D&ver=1.3&_returnType=json", station);
            string jsonText = string.Empty;
            string encoding = "utf-8";

            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                httpWebRequest.ContentType = "application/json; charset=UTF-8";
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding(encoding));
                jsonText = streamReader.ReadToEnd();
                streamReader.Close();
                httpWebResponse.Close();

                JsonTextParser parser = new JsonTextParser();
                JsonObject configObj = parser.Parse(jsonText);
                JsonObjectCollection config = (JsonObjectCollection)configObj;
                JsonArrayCollection items = (JsonArrayCollection)config["list"];

                try
                {
                    foreach (JsonObjectCollection item in items)
                    {
                        _Station = station;
                        _DataTime = (string)item["dataTime"].GetValue();
                        _So2Value = (string)item["so2Value"].GetValue();
                        _CoValue = (string)item["coValue"].GetValue();
                        _O3Value = (string)item["o3Value"].GetValue();
                        _No2Value = (string)item["no2Value"].GetValue();
                        _PM10Value = (string)item["pm10Value"].GetValue();
                        _PM25Value = (string)item["pm25Value"].GetValue();
                        break;
                    }

                }
                catch (Exception)
                {
                    return null;
                }

            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }
    }
}
