using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace WebApplication5.Dtos
{
    public class DataAccess
    {
        private readonly string _connectionString;

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task InsertFirmaAndAdresAsync(
            string firmaAdi,
            string vergiDairesiAd, // Updated parameter name
            string vergiNumarasi,
            string telefon,
            string email,
            string webSitesi,
            Guid createdBy,
            Guid? ulkeId,
            Guid? ilId,
            Guid? ilceId,
            string sokak,
            string postaKodu,
            bool isActive,
            bool isDefault)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FirmaAdi", firmaAdi);
                parameters.Add("@VergiDairesiAd", vergiDairesiAd); // Updated parameter name
                parameters.Add("@VergiNumarasi", vergiNumarasi);
                parameters.Add("@Telefon", telefon);
                parameters.Add("@Email", email);
                parameters.Add("@WebSitesi", webSitesi);
                parameters.Add("@CreatedBy", createdBy);
                parameters.Add("@UlkeId", ulkeId);
                parameters.Add("@IlId", ilId);
                parameters.Add("@IlceId", ilceId);
                parameters.Add("@Sokak", sokak);
                parameters.Add("@PostaKodu", postaKodu);
                parameters.Add("@IsActive", isActive);
                parameters.Add("@IsDefault", isDefault);

                await dbConnection.ExecuteAsync("InsertFirmaAndAdres", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
