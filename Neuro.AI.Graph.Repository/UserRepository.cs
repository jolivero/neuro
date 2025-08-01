﻿namespace Neuro.AI.Graph.Repository;

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
using Neuro.AI.Graph.Models.CustomModels;

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

	public async Task<IEnumerable<OperatorProfile>> Select_user_with_skills(string userId)
	{
		var sp = "sp_select_userProfile";
		var p = new DynamicParameters();
		p.Add("@UserId", userId);

		var userProfilDict = new Dictionary<string, OperatorProfile>();

		try
		{
			await _db.QueryAsync<User, UsersSkill, Skill, Company, User>(
				sp,
				(user, userSkill, skill, company) =>
				{
					if (!userProfilDict.TryGetValue(user.UserId.ToString(), out var userProfileData))
					{
                        userProfileData = new()
                        {
                            UserId = user.UserId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
							DocumentId = user.DocumentId,
							UserName = user.UserName,
                            Email = user.Email,
							Phone = user.Phone,
							Address = user.Address,
							BloodType = user.BloodType,
							EmployeeNumber = user.EmployeeNumber,
							Rol = user.Rol,
							CompanyId = user.CompanyId,
                            OperatorSkills = []
                        };

                        userProfilDict.Add(user.UserId.ToString(), userProfileData);
					}

					var skillsData = userProfileData.OperatorSkills.FirstOrDefault(s => s.SkillId == skill.SkillId);
					if (skillsData == null)
					{
						skillsData = new()
						{
							SkillId = skill.SkillId,
							Name = skill.Name,
							SkillLevel = userSkill.SkillLevel
						};

						userProfileData.OperatorSkills.Add(skillsData);
					}

					if (company != null && user.CompanyId == company.CompanyId) userProfileData.Company = company;

					return userProfileData;
				},
				p,
				splitOn: "SkillId, CompanyId",
				commandType: CommandType.StoredProcedure
			);

			return userProfilDict.Values;

		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			throw;
		}

	}

	public async Task<IEnumerable<User>> Select_users_with_monthlySchedule(int month, int year, string? userId)
	{

		var sp = "sp_select_operatorScheduleInfoByMonth";
		var p = new DynamicParameters();
		p.Add("@Month", month);
		p.Add("@Year", year);
		p.Add("@UserId", userId ?? null);

		var operatorScheduleDict = new Dictionary<string, User>();

		try
		{
			await _db.QueryAsync<User, DailyTask, Station, DailySchedule, Turn, TurnDetail, User>(
			sp,
			(user, task, station, dailySchedule, turn, turnDetail) =>
			{
				if (!operatorScheduleDict.TryGetValue(user.UserId.ToString(), out var operatorData))
				{
					operatorData = user;
					operatorData.DailyTasks = [];
					operatorScheduleDict.Add(user.UserId.ToString(), operatorData);
				}

				var taskData = operatorData.DailyTasks.FirstOrDefault(t => t.TaskId == task.TaskId);
				if (taskData == null)
				{
					taskData = task;
					taskData.Station = station;
					taskData.Day = dailySchedule;
					taskData.Turn = turn;
					taskData.Turn.TurnDetails = [];

					operatorData.DailyTasks.Add(taskData);
				}


				var turnDetailData = taskData.Turn?.TurnDetails.FirstOrDefault(td => td.TurnDetailId == turnDetail.TurnDetailId);
				if (turnDetailData == null)
				{
					turnDetailData = turnDetail;
					taskData.Turn?.TurnDetails.Add(turnDetailData);
				}


				return operatorData;
			},
			p,
			splitOn: "TaskId, StationId, DayId, TurnId, TurnDetailId",
			commandType: CommandType.StoredProcedure
		);

			return operatorScheduleDict.Values;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			throw new Exception(ex.Message);
		}

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
		p.Add("@Rol", userIpcDto.Rol);
		p.Add("@CompanyId", userIpcDto.CompanyId);
		p.Add("@Message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

		try
		{
			await _db.ExecuteAsync(
				sp,
				p,
				commandType: CommandType.StoredProcedure
			);

			return p.Get<string>("@Message");
		}
		catch (Exception ex)
		{
			return ex.Message;
		}
	}

	public async Task<string> Update_user_skills(UserSkillsDto userSkillsDto)
	{
		var sp = "sp_update_user_skills";
		var p = new DynamicParameters();
		p.Add("@UserId", userSkillsDto.UserId);

		try
		{
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
		catch (Exception ex)
		{
			return ex.Message;
		}
	}

	#endregion
}