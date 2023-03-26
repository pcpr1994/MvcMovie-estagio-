using MvcMovie.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MvcMovie.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MvcMovieEntityFrameworkCoreModule),
    typeof(MvcMovieApplicationContractsModule)
    )]
public class MvcMovieDbMigratorModule : AbpModule
{

}
