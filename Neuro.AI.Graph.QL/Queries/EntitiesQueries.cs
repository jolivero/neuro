using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Data.Entities;
using Data.Entities.Telered;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using Neuro.AI.Graph.Models.CustomModels;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.Manufacturing;
using TropigasMobile.Backend.Data;
using TropigasMobile.Backend.Data.Entities;


namespace Neuro.AI.Graph.QL.Queries;

//[Authorize]
public class EntitiesQueries
{
    #region AppDbContext

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<CreditCard> GetCreditCards(ApplicationDbContext context) => context.CreditCards;

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<OrdersSessions> GetOrdersSessions(ApplicationDbContext context) => context.OrdersSessions;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ApplicationUser> GetApplicationUsers(ApplicationDbContext context) => context.Users;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<AspNetUsersExtensions> GetAspNetUsersExtensions(ApplicationDbContext context) => context.AspNetUsersExtensions;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<AspNetUsersTypes> GetAspNetUsersTypes(ApplicationDbContext context) => context.AspNetUsersTypes;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<DeliveryLocation> GetDeliveryLocations(ApplicationDbContext context) => context.DeliveryLocations;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<DeliveryOption> GetDeliveryOptions(ApplicationDbContext context) => context.DeliveryOptions;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<LugarPoblado> GetLugarPoblados(ApplicationDbContext context) => context.LugarPoblados;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Order> GetOrders(ApplicationDbContext context) => context.Orders;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<OrderAddress> GetOrderAddresses(ApplicationDbContext context) => context.OrderAddresses;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<OrderDetail> GetOrderDetails(ApplicationDbContext context) => context.OrderDetails;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<OrderPaymentDetail> GetOrderPaymentDetails(ApplicationDbContext context) => context.OrderPaymentDetails;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<OrderStatus> GetOrderStatuses(ApplicationDbContext context) => context.OrderStatuses;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<OrdersSessionsItems> GetOrdersSessionsItems(ApplicationDbContext context) => context.OrdersSessionsItems;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<OrdersSessionsItemsInvoices> GetOrdersSessionsItemsInvoices(ApplicationDbContext context) => context.OrdersSessionsItemsInvoices;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<PaymentType> GetPaymentTypes(ApplicationDbContext context) => context.PaymentTypes;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Product> GetProducts(ApplicationDbContext context) => context.Products;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ProductPrice> GetProductPrices(ApplicationDbContext context) => context.ProductPrices;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Sugestion> GetSugestions(ApplicationDbContext context) => context.Sugestions;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<SurveyQuestion> GetSurveyQuestions(ApplicationDbContext context) => context.SurveyQuestions;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<UserAddress> GetUserAddresses(ApplicationDbContext context) => context.UserAddresses;

    #endregion

    #region ManufacturingDbContext

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Company> GetCompanies(ManufacturingDbContext context) => context.Companies
                                                                            .Include(c => c.Users)
                                                                            .Include(c => c.ProductionLines)
                                                                            .Where(c => c.Available == 1)
                                                                            .OrderByDescending(c => c.CreatedAt);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsersInfo(ManufacturingDbContext context) => context.Users;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Skill> GetSkills(ManufacturingDbContext context) => context.Skills;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ProductionLine> GetProductionLines(ManufacturingDbContext context) => context.ProductionLines
                                                                                            .Where(pl => pl.Available == 1)
                                                                                            .OrderByDescending(pl => pl.CreatedAt);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Group> GetGroups(ManufacturingDbContext context) => context.Groups
                                                                        .Where(g => g.Available == 1)
                                                                        .OrderByDescending(g => g.CreatedAt);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Station> GetStations(ManufacturingDbContext context) => context.Stations.OrderByDescending(s => s.CreatedAt);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    async public Task<IQueryable<Models.Manufacturing.Machine>> GetMachinesInfo(ManufacturingDbContext context)
    {
        var machines = await context.Machines
            .Include(m => m.CreatedByNavigation)
            .Include(m => m.ProductionLineRecipes).ThenInclude(r => r.Line)
            .Include(m => m.Stations)
            .Include(m => m.MachineReports).ThenInclude(mr => mr.Operator)
            .Include(m => m.MachineReports).ThenInclude(mr => mr.Technical)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
        foreach (var machine in machines)
        {
            machine.MachineReports = machine.MachineReports.OrderByDescending(mr => mr.CreatedAt).ToList();
        }

        return machines.AsQueryable();
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<MachineReport> GetReports(ManufacturingDbContext context) => context.MachineReports
                                                                                .Include(r => r.Station)
                                                                                .Include(r => r.Machine)
                                                                                .Include(r => r.Operator)
                                                                                .OrderByDescending(r => r.CreatedAt);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Part> GetPartsWithInventory(ManufacturingDbContext context) => context.Parts.Include(p => p.Inventory).OrderByDescending(p => p.CreatedAt);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    async public Task<IQueryable<Turn>> GetTurnsWithDetails(ManufacturingDbContext context)
    {
        var turns = await context.Turns.OrderBy(t => t.CreatedAt).Include(t => t.TurnDetails).Include(t => t.CreatedByNavigation).ToListAsync();
        foreach (var turn in turns)
        {
            turn.TurnDetails = turn.TurnDetails.OrderBy(td => td.CreatedAt).ToList();
        }

        return turns.AsQueryable();
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    async public Task<IQueryable<TurnWithTimeDetail>> GetTurnsWithTimeDetail(ManufacturingDbContext context)
    {
        var turnWitTimeDetail = await context.Turns
            .OrderBy(t => t.CreatedAt)
            .Select(t => new TurnWithTimeDetail
            {
                TurnId = t.TurnId,
                Name = t.Name!,
                Duration = t.Duration,
                TimeDetail = new()
                {
                    BeginAt = t.TurnDetails.Min(td => td.BeginAt),
                    EndAt = t.TurnDetails.Max(td => td.EndAt)
                }

            })
            .ToListAsync();

        return turnWitTimeDetail.AsQueryable();
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IQueryable<MonthlySchedule>> GetMonthlyAndDailyPlanning(ManufacturingDbContext context)
    {
        var schedules = await context.MonthlySchedules
            .OrderBy(ms => ms.CreatedAt)
            .Include(ms => ms.DailyPlannings.Where(ds => ds.Available == 1)).ThenInclude(ds => ds.DailyTasks).ThenInclude(dt => dt.User)
            .Include(ms => ms.DailyPlannings).ThenInclude(ds => ds.DailyTasks).ThenInclude(dt => dt.Station)
            .Include(ms => ms.DailyPlannings).ThenInclude(ds => ds.DailyTasks).ThenInclude(dt => dt.Machine).ToListAsync();
        foreach (var schedule in schedules)
        {
            schedule.DailyPlannings = schedule.DailyPlannings.OrderBy(ds => ds.ProductionDate).ToList();
        }

        return schedules.AsQueryable();
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<DailyTask> GetOperatorDailyTasks(ManufacturingDbContext context) => context.DailyTasks
                                                                                        .Include(dt => dt.User)
                                                                                        .Include(dt => dt.Station)
                                                                                        .Include(dt => dt.Machine)
                                                                                        .Include(dt => dt.Day).Where(dt => dt.Day!.Available == 1);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<RequestCategory> GetRequestCategories(ManufacturingDbContext context) => context.RequestCategories.OrderBy(rc => rc.Name);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ProductionChangeRequest> GetChangeRequests(ManufacturingDbContext context) => context.ProductionChangeRequests
                                                                                                .Include(pc => pc.Month)
                                                                                                .Include(pc => pc.ChangeRequestDetails)
                                                                                                .Include(pc => pc.RequestingUser)
                                                                                                .Include(pc => pc.ApprovalUser)
                                                                                                .Include(pc => pc.Users)
                                                                                                .OrderByDescending(pc => pc.CreatedAt);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ProductionRecord> GetProductionRecords(ManufacturingDbContext context) => context.ProductionRecords
                                                                                            .Include(pr => pr.CreatedByNavigation)
                                                                                            .OrderBy(pr => pr.CreatedAt);


    #endregion

}