
using System;
using System.Security.Cryptography;
using System.Text; 
using System.Configuration;

namespace SmartLib.Security
{

    /// <summary>
    /// Settings for the encryption.
    /// </summary>
    public class CryptographySettings
    {
        private bool _encrypt = true;
        private string _internalKey = "keyphrase";

        
        /// <summary>
        /// encryption option
        /// </summary>
        /// <param name="encrypt"></param>
        public CryptographySettings()
        {
        }


        /// <summary>
        /// encryption options
        /// </summary>
        /// <param name="encrypt"></param>
        /// <param name="key"></param>
        public CryptographySettings(bool encrypt, string key)
        {
            _encrypt = encrypt;
            _internalKey = key;
        }


        /// <summary>
        /// Whether or not to encrypt;
        /// Primarily used for unit testing.
        /// Default is to encrypt.
        /// </summary>
        public bool Encrypt
        {
            get { return _encrypt; }
        }


        /// <summary>
        /// Key used to encrypt a word.
        /// </summary>
        public string InternalKey
        {
            get { return _internalKey; }
        }
    }
}
