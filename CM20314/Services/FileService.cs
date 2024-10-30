namespace CM20314.Services
{
    /// <summary>
    /// Handles local file read operations to extract raw data for database initialisation
    /// </summary>
    public class FileService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public FileService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Reads files with a given name and location
        /// </summary>
        /// <param name="relativePath">Relative path</param>
        /// <param name="root">Absolute root</param>
        /// <param name="extension">File extension</param>
        /// <returns>A list of string lines read from the file</returns>
        public List<string> ReadLinesFromFileWithName(string relativePath, string root = Constants.SourceFilePaths.ROOT, string extension = Constants.SourceFilePaths.FILE_EXTENSION_DEFAULT)
        {
            return ReadLinesFromFile(GetPath(relativePath, root, extension));
        }

        /// <summary>
        /// Constructs a path from the relative path, root and extension
        /// </summary>
        /// <param name="relativePath">Relative path</param>
        /// <param name="root">Absolute root</param>
        /// <param name="extension">File extension</param>
        /// <returns>Absolute path</returns>
        public string GetPath(string relativePath, string root = Constants.SourceFilePaths.ROOT, string extension = Constants.SourceFilePaths.FILE_EXTENSION_DEFAULT)
        {
            string rootPath = _hostingEnvironment.ContentRootPath;
            string filePath = Path.Combine(rootPath, root, relativePath + extension);
            return filePath;
        }

        /// <summary>
        /// Reads files with a given name and location
        /// </summary>
        /// <param name="path">Absolute path</param>
        /// <returns>A list of string lines read from the file</returns>
        public List<string> ReadLinesFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            return lines.Select(line => line.Trim()).ToList();
        }

        /// <summary>
        /// Checks if a folder exists for a particular floor of a building
        /// </summary>
        /// <param name="building">Building to check</param>
        /// <param name="floor">Floor to check</param>
        /// <returns>True if the folder exists, otherwise False</returns>
        public bool FolderExistsForFloor(string building, int floor)
        {
            return Directory.Exists(GetPath(building + Constants.SourceFilePaths.BUILDING_FLOOR_SEPARATOR + floor, extension: string.Empty));
        }
    }
}
