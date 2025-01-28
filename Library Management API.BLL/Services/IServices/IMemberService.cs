using Library_Management_API.BLL.DTOs.MemberDto;
using Library_Management_API.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.BLL.Services.IServices
{
    public interface IMemberService
    {
        bool AddMember(AddMemberDto memberDto);
        bool UpdateMember(int id, UpdateMemberDto newMemberDto);
        List<GetMembersDto> GetMembers();
        bool DeleteMember(int id);
    }
}
