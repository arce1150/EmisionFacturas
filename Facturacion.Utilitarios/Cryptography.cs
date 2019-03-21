using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace Facturacion.Utilitarios
{
    /// <summary>
    /// Clase que proporciona servicios criptográficos de texto, entre los que se incluyen la codificación y descodificación segura de los datos y otras muchas operaciones como.
    /// </summary>
    public class Cryptography
    {
        #region PROPIEDADES NUEVAS
        /// <summary>
        /// La clave secreta que se va a utilizar para el algoritmo simétrico. 
        /// </summary>
        private byte[] Key
        {
            get { return new byte[] { 21, 16, 2, 10, 20, 8, 9, 7, 24, 29, 1, 23, 13, 3, 17, 15, 14, 1, 19, 6, 18, 5, 12, 20 }; }
        }

        /// <summary>
        /// El vector de inicialización que se va a utilizar para el algoritmo simétrico. 
        /// </summary>
        private byte[] IV
        {
            get { return new byte[] { 178, 109, 219, 26, 69, 200, 65, 68 }; }
        }

        #endregion

        #region PROCEDIMIENTOS NUEVOS
        /// <summary>
        /// Encripta una cadena de texto usando el algoritmo TripleDESCryptoServiceProvider.
        /// </summary>
        /// <param name="strData">Texto que se va a encriptar.</param>
        /// <returns></returns>
        public byte[] Encrypt(string strData)
        {
            var objCryptoService = new TripleDESCryptoServiceProvider();
            var objMemoryStream = new MemoryStream();

            var objCryptoStream = new CryptoStream(objMemoryStream, new TripleDESCryptoServiceProvider().CreateEncryptor(Key, IV), CryptoStreamMode.Write);
            byte[] toEncrypt = new ASCIIEncoding().GetBytes(strData);

            objCryptoStream.Write(toEncrypt, 0, toEncrypt.Length);
            objCryptoStream.FlushFinalBlock();
            byte[] rect = objMemoryStream.ToArray();

            objMemoryStream.Close();
            objCryptoStream.Close();
            objCryptoService.Clear();

            return rect;
        }

        /// <summary>
        /// Encripta la cadeba
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public string EncryptToString(string strData)
        {
            byte[] rect = Encrypt(strData);
            //System.Text.Encoding encoding = new UTF7Encoding();
            //var encodedBytes = encoding.GetString(rect);

            //return encodedBytes;

            var cookieString = new StringBuilder();
            foreach (byte item in rect)
            {
                cookieString.Append(item);
                cookieString.Append(",");
            }
            cookieString = cookieString.Remove(cookieString.Length - 1, 1);
            return cookieString.ToString();
        }

        /// <summary>
        /// Descencripta una secuencia de bytes usando el algoritmo TripleDESCryptoServiceProvider.
        /// </summary>
        /// <param name="bytInput">Secuencia de bytes que se va a descencriptar.</param>
        /// <returns>Cadena descencriptada</returns>
        public string Decrypt(byte[] bytInput)
        {
            var objMemoryStream = new MemoryStream(bytInput);
            var objCryptoStream = new CryptoStream(objMemoryStream, new TripleDESCryptoServiceProvider().CreateDecryptor(Key, IV), CryptoStreamMode.Read);
            var fromEncrypt = new byte[bytInput.Length];

            objCryptoStream.Read(fromEncrypt, 0, fromEncrypt.Length);
            objMemoryStream.Close();
            objCryptoStream.Close();

            return new ASCIIEncoding().GetString(fromEncrypt).TrimEnd(new[] { '\0' });
        }

        /// <summary>
        /// Descencripta una secuencia de bytes usando el algoritmo TripleDESCryptoServiceProvider
        /// </summary>
        /// <param name="inputString">Cadena que se va a descencriptar, el cual debe estra como texto </param>
        /// <returns>Cadena descencriptada</returns>
        public string DecryptString(string inputString)
        {
            if (string.IsNullOrEmpty(inputString)) return string.Empty;

            //var key = System.Text.Encoding.UTF7.GetBytes(inputString);
            //return Decrypt(key);

            var inputSplit = inputString.Split(',');
            var inputBytes = new byte[inputSplit.Length];

            int index = 0;
            foreach (string item in inputSplit)
            {
                inputBytes[index] = byte.Parse(item);
                index += 1;
            }

            return Decrypt(inputBytes);
        }

        /// <summary>
        /// Genera una contraseña aleatoria.
        /// </summary>
        /// <param name="bytCantidadCarecteres">Cantidad de caracteres que debe tener la contraseña, el máximo es 10.</param>
        /// <returns></returns>
        public static string RandomPassword(byte bytCantidadCarecteres = 6)
        {
            if (bytCantidadCarecteres > 10) bytCantidadCarecteres = 10;
            var strGuid = System.Guid.NewGuid().ToString().ToUpper();
            var newGuid = strGuid.Replace("-", "");
            return newGuid.Substring(0, bytCantidadCarecteres);
        }

        #endregion
        #region[ENCRIPTACION]
        private Byte[] _Key = {21,185,215,104,
                               153,64,168,197,
                               170,171,152,179,
                               142,248,106,190,
                               135,87,63,199,
                               77,146,55,113,
                               81,81,197,125,
                               113,195,62,243};
        private Byte[] _IV = { 89,234,63,78,
                              175,59,253,31,
                              21,23,244,102,
                              233,173,175,242};

        public String Encriptar(String inputText)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(inputText);
            byte[] encripted;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes.Length))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(_Key, _IV), CryptoStreamMode.Write))
                {
                    objCryptoStream.Write(inputBytes, 0, inputBytes.Length); objCryptoStream.FlushFinalBlock(); objCryptoStream.Close();
                }
                encripted = ms.ToArray();
            }
            return Convert.ToBase64String(encripted);
        }
        public String Desencriptar(String inputText)
        {
            byte[] inputBytes = Convert.FromBase64String(inputText);
            byte[] resultBytes = new byte[inputBytes.Length];
            string textoLimpio = String.Empty;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(_Key, _IV), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(objCryptoStream, true))
                    {
                        textoLimpio = sr.ReadToEnd();
                    }
                }
            }
            return textoLimpio;
        }
        #endregion
    }
}
