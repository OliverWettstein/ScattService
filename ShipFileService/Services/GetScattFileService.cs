namespace ScattService.Services {
  /// <summary>Download service.</summary>
  public class GetScattFileService : IGetScattFileService {
    private readonly IFileSystemAccessRepository _fileSystemAccessService;

    /// <summary>Initializes a new instance of the <see cref="GetScattFileService" /> class.</summary>
    public GetScattFileService(IFileSystemAccessRepository fileSystemAccessService) {
      _fileSystemAccessService = fileSystemAccessService;
    }

    /// <summary>Download a Scatt file</summary>
    /// <param name="fileGuid">The guid/name from the file</param>
    /// <returns>Return a byte struct wich containes the file</returns>
    public async Task<byte[]?> GetScattFile(string fileGuid) {
      byte[]? fileBytes = await _fileSystemAccessService.GetFile(fileGuid);
      return fileBytes;
    }
  }
}
