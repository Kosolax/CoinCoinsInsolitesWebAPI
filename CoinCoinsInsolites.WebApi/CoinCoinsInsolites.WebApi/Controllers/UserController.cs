namespace CoinCoinsInsolites.WebApi.Controllers
{
    using CoinCoinsInsolites.BusinessObject;
    using CoinCoinsInsolites.IBusiness;
    using CoinCoinsInsolites.WebApi.Route;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route(UserRoute.RoutePrefix)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        [Route(UserRoute.Authenticate)]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] User userToAuthent)
        {
            KeyValuePair<bool, User> result = await this.userBusiness.Authenticate(userToAuthent.Mail, userToAuthent.Password);

            if (!result.Key)
            {
                return this.NotFound();
            }

            return this.Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User userToCreate)
        {
            KeyValuePair<bool, User> result = await this.userBusiness.Create(userToCreate);

            if (!result.Key)
            {
                return this.BadRequest(result.Value.ValidationService.ModelState);
            }

            return this.Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            KeyValuePair<bool, User> result = await this.userBusiness.Delete(id);

            if (!result.Key)
            {
                return this.BadRequest(result.Value.ValidationService.ModelState);
            }

            return this.Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            User user = await this.userBusiness.Get(id);

            if (user == null)
            {
                return this.NotFound();
            }

            return this.Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return this.Ok(await this.userBusiness.List());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User userToUpdate)
        {
            KeyValuePair<bool, User> result = await this.userBusiness.Update(id, userToUpdate);

            if (!result.Key)
            {
                return this.BadRequest(result.Value.ValidationService.ModelState);
            }

            return this.Ok(result.Value);
        }
    }
}