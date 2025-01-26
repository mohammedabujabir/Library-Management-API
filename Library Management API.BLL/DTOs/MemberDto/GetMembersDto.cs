namespace Library_Management_API.BLL.DTOs.MemberDto
{
    public record GetMembersDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string MemberShipType { get; set; }
    }
}
