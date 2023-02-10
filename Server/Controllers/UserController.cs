using markdown.Server.Data.Repositories;
using markdown.Shared.Models;
using markdown.Shared.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace markdown.Server.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserRepository _userRespository;

		public UserController(IUserRepository userRespository)
		{
			this._userRespository = userRespository ?? throw new ArgumentNullException(nameof(userRespository));
		}

		// GET: api/<UserController>/GetUser
		[HttpGet]
		[Route("api/UserController/GetUser")]
		public async Task<ActionResult<User>> GetUser(Guid userId)
		{
			try
			{
				User? user = await _userRespository.GetUser(userId);
				return Ok(user);
			} 
			catch(ArgumentNullException e)
			{
				return NotFound(e.Message);
			}
			catch(Exception e) 
			{ 
				return BadRequest(e.Message);
			}
		}

		// POST: api/<UserController>/PostUser
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> PostUser(CreateUserDto userDto)
		{
			User userToCreate = new User()
			{
				UserName = userDto.UserName,
				Email = userDto.Email,
				Password = userDto.Password,
			};
			try
			{
				await _userRespository.AddUser(userToCreate);
				return Ok("User added");
			}
			catch(Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		// PUT: api/<UserController>/PutUser
		public async Task<ActionResult> PutUser(User user)
		{
			try
			{
				await _userRespository.UpdateUser(user);
				return Ok("User updated");
			}
			catch (ArgumentNullException e)
			{
				return NotFound(e.Message);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
			
		}

		// DELETE: api/<UserController>/DeleteUser
		[HttpDelete]
		public async Task<ActionResult> DeleteUser(Guid userId)
		{
			try
			{
				await _userRespository.DeleteUser(userId);
				return Ok("User deleted");
			}
			catch (ArgumentNullException e)
			{
				return NotFound(e.Message);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
