using Hackathon.Api.Request;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Hackathon.Api.Predictors;

public class KocarPredictor
{
    private const string TrainedModelPath = "./TrainedModel/kocar_culp_xgbmodel.onnx";
    private readonly InferenceSession _inferenceSession;
    private readonly ILogger<KocarPredictor> _logger;

    public KocarPredictor(ILogger<KocarPredictor> logger)
    {
        _logger = logger;
        // TODO: Change to load model from blob storage
        _inferenceSession = new InferenceSession(TrainedModelPath);
        _logger.LogInformation("Loaded model from {TrainedModelPath}", TrainedModelPath);
    }

    public float PredictKocar(KocarInputs inputs)
    {
        var inputTensor = new DenseTensor<float>(new[]
            {
                inputs.Territ,
                inputs.UtsteinCohort,
                inputs.Vasc,
                inputs.InitialRhythm,
                inputs.Age,
                inputs.NormalEcg,
                inputs.Ste,
                inputs.Rbbb,
                inputs.Tte
            },
            new[] { 1, 9 });
        var featuresInput = new List<NamedOnnxValue>
            { NamedOnnxValue.CreateFromTensor("float_input", inputTensor) };

        using var output = _inferenceSession.Run(featuresInput);
        var result = output.Last().AsTensor<float>().GetValue(0);
        _logger.LogInformation("Received inputs {Inputs}. Result = {Result}", inputs, result);

        return result;
    }
}