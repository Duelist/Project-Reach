public abstract class effect {
	private int damage = 1;
	private float duration;
	private String effect;
	private float armour = 100;
	private float speed = 100;
	private float armour_reduction = 0.5;
	private float speed_reduction = 0.5;
	private float damage_increase = 2.0;

	// Effect Constructor
	public void effect ( String eff, float dur ) {
		type = eff;
		duration = dur;	
	}
	
	// Setters and getters
	public String getEffect() {
		return this.effect;
	}

	public void set_Effect() {
		if ( this.type == "earth" ) {
			armour = armour * armour_reduction;
			effect = "armour reduction";
		} else if ( this.type == "wind" ) {
			speed = speed * speed_reduction;
			effect = "slow";
		} else if ( this.type == "fire" ) {
			damage = damage * damage_increase;
			effect = "increased damage";
		} else if ( this.type == "ice" ) { // Ice increase the speed, then drops it to zero.
			if (duration > 1) { // enemy must go faster if the timer is greater than 2 seconds
				this.setSpeed(this.getArmour * S);
			} else if ( duration == 0 ) {
				speed = 0;
			}
			effect = "slip and slide";
		}
	}

	public void setarmourReduction( float red ) {
		armour_reduction = red;	
	}

	public void setdamageIncrease( float red ) {
		speed_reduction = red;	
	}		

	public void setarmourReduction( float inc ) {
		damage_increase = inc;	
	}

	public void getarmourReduction( float red ) {
		return armour_reduction;	
	}

	public void setdamageIncrease( float red ) {
		return speed_reduction;	
	}		

	public void setarmourReduction( float inc ) {
		return damage_increase;	
	}	
}