namespace ScattService.Services {
  public interface IGetScattFileService {
    Task<byte[]?> GetScattFile(string fileGuid);
  }
}
