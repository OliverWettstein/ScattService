using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShipFileService.Services;

namespace ShipFileService.Tests {
  [TestClass]
  public class AutoDeleteFileHostedServiceTests {

    [TestInitialize]
    public void Initialize() {

    }

    [TestMethod]
    public void StartAsync_IntervalZero() {
      var settings = new ShipFileServiceSettings();

      settings.AutoFileDeleteIntervalInMinutes = 0;
      var optionsMock = new Mock<IOptions<ShipFileServiceSettings>>();
      optionsMock.Setup(x => x.Value).Returns(settings);
      var hostedService = new AutoDeleteFileHostedService(serviceProvider: null, optionsMock.Object);

      Assert.ThrowsException<ArgumentException>(() => hostedService.StartAsync(CancellationToken.None));
    }

    [TestMethod]
    public void StartAsync_IntervalNegative() {
      var settings = new ShipFileServiceSettings();

      settings.AutoFileDeleteIntervalInMinutes = -1;
      var optionsMock = new Mock<IOptions<ShipFileServiceSettings>>();
      optionsMock.Setup(x => x.Value).Returns(settings);
      var hostedService = new AutoDeleteFileHostedService(serviceProvider: null, optionsMock.Object);

      Assert.ThrowsException<ArgumentException>(() => hostedService.StartAsync(CancellationToken.None));
    }

    [TestMethod]
    public async Task StopAsync_AfterStartAsync() {
      var services = new ServiceCollection();
      services.AddScoped<IFileSystemAccessRepository, FileSystemAccessRepositoryMock>();

      var serviceProvider = services.BuildServiceProvider();

      var settings = new ShipFileServiceSettings();
      settings.AutoFileDeleteIntervalInMinutes = 5;
      var optionsMock = new Mock<IOptions<ShipFileServiceSettings>>();
      optionsMock.Setup(x => x.Value).Returns(settings);

      var cancellationTokenSource = new CancellationTokenSource();

      var hostedService = new AutoDeleteFileHostedService(serviceProvider, optionsMock.Object);
      await hostedService.StartAsync(cancellationTokenSource.Token);
      await Task.Delay(2000);
      var stopTask = hostedService.StopAsync(cancellationTokenSource.Token);
      Assert.IsFalse(stopTask.IsCompleted);
      cancellationTokenSource.Cancel();
      await Task.Delay(100);
      Assert.IsTrue(stopTask.IsCompleted);
    }
  }
}
