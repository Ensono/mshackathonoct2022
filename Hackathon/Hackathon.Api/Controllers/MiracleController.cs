using Hackathon.Api.ModelInput;
using Hackathon.Api.Predictors;
using Hackathon.Api.Request;
using Hackathon.Api.Response;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MiracleController : ControllerBase
    {
        private readonly MiraclePredictor _miraclePredictor;

        public MiracleController(MiraclePredictor miraclePredictor)
        {
            _miraclePredictor = miraclePredictor;
        }

        [HttpPost(Name = "Get Miracle scores")]
        public ActionResult<MiracleResponse> Post(MiracleRequest request)
        {
            var input = new MiracleInput(
                request.UnWitnessed ? 1 : 0,
                request.InitialRhythm ? 1 : 0,
                request.TwoRhythms ? 1 : 0,
                request.Age > 60 ? 1 : 0,
                request.Age > 80 ? 1 : 0,
                request.LowpH ? 1 : 0,
                request.UnreactivePupils ? 1 : 0,
                request.Adrenaline ? 1 : 0);

            var miracleResults = _miraclePredictor.CalculateMiracleResults(input);

            return Ok(new MiracleResponse(miracleResults.MiracleScore, miracleResults.Algorithm1Result, miracleResults.Algorithm2Result));
        }
    }
}
