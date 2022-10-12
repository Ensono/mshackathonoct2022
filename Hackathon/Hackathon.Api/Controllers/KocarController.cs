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
        var inputs = new float[]
        {
            request.Territ ? 1 : 0,
            request.UtsteinCohort ? 1 : 0,
            request.Vasc ? 1 : 0,
            request.InitialRhythm ? 1 : 0,
            request.Age ? 1 : 0,
            request.NormalEcg ? 1 : 0,
            request.Ste ? 1 : 0,
            request.Rbbb ? 1 : 0,
            request.Tte ? 1 : 0
        };
        return Ok(new KocarResponse(_kocarPredictor.PredictKocar(inputs)));
    }
}