# Utilities
Small Library to provide simple functionality shared by multiple projects.

StringValueAttribute:
Defines an Attribute called StringValue that can be added to other objects. 
Uses an extension method to access it, currently only implemented for the Enum type.
Example: 

enum myEnum {
   [StringValue("This can be any string)]
   any = 0,
   [StringValue("3ven a string that starts with a number")]
   numbers = 1,
   [StringValue("Or a string that contains *-Special-* characters")]
   special = 2
}

string someString = myEnum.any.GetStringValue()

GeoLib:
Small lightweight library to work easily with Geographic Coordinates.
   Coordinate: Simple class that holds the Degrees, Minutes, Seconds (DMS) and Hemisphere of Lattitude or Longitude with quick conversion to distance and formatted string output.
   GeoPoint: Simple class to represent a 3-Dimensional Map Poisition using the Coordinate Class. Includes distance calculations between points and formatted string output.
   
Logging:
Very light and simple Error Logging.