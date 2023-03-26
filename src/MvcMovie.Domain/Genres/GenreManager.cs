using JetBrains.Annotations;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using System;

namespace MvcMovie.Genres;

public class GenreManager : DomainService
{
    private readonly IGenreRepository _genreRepository;

    public GenreManager(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<Genre> CreateAsync(
        [NotNull] string description)
    {
        Check.NotNullOrWhiteSpace(description, nameof(description));

        var existingGenre = await _genreRepository.FindByDescriptionAsync(description); 
        if (existingGenre != null) 
        {
            throw new GenreAlreadyExistsException(description);
        }
        return new Genre(
            GuidGenerator.Create(),
            description
            );

    }

    public async Task ChangeNameAsync(
        [NotNull] Genre genre,
        [NotNull] string newName)
    {
        Check.NotNull(genre, nameof(genre));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingGenre = await _genreRepository.FindByDescriptionAsync(newName);
        if (existingGenre == null) 
        {
            throw new GenreAlreadyExistsException(newName);
        }

        genre.ChangeDescription(newName);
    }
}
