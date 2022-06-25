#pragma warning disable CS1591
namespace WebApi.Models
{
    public interface IData
    {
        int? Id { get; set; }
        int UserId { get; set; }
        DateTime DateTimeOfPurchace { get; set; }
        int CategoryId { get; set; }
        int CurrencyId { get; set; }
        decimal Summ { get; set; }
        int? SubCategoryId { get; set; }
        string? Comment { get; set; }
    }
    public class DataModel : IData
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateTimeOfPurchace { get; set; }
        public int CategoryId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Summ { get; set; }
        public int? SubCategoryId { get; set; }
        public string? Comment { get; set; }
    }
}
