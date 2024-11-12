using Microsoft.Extensions.Options;
using Serilog;

namespace ScattService.Services {
  public class FileSystemAccessRepository : IFileSystemAccessRepository {
    private readonly ScattServiceSettings _options;

    /// <summary>Initializes a new instance of the <see cref="FileSystemAccessRepository" /> class.</summary>
    /// <param name="options">The ScattService options</param>
    public FileSystemAccessRepository(IOptions<ScattServiceSettings> options) {
      _options = options.Value;
      CheckFolder();
    }

    /// <summary>Get the scatt file from the local System</summary>
    /// <param name="fileGuid">The guid/name from the file</param>
    /// <returns>Return a byte struct wich containes the file</returns>
    public async Task<byte[]?> GetFile(string fileGuid) {
      string filePath = Path.Combine(_options.filesLocation, fileGuid);
      if (!File.Exists(filePath)) {
        return null;
      }
      byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
      return fileBytes;
    }

    /// <summary>Save the Scatt file on the local System</summary>
    /// <param name="file">The scatt file to save</param>
    /// <returns>Returns the uri from the saved file</returns>
    public async Task<Guid> SaveFile(IFormFile file) {
      Guid id = Guid.NewGuid();

      var folderPath = _options.filesLocation;

      string filePath = Path.Combine(folderPath, id.ToString());
      using (Stream fileStream = new FileStream(filePath, FileMode.Create)) {
        await file.CopyToAsync(fileStream);
      }

      return id;
    }

    /// <summary>Delete files older than the set duration</summary>
    public async Task DeleteOldFiles(CancellationToken cancellationToken, DateTime fileDateLimit, String fileLocation) {
      await Task.Run(() => {
        DirectoryInfo folder = new DirectoryInfo(fileLocation);
        FileInfo[] files = folder.GetFiles();
        foreach (FileInfo file in files) {
          try {
            cancellationToken.ThrowIfCancellationRequested();
            if (DateTime.Compare(file.CreationTime, fileDateLimit) < 0) {
              Log.Information("Deleting file {FileName}", file.Name);
              file.Delete();
            }
          }
          catch (Exception ex) {
            Log.Error(ex, "Error deleting file {FileName}", file.Name);
          }
        }
      });
    }
    
    /// <summary>Checks if folder exists else create new one</summary>
    public void CheckFolder() {
      var folderPath = _options.filesLocation;
      if (!Directory.Exists(folderPath)) {
        Directory.CreateDirectory(folderPath);
      }
    }
  }
}
