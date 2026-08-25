using System;
using AplikacijaZaUpravljanjeSkladistem.Models;

namespace AplikacijaZaUpravljanjeSkladistem.Services;

// Kreacioni sablon: Factory Method
public static class NalogFactory
{
    public static Nalog Kreiraj(TipNaloga tip) => tip switch
    {
        TipNaloga.Prijemnica => new Prijemnica(),
        TipNaloga.Otpremnica => new Otpremnica(),
        _ => throw new ArgumentOutOfRangeException(nameof(tip))
    };
}
