using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MvcMovie.Data;

/* This is used if database provider does't define
 * IMvcMovieDbSchemaMigrator implementation.
 */
public class NullMvcMovieDbSchemaMigrator : IMvcMovieDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
