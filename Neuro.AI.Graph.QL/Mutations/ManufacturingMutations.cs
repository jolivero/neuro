﻿using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Repository;

namespace Neuro.AI.Graph.QL.Mutations
{
    public class ManufacturingMutations
    {
        #region Compañias

        public async Task<string> repo_create_companies(CompanyRepository repository, CompanyDto companyDto)
        {
            return await repository.Create_companies(companyDto);
        }

        public async Task<string> repo_update_companies(CompanyRepository repository, string companyId, CompanyDto companyDto)
        {
            return await repository.Update_companies(companyId, companyDto);
        }

        #endregion

        #region Usuarios

        public async Task<string> repo_create_update_user(UserRepository repository, UserIpcDto userIpcDto)
        {
            return await repository.Create_update_user(userIpcDto);
        }

        public async Task<string> repo_create_update_userSkills(UserRepository repository, UserSkillsDto userSkillsDto)
        {
            return await repository.Update_user_skills(userSkillsDto);
        }

        #endregion

        #region Línea de producción

        public async Task<string> repo_create_productionLine(ProductionLineRepository repository, ProductionLineDto plDto)
        {
            return await repository.Create_productionLine(plDto);
        }

        public async Task<string> repo_update_productionLine(ProductionLineRepository repository, string lineId, ProductionLineDto plDto)
        {
            return await repository.Update_productionLine(lineId, plDto);
        }

        public async Task<string> repo_create_productionLine_steps(ProductionLineRepository repository, ProductionLineConfigDto plConfigDto)
        {
            return await repository.Create_productionLine_steps(plConfigDto);
        }

        public async Task<string> repo_update_productionLine_steps(ProductionLineRepository repository, ProductionLineUpdateDto plUpdateDto)
        {
            return await repository.Update_productionLine_steps(plUpdateDto);
        }

        #endregion

        #region Grupos

        public async Task<string> repo_create_groups(GroupRepository repository, GroupDto groupDto)
        {
            return await repository.Create_groups(groupDto);
        }

        public async Task<string> repo_update_groups(GroupRepository repository, string groupId, GroupDto groupDto)
        {
            return await repository.Update_groups(groupId, groupDto);
        }

        #endregion

        #region Estaciones

        public async Task<string> repo_create_stations(StationRepository repository, StationDto stationDto)
        {
            return await repository.Create_stations(stationDto);
        }

        public async Task<string> repo_update_stations(StationRepository repository, string stationId, StationDto stationDto)
        {
            return await repository.Update_stations(stationId, stationDto);
        }

        #endregion

        #region Máquinas

        public async Task<string> repo_create_machines(MachineRepository repository, MachineDto machineDto)
        {
            return await repository.Create_machines(machineDto);
        }

        public async Task<string> repo_update_machines(MachineRepository repository, string machineId, MachineDto machineDto)
        {
            return await repository.Update_machines(machineId, machineDto);
        }

        public async Task<string> repo_create_machine_report(MachineRepository repository, MachineReportDto machineReportDto)
        {
            return await repository.Create_machine_report(machineReportDto);
        }

        public async Task<string> repo_update_machine_report(MachineRepository repository, string reportId, MachineReportDto machineReportDto)
        {
            return await repository.Update_machine_report(reportId, machineReportDto);
        }

        #endregion

        #region Piezas

        public async Task<string> repo_create_parts(PartRepository repository, PartDto partDto)
        {
            return await repository.Create_parts(partDto);
        }

        public async Task<string> repo_update_parts(PartRepository repository, string partId, PartDto partDto)
        {
            return await repository.Update_parts(partId, partDto);
        }

        #endregion

        #region Turnos-Detalles

        public async Task<string> repo_create_turn_with_details(TurnRepository repository, TurnDto turnDto)
        {
            return await repository.Create_Update_turns(turnDto);
        }

        public async Task<string> repo_update_turn_with_details(TurnRepository repository, TurnDto turnDto, string turnId)
        {
            return await repository.Create_Update_turns(turnDto, turnId);
        }

        #endregion

        #region Planeación mensual

        public async Task<string> repo_create_monthlySchedule(MonthlyScheduleRepository repository, MonthlyScheduleDto msDto)
        {
            return await repository.Create_monthly_schedule(msDto);
        }

        public async Task<string> repo_update_monthlyGoalSchedule(MonthlyScheduleRepository repository, UpdateMonthlyScheduleDto mgDto)
        {
            return await repository.Update_monthlyGoal_schedule(mgDto);
        }

        public async Task<string> repo_update_monthlyDaysSchedule(MonthlyScheduleRepository repository, UpdateMonthlyScheduleDto mdDto)
        {
            return await repository.Update_monthlyDays_schedule(mdDto);
        }

        public async Task<string> repo_delete_monthlyDaysSchedule(MonthlyScheduleRepository repository, string monthId)
        {
            return await repository.Delete_monthlyDays_schedule(monthId);
        }

        #endregion

        #region Asignaciones diarias

        public async Task<string> repo_create_dailyTasks(DailyTaskRepository repository, DailyTaskDto dtTaskDto)
        {
            return await repository.Create_dailyTask(dtTaskDto);
        }



        #endregion

        #region Solicitud de cambios

        public async Task<string> repo_create_monthlyGoal_request(ChangeRequestRepository repository, MonthlyChangeRequestDto mRequestDto)
        {
            return await repository.Create_monthlyGoal_reguest(mRequestDto);
        }

        public async Task<string> repo_update_status_request(ChangeRequestRepository repository, UpdateStatusRequestDto usRequestDto)
        {
            return await repository.Update_status_request(usRequestDto);
        }

        #endregion



    }
}
