#pragma strict
	
function Wall( xS : int, zS : int, xE : int, zE : int, ele : Array, dur : int) 
{

	var xStart : int;
	var zStart : int;
	var xEnd : int;
	var zEnd : int;
	var elementList : Array;
	var duration : int;
	var position : Vector3;

	// Wall Constructor
	xStart = xS;
	zStart = zS;
	xEnd = xE;
	zEnd = zE;
	position = new Vector3(xStart, 0, zStart);
	
	duration = dur;
	
	elementList = new Array ();
	if (ele != null) {
		elementList = ele;
	}
	
	var xDiff = Mathf.Abs(xStart - xEnd);
	var zDiff = Mathf.Abs(zStart - zEnd);
	
	if (xStart == xEnd) {  // We want a vertical wall
		for (var j : int = 0; j <= zDiff; j++) {
			var xCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			xCube.transform.position = Vector3(xStart, zStart + j, 0);
			xCube.transform.localScale = Vector3(1,1,1);
		}
	} else if (zStart == zEnd) {  // We want a horizontal wall
		for (var i : int = 0; i <= zDiff; j++) {
			var zCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			zCube.transform.position = Vector3(xStart + i, zStart, 0);
			zCube.transform.localScale = Vector3(1,1,1);
		}
	}
 
	// Returns true if element is in element list, false otherwise.
	var containsElement = function ( obj : String) : boolean {
		var i = elementList.length;
		while (i--) {
			if (elementList[i] === obj) {
				return true;
			}
		}
		return false;
	};

	// Returns position of element in element list, -1 otherwise.
	var indexOfElement = function ( arg : String ) : int {
		for ( var ix : int = 0; ix < elementList.length; ix++ ) {
			if ( arg === elementList[ix] ) return ix;
			}
		return -1;
	};
	
	// returns x starting position, as int
	var getXStartPos = function () : int
	{
		return xStart;
	};

	// returns z starting position, as int
	var getZStartPos = function () : int
	{
		return zStart;
	};

	// returns x end position, as int
	var getXEndPos = function () : int
	{
		return xEnd;
	};

	// returns z end position, as int
	var getZEndPos = function () : int
	{
		return zEnd;
	};

	// Returns Array <String> of Elements that Wall is effected by.
	var getElementList = function () : Array 
	{
		return elementList;
	};

	// Returns duration of wall, as int
	var getDuration = function () : int
	{
		return duration;
	};

	// Setters of X and Z coordinates //
	var setXStartPos = function (x : int) 
	{
		xStart = x;
	};

	var setZStartPos = function (z : int) 
	{
		zStart = z;
	};

	var setXEndPos = function (x : int) 
	{
		xEnd = x;
	};

	var setZEndPos = function (z : int) 
	{
		zEnd = z;
	};

	// Adds element to wall element list, returns true if succeeded, false otherwise
	var addElement = function (ele : String) 
	{
		if ( !(containsElement(ele)) ) 
		{
			elementList.push(ele);
			return true;
		}
		return false;
	};

	// removes element from wall element list //
	var removeElement = function (ele : String) 
	{
		var idx = indexOfElement(ele);
		if (idx != -1 ) {
			elementList.splice(idx, 1);
		}
	};

	// Set duration of wall.
	var setDuration = function (dur : int) {
		duration = dur;
	};
}