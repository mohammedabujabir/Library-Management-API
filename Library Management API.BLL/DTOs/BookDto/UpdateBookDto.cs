namespace Library_Management_API.BLL.DTOs.BookDto
{
    public record UpdateBookDto
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public string ISBN { get; set; }

        public int Quantity { get; set; }
    }
}
