using MvcMovie.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MvcMovie;

[DependsOn(
    typeof(MvcMovieEntityFrameworkCoreTestModule)
    )]
public class MvcMovieDomainTestModule : AbpModule
{

}
