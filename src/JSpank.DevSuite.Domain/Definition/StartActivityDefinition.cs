namespace JSpank.DevSuite.Domain.Definition
{
    public sealed class StartActivityDefinition
    {
        public const string SearchPatternDirectory = "*";
        public const string SearchPatternFile = SearchPatternDirectory;
        public const string ZipExtension = ".zip";
        public const char AppKeySplitDelimiter = ';';

        public const string AppKeyTargetDirectory = "AppInitActivity.TargetDirectory";
        public const string AppKeyDestinationDirectory = "AppInitActivity.DestinationDirectory";
        public const string AppKeyDestinationDirectoryFormatDate = "AppInitActivity.DestinationDirectoryFormatDate";
        public const string AppKeyIgnoreFile = "AppInitActivity.IgnoreFile";
        public const string AppKeyIgnoreFolder = "AppInitActivity.IgnoreFolder";

        public const string MessageInvalid = "{0} is inválid";
        public const string Message = "Message: {0}";
    }
}
