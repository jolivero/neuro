using Data.Entities;
using Data.Entities.Telered;
using HotChocolate.Authorization;
using TropigasMobile.Backend.Data;
using TropigasMobile.Backend.Data.Entities;


namespace Neuro.AI.Graph.QL.Queries;

[Authorize]
public class EntitiesQueries
{
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

}