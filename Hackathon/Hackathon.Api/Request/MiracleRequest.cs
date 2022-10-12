namespace Hackathon.Api.Request;

public record MiracleRequest(
    bool UnWitnessed,
    bool InitialRhythm,
    bool TwoRhythms,
    int Age,
    bool LowpH,
    bool UnreactivePupils,
    bool Adrenaline);