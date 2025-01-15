namespace Library_Management_API.BLL.DTOs.BorrowingDto
{
    public record GetBorrowingDto
    {
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
