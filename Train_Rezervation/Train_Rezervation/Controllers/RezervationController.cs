using Microsoft.AspNetCore.Mvc;
using Train_Rezervation.Model;
using Train_Rezervation.Services;

namespace Train_Rezervation.Controllers
{
    [Route("api/[Controller]")]
    public class RezervationController : ControllerBase
    {
        private readonly IRezervationService service;

        public RezervationController(IRezervationService service)
        {
            this.service = service;
        }
        
        [HttpPost("rezervationstatus")]
        public async Task<RezervationResponse> GetRezervation([FromBody] RezervationRequest request)
        {
            return await service.GetRezervation(request);
        }
    }
}
