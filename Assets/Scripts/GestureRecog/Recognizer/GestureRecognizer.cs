using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestureRecognizer
{
    // recognizer settings
    static int maxPoints = 64;					// max number of point in the gesture
    static int sizeOfScaleRect = 500;			// the size of the bounding box
    static int compareDetail = 15;				// Number of matching iterations (CPU consuming) 
    static int angleRange = 45;					// Angle detail level of when matching with templates 
    public static int recordDone = 0;
    public static string stringToEdit = "Enter a Template name";
    public static ArrayList newTemplateArr;
    
    public static string startRecognizer (ArrayList pointArray)
    {
	    // main recognizer function
	    pointArray = optimizeGesture(pointArray, maxPoints);
	    Vector2 center = calcCenterOfGesture(pointArray);
        Vector2 v = (Vector2)pointArray[0];
	    float radians = Mathf.Atan2(center.y - v.y, center.x - v.x);
	    pointArray = RotateGesture(pointArray, -radians, center);
	    pointArray = ScaleGesture(pointArray, sizeOfScaleRect);
	    pointArray = TranslateGestureToOrigin(pointArray);
	    string match = gestureMatch(pointArray); 
		return match;
    }

    public static void recordTemplate (ArrayList pointArray)
    {
	    // record function
	    pointArray = optimizeGesture(pointArray, maxPoints);
        Vector2 center = calcCenterOfGesture(pointArray);
        Vector2 v = (Vector2)pointArray[0];
	    float radians = Mathf.Atan2(center.y - v.y, center.x - v.x);
	    pointArray = RotateGesture(pointArray, -radians, center);
	    pointArray = ScaleGesture(pointArray, sizeOfScaleRect);
	    pointArray = TranslateGestureToOrigin(pointArray);
    	
	    newTemplateArr = new ArrayList ();
	    newTemplateArr = pointArray;

	    recordDone = 1;
    }

    static ArrayList optimizeGesture (ArrayList pointArray, int maxPoints)
    {
	    // take all the points in the gesture and finds the correct points compared with distance and the maximun value of points
    	
	    // calc the interval relative the length of the gesture drawn by the user
	    float interval = calcTotalGestureLength(pointArray) / (maxPoints - 1);
    	
	    // use the same starting point in the new array from the old one. 
	    ArrayList optimizedPoints = new ArrayList();
        optimizedPoints.Add(pointArray[0]);
    	
	    float tempDistance = 0.0f;
    	
	    // run through the gesture array. Start at i = 1 because we compare point two with point one)
	    for (int i = 1; i < pointArray.Count; ++i)
        {
            float currentDistanceBetween2Points = calcDistance((Vector2)pointArray[i - 1], (Vector2)pointArray[i]);
    		
		    if ((tempDistance + currentDistanceBetween2Points) >= interval)
            {
                Vector2 v1 = (Vector2)pointArray[i - 1];
                Vector2 v = (Vector2)pointArray[i];

			    // the calc is: old pixel + the differens of old and new pixel multiply  
			    float newX = v1.x + ((interval - tempDistance) / currentDistanceBetween2Points) * (v.x - v1.x);
			    float newY = v1.y + ((interval - tempDistance) / currentDistanceBetween2Points) * (v.y - v1.y);
    			
			    // create new point
			    Vector2 newPoint = new Vector2(newX, newY);
    			
			    // set new point into array
			    optimizedPoints.Add(newPoint);

                ArrayList temp = pointArray.GetRange(i, pointArray.Count - i - 1);
                Vector2 last = (Vector2)pointArray[pointArray.Count - 1];
                pointArray.SetRange(i + 1, temp);
                pointArray.Add(last);
                //pointArray.InsertRange(i + 1, temp);
			    pointArray.Insert(i, newPoint);
    			
			    tempDistance = 0.0f;
		    }
            else
            {
			    // the point was too close to the last point compared with the interval,. Therefore the distance will be stored for the next point to be compared.
			    tempDistance += currentDistanceBetween2Points;
		    }
	    }
    	
	    // Rounding-errors might happens. Just to check if all the points are in the new array
	    if (optimizedPoints.Count == maxPoints - 1) 
        {
            Vector2 v = (Vector2)pointArray[pointArray.Count - 1];
		    optimizedPoints.Add(new Vector2(v.x, v.y));
	    }

	    return optimizedPoints;
    }


    static ArrayList RotateGesture(ArrayList pointArray, float radians, Vector3 center)  
    {
	    // loop through original array, rotate each point and return the new array
	    ArrayList newArray = new ArrayList();
	    float cos = Mathf.Cos(radians);
	    float sin = Mathf.Sin(radians);
    	
	    for (int i = 0; i < pointArray.Count; ++i) 
        {
            Vector2 v = (Vector2)pointArray[i];
		    float newX = (v.x - center.x) * cos - (v.y - center.y) * sin + center.x;
		    float newY = (v.x - center.x) * sin + (v.y - center.y) * cos + center.y;
		    newArray.Add(new Vector2(newX, newY));
	    }
	    return newArray;
    }

    static ArrayList ScaleGesture(ArrayList pointArray, int size)
    {
	    // equal min and max to the opposite infinity, such that every gesture size can fit the bounding box.
	    float minX = Mathf.Infinity;
	    float maxX = Mathf.NegativeInfinity; 
	    float minY = Mathf.Infinity;
	    float maxY = Mathf.NegativeInfinity;
    	
	    // loop through array. Find the minimum and maximun values of x and y to be able to create the box
        foreach (Vector2 v in pointArray)
        {
		    if (v.x < minX) minX = v.x;
		    if (v.x > maxX) maxX = v.x;
		    if (v.y < minY) minY = v.y;
		    if (v.y > maxY) maxY = v.y;
	    }
    	
	    // create a rectangle surronding the gesture as a bounding box.
	    Rect BoundingBox = new Rect(minX, minY, maxX - minX, maxY - minY);
	    ArrayList newArray = new ArrayList();

        foreach (Vector2 v in pointArray)
        {
		    float newX = v.x * (size / BoundingBox.width);
		    float newY = v.y * (size / BoundingBox.height);
		    newArray.Add(new Vector2(newX, newY));
	    }

	    return newArray;
    }


    static ArrayList TranslateGestureToOrigin(ArrayList pointArray) 
    {
        Vector2 origin = new Vector2(0,0);
	    Vector3 center = calcCenterOfGesture(pointArray);
	    ArrayList newArray = new ArrayList();

        foreach (Vector2 v in pointArray)
        {
		    float newX = v.x + origin.x - center.x;
		    float newY = v.y + origin.y - center.y;
		    newArray.Add(new Vector2(newX, newY));
	    }

	    return newArray;
    }


    // --------------------------------  		     GESTURE OPTIMIZING DONE   		----------------------------------------------------------------
    // -------------------------------- 		START OF THE MATCHING PROCESS	----------------------------------------------------------------

    static string gestureMatch(ArrayList pointArray) 
    {
	    float tempDistance = Mathf.Infinity;
	    int count = 0;

	    for (int i = 0; i < GestureTemplates.Templates.Count; ++i) 
        {
		    float distance = calcDistanceAtOptimalAngle(pointArray, (ArrayList)GestureTemplates.Templates[i], -angleRange, angleRange);
    		
		    if (distance < tempDistance)	
            {
			    tempDistance = distance;
			    count = i;
		    }
	    }

	    float HalfDiagonal = 0.5f * Mathf.Sqrt(Mathf.Pow(sizeOfScaleRect, 2) + Mathf.Pow(sizeOfScaleRect, 2));
	    float score = 1.0f - (tempDistance / HalfDiagonal);
    	
	    // print the result
    	
	    if (score < 0.6f)
        {
		   //Debug.Log("NO MATCH " + score );
//		    Gesture.GuiText.guiText.text = "RESULT: NO MATCH " +  "\n" + "SCORE: " + Mathf.Round(100 * score) +"%";
			return "none";
	    } else {
		    //Debug.Log("RESULT: " + GestureTemplates.TemplateNames[count] + " SCORE: " + score);
//		    Gesture.GuiText.guiText.text = "RESULT: " + GestureTemplates.TemplateNames[count] + "\n" + "SCORE: " + Mathf.Round(100 * score) +"%";
			return (string)(GestureTemplates.TemplateNames[count]);
	    }    

    }


    // --------------------------------  		   GESTURE RECOGNIZER DONE   		----------------------------------------------------------------
    // -------------------------------- 		START OF THE HELP FUNCTIONS		----------------------------------------------------------------


    static Vector2 calcCenterOfGesture(ArrayList pointArray)
    {
	    // finds the center of the drawn gesture
    	
	    float averageX = 0.0f;
	    float averageY = 0.0f;
    	
        foreach (Vector2 v in pointArray)
        {
		    averageX += v.x;
		    averageY += v.y;
	    }
    	
	    averageX = averageX / pointArray.Count;
	    averageY = averageY / pointArray.Count;
    	
	    return new Vector2(averageX, averageY);
    }	

    static float calcDistance(Vector2 point1, Vector2 point2)
    {
	    // distance between two vector points.
	    float dx = point2.x - point1.x;
	    float dy = point2.y - point1.y;
    	
	    return Mathf.Sqrt(dx * dx + dy * dy);
    }

    static float calcTotalGestureLength(ArrayList pointArray)
    { 
	    // total length of gesture path
	    float length = 0.0f;
	    for (int i = 1; i < pointArray.Count; ++i)
        {
            length += calcDistance((Vector2)pointArray[i - 1], (Vector2)pointArray[i]);
	    }

	    return length;
    }


    static float calcDistanceAtOptimalAngle(ArrayList pointArray, ArrayList T, float negativeAngle, float positiveAngle) {
	    // Create two temporary distances. Compare while running through the angles. 
	    // Each time a lower distace between points and template points are foound store it in one of the temporary variables. 
    	
	    float radian1 = Mathf.PI * negativeAngle + (1.0f - Mathf.PI ) * positiveAngle;
	    float tempDistance1 = calcDistanceAtAngle(pointArray, T, radian1);
    	
	    float radian2 = (1.0f - Mathf.PI ) * negativeAngle + Mathf.PI  * positiveAngle;
	    float tempDistance2 = calcDistanceAtAngle(pointArray, T, radian2);
    	
	    // the higher the number compareDetail is, the better recognition this system will perform. 
	    for (int i = 0; i < compareDetail; ++i)
        {
		    if (tempDistance1 < tempDistance2)
            {
			    positiveAngle = radian2;
			    radian2 = radian1;
			    tempDistance2 = tempDistance1;
			    radian1 = Mathf.PI * negativeAngle + (1.0f - Mathf.PI) * positiveAngle;
			    tempDistance1 = calcDistanceAtAngle(pointArray, T, radian1);
		    } 
            else 
            {
			    negativeAngle = radian1;
			    radian1 = radian2;
			    tempDistance1 = tempDistance2;
			    radian2 = (1.0f - Mathf.PI) * negativeAngle + Mathf.PI * positiveAngle;
			    tempDistance2 = calcDistanceAtAngle(pointArray, T, radian2);
		    }
	    }

	    return Mathf.Min(tempDistance1, tempDistance2);
    }

    static float calcDistanceAtAngle(ArrayList pointArray, ArrayList T, float radians) 
    {
	    // calc the distance of template and user gesture at 
	    Vector2 center = calcCenterOfGesture(pointArray);
	    ArrayList newpoints = RotateGesture(pointArray, radians, center);

	    return calcGestureTemplateDistance(newpoints, T);
    }	

    static float calcGestureTemplateDistance(ArrayList newRotatedPoints, ArrayList templatePoints) 
    {
	    // calc the distance between gesture path from user and the template gesture
	    float distance = 0.0f;

        // assumes newRotatedPoints.length == templatePoints.length
	    for (int i = 0; i < newRotatedPoints.Count; ++i)
        {
            distance += calcDistance((Vector2)newRotatedPoints[i], (Vector2)templatePoints[i]);
	    }

	    return distance / newRotatedPoints.Count;
    }
}