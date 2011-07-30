#pragma strict
function Enemy (n : String, x : int, z : int, hp : int, ms: int, arm : int, imList : Array, tZone : Array) {
	var name : String;
	var position : Vector3;
	var maxHP : int;
	var curHP : int;
	var moveSpeed : int;
	var armour :  int; // a damage percentage that the enemy reduces
	// Typing:
	// immunityList : Array <String>
	// timeZone : Array <String>
	// debuffList : Array <String>
	var immunityList : Array;
	var timeZone : Array;
	var debuffList : Array;
	
	// Enemy Constructor
	name = n;
	position = new Vector3 (x,0,z);
	maxHP = hp;
	curHP = hp;
	moveSpeed = ms;
	
	if (arm > 100){
		armour = 100;
	}
	else {
		armour = arm;
	}
	
	immunityList = new Array();
	if (imList != null){
		immunityList = imList;
	}
	
	timeZone = new Array();
	if (tZone != null){
		timeZone = tZone;
	}
	debuffList = new Array();
	
	// Big Mob Constructor
	/*var bigMobCons = function (x : int, y : int, ms : int, hp : int, arm, String im, String tZone){
		position = new Vector (x,y);
		maxHP = hp;
		curHP = hp;
		setArmour(arm);
		immunityList = new ArrayList<String>();
		immunityList.add(im);
		timeZone = new ArrayList<String>();
		debuffList = new Arraylist<String>();
		timeZone.add(tZone);
	}
	
	// Basic Mob Constructor
	public void enemy (int x, int y, int hp){
		position = new Vector (x,y);
		maxHP = hp;
		curHP = hp;
		armour = 0;
		immunityList = new ArrayList<String>();		
		timeZone = new ArrayList<String>();
		debuffList = new Arraylist<String>();
	}*/
	
	// Getters and Setters
	// returns Vector3
	var getPosition = function (){
		return position;
	};
	// returns int
	var getPositionX = function (){
		return position.x;
	};
	// returns int
	var getPositionZ = function (){
		return position.z;
	};
	var setPositionVec = function (p : Vector3){
		position = p;
	};
	var setPosition = function (x : int, z : int){
		position.x = x;
		position.z = z;
	};

	// returns int
	var getMaxHP = function (){
		return maxHP;
	};
	var setMaxHP = function (mHP : int){
		maxHP = mHP;
	};
	
	// returns int
	var getCurHP = function (){
		return curHP;
	};
	var setCurHP = function (cHP : int){
		 curHP = cHP;
	};
	
	// returns int
	var getMoveSpeed = function (){
		return moveSpeed;
	};
	var setMoveSpeed = function (ms : int){
		 moveSpeed = ms;
	};
	
	// returns int
	var getArmour = function (){
		return armour;
	};
	var setArmour = function (arm : int){
		if (arm > 100){
			armour = 100;
		}
		else {
			armour = arm;
		}
	};
	
	// ArrayList getters and Modifiers
	// -- ImmunityList -- //
	// Returns Array <String>
	var getImmunityList = function (){
		return immunityList;
	};
	// Returns true if the immunity exists in the list, false ow
	var containsImmunity = function (imType: String){
		var temp = false;
		for (var i = 0; i < immunityList.length; i++){
			if (immunityList[i] == imType){
				temp = true;
			}
		}
		return temp;
	};
	// Returns true if an Immunity is added, false ow
	var addImmunity = function (imType: String){
		if (containsImmunity(imType)){
			return false;
		}
		immunityList.push(imType);
		return true;
	};
	// Returns true if an Immunity was removed, false ow
	var removeImmunity = function (imType: String){
		var temp = false;
		for (var i = 0; i < immunityList.length; i++){
			if (immunityList[i] == imType){
				immunityList.RemoveAt(i);
				temp = true;
			}
		}
		return temp;
	};
	
	// -- TimeZone -- //
	// Returns Array <String>
	var getTimeZone = function (){
		return timeZone;
	};
	// Returns true if the timezone exists in the list, false ow
	var containsTimeZone = function (tzType : String){
		var temp = false;
		for (var i = 0; i < timeZone.length; i++){
			if (timeZone[i] == tzType){
				temp = true;
			}
		}
		return temp;
	};
	// Returns true if a TimeZone was added, false ow
	var addTimeZone = function (tzType : String){
		if (containsTimeZone(tzType)){
			return false;
		}
		timeZone.push(tzType);
		return true;
	};
	// Returns true if a TimeZone was removed, false ow
	var removeTimeZone = function (tzType : String){
		for (var i = 0; i < timeZone.length; i++){
			if (timeZone[i] == tzType){
				timeZone.RemoveAt(i);
			}
		}
	};
	
	// -- DebuffList -- //
	// Returns Array <String>
	var getDebuffList = function (){
		return debuffList;
	};
	// Returns true if the debuff exists in the list, false ow
	var containsDebuff = function (dbType: String){
		var temp = false;
		for (var i = 0; i < debuffList.length; i++){
			if (debuffList[i] == dbType){
				temp = true;
			}
		}
		return temp;
	};
	// Returns true if a debuff was added, false ow
	var addDebuff = function (dbType: String){
		if (containsDebuff(dbType)){
			return false;
		}
		debuffList.push(dbType);
		return true;	
	};
	// Returns true if a Debuff was removed, false ow
	var removeDebuff = function(dbType: String){
		var temp = false;
		for (var i = 0; i < debuffList.length; i++){
			if (debuffList[i] == dbType){
				debuffList.RemoveAt(i);
				temp = true;
			}
		}
		return temp;
	};
}