﻿namespace Hackathon.Api.Request;

public record KocarRequest(
    bool Territ,
    bool UtsteinCohort,
    bool Vasc,
    bool InitialRhythm,
    int Age,
    bool NormalEcg,
    bool Ste,
    bool Rbbb,
    bool Tte);