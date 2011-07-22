public abstract class enemy {
	private Vector<int> position;
	private int maxHP, curHP;
	private int moveSpeed;
	private int armour; // a damage percentage that the enemy reduces
	private ArrayList<String> immunityList;
	private ArrayList<String> timeZone;
	private ArrayList<String> debuffList;
	
	// Boss Constructor
	public void enemy (int x, int y, int hp, int arm, ArrayList<String> imList, ArrayList<String> tZone){
		position = new Vector (x,y);
		maxHP = hp;
		curHP = hp;
		setArmour(arm);
		
		immunityList = new ArrayList<String>();
		if (imList != null){
			immunityList = imList;
		}
		
		timeZone = new ArrayList<String>();
		if (tZone != null){
			timeZone = tZone;
		}
		
		debuffList = new Arraylist<String>();
	}
	
	// Big Mob Constructor
	public void enemy (int x, int y, int hp, int arm, String im, String tZone){
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
	}
	
	// Getters and Setters
	public Vector getPosition(){
		return position;
	}
	public int getPositionX(){
		position.get(0)
	}
	public int getPositionY(){
		position.get(1)
	}
	public void setPosition(Vector<int> p){
		if (p != null && p.size() == 2){
			position = p;
		}
	}
	public void setPosition(int x, int y){
		position.set(0, x);
		position.set(1, y);
	}
	
	
	public int getMaxHP (){
		return maxHP;
	}
	public void setMaxHP (int mHP){
		 maxHP = mHP;
	}
	
	public int getCurHP (){
		return curHP;
	}
	public void setCurHP (int cHP){
		 curHP = cHP;
	}
	
	public int getMoveSpeed (){
		return moveSpeed;
	}
	public void setMoveSpeed (int ms){
		 moveSpeed = ms;
	}
	
	public int getArmour (){
		return armour;
	}
	public void setArmour (int arm){
		if (arm > 100){
			armour = 100;
		}
		else {
			armour = arm;
		}
	}
	
	// ArrayList getters and Modifiers
	// -- ImmunityList -- //
	public ArrayList<String> getImmunityList (){
		return immunityList;
	}
	public void addImmunity(String imType){
		immunityList.add(imType);
	}
	public void removeImmunity(String imType){
		for (int i = 0; i < immunityList.size(); i++){
			if (immunityList.get(i).equals(imType)){
				immunityList.remove(i);
				break;
			}
		}
	}
	public Boolean containsImmunity (String imType){
		return immunityList.contains(imtype);
	}
	
	// -- TimeZone -- //
	public ArrayList<String> getTimeZone (){
		return timeZone;
	}
	public void addTimeZone(String tzType){
		timeZone.add(tzType);
	}
	public void removeTimeZone(String tzType){
		for (int i = 0; i < timeZone.size(); i++){
			if (timeZone.get(i).equals(tzType)){
				timeZone.remove(i);
				break;
			}
		}
	}
	public Boolean containsTimeZone (String tzType){
		return timeZone.contains(tztype);
	}
	
	// -- DebuffList -- //
	public ArrayList<String> getDebuffList (){
		return getDebuffList;
	}
	public void addDebuff(String dbType){
		debuffList.add(dbType);
	}
	public void removeDebuff(String dbType){
		for (int i = 0; i < debuffList.size(); i++){
			if (debuffList.get(i).equals(dbType)){
				debuffList.remove(i);
				break;
			}
		}
	}
	public Boolean containsDebuff (String dbType){
		return debuffList.contains(dbtype);
	}
}