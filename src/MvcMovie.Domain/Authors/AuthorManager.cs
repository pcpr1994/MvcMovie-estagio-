using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace MvcMovie.Authors;

public class AuthorManager : DomainService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorManager(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<Author> CreateAsync(
        [NotNull] string name, DateTime birthday, string imdbId)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var existingAuthor = await _authorRepository.FindByNameAsync(name);
        if (existingAuthor != null) 
        {
            throw new AuthorAlreadyExistsException(name);
        }
        return new Author(
            GuidGenerator.Create(),
            name,
            birthday,
            imdbId
            );
    }

    public async Task ChangeNameAsync(
        [NotNull] Author author,
        [NotNull] string name,
        [NotNull]  DateTime birthday,
        string imdbId)
    {
        Check.NotNull(author, nameof(author));
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var existingAuthor = await _authorRepository.FindByNameAsync(name);
        if (existingAuthor == null) 
        {
            throw new AuthorAlreadyExistsException(name);
        }

        author.ChangeName(name, imdbId, birthday);

    }


}
