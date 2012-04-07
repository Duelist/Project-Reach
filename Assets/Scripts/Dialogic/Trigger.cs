using UnityEngine;
using System.Collections.Generic;

public class Trigger
{
	public enum Type {player_health_gt, player_health_lt, wave_number_eq, victory, defeat};
	private List<Type> type;
	private List<int> modifier;
	
	public Trigger(Type type)
	{
		this.type = new List<Type>();
		this.modifier = new List<int>();
	}
	
	public bool CheckTrigger()
	{
		for (int i = 0; i < type.Count; i++)
		{
			if (!TypeCheck(type[i],modifier[i]))
				return false;
		}
		
		return true;
	}
	
	public bool TypeCheck(Type type, int modifier)
	{
		if (type == Type.player_health_gt)
			if (GameStorage.player.GetHealth() <= modifier)
				return false;
		else if (type == Type.player_health_lt)
			if (GameStorage.player.GetHealth() >= modifier)
				return false;
		else if (type == Type.wave_number_eq)
			if (GameStorage.currentWave == modifier)
				return false;
		return true;
	}
}
