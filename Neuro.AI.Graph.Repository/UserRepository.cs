namespace Neuro.AI.Graph.Repository;

using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

public class UserDto
{
	public string? UserName { get; set; }
	public string? NickName { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Email { get; set; }
	public bool EmailConfirmed { get; set; }
	public string? PersonalId { get; set; }
	public string? PhoneNumber { get; set; }
	public string? MobilePhoneNumber { get; set; }
}


public class UserRepository
{
	private const string ConnectionString = "Server=tcp:tropigas-development.database.windows.net,1433;Database=tropigas-mobile;User=tropi-cloud-adm_db_dev;Password=&AKD@S*jT6#G_By6Dg@.eE->dv;";

	public async Task<IEnumerable<UserDto>> GetAllUsersAsync(DateTime? dateSearch = null, int? filters = null)
	{
		using var connection = new SqlConnection(ConnectionString);

		var parameters = new DynamicParameters();
		
		parameters.Add("@dateSearh", dateSearch, DbType.DateTime);
		parameters.Add("@filters", filters, DbType.Int32);

		return await connection.QueryAsync<UserDto>(
			"sp_s_AllUsers",
			parameters,
			commandType: CommandType.StoredProcedure
		);
	}

}
