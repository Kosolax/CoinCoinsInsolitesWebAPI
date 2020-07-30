namespace CoinCoinsInsolites.WebApi.Controllers
{
    using CoinCoinsInsolites.BusinessObject;
    using CoinCoinsInsolites.IBusiness;
    using CoinCoinsInsolites.WebApi.Route;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route(PlaceRoute.RoutePrefix)]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceBusiness placeBusiness;

        public PlaceController(IPlaceBusiness placeBusiness)
        {
            this.placeBusiness = placeBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Place placeToCreate)
        {
            KeyValuePair<bool, Place> result = await this.placeBusiness.Create(placeToCreate);

            if (!result.Key)
            {
                return this.BadRequest(result.Value.ValidationService.ModelState);
            }

            return this.Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            KeyValuePair<bool, Place> result = await this.placeBusiness.Delete(id);

            if (!result.Key)
            {
                return this.BadRequest(result.Value.ValidationService.ModelState);
            }

            return this.Ok(result.Value);
        }

        [Route(PlaceRoute.FindMostRecentPlace + "{count}")]
        [HttpGet]
        public async Task<IActionResult> FindMostRecentPlace(int count)
        {
            return this.Ok(await this.placeBusiness.FindMostRecentPlace(count));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Place place = await this.placeBusiness.Get(id);

            if (place == null)
            {
                return this.NotFound();
            }

            return this.Ok(place);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return this.Ok(await this.placeBusiness.List());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Place placeToUpdate)
        {
            KeyValuePair<bool, Place> result = await this.placeBusiness.Update(id, placeToUpdate);

            if (!result.Key)
            {
                return this.BadRequest(result.Value.ValidationService.ModelState);
            }

            return this.Ok(result.Value);
        }
    }
}