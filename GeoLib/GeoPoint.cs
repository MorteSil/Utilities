using System.Numerics;
using System.Text;
using Utilities.Attributes;

namespace Utilities.GeoLib
{
    /// <summary>
    ///  Represents a 3D Point on a Map
    /// </summary>
    public class GeoPoint
    {
        #region Properties
        /// <summary>
        /// The String Representation of the Lattitude (N/S) of this <see cref="GeoPoint"/> in Degress Minutes Seconds (DMS) Notation.
        /// </summary>
        public string Latitude
        { get { return _Latitude.ToString(); } }
        /// <summary>
        /// The String Representation of the Longitude (E/W) of this <see cref="GeoPoint"/> in Degress Minutes Seconds (DMS) Notation.
        /// </summary>
        public string Longitude
        { get { return _Longitude.ToString(); } }
        /// <summary>
        /// <para>The X Value of this <see cref="GeoPoint"/>.</para> 
        /// <para>The X Value is associated with Longitude. Negative Values indicate West Longitude Values.</para>
        /// </summary>
        public double X
        {
            get { return _Longitude.ToDouble(); }
            set { _Longitude = new Coordinate(value, value < 0 ? CardinalDirection.West : CardinalDirection.East); }
        }
        /// <summary>
        /// <para>The Y Value of this <see cref="GeoPoint"/>.</para> 
        /// <para>The Y Value is associated with Latitude. Negative Values indicate South Latitude Values.</para>
        /// </summary>
        public double Y
        {
            get { return _Latitude.ToDouble(); }
            set { _Latitude = new Coordinate(value, value < 0 ? CardinalDirection.South : CardinalDirection.North); }
        }
        /// <summary>
        /// The Elevation Component of this <see cref="GeoPoint"/>.
        /// </summary>
        public double Elevation
        {
            get { return _Elevation; }
            set { _Elevation = value; }
        }


        #endregion Properties

        #region Fields
        private Coordinate _Latitude = new ();
        private Coordinate _Longitude = new ();
        private double _Elevation = 0;
        #endregion Fields

        #region Functional Methods
        /// <summary>
        /// Set the Latitude Component using Degrees, Minutes, and Seconds with a Cardinal Direction
        /// </summary>
        /// <param name="degrees">Degrees</param>
        /// <param name="minutes">Minutes</param>
        /// <param name="seconds">Seconds</param>
        /// <param name="direction"><see cref="CardinalDirection"/> value. An <see cref="ArgumentException"/> will be thrown if the value is not East or West.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="direction"/> is not East or West.</exception>
        public void SetLatitude(double degree, double minutes, double seconds, CardinalDirection direction)
        {
            if (direction == CardinalDirection.NorthEast | direction == CardinalDirection.SouthEast | direction == CardinalDirection.SouthWest | direction == CardinalDirection.NorthWest | direction == CardinalDirection.North | direction == CardinalDirection.South)
                throw new ArgumentException("direction must be East or West.");
            _Latitude = new Coordinate(degree, minutes, seconds, direction);
            if (_Latitude.ToDouble() < 0)
                if (_Latitude.Position == CardinalDirection.East)
                    _Latitude.Position = CardinalDirection.West;
                else if (_Latitude.Position == CardinalDirection.West)
                    _Latitude.Position = CardinalDirection.East;
        }
        /// <summary>
        /// Set the Latitude Component using Degrees, Minutes, and Seconds with a Cardinal Direction
        /// </summary>
        /// <param name="degrees">Degrees</param>
        /// <param name="minutes">Minutes</param>
        /// <param name="seconds">Seconds</param>
        /// <param name="direction"><see cref="CardinalDirection"/> value. An <see cref="ArgumentException"/> will be thrown if the value is not North or South.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="direction"/> is not North or South.</exception>
        public void SetLongitude(double degrees, double minutes, double seconds, CardinalDirection direction)
        {
            if (direction == CardinalDirection.NorthEast | direction == CardinalDirection.SouthEast | direction == CardinalDirection.SouthWest | direction == CardinalDirection.NorthWest | direction == CardinalDirection.East | direction == CardinalDirection.West)
                throw new ArgumentException("direction must be North or South.");
            _Longitude = new Coordinate(degrees, minutes, seconds, direction);
            if (_Longitude.ToDouble() < 0)
                if (_Longitude.Position == CardinalDirection.East)
                    _Longitude.Position = CardinalDirection.West;
                else if (_Longitude.Position == CardinalDirection.West)
                    _Longitude.Position = CardinalDirection.East;
        }
        /// <summary>
        /// Calculates the distance between two Geographic Points.
        /// </summary>
        /// <param name="point1">A Point in the Calculation.</param>
        /// <param name="point2">A Point in the Calculation.</param>
        /// <param name="includeElevation">When <see langword="true"/>, includes the elevation in the distance calculation.</param>
        /// <returns>The distance between the two points.</returns>
        public static double CalculateDistance(GeoPoint point1, GeoPoint point2, bool includeElevation = false)
        {
            double dx = (point2.X - point1.X) * (point2.X-point1.X);
            double dy = (point2.Y - point2.Y) * (point2.Y-point1.Y);
            double dz = includeElevation ? (point2.Elevation - point1.Elevation) * (point2.Elevation - point1.Elevation) : 0;
            return Math.Sqrt(dx+dy+dz);
        }
        /// <summary>
        /// Calculates the distance between this Point and another Point.
        /// </summary>
        /// <param name="otherPoint">The Point to calculate the distance to.</param>
        /// <returns></returns>
        public double DistanceTo(GeoPoint otherPoint)
        {
            return CalculateDistance(this, otherPoint);
        }
        /// <summary>
        /// Calculates the 3-Component Vector between two Points.
        /// </summary>
        /// <param name="point1">One Point in the Calculation.</param>
        /// <param name="point2">One Point in the Calculation.</param>
        /// <returns></returns>
        public static Vector3 GetVector(GeoPoint point1, GeoPoint point2)
        {
            
            return new Vector3((float)(point2.X - point1.X), (float)(point2.Y - point1.Y), (float)(point2.Elevation - point1.Elevation));
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Latitude.ToString() + ", " + Longitude.ToString() + " Elevation: " + Elevation);
            return sb.ToString();
        }
        #endregion Functional Methods

        #region Constructors
        /// <summary>
        /// Initializes a default instance of the <see cref="GeoPoint"/> object.
        /// </summary>
        public GeoPoint()
        {

        }
        /// <summary>
        /// Initializes an instance of the <see cref="GeoPoint"/> object with the supplied values.
        /// </summary>
        /// <param name="x">The X (Longitude) Component</param>
        /// <param name="y">The Y (Latitude) Component</param>
        public GeoPoint(double x, double y)
        {
            X = x; Y = y;
        }
        /// <summary>
        /// Initializes an instance of the <see cref="GeoPoint"/> object with the supplied values.
        /// </summary>
        /// <param name="x">The X (Longitude) Component</param>
        /// <param name="y">The Y (Latitude) Component</param>
        /// <param name="elevation">The Elevation Component</param>
        public GeoPoint(double x, double y, double elevation)
            : this(x, y)
        {
            _Elevation = elevation;
        }
        /// <summary>
        /// Initializes a copy of the supplied <see cref="GeoPoint"/> object.
        /// </summary>
        /// <param name="geopoint">The <see cref="GeoPoint"/> object with the values to copy.</param>
        public GeoPoint(GeoPoint geopoint)
        {
            _Latitude = geopoint._Latitude;
            _Longitude = geopoint._Longitude;
            _Elevation = geopoint._Elevation;
        }
        /// <summary>
        /// Initializes a new <see cref="GeoPoint"/> object with the supplied values.
        /// </summary>
        /// <param name="x">The X (Longitude) Component</param>
        /// <param name="y">The Y (Latitude) Component</param>
        /// <param name="elevation">The Elevation Component</param>
        public GeoPoint(Coordinate x, Coordinate y, double elevation)
        {
            _Latitude = y;
            _Longitude = x;
            _Elevation = elevation;
        }
        #endregion Constructors
    }
}
