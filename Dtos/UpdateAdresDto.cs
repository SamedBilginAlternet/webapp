namespace WebApplication5.Dtos
{
    public class UpdateAdresDto
    {
        public Guid AdresId { get; set; }
        public Guid FirmaId { get; set; }
        public Guid UlkeId { get; set; }
        public Guid IlId { get; set; }
        public Guid IlceId { get; set; }
        public string FirmaAdi { get; set; }
        public string UlkeAdi { get; set; }
        public string IlAdi { get; set; }
        public string IlceAdi { get; set; } // Ensure this is correct
        public string Sokak { get; set; }
        public string PostaKodu { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public string VergiNumarasi { get; set; }
        public string Telefon { get; set; }
        public string FirmaEmail { get; set; }
        public string WebSitesi { get; set; }
    }
}
