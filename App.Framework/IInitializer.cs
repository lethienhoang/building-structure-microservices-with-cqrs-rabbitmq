using System.Threading.Tasks;

namespace App.Framework
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}
