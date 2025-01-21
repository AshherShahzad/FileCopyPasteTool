namespace FileCopyAutomationTool.Models
{
    public class FileCopyModel
    {
        public string SourcePath { get; set; } = string.Empty; // Default value to avoid null
        public string DestinationPath { get; set; } = string.Empty; // Default value
        public string ResultMessage { get; set; } = string.Empty; // Default value
    }

}
