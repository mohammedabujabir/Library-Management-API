

using Library_Management_API.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Repositories.IRepositories
{
    public interface IMemberRepository
    {
        List<Member> GetAllMembers();
        bool AddMember(Member member);
        Member GetMemberById(int id);
        bool DeleteMember(int  id);
        bool UpdateMember(int id,Member updateMember);
    }
}
