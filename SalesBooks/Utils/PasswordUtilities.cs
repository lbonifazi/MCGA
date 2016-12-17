using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Utils
{
    public class PasswordUtilities
    {
        #region Constants
        private static readonly char[] LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static readonly char[] NUMBERS = "1234567890".ToCharArray();
        private static readonly char[] SYMBOLS = "!@#$%&*()_-+=".ToCharArray();
        private static byte[] _salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");
        private static readonly string sharedSecret = ConfigurationManager.AppSettings["encryptionKey"].ToString();
        #endregion

        #region Class Members
        private static RNGCryptoServiceProvider _Random = new RNGCryptoServiceProvider();
        private static byte[] _bytes = new byte[4];
        #endregion

        #region Members
        int m_minimumLength, m_maximumLength;
        bool m_includeUpper, m_includeLower, m_includeNumber, m_includeSpecial;
        string[] m_characterTypes;
        #endregion

        #region Enums
        enum CharacterType
        {
            Uppercase,
            Lowercase,
            Special,
            Number
        }
        #endregion

        #region Properties
        public bool IncludeUpper
        {
            get
            {
                return m_includeUpper;
            }
            set
            {
                m_includeUpper = value;
            }
        }

        public bool IncludeLower
        {
            get
            {
                return m_includeLower;
            }
            set
            {
                m_includeLower = value;
            }
        }

        public bool IncludeNumber
        {
            get
            {
                return m_includeNumber;
            }
            set
            {
                m_includeNumber = value;
            }
        }

        public bool IncludeSpecial
        {
            get
            {
                return m_includeSpecial;
            }
            set
            {
                m_includeSpecial = value;
            }
        }

        public int MinimumLength
        {
            get
            {
                return m_minimumLength;
            }
            set
            {
                if (value > m_maximumLength)
                {
                    throw new ArgumentOutOfRangeException("MinimumLength must be greater than MaximumLength");
                }
                m_minimumLength = value;
            }
        }

        public int MaximumLength
        {
            get
            {
                return m_maximumLength;
            }
            set
            {
                if (value < m_minimumLength)
                {
                    throw new ArgumentOutOfRangeException("MaximumLength must be greater than MinimumLength");
                }
                m_maximumLength = value;
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance for generating alphanumeric passwords.
        /// </summary>
        public PasswordUtilities()
        {
            m_includeSpecial = false;
            m_includeNumber = true;
            m_includeUpper = true;
            m_includeLower = true;
        }

        /// <summary>
        /// Creates an instance for generating passwords including the selected character types.
        /// </summary>
        public PasswordUtilities(bool includeSpecial, bool includeNumber, bool includeUpper, bool includeLower)
            : this()
        {
            m_includeNumber = includeNumber;
            m_includeSpecial = includeSpecial;
            m_includeUpper = includeUpper;
            m_includeLower = includeLower;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Randomly creates a password with size between minimum and maximum values.
        /// </summary>
        /// <returns>A random string of characters.</returns>
        public string GeneratePassword(int minimumLength, int maximumLength)
        {
            m_characterTypes = GetCharacterTypes();

            StringBuilder password = new StringBuilder(maximumLength);

            //Get a random length for the password.
            int currentPasswordLength = maximumLength;//RandomUtilities.Next(m_maximumLength);

            //Only allow for passwords greater than or equal to the minimum length.
            /*if (currentPasswordLength < m_minimumLength)
            {
                currentPasswordLength = m_minimumLength;
            }*/

            //Generate the password
            for (int i = 0; i < currentPasswordLength; i++)
            {
                password.Append(GetCharacter());
            }

            return password.ToString();
        }

        /// <summary>
        /// Randomly creates a password with a length of 6 characters.
        /// </summary>
        /// <returns>A random string of characters.</returns>
        public string GeneratePassword()
        {
            return this.GeneratePassword(6, 6);
        }

        /// <summary>
        /// Determines which character types should be used to generate
        /// the current password.
        /// </summary>
        /// <returns>A string[] of character that should be used to generate the current password.</returns>
        private string[] GetCharacterTypes()
        {
            ArrayList characterTypes = new ArrayList();
            foreach (string characterType in Enum.GetNames(typeof(CharacterType)))
            {
                CharacterType currentType = (CharacterType)Enum.Parse(typeof(CharacterType), characterType, false);
                bool addType = false;
                switch (currentType)
                {
                    case CharacterType.Lowercase:
                        addType = IncludeLower;
                        break;
                    case CharacterType.Number:
                        addType = IncludeNumber;
                        break;
                    case CharacterType.Special:
                        addType = IncludeSpecial;
                        break;
                    case CharacterType.Uppercase:
                        addType = IncludeUpper;
                        break;
                }
                if (addType)
                {
                    characterTypes.Add(characterType);
                }
            }
            return (string[])characterTypes.ToArray(typeof(string));
        }

        /// <summary>
        /// Randomly determines a character type to return from the 
        /// available CharacterType enum.
        /// </summary>
        /// <returns>The string character to append to the password.</returns>
        private string GetCharacter()
        {
            string characterType = m_characterTypes[PasswordUtilities.Next(m_characterTypes.Length)];
            CharacterType typeToGet = (CharacterType)Enum.Parse(typeof(CharacterType), characterType, false);
            switch (typeToGet)
            {
                case CharacterType.Lowercase:
                    return LETTERS[PasswordUtilities.Next(LETTERS.Length)].ToString().ToLower();
                case CharacterType.Uppercase:
                    return LETTERS[PasswordUtilities.Next(LETTERS.Length)].ToString().ToUpper();
                case CharacterType.Number:
                    return NUMBERS[PasswordUtilities.Next(NUMBERS.Length)].ToString();
                case CharacterType.Special:
                    return SYMBOLS[PasswordUtilities.Next(SYMBOLS.Length)].ToString();
            }
            return null;
        }

        /// <summary>
        /// Randomly selects an int value
        /// </summary>
        /// <returns>A randomly selected int value.</returns>
        public static int Next(int max)
        {
            if (max <= 0)
            {
                throw new ArgumentOutOfRangeException("max");
            }
            _Random.GetBytes(_bytes);
            int value = BitConverter.ToInt32(_bytes, 0) % max;
            if (value < 0)
            {
                value = -value;
            }
            return value;
        }
        #endregion

        #region Ecryption

        public static bool ComparePassword(string plainText, string encryptedText)
        {
            string decrypText = DecryptStringAES(encryptedText);

            if (decrypText != null && plainText == decrypText)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Encrypt the given string using AES.  The string can be decrypted using 
        /// DecryptStringAES().  The sharedSecret parameters must match.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for encryption.</param>
        public static string EncryptStringAES(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("plainText");
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("sharedSecret");

            string outStr = null;// Encrypted string to return
            RijndaelManaged aesAlg = null;// RijndaelManaged object used to encrypt the data.

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                // Create a RijndaelManaged object
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // prepend the IV
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                    }
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return outStr;
        }

        /// <summary>
        /// Decrypt the given string.  Assumes the string was encrypted using 
        /// EncryptStringAES(), using an identical sharedSecret.
        /// </summary>
        /// <param name="cipherText">The text to decrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for decryption.</param>
        public static string DecryptStringAES(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException("cipherText");
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("sharedSecret");

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                // Create the streams used for decryption.                
                byte[] bytes = Convert.FromBase64String(cipherText);
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    // Get the initialization vector from the encrypted stream
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }

        private static byte[] ReadByteArray(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
        
        #endregion
    }
}
