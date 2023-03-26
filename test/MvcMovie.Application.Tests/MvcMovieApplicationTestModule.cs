using Volo.Abp.Modularity;

namespace MvcMovie;

[DependsOn(
    typeof(MvcMovieApplicationModule),
    typeof(MvcMovieDomainTestModule)
    )]
public class MvcMovieApplicationTestModule : AbpModule
{

}
