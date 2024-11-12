using Microsoft.Extensions.Options;

namespace ScattService.Services {
  public class SaveScattFileService : ISaveScattFileService {
    private readonly IFileSystemAccessRepository _fileSystemAccessService;
    private readonly ScattServiceSettings _options;

    /// <summary>Initializes a new instance of the <see cref="SaveScattFileService" /> class.</summary>
    public SaveScattFileService(IFileSystemAccessRepository fileSystemAccessService, IOptions<ScattServiceSettings> options) {
      _fileSystemAccessService = fileSystemAccessService;
      _options = options.Value;
    }

    /// <param name="file">The scatt file to upload</param>
    public async Task<Uri> SaveScattFile(IFormFile file) {
      Guid fileName = await _fileSystemAccessService.SaveFile(file);
      Uri absoluteUri = CreateUri(fileName);
      return absoluteUri;
    }

    public Uri CreateUri(Guid fileName) {
      Uri baseUri = new Uri(_options.ScattServiceUrl);
      string relativeUri = Constants.DownloadUrlRoute + fileName.ToString();
      Uri absoluteUri = new Uri(baseUri, relativeUri);

      return absoluteUri;
    }
  }
}
