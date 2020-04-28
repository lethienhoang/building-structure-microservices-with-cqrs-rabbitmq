using System.Threading.Tasks;
namespace Framework
{
    public interface IStartupInitializer
    {
        Task InitializeAsync();
    }
}
