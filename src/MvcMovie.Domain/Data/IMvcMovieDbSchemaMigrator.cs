using System.Threading.Tasks;

namespace MvcMovie.Data;

public interface IMvcMovieDbSchemaMigrator
{
    Task MigrateAsync();
}
