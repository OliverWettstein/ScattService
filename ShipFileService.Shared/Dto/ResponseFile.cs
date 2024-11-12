
using Tools.WebClient;

namespace ShipFileService.Shared.Dto {
  /// <summary>The file that is given by the ship service</summary>
  public class ResponseFileDto {
    /// <summary>Gets or sets the file.</summary>
    /// <value>The file from ship service</value>
    public required FileData ResponseFile { get; set; }
  }
}
