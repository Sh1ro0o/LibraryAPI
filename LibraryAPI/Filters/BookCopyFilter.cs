﻿using LibraryAPI.Model;

namespace LibraryAPI.Filters
{
    public class BookCopyFilter
    {
        public int? RecordId { get; set; }
        public string? SerialNumber { get; set; }
        public bool? IsAvailable { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? BookId { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
