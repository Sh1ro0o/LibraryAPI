﻿namespace LibraryAPI.Filters
{
    public class BookFilter
    {
        public int? RecordId { get; set; }
        public string? Title { get; set; }
        public DateOnly? PublishDate { get; set; }
        public string? ISBN { get; set; }
        public bool IncludeAuthors { get; set; } = true;

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
