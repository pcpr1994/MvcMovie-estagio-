using MvcMovie.Genres;
using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;
using Xunit;

namespace MvcMovie.Movies;

public class MovieAppService_Tests : MvcMovieApplicationTestBase
{
    private readonly IMovieAppService _movieAppService;
    private readonly IGenreAppService _genreAppService;

    public MovieAppService_Tests()
    {
        _movieAppService = GetRequiredService<IMovieAppService>();
        _genreAppService = GetRequiredService<IGenreAppService>();
    }

    [Fact]
    public async Task Should_Get_List_Of_Movies()
    {
        //Act
        var result = await _movieAppService.GetListAsync(
            new GetMovieListDto()
        );

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
        result.Items.ShouldContain(b => b.GenreDescription == "Romantic Comedy" &&
                                    b.Title == "When Harry Met Sally");
    }

    [Fact]
    public async Task Should_Create_A_Valid_Movie()
    {
        var genres = await _genreAppService.GetListAsync(new GetGenreListDto());
        var firstGenre = genres.Items.First();

        float x = 7.99f;

        //Act
        var result = await _movieAppService.CreateAsync(
            new CreateUpdateMovieDto
            {
                Genreid = firstGenre.Id,
                Title = "Wather",
                ReleaseDate = System.DateTime.Now,
                Price= x,
                Rating= 7.5f
            }
        );

        //Assert
        result.Id.ShouldNotBe(Guid.Empty);
        result.Title.ShouldBe("Wather");
    }

    [Fact]
    public async Task Should_Not_Create_A_Movie_Without_Title()
    {
        var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
        {
            await _movieAppService.CreateAsync(
                new CreateUpdateMovieDto
                {
                    Title = "",
                    Price = 10,
                    ReleaseDate = DateTime.Now
                    
                }
            );
        });

        exception.ValidationErrors
            .ShouldContain(err => err.MemberNames.Any(m => m == "Title"));
    }
}
