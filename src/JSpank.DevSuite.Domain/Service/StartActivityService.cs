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
            var messages = new Collection<string>();
            var targetDirectory = new DirectoryInfo(this.AppKeyValue(StartActivityDefinition.AppKeyTargetDirectory));
            var destinationDirectoryTempPath = string.Concat(this.AppKeyValue(StartActivityDefinition.AppKeyDestinationDirectory), Guid.NewGuid(), "/");
            var destinationDirectotyTemp = Directory.CreateDirectory(destinationDirectoryTempPath);

            this.Move_Files_To(targetDirectory, destinationDirectoryTempPath, ref messages);
            this.Move_Directory_To(targetDirectory, destinationDirectoryTempPath, ref messages);

            this.Zip(destinationDirectotyTemp.FullName);
            destinationDirectotyTemp.Delete(true);

            if (messages.Any())
            {
                foreach (var message in messages)
                    Console.WriteLine(StartActivityDefinition.Message, message);

                Console.ReadKey();
            }
        }

        private void Move_Directory_To(DirectoryInfo targetDirectory, string directoryDestination, ref Collection<string> messages)
        {
            if (!targetDirectory.Exists)
                messages.Add(string.Format(StartActivityDefinition.MessageInvalid, targetDirectory.FullName));
            else
            {
                foreach (var value in targetDirectory.GetDirectories(StartActivityDefinition.SearchPatternDirectory))
                {
                    if (this.IgnoreFolders.Contains(value.Name))
                        continue;

                    try
                    {
                        Directory.Move(value.FullName, string.Concat(directoryDestination, "/", value.Name));
                    }
                    catch (Exception exp)
                    {
                        //this.Logger.Write(exp); TODO
                        messages.Add(exp.Message);
                    }
                }
            }
        }

        private void Move_Files_To(DirectoryInfo directoryInfo, string directoryDestination, ref Collection<string> messages)
        {
            if (!directoryInfo.Exists)
                messages.Add(string.Format(StartActivityDefinition.MessageInvalid, directoryInfo.FullName));
            else
            {
                foreach (var value in directoryInfo.GetFiles(StartActivityDefinition.SearchPatternFile))
                {
                    if (this.IgnoreFiles.Contains(value.Name))
                        continue;

                    try
                    {
                        File.Move(value.FullName, string.Concat(directoryDestination, "/", value.Name));
                    }
                    catch (Exception exp)
                    {
                        //this.Logger.Write(exp); TODO
                        messages.Add(exp.Message);
                    }
                }
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
