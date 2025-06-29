using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Data.Entities;
using Data.Entities.Telered;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using Neuro.AI.Graph.Models.Dtos;
using Neuro.AI.Graph.Models.Manufacturing;
using Neuro.AI.Graph.Models.Response;
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
    public IQueryable<Skill> GetSkills(ManufacturingDbContext context) => context.Skills;

    // [UseProjection]
    // [UseFiltering]
    // [UseSorting]
    // public IQueryable<ProductionLine> GetProductionLines(ManufacturingDbContext context) => context.ProductionLines;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<EFProductionLineList> GetProductionLines(ManufacturingDbContext context)
    {
        var response =
            from pl in context.ProductionLines
            join u in context.Users on pl.CreatedBy equals u.UserId
            join c in context.Companies on pl.CompanyId equals c.CompanyId
            select new EFProductionLineList
            {
                LineId = pl.LineId.ToString(),
                Name = pl.Name,
                Status = pl.Status.Value,
                CreatedAt = pl.CreatedAt.Value,
                UpdatedAt = pl.UpdatedAt.Value,
                CompanyId = c.CompanyName,
                Company = c.CompanyName,
                CreatedBy = $"{u.FirstName} {u.LastName}",
                UserId = u.UserId.ToString()
            };

        return response;
    }

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
    public IQueryable<Models.Manufacturing.Machine> GetMachines(ManufacturingDbContext context) => context.Machines.Include(m => m.MachineReports);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Part> GetParts(ManufacturingDbContext context) => context.Parts.Include(p => p.Inventory);

    #endregion

}