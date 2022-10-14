using Hackathon.Api.ModelInput;
using Hackathon.Api.Predictors;
using Hackathon.Api.Request;
using Hackathon.Api.Response;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class KocarController : ControllerBase
{
    private readonly KocarPredictor _kocarPredictor;

    public KocarController(KocarPredictor kocarPredictor)
    {
        _kocarPredictor = kocarPredictor;
    }

    [HttpPost(Name = "GetKocarScore")]
    public ActionResult<KocarResponse> Post(KocarRequest request)
    {
        var input = new KocarInput(
            request.Territ ? 1 : 0,
            request.UtsteinCohort ? 0 : 1,
            request.Vasc ? 1 : 0,
            request.InitialRhythm ? 0 : 1,
            request.Age switch
            {
                <= 40 => 0,
                <= 70 => 1,
                > 70 => 2
            },
            request.NormalEcg ? 1 : 0,
            request.Ste ? 1 : 0,
            request.Rbbb ? 1 : 0,
            request.Tte ? 0 : 1);
        
        return Ok(new KocarResponse(_kocarPredictor.PredictKocar(input)));
    }
}