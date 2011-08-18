#pragma strict
/* var damage : int = 1;
var effectDuration : int;
var eff : String;
var type : String;
// Armour and speed are percentages.
var armour : int = 1;
var speed : int = 1;
var armourReduction = 0.5;
var speedReduction = 0.5;
var speedIncrease = 2;
var damage_increase = 2;
*/
// Effect Constructor

function effect (ele : String, dur : int) {
	var damage : int = 1;
	var effectDuration : int;
	var eff : String;
	var element : String;
	// Armour and speed are percentages.
	var armour : int = 1;
	var speed : int = 1;
	var armourReduction = 0.5;
	var speedReduction = 0.5;
	var speedIncrease = 2;
	var damageIncrease = 2;
	
	element = ele;
	effectDuration = dur;
	
	// returns effect
	var getEffect = function() {
		return eff;
	};

	//returns type
	var getElement = function () {
		return element;
	};

	var setEffect = function() {
		if (element == "earth" ) {
			armour = armour * armourReduction;
			eff = "armour reduction";
		} else if ( element == "wind" ) {
			speed = speed * speedReduction;
			eff = "slow";
		} else if ( element == "fire" ) {
			damage = damage * damageIncrease;
			eff = "increased damage";
		} else if ( element == "ice" ) {
			if (effectDuration > 1) {
				speed = speed * speedIncrease;
			} else if ( effectDuration == 0 ) {
				speed = 0;
			}
			eff = "slip and slide";
		}
	};


	var setarmourReduction = function ( red : int ) {
		armourReduction = red;	
	};

	var setdamageIncrease = function ( red : int ) {
		speedReduction = red;	
	};		

	var setDamageIncrease = function ( inc : int ) {
		damageIncrease = inc;	
	};

	var getarmourReduction = function () {
		return armourReduction;	
	};

	var getDamageIncrease = function() {
		return damageIncrease;	
	};		

	var setSpeed = function ( spd : int ) {
		speed = spd;	
	};

}
