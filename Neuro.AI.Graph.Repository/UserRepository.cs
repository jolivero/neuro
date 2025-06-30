namespace Neuro.AI.Graph.Repository;

using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Neuro.AI.Graph.Connectors;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.Manufacturing;
using Microsoft.EntityFrameworkCore;

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
	private readonly IDbConnection _db;
	private const string ConnectionString = "Server=tcp:tropigas-development.database.windows.net,1433;Database=tropigas-mobile;User=tropi-cloud-adm_db_dev;Password=&AKD@S*jT6#G_By6Dg@.eE->dv;";
	public UserRepository(ManufacturingConnector manufacturingConnector)
	{
		_db = manufacturingConnector.Connect();
	}

	#region TpMobil

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

	#endregion

	#region ManufactoringQueries

	public async Task<IEnumerable<string>> Select_skill_options()
	{
		var sp = "sp_select_skill_level_options";

		return await _db.QueryAsync<string>(
			sp,
			commandType: CommandType.StoredProcedure
		);
	}

	#endregion

	#region ManufactoringMutations

	public async Task<string> Create_update_user(UserIpcDto userIpcDto)
	{
		var sp = "sp_create_update_user";
		var p = new DynamicParameters();
		p.Add("@UserId", userIpcDto.UserId);
		p.Add("@FirstName", userIpcDto.FirstName);
		p.Add("@LastName", userIpcDto.LastName);
		p.Add("@DocumentId", userIpcDto.DocumentId);
		p.Add("@UserName", userIpcDto.UserName);
		p.Add("@Email", userIpcDto.Email);
		p.Add("@Phone", userIpcDto.Phone);
		p.Add("@Address", userIpcDto.Address);
		p.Add("@BloodType", userIpcDto.BloodType);
		p.Add("@EmployeeNumber", userIpcDto.EmployeeNumber);
		p.Add("@CompanyId", userIpcDto.CompanyId);
		p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

		await _db.ExecuteAsync(
			sp,
			p,
			commandType: CommandType.StoredProcedure
		);

		return p.Get<string>("@Message");
	}

	public async Task<string> Update_user_skills(UserSkillsDto userSkillsDto)
	{
		var sp = "sp_update_user_skills";
		var p = new DynamicParameters();
		p.Add("@UserId", userSkillsDto.UserId);

		foreach (var userSkill in userSkillsDto.Skills)
		{
			p.Add("@SkillId", userSkill.SkillId);
			p.Add("@SkillLevel", userSkill.Level);
			p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

			await _db.ExecuteAsync(
				sp,
				p,
				commandType: CommandType.StoredProcedure
			);
		}

		return p.Get<string>("@Message");
	}

	#endregion
}