using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Net.Http;

namespace p99FileUpdater
{
    public class p99FileUpdaterView
    {
        public String UpdateFileURI;

        public String EQDirectoryPath;

        //Dictionary<String, Tuple<String, String>> listOfFilesFromURI { get; set; }

        public byte[] checksumHashFromFileUrl, checksumHashFromApp;

        public bool? overrideChecksumValidation = false;

        public bool? messageDisplayed = false;

        public String messages = String.Empty;

        public Hashtable fileAndChecksum = new Hashtable();

        public MemoryStream memoryStream;

        public Uri downloadAddress;

        public HttpClient downloadClient;
    }
}
