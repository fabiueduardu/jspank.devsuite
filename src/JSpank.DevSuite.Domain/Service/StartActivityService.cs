using JSpank.DevSuite.Domain.Abstraction;
using JSpank.DevSuite.Domain.Definition;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace JSpank.DevSuite.Domain.Service
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
            if (string.IsNullOrEmpty(this.AppKeyValue(StartActivityDefinition.AppKeyTargetDirectory)))
                yield return string.Format(StartActivityDefinition.MessageInvalid, "AppKeyTargetDirectoty");

            if (string.IsNullOrEmpty(this.AppKeyValue(StartActivityDefinition.AppKeyDestinationDirectory)))
                yield return string.Format(StartActivityDefinition.MessageInvalid, "AppKeyDestinationDirectory");
        }

        private void Start_Copy()
        {
            var result = new List<string>();
            var targetDirectory = new DirectoryInfo(this.AppKeyValue(StartActivityDefinition.AppKeyTargetDirectory));
            var destinationDirectoryTempPath = string.Concat(this.AppKeyValue(StartActivityDefinition.AppKeyDestinationDirectory), Guid.NewGuid(), "/");
            var destinationDirectotyTemp = Directory.CreateDirectory(destinationDirectoryTempPath);

            result.AddRange(this.Move_Files_To(targetDirectory, destinationDirectoryTempPath));
            result.AddRange(this.Move_Directory_To(targetDirectory, destinationDirectoryTempPath));

            if (result.Any())
                File.WriteAllLines(string.Concat(destinationDirectotyTemp.FullName, StartActivityDefinition.SummaryFileName), result);

            this.Zip(destinationDirectotyTemp.FullName);
            destinationDirectotyTemp.Delete(true);

            foreach (var value in result)
                Console.WriteLine(StartActivityDefinition.Message, value);
        }

        private IEnumerable<string> Move_Directory_To(DirectoryInfo targetDirectory, string directoryDestination)
        {
            foreach (var value in targetDirectory.GetDirectories(StartActivityDefinition.SearchPatternDirectory))
            {
                if (this.IgnoreFolders.Contains(value.Name))
                    continue;

                var fullNAme = value.FullName;
                try
                {
                    foreach (var value_file in value.GetFiles(StartActivityDefinition.SearchPatternFile, SearchOption.AllDirectories))
                        fullNAme += string.Concat(Environment.NewLine, value_file.FullName);

                    Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(value.FullName, string.Concat(directoryDestination, "/", value.Name));
                }
                catch (Exception exp)
                {
                    //this.Logger.Write(exp); TODO
                    fullNAme = string.Concat(fullNAme, Environment.NewLine, exp.Message);
                    continue;
                }

                yield return fullNAme;
            }
        }

        private IEnumerable<string> Move_Files_To(DirectoryInfo directoryInfo, string directoryDestination)
        {
            foreach (var value in directoryInfo.GetFiles(StartActivityDefinition.SearchPatternFile))
            {
                if (this.IgnoreFiles.Contains(value.Name))
                    continue;

                var fullNAme = value.FullName;
                try
                {
                    File.Move(value.FullName, string.Concat(directoryDestination, "/", value.Name));
                }
                catch (Exception exp)
                {
                    //this.Logger.Write(exp); TODO
                    fullNAme = string.Concat(fullNAme, Environment.NewLine, exp.Message);
                    continue;
                }

                yield return fullNAme;
            }
        }

        private void Zip(string sourceDirectoryName)
        {
            var zipFormatName = UtilService.Date.ToString(this.AppKeyValue(StartActivityDefinition.AppKeyDestinationDirectoryFormatDate));
            var zipFileName = string.Concat(this.AppKeyValue(StartActivityDefinition.AppKeyDestinationDirectory), zipFormatName, StartActivityDefinition.ZipExtension);
            var zipCount = 1;

            while (File.Exists(zipFileName))
            {
                zipFileName = string.Concat(this.AppKeyValue(StartActivityDefinition.AppKeyDestinationDirectory), zipFormatName, "_", zipCount, StartActivityDefinition.ZipExtension);
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
                foreach (var value in values.Split(StartActivityDefinition.AppKeySplitDelimiter))
                    yield return value;
        }

        private IEnumerable<string> IgnoreFolders
        {
            get
            {
                return this.AppKeyValueIgnoreItens(StartActivityDefinition.AppKeyIgnoreFolder);
            }
        }

        private IEnumerable<string> IgnoreFiles
        {
            get
            {
                return this.AppKeyValueIgnoreItens(StartActivityDefinition.AppKeyIgnoreFile);
            }
        }

    }
}
