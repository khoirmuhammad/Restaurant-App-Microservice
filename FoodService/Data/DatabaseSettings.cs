namespace FoodService.Data
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string DatabaseName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string FoodCategoriesCollectionName { get; set; } = string.Empty;
        public string FoodsCollectionName { get; set; } = string.Empty;
    }
}
