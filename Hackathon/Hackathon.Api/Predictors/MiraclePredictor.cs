using Hackathon.Api.ModelInput;

namespace Hackathon.Api.Predictors;

public class MiraclePredictor
{
    private readonly ILogger<MiraclePredictor> _logger;

    public MiraclePredictor(ILogger<MiraclePredictor> logger)
    {
        _logger = logger;
    }

    private static double CalculateRisk(double linearComp)
    {
        var linearCompExp = Math.Exp(linearComp);
        var risk = linearCompExp / (1 + linearCompExp);
        return risk;
    }

    private static int CalculateMiracleScore(MiracleInput input)
    {
        var miracleScore = input.UnWitnessed +
            input.InitialRhythm +
            input.TwoRhythms +
            input.Age60 +
            2 * input.Age80 +
            input.LowpH +
            input.UnreactivePupils +
            2 * input.Adrenaline;

        return miracleScore;
    }

    private static double CalculateAlgorithm1Result(MiracleInput input)
    {
        var linearComp = -3.8424 +
                         1.1220 * input.UnWitnessed +
                         1.4810 * input.InitialRhythm +
                         1.3339 * input.TwoRhythms +
                         1.5070 * input.Age60 +
                         1.5977 * input.Age80 +
                         0.8378 * input.LowpH +
                         0.9460 * input.UnreactivePupils +
                         2.0339 * input.Adrenaline;

        return CalculateRisk(linearComp);
    }

    private static double CalculateAlgorithm2Result(int miracleScore)
    {
        var linearComp = -3.7570 +
                         1.0781 * miracleScore;

        return CalculateRisk(linearComp);
    }

    public (int MiracleScore, double Algorithm1Result, double Algorithm2Result) CalculateMiracleResults(MiracleInput input)
    {
        var miracleScore = CalculateMiracleScore(input);
        var algorithm1Result = CalculateAlgorithm1Result(input);
        var algorithm2Result = CalculateAlgorithm2Result(miracleScore);

        _logger.LogInformation(
            "MIRACLE: Inputs: {Inputs}, MIRACLE Score: {MiracleScore}, Algorithm 1 Result: {Algorithm1Result}, Algorithm 2 Result: {Algorithm2Result}",
            input,
            miracleScore,
            algorithm1Result,
            algorithm2Result);

        return (miracleScore, algorithm1Result, algorithm2Result);
    }
}