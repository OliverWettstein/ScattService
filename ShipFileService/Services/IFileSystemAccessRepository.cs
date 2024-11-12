namespace ScattService.Services {
  public interface IFileSystemAccessRepository {
    Task<byte[]?> GetFile(string fileGuid);

    Task<Guid> SaveFile(IFormFile file);

  }
}
