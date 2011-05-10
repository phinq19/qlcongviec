using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace NewProject
{
    public class Encryption
    {
        #region Variables

        private SymmetricAlgorithm objEncrypt;

        public byte[] aPublicKey = new byte[] { 195, 208, 49, 232, 142, 85, 177, 250 };
        public byte[] aPrivateKey = new byte[] { 252, 0, 205, 180, 172, 154, 116, 57 };

        #endregion

        #region Constructor

        public Encryption()
        {
            objEncrypt = SymmetricAlgorithm.Create("DES");
        }

        #endregion

        #region Methods

        // C# to convert a string to a byte[8].
        public byte[] StrToByteArray(string str)
        {
            byte[] arrbyte;
            byte[] arrbyteOut= new byte[8];
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            arrbyte = encoding.GetBytes(str);
            int leng = arrbyte.Length;
            if (leng > 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    arrbyteOut[i] = arrbyte[i];
                }
            }
            return arrbyteOut;
        }

        /// <summary>
        /// This method used to encrypt data
        /// </summary>
        /// <param name="sInputText">string needs to encrypt</param>
        /// <returns>string was encrypted</returns>
        public string EncryptData(string sInputText)
        {
            string sOutputText = "";
            try
            {
                

                if (sInputText == null)
                    sInputText = "";

                if (objEncrypt != null)
                {
                    objEncrypt.IV = aPublicKey;

                    objEncrypt.Key = aPrivateKey;

                    byte[] buf = ASCIIEncoding.ASCII.GetBytes(sInputText);

                    buf = objEncrypt.CreateEncryptor().TransformFinalBlock(buf, 0, buf.Length);

                    sOutputText = Convert.ToBase64String(buf);
                   
                }
                return sOutputText;
            }
            catch (Exception)
            {
                
                return "";
            }

           
        }

        /// <summary>
        /// This method used to decrypt data
        /// </summary>
        /// <param name="sInputText">string needs to decrypt</param>
        /// <returns>string was decrypted</returns>
        public string DecryptData(string sInputText)
        {
            string sOutputText = "";

            try
            {
               
                if (sInputText == null)
                    sInputText = "";

                if (objEncrypt != null)
                {
                    objEncrypt.IV = aPublicKey;

                    objEncrypt.Key = aPrivateKey;

                    byte[] buf = Convert.FromBase64String(sInputText);

                    buf = objEncrypt.CreateDecryptor().TransformFinalBlock(buf, 0, buf.Length);

                    sOutputText = ASCIIEncoding.ASCII.GetString(buf);
                   
                }
                return sOutputText;
            }
            catch (Exception)
            {

                return "";
            }

            
        }


        public string EncryptData(string sInputText, string sKey)
        {
            string sOutputText = "";
            try
            {
                

            if (sInputText == null)
                sInputText = "";
            if (sKey == null)
                sKey = "";
            if (objEncrypt != null)
            {
                objEncrypt.IV = aPublicKey;

                objEncrypt.Key = StrToByteArray(sKey);//aPrivateKey;

                byte[] buf = ASCIIEncoding.ASCII.GetBytes(sInputText+sKey);

                buf = objEncrypt.CreateEncryptor().TransformFinalBlock(buf, 0, buf.Length);

                sOutputText = Convert.ToBase64String(buf);
                
            }
            return sOutputText;
            }
            catch (Exception)
            {

                return "";
            }

           
        }

        /// <summary>
        /// This method used to decrypt data
        /// </summary>
        /// <param name="sInputText">string needs to decrypt</param>
        /// <returns>string was decrypted</returns>
        public string DecryptData(string sInputText, string sKey)
        {
            string sOutputText = "";
            try
            {
               

            if (sInputText == null)
                sInputText = "";
            if (sKey == null)
                sKey = "";
            if (objEncrypt != null)
            {
                objEncrypt.IV = aPublicKey;

                objEncrypt.Key = StrToByteArray(sKey);//aPrivateKey;

                byte[] buf = Convert.FromBase64String(sInputText);

                buf = objEncrypt.CreateDecryptor().TransformFinalBlock(buf, 0, buf.Length);

                sOutputText = ASCIIEncoding.ASCII.GetString(buf);
                sOutputText = sOutputText.Substring(0, sOutputText.Length - sKey.Length);
               
            }
            return sOutputText;
            }
            catch (Exception)
            {

                return "";
            }

           
        }
        #endregion
    }
}
