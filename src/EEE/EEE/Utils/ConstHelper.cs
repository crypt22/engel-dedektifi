using System.Globalization;

namespace EEE.Utils
{
    public class ConstHelper
    {
        public const string MongoCnnStr = "MongoCnnStr";
        public const string MongoDBName = "MongoDBName";

        public const string FailMsg = "Bir sorun oluştu lütfen tekrar deneyiniz...";

        private static CultureInfo _cultureTR;
        public static CultureInfo CultureTR
        {
            get { return _cultureTR ?? (_cultureTR = new CultureInfo("tr-TR")); }
        }

        private static CultureInfo _cultureEN;
        public static CultureInfo CultureEN
        {
            get { return _cultureEN ?? (_cultureEN = new CultureInfo("en-US")); }
        }

    }
}