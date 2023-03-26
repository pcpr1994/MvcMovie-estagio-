using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MvcMovie.Genres;

public class GenreAppService_Tests : MvcMovieApplicationTestBase
{
    private readonly IGenreAppService _genreAppService;

    public GenreAppService_Tests()
    {
        _genreAppService = GetRequiredService<IGenreAppService>();
    }

    [Fact]
    public async Task Should_Get_All_Genres_Without_Any_Filter()
    {
        var result = await _genreAppService.GetListAsync(new GetGenreListDto());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
        result.Items.ShouldContain(genre => genre.Description == "Comedy");
        result.Items.ShouldContain(genre => genre.Description == "Romantic Comedy");
    }

    [Fact]
    public async Task Should_Get_Filtered_Genres()
    {
        var result = await _genreAppService.GetListAsync( new GetGenreListDto { Filter = "Romantic" });

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(genre => genre.Description == "Romantic Comedy");
        result.Items.ShouldNotContain(genre => genre.Description == "Western");
    }

    [Fact]
    public async Task Should_Create_A_New_Genre()
    {
        var authorDto = await _genreAppService.CreateAsyncGenre(
            new CreateGenreDto
            {
                Description = "Suspense"
            }
        );

        authorDto.Id.ShouldNotBe(Guid.Empty);
        authorDto.Description.ShouldBe("Suspense");
    }

    [Fact]
    public async Task Should_Not_Allow_To_Create_Duplicate_()
    {
        await Assert.ThrowsAsync<GenreAlreadyExistsException>(async () =>
        {
            await _genreAppService.CreateAsyncGenre(
                new CreateGenreDto
                {
                    Description = "Romantic Comedy"
                }
            );
        });
    }

}
