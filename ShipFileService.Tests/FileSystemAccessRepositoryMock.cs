using Microsoft.AspNetCore.Http;
using ShipFileService.Services;

namespace ShipFileService.Tests {
	public class FileSystemAccessRepositoryMock : IFileSystemAccessRepository {

		/// <summary>Initializes a new instance of the <see cref="FileSystemAccessRepositoryMock" /> class.</summary>
		public FileSystemAccessRepositoryMock() {
		}  

		public async Task<Guid> SaveFile(IFormFile file) {
			Guid id = Guid.NewGuid();

			return id;
		}

		public async Task DeleteOldFiles(CancellationToken cancellationToken, DateTime fileDateLimit, String fileLocation) {
			try {
        await Task.Run(() => Task.Delay(4000/*, cancellationToken*/)); //cancellationToken omitted to simulate a faulty methode.
      } catch (TaskCanceledException) {
        cancellationToken.ThrowIfCancellationRequested();
			}
		}

		public void CheckFolder() {
			throw new NotImplementedException();
		}

		public async Task<byte[]?> GetFile(string fileGuid) {
      throw new NotImplementedException();
    }
	}
}
