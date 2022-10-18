using Hackathon.Api.ModelInput;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Hackathon.Api.Predictors;

public class KocarPredictor
{
    private const string TrainedModelPath = "./TrainedModel/kocar_culp_xgbmodel.onnx";
    private const string TrainedModelInputTensorName = "float_input";
    
    private readonly InferenceSession _inferenceSession;
    private readonly ILogger<KocarPredictor> _logger;

    public KocarPredictor(ILogger<KocarPredictor> logger)
    {
        _logger = logger;
        // TODO: Change to load model from blob storage
        _inferenceSession = new InferenceSession(TrainedModelPath);
        _logger.LogInformation("Loaded model from {TrainedModelPath}", TrainedModelPath);
    }

    public float PredictKocar(KocarInput input)
    {
        var inputs = new float[]
        {
            input.Territ,
            input.UtsteinCohort,
            input.Vasc,
            input.InitialRhythm,
            input.Age,
            input.NormalEcg,
            input.Ste,
            input.Rbbb,
            input.Tte
        };
        var inputTensor = new DenseTensor<float>(inputs, new[] { 1, 9 });
        var featuresInput = new List<NamedOnnxValue>
            { NamedOnnxValue.CreateFromTensor(TrainedModelInputTensorName, inputTensor) };

        using var output = _inferenceSession.Run(featuresInput);
        var result = output.Last().AsTensor<float>().GetValue(1);
        
        _logger.LogInformation("KOCAR: Inputs: {Inputs}, Output: {Output}", input, result);

        return result;
    }
}