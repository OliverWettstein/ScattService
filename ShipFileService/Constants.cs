namespace ScattService {

  /// <summary>Constants</summary>
  public static class Constants {

    public const String ScattUploadRoute = $"api/ScattUpload";

    public const String ScattDownloadRoute = $"api/ScattDownload";

    public const String UploadUri = $"uploadScattFile";

    public const String DownloadUri = $"downloadScattFile";

    public const String TestUri = $"test";

    public static String DownloadUrlRoute = $"{DownloadScattFileRoute}/?fileGuid=";

    public static String UploadScattFileRoute => $"{ScattUploadRoute}/{UploadUri}";

    public static String DownloadScattFileRoute => $"{ScattDownloadRoute}/{DownloadUri}";

  }
}
