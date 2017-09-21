using JSpank.DevSuite.Domain.Abstraction;
using JSpank.DevSuite.Domain.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace JSpank.DevSuite.AppInitActivity.Services
{
    public class StartActivityService : IStartActivityService
    {
        readonly ILogger Logger;

        const string searchPatternDirectory = "*";
        const string searchPatternFile = searchPatternDirectory;
        const string zipExtension = ".zip";
        const string appKeyTargetDirectory = "AppInitActivity.TargetDirectory";
        const string appKeyDestinationDirectory = "AppInitActivity.DestinationDirectory";
        const string appKeyDestinationDirectoryFormatDate = "AppInitActivity.DestinationDirectoryFormatDate";

        private string AppKeyTargetDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings[appKeyTargetDirectory];
            }
        }

        private string AppKeyDestinationDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings[appKeyDestinationDirectory];
            }
        }

        private string AppKeyDestinationDirectoryFormatDate
        {
            get
            {
                return ConfigurationManager.AppSettings[appKeyDestinationDirectoryFormatDate];
            }
        }

        public StartActivityService(ILogger Logger)
        {
            this.Logger = Logger;
        }

        public void Start()
        {
            var validateMessages = this.Validate();

            foreach (var value in validateMessages)
                Console.WriteLine(value);

            if (!validateMessages.Any())
                this.Start_Copy();
        }

        private IEnumerable<string> Validate()
        {
            if (string.IsNullOrEmpty(this.AppKeyTargetDirectory))
                yield return "AppKeyTargetDirectoty is inválid!";

            if (string.IsNullOrEmpty(this.AppKeyDestinationDirectory))
                yield return "AppKeyDestinationDirectoty is inválid!";
        }

        private void Start_Copy()
        {
            var targetDirectory = new DirectoryInfo(this.AppKeyTargetDirectory);
            var destinationDirectotyTempPath = string.Concat(this.AppKeyDestinationDirectory, Guid.NewGuid(), "/");
            var destinationDirectotyTemp = Directory.CreateDirectory(destinationDirectotyTempPath);


            this.Copy_Files_To(targetDirectory, destinationDirectotyTempPath);
            foreach (var value in targetDirectory.GetDirectories(searchPatternDirectory, SearchOption.AllDirectories))
            {
                var directoryNew = Directory.CreateDirectory(string.Concat(destinationDirectotyTempPath, value.FullName.Replace(targetDirectory.FullName, string.Empty)));
                this.Copy_Files_To(value, directoryNew.FullName);
            }

            this.Zip(destinationDirectotyTemp.FullName);
            destinationDirectotyTemp.Delete(true);
        }

        private void Copy_Files_To(DirectoryInfo directoryInfo, string directoryDestination)
        {
            foreach (var value in directoryInfo.GetFiles(searchPatternFile))
                File.Copy(value.FullName, string.Concat(directoryDestination, "/", value.Name));
        }

        void Zip(string sourceDirectoryName)
        {
            var zipFormatName = UtilService.Date.ToString(this.AppKeyDestinationDirectoryFormatDate);
            var zipFileName = string.Concat(this.AppKeyDestinationDirectory, zipFormatName, zipExtension);
            var zipCount = 1;

            while (File.Exists(zipFileName))
            {
                zipFileName = string.Concat(this.AppKeyDestinationDirectory, zipFormatName, "_", zipCount, zipExtension);
                zipCount++;
            }

            ZipFile.CreateFromDirectory(sourceDirectoryName, zipFileName);
        }
    }
}
