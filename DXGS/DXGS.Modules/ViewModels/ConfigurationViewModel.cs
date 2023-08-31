using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DXGS.Common;
using DXGS.Modules.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DXGS.Modules.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase, IDocumentModule, ISupportState<ConfigurationViewModel.Info>
    {
        public string Caption { get; private set; }
        public virtual bool IsActive { get; set; }

        public static ConfigurationViewModel Create(string caption)
        {
            return ViewModelSource.Create(() => new ConfigurationViewModel()
            {
                Caption = caption,
            });
        }

        public ConfigurationModel Config
        {
            get { return GetProperty(() => Config); }
            set { SetProperty(() => Config, value); }
        }

        protected override void OnInitializeInDesignMode()
        {
            base.OnInitializeInDesignMode();
            Config = new ConfigurationModel();
        }

        protected override void OnInitializeInRuntime()
        {
            base.OnInitializeInRuntime();
            Config = new ConfigurationModel();
        }

        //public ConfigurationViewModel()
        //{
        //    Config = new ConfigurationModel();
        //}

        // Add methods for encryption and decryption
        private string Encrypt(string value)
        {
            string key = "YourEncryptionKey"; // Replace with your own encryption key
            byte[] encryptedBytes;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] valueBytes = Encoding.UTF8.GetBytes(value);

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(valueBytes, 0, valueBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        encryptedBytes = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        private string Decrypt(string value)
        {
            string key = "YourEncryptionKey"; // Replace with your own encryption key
            byte[] decryptedBytes;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                byte[] encryptedBytes = Convert.FromBase64String(value);

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        decryptedBytes = memoryStream.ToArray();
                    }
                }
            }

            return Encoding.UTF8.GetString(decryptedBytes);
        }

        #region Serialization

        [Serializable]
        public class Info
        {
            public string Caption { get; set; }
        }

        Info ISupportState<Info>.SaveState()
        {
            return new Info()
            {
                Caption = this.Caption,
            };
        }

        void ISupportState<Info>.RestoreState(Info state)
        {
            this.Caption = state.Caption;
        }

        #endregion Serialization
    }
}