using Utilities.Attributes;

namespace Utilities.GeoLib
{
    public enum CardinalDirection
    {
        [StringValue("N")]
        North,
        [StringValue("NE")]
        NorthEast,
        [StringValue("E")]
        East,
        [StringValue("SE")]
        SouthEast,
        [StringValue("S")]
        South,
        [StringValue("SW")]
        SouthWest,
        [StringValue("W")]
        West,
        [StringValue("NW")]
        NorthWest,
    }
}
