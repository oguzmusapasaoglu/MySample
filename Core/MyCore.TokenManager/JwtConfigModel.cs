namespace MyCore.Common.Token
{
    public class JwtConfigModel
    {
        public const string SectionName = "Jwt";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
}
