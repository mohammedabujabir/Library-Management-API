using Library_Management_API.BLL.Services;
using Library_Management_API.BLL.Services.IServices;
using Library_Management_API.BLL.DTOs.MemberDto;
using Library_Management_API.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace Library_Management_API.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MembersController : Controller
    {
        private readonly IMemberService memberService;

        public MembersController(IMemberService MemberService) 
        {
            memberService = MemberService;
        }
        [HttpGet]
        [Authorize]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetMembers()
        {
            try
            {
                Log.Information("A request has been sent to bring the organs");
                var members = memberService.GetMembers();
                if (members.Count() == 0) {
                    return NotFound(members);
                }else
                return Ok(members);
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while fetching organs: {ex.Message}");
                return BadRequest("False:Failed to bring in organs");

            }
            
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult AddMember(AddMemberDto memberDto) {
            try
            {
                Log.Information("A request has been sent to add a new member");
                var status= memberService.AddMember(memberDto);
                if(status)
                return Ok($"status: {status} The member has been added successfully");
                else return BadRequest(status);
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while adding the member: {ex.Message}");
                return BadRequest("False:Failed to add member");

            }
           
        
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateMember(int id , UpdateMemberDto newMemberDto)
        {
            try
            {
                Log.Information("A request has been sent to update the data of the selected member");
                var status = memberService.UpdateMember(id, newMemberDto);
                if (status)
                    return Ok("Member data has been successfully modified");
                else return BadRequest($"status: {status} Failed to add a member data the a member may not exist or the data is invalid");

            }
            catch (Exception ex) {
                Log.Error($"An error occurred while updating data for a member: {ex.Message}");
                return BadRequest("False:Failed to update a member data");

            }
          
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteMember(int id)
        {
            try
            {
                Log.Information("A request has been sent to delete the selected member");
                var status = memberService.DeleteMember(id);
                if(status)
                return Ok($"status : {status} The member has been deleted successfully");
                else return BadRequest($"status: {status} Failed to delete a member data does not exist");
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while deleting a member: {ex.Message}");
                return BadRequest("False: Failed to delete member");
            }
            
        }

    }
}
