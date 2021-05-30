namespace AndresFlorez.RedSocial.Api.Models
{
    public class AppSettings
    {
        public string JWTSecret { get; set; }
        public string JWTAudience { get; set; }
        public string JWTIssuer { get; set; }

        public string DIRFolderVideos { get; set; }
        public string DIRFolderImagenes { get; set; }
        public string DIRFolderArchivos { get; set; }
    }
}

