using Library_Management_API.BLL.Services.IServices;
using Library_Management_API.BLL.DTOs.MemberDto;
using Library_Management_API.DAL.Entities;
using Library_Management_API.DAL.Repositories;
using Library_Management_API.DAL.Repositories.IRepositories;
using Library_Management_API.DAL.Repositories.RepositoriesImpl;
using Mapster;
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
        public List<GetMembersDto> GetMembers()
        {
            try
            {
                var members = memberRepository.GetAllMembers();
                var memberDto = members.Adapt<List<GetMembersDto>>();
                Log.Information("All members have been successfully brought");
                return memberDto;
            }
            catch (Exception ex) {
                Log.Error("An error occurred while brought the member", ex.Message);
                throw new Exception("An error occurred while brought the member", ex);
            }
        }
        public bool AddMember(AddMemberDto memberDto)
        {
            try
            {
                var member = memberDto.Adapt<Member>();
                var result = memberRepository.AddMember(member);
                if (result)
                    Log.Information("The new member has been added successfully", member.Id, member.Name);
                else
                    Log.Error($"Failed to add the new member");
                return result;
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while adding the member: {ex.Message}");
                throw new Exception("An error occurred while adding the member", ex);
            }

        }

        public bool UpdateMember(int id, UpdateMemberDto newMemberDto)
        {
            try
            {
                var member = newMemberDto.Adapt<Member>();
                var result = memberRepository.UpdateMember(id, member);
                if (result)
                    Log.Information("The member data has been updated successfully", member.Id, member.Name);
                else
                    Log.Error($"The member with the id {id} does not exist or update failed");
                return result;
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while updating the member: {ex.Message}");
                throw new Exception("An error occurred while updating the member", ex);
            }
        }

        public bool DeleteMember(int id)
        {
            try
            {
                var result = memberRepository.DeleteMember(id);
                if (result)
                    Log.Information("The a member has been successfully deleted", id);
                else
                    Log.Error($"Failed to delete a member data", id);
                return result;

            }
            catch (Exception ex) {
                Log.Error($"An error occurred while deleting the member: {ex.Message}");
                throw new Exception("An error occurred while updating the member", ex);
            }
            
        }
    }
}
