using JSpank.DevSuite.AppInitActivity.Definitions;
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
            if (string.IsNullOrEmpty(this.AppKeyValue(AppInitActivityDefinition.AppKeyTargetDirectory)))
                yield return string.Format(AppInitActivityDefinition.MessageInvalid, "AppKeyTargetDirectoty");

            if (string.IsNullOrEmpty(this.AppKeyValue(AppInitActivityDefinition.AppKeyDestinationDirectory)))
                yield return string.Format(AppInitActivityDefinition.MessageInvalid, "AppKeyDestinationDirectory");
        }

        private void Start_Copy()
        {
            var targetDirectory = new DirectoryInfo(this.AppKeyValue(AppInitActivityDefinition.AppKeyTargetDirectory));
            var destinationDirectoryTempPath = string.Concat(this.AppKeyValue(AppInitActivityDefinition.AppKeyDestinationDirectory), Guid.NewGuid(), "/");
            var destinationDirectotyTemp = Directory.CreateDirectory(destinationDirectoryTempPath);

            this.Copy_Files_To(targetDirectory, destinationDirectoryTempPath);
            this.Copy_Directory_To(targetDirectory, destinationDirectoryTempPath);

            this.Zip(destinationDirectotyTemp.FullName);
            destinationDirectotyTemp.Delete(true);
        }

        private void Copy_Directory_To(DirectoryInfo targetDirectory, string destinationDirectoryTempPath)
        {
            foreach (var value in targetDirectory.GetDirectories(AppInitActivityDefinition.SearchPatternDirectory, SearchOption.AllDirectories))
            {
                if (this.IgnoreFolders.Contains(value.Name))
                    continue;

                var directoryNew = Directory.CreateDirectory(string.Concat(destinationDirectoryTempPath, value.FullName.Replace(targetDirectory.FullName, string.Empty)));
                this.Copy_Files_To(value, directoryNew.FullName);
            }
        }

        private void Copy_Files_To(DirectoryInfo directoryInfo, string directoryDestination)
        {
            foreach (var value in directoryInfo.GetFiles(AppInitActivityDefinition.SearchPatternFile))
            {
                if (this.IgnoreFiles.Contains(value.Name))
                    continue;

                File.Move(value.FullName, string.Concat(directoryDestination, "/", value.Name));
            }
        }

        private void Zip(string sourceDirectoryName)
        {
            var zipFormatName = UtilService.Date.ToString(this.AppKeyValue(AppInitActivityDefinition.AppKeyDestinationDirectoryFormatDate));
            var zipFileName = string.Concat(this.AppKeyValue(AppInitActivityDefinition.AppKeyDestinationDirectory), zipFormatName, AppInitActivityDefinition.ZipExtension);
            var zipCount = 1;

            while (File.Exists(zipFileName))
            {
                zipFileName = string.Concat(this.AppKeyValue(AppInitActivityDefinition.AppKeyDestinationDirectory), zipFormatName, "_", zipCount, AppInitActivityDefinition.ZipExtension);
                zipCount++;
            }

            ZipFile.CreateFromDirectory(sourceDirectoryName, zipFileName);
        }

        private string AppKeyValue(string key)
        {
            return ConfigurationManager.AppSettings[key] as string;
        }

        private IEnumerable<string> AppKeyValueIgnoreItens(string key)
        {
            var values = this.AppKeyValue(key);
            if (!string.IsNullOrEmpty(values))
                foreach (var value in values.Split(AppInitActivityDefinition.AppKeySplitDelimiter))
                    yield return value;
        }

        private IEnumerable<string> IgnoreFolders
        {
            get
            {
                return this.AppKeyValueIgnoreItens(AppInitActivityDefinition.AppKeyIgnoreFolder);
            }
        }

        private IEnumerable<string> IgnoreFiles
        {
            get
            {
                return this.AppKeyValueIgnoreItens(AppInitActivityDefinition.AppKeyIgnoreFile);
            }
        }

    }
}
