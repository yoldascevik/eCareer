using System.Threading.Tasks;

namespace Career.Migration.DataSeeder;

public interface IDataSeeder
{
    Task SeedDataAsync();
}