using RadioCarSimulator.Entities;
using System;

namespace RadioCarSimulator.Extensions
{
    internal static class OrientationHelper
    {
        internal static Orientation ParseOrientation(string orientation) => orientation.ToUpper() switch
        {
            "N" => Orientation.North,
            "S" => Orientation.South,
            "W" => Orientation.West,
            "E" => Orientation.East,
            _ => throw new ArgumentOutOfRangeException(nameof(orientation), $"Unexpected orientation value {orientation}")
        };
    }
}
