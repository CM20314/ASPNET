namespace CM20314.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public FileService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public List<string> ReadLinesFromFileWithName(string relativePath, string root = Constants.SourceFilePaths.ROOT, string extension = Constants.SourceFilePaths.FILE_EXTENSION_DEFAULT)
        {
            string rootPath = _hostingEnvironment.ContentRootPath;
            string filePath = Path.Combine(rootPath, root, relativePath + extension);

            return ReadLinesFromFile(filePath);
        }

        public List<string> ReadLinesFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            return lines.Select(line => line.Trim()).ToList();
        }
    }
}
