using Library_Management_API.BLL.Services.IServices;
using Library_Management_API.DAL.Models;
using Library_Management_API.DAL.Repositories;
using Library_Management_API.DAL.Repositories.IRepositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.BLL.Services.ServicesImpl
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository memberRepository;

        public MemberService(IMemberRepository MemberRepository)
        {
            memberRepository = MemberRepository;
        }

        public void AddMember(Member member)
        {
            var members = memberRepository.GetALLMembers();
            member.Id = members.Count() > 0 ? members.Max(m => m.Id) + 1 : 1;
            members.Add(member);
            memberRepository.SaveMembers(members);
            Log.Information("The new member has been added successfully", member.Id, member.Name);
        }

        public void UpdateMember(int id, Member NewMember)
        {

            var members = memberRepository.GetALLMembers();
            var member = members.FirstOrDefault(m => m.Id == id);
            if (member == null)
            {
                throw new KeyNotFoundException("member not found");

            }
            member.Name = NewMember.Name;
            member.Email = NewMember.Email;
            member.MemberShipType = NewMember.MemberShipType;
            memberRepository.SaveMembers(members);
            Log.Information("The member data has been updated successfully", member.Id, member.Name);
        }

        public List<Member> GetMembers()
        {
            var members = memberRepository.GetALLMembers();
            Log.Information("All members have been successfully brought");
            return members;

        }

        public void DeleteMember(int id)
        {
            var members = memberRepository.GetALLMembers();
            var member = members.FirstOrDefault(m => m.Id == id);
            if (member == null)
            {

                throw new KeyNotFoundException("member not found");
            }
            members.Remove(member);
            memberRepository.SaveMembers(members);
            Log.Information("The member has been successfully deleted", member.Id, member.Name);
        }
    }
}
