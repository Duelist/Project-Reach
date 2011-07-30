#pragma strict
// Stores a list of enemy references
var enemyArray = new Array ();
// wayPointArray object
var waypointArray : Array;
// Stores the waypoint information for each enemy
var waypointCounterArray : int[];
// need spawn to append an extra element to the wayPointCounterArray when an enemy is spawned

function Update () {
	spawn();
	mobMovement(waypointArray);
}

private function spawn (){
}

// Waypoint array is a list of Waypoints that contain the position of waypoints and the direction to move.
private function mobMovement (wayPointArray){
	// each enemy's position is compared to their next waypoint's position.
	// if they are at the last way point, player loses life.
	// if they are at the waypoint, increase the waypoint counter array at i.
	// if they are not at the waypoint, move towards the waypoint at its speed.
	for (var i = 0; i < waypointCounterArray.length; i++){
		// i know ian said the waypoints are an area, but this will have to do for now...
		// TODO: will have to write a comparison operation
		/*if (enemyArray[i].getPosition() == waypointArray[waypointCounterArray[i]].getPosition())*/
		if (true){
			//waypointCounterArray[i] = waypointCounterArray[i] + 1;
			if (waypointCounterArray[i] >= waypointArray.length){
				playerDamage();
			}
		}
		else {
			var xAdd = 0;
			var yAdd = 0;
			/*if (waypointArray[waypointCounterArray[i]].getDirection().equals("up")){
				yAdd = -(enemyArray[i].getMoveSpeed());
			}
			else if (waypointArray[waypointCounterArray[i]].getDirection().equals("down")){
				yAdd = enemyArray[i].getMoveSpeed();
			}
			else if (waypointArray[waypointCounterArray[i]].getDirection().equals("left")){
				xAdd = -(enemyArray[i].getMoveSpeed());
			}
			else if (waypointArray[waypointCounterArray[i]].getDirection().equals("right")){
				xAdd = enemyArray[i].getMoveSpeed();
			}
			else{
			}
			enemyArray[i].setPosition(enemyArray[i].getPositionX + xAdd, enemyArray[i].getPositionY + yAdd);
			*/
		}
	}
}

private function playerDamage(){
}