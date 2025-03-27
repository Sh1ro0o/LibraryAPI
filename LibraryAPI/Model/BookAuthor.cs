namespace LibraryAPI.Model
{
    public class BookAuthor
    {
        //Book
        public int BookId { get; set; }
        public Book? Book { get; set; }

        //Author
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
