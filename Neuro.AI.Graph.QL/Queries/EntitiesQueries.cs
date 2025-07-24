using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Data.Entities;
using Data.Entities.Telered;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
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
    public IQueryable<Company> GetCompanies(ManufacturingDbContext context) => context.Companies.Include(c => c.Users).Include(c => c.ProductionLines);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsersInfo(ManufacturingDbContext context) => context.Users.Include(u => u.UsersSkills);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Skill> GetSkills(ManufacturingDbContext context) => context.Skills;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ProductionLine> GetProductionLines(ManufacturingDbContext context) => context.ProductionLines;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Group> GetGroups(ManufacturingDbContext context) => context.Groups;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Station> GetStations(ManufacturingDbContext context) => context.Stations;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    async public Task<IQueryable<Models.Manufacturing.Machine>> GetMachinesWithReports(ManufacturingDbContext context)
    {
        var machines = await context.Machines
            .Include(m => m.CreatedByNavigation)
            .Include(m => m.MachineReports).ThenInclude(mr => mr.Operator)
            .Include(m => m.MachineReports).ThenInclude(mr => mr.Technical).ToListAsync();
        foreach (var machine in machines)
        {
            machine.MachineReports = machine.MachineReports.OrderByDescending(mr => mr.CreatedAt).ToList();
        }

        return machines.AsQueryable();
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Part> GetPartsWithInventory(ManufacturingDbContext context) => context.Parts.Include(p => p.Inventory).OrderBy(p => p.CreatedAt);

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
    public async Task<IQueryable<MonthlySchedule>> GetMonthlyAndDailySchedule(ManufacturingDbContext context)
    {
        var schedules = await context.MonthlySchedules
            .OrderBy(ms => ms.CreatedAt)
            .Include(ms => ms.DailySchedules.Where(ds => ds.Available > 0)).ThenInclude(ds => ds.DailyTasks).ThenInclude(dt => dt.User)
            .Include(ms => ms.DailySchedules).ThenInclude(ds => ds.DailyTasks).ThenInclude(dt => dt.Station)
            .Include(ms => ms.DailySchedules).ThenInclude(ds => ds.DailyTasks).ThenInclude(dt => dt.Machine).ToListAsync();
        foreach (var schedule in schedules)
        {
            schedule.DailySchedules = schedule.DailySchedules.OrderBy(ds => ds.ProductionDate).ToList();
        }

        return schedules.AsQueryable();
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IQueryable<DailySchedule>> GetDailyTasks(ManufacturingDbContext context)
    {
        var tasks = await context.DailySchedules
            .OrderBy(ds => ds.ProductionDate)
            .Include(ds => ds.DailyTasks.Where(dt => dt.EndAt == null))
            .Include(ds => ds.DailyTasks).ThenInclude(dt => dt.Station)
            .Include(ds => ds.DailyTasks).ThenInclude(dt => dt.Machine)
            .Include(ds => ds.DailyTasks).ThenInclude(dt => dt.User)
            .ToListAsync();
        foreach (var task in tasks)
        {
            task.DailyTasks = task.DailyTasks.OrderBy(dt => dt.CreatedAt).ToList();
        }

        return tasks.AsQueryable();
    }

    #endregion

}