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
    public ActionResult<KocarResponse> Post(KocarInputs request)
    {
        return Ok(new KocarResponse(_kocarPredictor.PredictKocar(request)));
    }
}