using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MvcMovie.Pages;

public class Index_Tests : MvcMovieWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
