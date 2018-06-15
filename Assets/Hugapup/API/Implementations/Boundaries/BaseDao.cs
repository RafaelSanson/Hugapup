using System.Threading.Tasks;
using Firebase.Database;

namespace Hugapup.API.Tests.Editor.Boundaries
{
    public interface BaseDao<in T>
    {
        Task Save(T saveObject, string parent);
        Task<DataSnapshot> Retrieve(string parent, string key);
    }
}