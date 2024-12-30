using Library_Management_API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.BLL.Services.IServices
{
    public interface IMemberService
    {
        void AddMember(Member member);
        void UpdateMember(int id, Member NewMember);
        List<Member> GetMembers();
        void DeleteMember(int id);
    }
}
