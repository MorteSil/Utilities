namespace Utilities.GeoLib
{
    /// <summary>
    /// Represents a 1-Dimensional Scalar Component of a Geographic Coordiante (Lattitude or Longitude)
    /// </summary>
    public class Coordinate
    {

        #region Properties
        /// <summary>
        /// Degrees Component of a Lattitude or Longitude Coordinate
        /// </summary>
        public double Degrees { get; set; } = 0;
        /// <summary>
        /// Minutes Component of a Lattitude or Longitude Coordinate
        /// </summary>
        public double Minutes { get; set; } = 0;
        /// <summary>
        /// Seconds Component of a Lattitude or Longitude Coordinate
        /// </summary>
        public double Seconds { get; set; } = 0;
        /// <summary>
        /// Global Hemisphere of the coordinate
        /// </summary>
        public CardinalDirection Position { get; set; } = CardinalDirection.North;
        #endregion Properties // Checked

        #region Functional Methods
        /// <summary>
        /// Convert the coordinate into a Double Value
        /// </summary>
        /// <returns></returns>
        public double ToDouble()
        {
            var result = Degrees + Minutes / 60 + Seconds / 3600;
            return Position == CardinalDirection.West || Position == CardinalDirection.South ? -result : result;
        }
        /// <summary>
        /// Get the <see cref="string"/> representation of the Coordinate.
        /// </summary>
        /// <returns>Coordinate in the "Degrees Minutes Seconds" format</returns>
        public override string ToString()
        {
            return Degrees + "º " + Minutes + "' " + Seconds + "'' " + Position;
        }
        #endregion Functional Methods

        #region Constructors
        /// <summary>
        /// Default Constructor for the <see cref="Coordinate"/> object.
        /// </summary>
        public Coordinate() { }
        /// <summary>
        /// Initializes a <see cref="Coordinate"/> based on the number of meters in <paramref name="value"/> and the <see cref="CardinalDirection"/> in <paramref name="direction"/>
        /// </summary>
        /// <param name="value">Distance in meters from the Prime Meridian (E/W) or Equator (N/S).</param>
        /// <param name="direction"><see cref="CardinalDirection"/> value. An <see cref="ArgumentException"/> will be thrown if the value is not North, South, East, or West.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="direction"/> is not one of the primary cardinal directions.</exception>
        public Coordinate(double value, CardinalDirection direction)
        {
            if (direction == CardinalDirection.NorthEast | direction == CardinalDirection.SouthEast | direction == CardinalDirection.SouthWest | direction == CardinalDirection.NorthWest)
                throw new ArgumentException("direction must be primary cardinal direction: N, E, S, or W.");
            // Validate Cardinal Directions
            if (value < 0 && direction == CardinalDirection.North)
                direction = CardinalDirection.South;
            if (value < 0 && direction == CardinalDirection.East)
                direction = CardinalDirection.West;
            if (value > 0 && direction == CardinalDirection.South)
                direction = CardinalDirection.North;
            if (value > 0 && direction == CardinalDirection.West)
                direction = CardinalDirection.East;

            var decimalValue = Convert.ToDecimal(value);

            decimalValue = Math.Abs(decimalValue);

            var degrees = decimal.Truncate(decimalValue);
            decimalValue = (decimalValue - degrees) * 60;

            var minutes = decimal.Truncate(decimalValue);
            var seconds = (decimalValue - minutes) * 60;

            Degrees = Convert.ToDouble(degrees);
            Minutes = Convert.ToDouble(minutes);
            Seconds = Convert.ToDouble(seconds);
            Position = direction;
        }
        /// <summary>
        /// Initializes a <see cref="Coordinate"/> object with the DMS Latitude and Longitude values provided.
        /// </summary>
        /// <param name="degrees">Degrees</param>
        /// <param name="minutes">Minutes</param>
        /// <param name="seconds">Seconds</param>
        /// <param name="direction"><see cref="CardinalDirection"/> value. An <see cref="ArgumentException"/> will be thrown if the value is not North, South, East, or West.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="direction"/> is not one of the primary cardinal directions.</exception>
        public Coordinate(double degrees, double minutes, double seconds, CardinalDirection direction)
        {
            if (direction == CardinalDirection.NorthEast | direction == CardinalDirection.SouthEast | direction == CardinalDirection.SouthWest | direction == CardinalDirection.NorthWest)
                throw new ArgumentException("direction must be primary cardinal direction: N, E, S, or W.");

            Degrees = degrees;
            Minutes = minutes;
            Seconds = seconds;
            Position = direction;
        }
        /// <summary>
        /// Makes a copy of an existing <see cref="Coordinate"/> object.
        /// </summary>
        /// <param name="coordinate"><see cref="Coordinate"/> object with the values to copy.</param>
        public Coordinate(Coordinate coordinate)
        {
            Degrees = coordinate.Degrees;
            Minutes = coordinate.Minutes;
            Seconds = coordinate.Seconds;
            Position = coordinate.Position;
        }
        #endregion Constructors

    }


}
