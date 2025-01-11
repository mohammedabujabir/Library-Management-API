namespace Library_Management_API.BLL.DTOs.MemberDto
{
    public record UpdateMemberDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string MemberShipType { get; set; }
    }
}
