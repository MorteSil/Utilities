using Utilities.Attributes;

namespace Utilities.GeoLib
{
    public enum CardinalDirection
    {
        [StringValue("N")]
        North = 0,
        [StringValue("NE")]
        NorthEast = 4,
        [StringValue("E")]
        East = 1,
        [StringValue("SE")]
        SouthEast = 5,
        [StringValue("S")]
        South = 2,
        [StringValue("SW")]
        SouthWest = 6,
        [StringValue("W")]
        West = 3,
        [StringValue("NW")]
        NorthWest = 7,
    }
}
