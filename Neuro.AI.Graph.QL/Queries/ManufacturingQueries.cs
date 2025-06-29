﻿using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.Manufacturing;
using Neuro.AI.Graph.Repository;

namespace Neuro.AI.Graph.QL.Queries
{
    public class ManufacturingQueries
    {
        #region Compañias

        #endregion

        #region Línea de producción

        public async Task<IQueryable<ProductionLine>> repo_productionLines_with_details(ProductionLineRepository repository, string lineId)
        {
            return (await repository.Select_productionLines_With_Details(lineId)).AsQueryable();
        }

        public async Task<IQueryable<ProductionLineMachineHoursPerCut>> repo_productionLines_with_machineHoursCut(ProductionLineRepository repository, string lineId)
        {
            return (await repository.Select_productionLines_With_MachineHoursPerCut(lineId)).AsQueryable();
        }

        #endregion

        #region Grupos [Líneas de producción]


        #endregion

        #region Estaciones [Asignadas a grupos]

        public async Task<StationConfigInfo?> repo_station_with_configInfo(StationRepository repository, string stationId, string machineId, string partId)
        {
            return await repository.Select_station_with_configInfo(stationId, machineId, partId);
        }

        #endregion

            #region Máquinas

            #endregion

            #region Piezas

            #endregion

        }
}
