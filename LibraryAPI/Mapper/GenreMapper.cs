using LibraryAPI.Dto.Genre;
using LibraryAPI.Model;

namespace LibraryAPI.Mapper
{
    public static class GenreMapper
    {
        public static GenreDto ToGenreDto(this Genre genre)
        {
            return new GenreDto
            {
                RecordId = genre.RecordId,
                Name = genre.Name,
                Description = genre.Description,
            };
        }

        public static Genre ToGenreFromCreateDto (this CreateGenreDto model)
        {
            return new Genre
            {
                Name = model.Name,
                Description = model.Description,
            };
        } 
    }
}
