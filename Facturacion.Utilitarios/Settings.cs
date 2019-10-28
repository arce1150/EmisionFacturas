using System; 
using System.ComponentModel;
using System.Configuration; 
namespace Facturacion.Utilitarios
{
    public class Settings
    {
        public static T Get<T>(string key)
        {
            var appSetting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(appSetting)) return default(T);

            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)(converter.ConvertFromInvariantString(appSetting));
        } 
        public static string conexion
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            }
        }
        public static string ApiUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["Api.Url"].ToString();
            }
        }
        public static string ApiToken
        {
            get
            {
                return ConfigurationManager.AppSettings["Api.Token"].ToString();
            }
        }
    }
}
