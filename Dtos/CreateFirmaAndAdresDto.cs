namespace WebApplication5.Dtos
{
    public class CreateFirmaAndAdresDto
    {
        public string FirmaAdi { get; set; }
        public Guid? VergiDairesiId { get; set; }
        public string VergiNumarasi { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string WebSitesi { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UlkeId { get; set; }
        public Guid? IlId { get; set; }
        public Guid? IlceId { get; set; }
        public string Sokak { get; set; }
        public string PostaKodu { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }

}
