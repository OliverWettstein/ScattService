namespace ScattService.Services {
  public interface ISaveScattFileService {
    Task<Uri> SaveScattFile(IFormFile file);
  }
}
