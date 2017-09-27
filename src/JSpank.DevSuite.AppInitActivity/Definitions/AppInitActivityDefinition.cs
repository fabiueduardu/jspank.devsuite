namespace JSpank.DevSuite.AppInitActivity.Definitions
{
    public sealed class AppInitActivityDefinition
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
    }
}
