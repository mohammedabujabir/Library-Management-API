using Library_Management_API.BLL.Services;
using Library_Management_API.BLL.Services.IServices;
using Library_Management_API.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Library_Management_API.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : Controller
    {
        private readonly IMemberService memberService;

        public MembersController(IMemberService MemberService) 
        {
            memberService = MemberService;
        }
        [HttpGet]
        public IActionResult GetMembers()
        {
            Log.Information("A request has been sent to bring the organs");
            var members= memberService.GetMembers();
            return Ok(members);
        }
        [HttpPost]
        public IActionResult AddMember([FromBody] Member member) {
            Log.Information("A request has been sent to add a new member");
            memberService.AddMember(member);
        return Ok("The member has been added successfully");
        
        }

        [HttpPut]
        public IActionResult UpdateMember(int id , [FromBody] Member NewMember)
        {
            Log.Information("A request has been sent to update the data of the selected member");
            memberService.UpdateMember(id, NewMember);
            return Ok("Member data has been successfully modified");
        }

        [HttpDelete]
        public IActionResult DeleteMember(int id)
        {
            Log.Information("A request has been sent to delete the selected member");
            memberService.DeleteMember(id);
            return Ok("The member has been deleted successfully");
        }

    }
}
