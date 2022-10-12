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

    public float PredictKocar(float[] inputs)
    {
        var inputTensor = new DenseTensor<float>(inputs, new[] { 1, 9 });
        var featuresInput = new List<NamedOnnxValue>
            { NamedOnnxValue.CreateFromTensor(TrainedModelInputTensorName, inputTensor) };

        using var output = _inferenceSession.Run(featuresInput);
        var result = output.Last().AsTensor<float>().GetValue(0);
        _logger.LogInformation("Received inputs {Inputs}. Output = {Result}", inputs, result);

        return result;
    }
}