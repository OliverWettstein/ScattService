namespace ShipFileService.Shared {

  /// <summary>Constants</summary>
  public static class Constants {

    public const String ShipUploadRoute = $"api/ShipUpload";

    public const String ShipDownloadRoute = $"api/ShipDownload";

    public const String UploadUri = $"uploadShipFile";

    public const String DownloadUri = $"downloadShipFile";

    public static String DownloadUrlRoute = $"{DownloadShipFileRoute}/?fileGuid=";

    public static String UploadShipFileRoute => $"{ShipUploadRoute}/{UploadUri}";

    public static String DownloadShipFileRoute => $"{ShipDownloadRoute}/{DownloadUri}";

  }
}
