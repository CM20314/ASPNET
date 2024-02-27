namespace CM20314.Services
{
    // Handles local file read operations to extract raw data for database initialisation
    public class FileService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public FileService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public List<string> ReadLinesFromFileWithName(string relativePath, string root = Constants.SourceFilePaths.ROOT, string extension = Constants.SourceFilePaths.FILE_EXTENSION_DEFAULT)
        {
            return ReadLinesFromFile(GetPath(relativePath, root, extension));
        }

        public string GetPath(string relativePath, string root = Constants.SourceFilePaths.ROOT, string extension = Constants.SourceFilePaths.FILE_EXTENSION_DEFAULT)
        {
            string rootPath = _hostingEnvironment.ContentRootPath;
            string filePath = Path.Combine(rootPath, root, relativePath + extension);
            return filePath;
        }

        public List<string> ReadLinesFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            return lines.Select(line => line.Trim()).ToList();
        }

        public bool FolderExistsForFloor(string building, int floor)
        {
            return Directory.Exists(GetPath(building + Constants.SourceFilePaths.BUILDING_FLOOR_SEPARATOR + floor, extension: string.Empty));
        }
    }
}
