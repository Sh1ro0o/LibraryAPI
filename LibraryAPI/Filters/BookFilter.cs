﻿namespace LibraryAPI.Filters
{
    public class BookFilter
    {
        public int? RecordId { get; set; }
        public string? Title { get; set; }
        public DateOnly? PublishDate { get; set; }
        public string? ISBN { get; set; }
        public string? Description { get; set; }

        public List<int> GenreIds { get; set; } = new List<int>();
        public List<int> AuthorIds { get; set; } = new List<int>();

        public bool IncludeAuthors { get; set; } = true; //Include Authors
        public bool IncludeGenres { get; set; } = true; //Include Genres

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
