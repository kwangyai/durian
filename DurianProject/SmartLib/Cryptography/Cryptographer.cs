
using System;
using System.Security.Cryptography;
using System.Text; 
using System.Configuration;

namespace SmartLib.Security
{

    /// <summary>
    /// Cryptography service to encrypt and decrypt strings.
    /// </summary>
    public class Cryptographer
	{
        private static ICryptographyService _provider;


        /// <summary>
        /// Create default instance of symmetric cryptographer.
        /// </summary>
        static Cryptographer()
        {
            _provider = new CryptographerServiceSymmetric();
        }


        /// <summary>
        /// Initialize to new provider.
        /// </summary>
        /// <param name="service"></param>
        public static void Init(ICryptographyService service)
        {
            _provider = service;
        }


        /// <summary>
        /// Get reference to current encryption provider.
        /// </summary>
        public static ICryptographyService Provider
        {
            get { return _provider; }
        }        


		/// <summary>
		/// Encrypts the plaintext using an internal private key.
		/// </summary>
		/// <param name="plaintext">The text to encrypt.</param>
		/// <returns>An encrypted string in base64 format.</returns>
		public static string Encrypt( string plaintext )
		{
            return _provider.Encrypt(plaintext);
		}


		/// <summary>
		/// Decrypts the base64key using an internal private key.
		/// </summary>
		/// <param name="base64Text">The encrypted string in base64 format.</param>
		/// <returns>The plaintext string.</returns>
        public static string Decrypt( string base64Text )
		{
            return _provider.Decrypt(base64Text);
		}


        /// <summary>
        /// Determine if the plain text and encrypted are ultimately the same.
        /// </summary>
        /// <param name="encrypted"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static bool IsMatch(string encrypted, string plainText)
        {
            return _provider.IsMatch(encrypted, plainText);
        }
	}
}
