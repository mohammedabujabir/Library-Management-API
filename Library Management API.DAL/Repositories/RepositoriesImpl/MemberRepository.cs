
using Library_Management_API.DAL.Entities;
using Library_Management_API.DAL.Repositories.IRepositories;
using Mapster;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Repositories.RepositoriesImpl
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext dbContext;

        public MemberRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

      

        public List<Member> GetAllMembers()
        {
            try
            {
                var members = dbContext.Members.ToList();
                Log.Information("The member were fetched from the database successfully");
                return members;
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while return the members from database : {ex.Message}");
                throw new Exception("An error occurred while return the members from database", ex);
            }
           

        }

        public Member GetMemberById(int id)
        {
            try
            {
                var member = dbContext.Members.Find(id);
                if (member is null)
                    Log.Information("Failed to return the member using the ID from the database ");
                else
                    Log.Information("The member was returned using the id from the database successfully ");
                    return member;
            }
            catch (Exception ex) {
                Log.Error("An error occurred while return the member using the ID from database", ex.Message);
                throw new Exception("An error occurred while return the member using the ID from database", ex);
            }
           
        }

        public bool AddMember(Member member)
        {
            try
            {
               dbContext.Members.Add(member);
                dbContext.SaveChanges();
                Log.Information("The new member has been added to the database successfully");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred while adding the member to database : {ex.Message}");
                throw new Exception("An error occurred while adding the member to database ", ex);

            }
        }

        public bool UpdateMember(int id, Member updateMember)
        {
            try
            {
                var member =GetMemberById(id);
                if (member == null)
                {
                    Log.Error($"The member with identifier {id} does not exist in the database");
                    return false;

                }
                member.Name = updateMember.Name;
                member.Email = updateMember.Email;
                member.MemberShipType = updateMember.MemberShipType;
                dbContext.SaveChanges();
                Log.Information("The member's data in the database has been updated successfully");
                return true;
            }
            catch (Exception ex)
            {

                Log.Error($"An error occurred while updating the member in database : {ex.Message}");
                throw new Exception("An error occurred while updating the member in database ", ex);

            }
        }
        public bool DeleteMember(int id)
        {
            try
            {
                var member = GetMemberById(id);
                if (member == null)
                {

                    Log.Error($"The member with identifier {id} does not exist in the database");
                    return false;
                }
                dbContext.Remove(member);
                dbContext.SaveChanges();
                Log.Information("The member was successfully deleted from the database");
                return true;
            }
            catch (Exception ex)
            {

                Log.Error($"An error occurred while deleting the member from database : {ex.Message}");
                throw new Exception("An error occurred while deleting the member from database  ", ex);

            }
        }
    }
}
