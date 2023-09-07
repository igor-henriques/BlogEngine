namespace BlogEngine.Domain.Interfaces.Repositories;

public interface IDataSeeder
{
    Task SeedFakeDataAsync();
    Task SeedInitialProductionDataAsync();
}
