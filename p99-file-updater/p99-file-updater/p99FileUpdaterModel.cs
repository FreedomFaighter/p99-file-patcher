﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Net.Http;

namespace p99FileUpdater
{
    public class p99FileUpdaterModel
    {
        internal p99FileUpdaterModel() {
            EQDirectoryPath = default;
            checksumHashFromApp = default;
            checksumHashFromFileUrl = default;
            overrideChecksumValidation = false;
            operationEnabled = false;
            messages = String.Empty;
            downloadAddress = default;
        }
        public String EQDirectoryPath;

        //Dictionary<String, Tuple<String, String>> listOfFilesFromURI { get; set; }

        public byte[] checksumHashFromFileUrl, checksumHashFromApp;

        public bool? overrideChecksumValidation = false;

        public bool? operationEnabled = false;

        public String messages = String.Empty;

        public Uri downloadAddress;
    }
}
