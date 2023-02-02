namespace FoodService.Data
{
    public interface IDatabaseSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string FoodCategoriesCollectionName { get; set; }
        string FoodsCollectionName { get; set; }
        
    }
}
