using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AppUIBasics.Common;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.IO.Compression;
using System.Linq;

namespace p99FileUpdater
{
    /// <summary>
    /// file downloader class
    /// </summary>
    public class p99FileDownloaderViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// ICommand interface included from another repository
        /// </summary>
        public ICommand DownloadFromSetURI { get; }
        /// <summary>
        /// Model for updater
        /// </summary>
        private p99FileUpdaterModel p99fuv = new p99FileUpdaterModel();
        /// <summary>
        /// write message to text box
        /// </summary>
        /// <param name="message"></param>
        private void WriteToTextBoxWithString(String message)
        {
            MessageBox = String.Join(String.Empty, new String[] { message, "\n" , MessageBox});
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        
        private async void DownloadFile()
        {
            if (MessageBox != String.Empty)
                MessageBox = String.Empty;
            WriteToTextBoxWithString($"Operation Enabled: {OperationEnabled.ToString()}");
            if (OperationEnabled == false)
                OperationEnabled = true;
            else
                return;
            WriteToTextBoxWithString($"Operation status: {OperationEnabled.ToString()}.");
            if (ChecksumHashFromFileUrl != default)
                ChecksumHashFromFileUrl = default;
            try
            {
                WriteToTextBoxWithString("creating httpclient");

                HttpClient httpClient = new HttpClient();

                WriteToTextBoxWithString("creating stream object");
                using (Stream response = await httpClient.GetStreamAsync(DownloadAddress.ToString()))
                {
                    MemoryStream memoryStream = new MemoryStream();
                    await response.CopyToAsync(memoryStream);
                    WriteToTextBoxWithString($"Length of stream {memoryStream.Length}.");

                    using (SHA256 memorySha = SHA256.Create())
                    {
                        ChecksumHashFromFileUrl = memorySha.ComputeHash(memoryStream.ToArray());

                        if (OverrideChecksumValidation.HasValue && !OverrideChecksumValidation.Value)
                        {
                            if (Enumerable.Range(0, ChecksumHashFromApp.Length).All(i => ChecksumHashFromApp[i] == ChecksumHashFromFileUrl[i]))
                            {
                                WriteToTextBoxWithString("Checksum values from hashed file match");
                            }
                            else
                            {
                                WriteToTextBoxWithString("Checksum values from hashed file do not match, exiting download and validation.");
                                return;
                            }
                        }
                        //memoryStream is reset to position zero to be read as zip archive after checksuming
                        setStreamAtInitialPosition(ref memoryStream);
                        //ZipArchive is used too read the in memory stream of files
                        ZipArchive za = new ZipArchive(memoryStream, ZipArchiveMode.Read);
                        //loop through each entry in the ZipArchive
                        foreach (ZipArchiveEntry zae in za.Entries)
                        {
                            byte[] fileInMemoryHash = memorySha.ComputeHash(zae.Open());
                            WriteToTextBoxWithString(String.Join(":", "Zip Entry", zae.FullName));
                            String currentFilePath = Path.Combine($"{EQDirectoryPath}{Path.DirectorySeparatorChar}{zae.FullName}");
                            if (Directory.Exists(EQDirectoryPath) && File.Exists(currentFilePath))
                            {
                                byte[] currentByteHash = SHA256.Create().ComputeHash(new FileStream(currentFilePath, FileMode.Open, FileAccess.Read));
                                if (!fileInMemoryHash.Equals(currentByteHash))
                                {
                                    WriteToTextBoxWithString($"{zae.FullName} checksum does not match");
                                    FileStream zipFileArchiveStream = zae.Open() as FileStream;
                                    FileStream fileThatMaybeOverwritten = new FileStream(currentFilePath, FileMode.OpenOrCreate, FileAccess.Write);
                                    if (zipFileArchiveStream.CanRead && !zipFileArchiveStream.Equals(fileThatMaybeOverwritten))
                                    {
                                        WriteToTextBoxWithString($"{fileThatMaybeOverwritten.Name} is not an exact mismatch and is being overwritten.");
                                        if (fileThatMaybeOverwritten.CanWrite)
                                        {
                                            WriteToTextBoxWithString($"{fileThatMaybeOverwritten.Name} can be written to an attempting to write with {zipFileArchiveStream.Name}");
                                            zipFileArchiveStream.CopyTo(fileThatMaybeOverwritten);
                                        }
                                    }
                                }
                                else
                                {
                                    WriteToTextBoxWithString($"{currentFilePath} checksum matches, not writing to file");
                                }
                            }
                        }
                    }

                    WriteToTextBoxWithString(String.Join(",", "Length of stream", memoryStream.Length));
                }
            }
            catch (Exception ex)
            {
                WriteToTextBoxWithString(String.Join(":", new String[] { "Exception", ex.Message, ex.StackTrace }));
            }
            finally
            {
                OperationEnabled = false;
            }
            
            void setStreamAtInitialPosition(ref MemoryStream stream)
            {
                stream.Position = 0;
            }
        }

        public p99FileDownloaderViewModel()
        {
            DownloadFromSetURI = new RelayCommand(() => DownloadFile());
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        bool EnableUpdateButton()
        {
            if (Directory.Exists(p99fuv.EQDirectoryPath) && DownloadAddress.AbsolutePath != String.Empty)
            {
                return true;
            }
            else
            return false;
        }

        public String MessageBox { get => p99fuv.messages; set => SetProperty(ref p99fuv.messages, value); }
        public bool? OperationEnabled { get => p99fuv.operationEnabled; set => SetProperty(ref p99fuv.operationEnabled, value); }
        public string EQDirectoryPath { get => p99fuv.EQDirectoryPath; set => SetProperty(ref p99fuv.EQDirectoryPath, value); }
        public byte[] ChecksumHashFromFileUrl { get => p99fuv.checksumHashFromFileUrl; set => SetProperty(ref p99fuv.checksumHashFromFileUrl, value); }
        public byte[] ChecksumHashFromApp { get => p99fuv.checksumHashFromApp; set => SetProperty(ref p99fuv.checksumHashFromApp, value); }
        public bool? OverrideChecksumValidation { get => p99fuv.overrideChecksumValidation; set => SetProperty(ref p99fuv.overrideChecksumValidation, value); }
        public Uri DownloadAddress { get => p99fuv.downloadAddress; set => SetProperty(ref p99fuv.downloadAddress, value); }
        public bool DisableDownloadButton { get => p99fuv.operationEnabled.HasValue ? !p99fuv.operationEnabled.Value : false; }
    }
}
