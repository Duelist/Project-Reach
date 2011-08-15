using UnityEngine;
using System.Collections;

public class AStar 
{
	public static PriorityQueue open;
	public static PriorityQueue closed;
	
	public static ArrayList Search(Tile start, Tile goal, Map map, float heuristicWeight)
	{
		open = new PriorityQueue();
		open.Push(start);
		start.costSoFar = 0;
		start.estimatedTotalCost = HeuristicEstimate(start,goal,heuristicWeight);
		
		closed = new PriorityQueue();
		
		Tile currentTile = null;
		
		while (open.GetLength() != 0)
		{
			currentTile = open.Front();
			
			if (currentTile == goal)
				break;
			
			ArrayList neighbours = new ArrayList();
			neighbours = map.GetNeighbours(currentTile, goal);
			
			for (int n = 0; n != neighbours.Count; n++)
			{
				Tile endTile = (Tile) neighbours[n];
				float incCost = GetCost(currentTile, endTile);
				float endTileCost = currentTile.costSoFar + incCost;
				
				if (closed.Contains(endTile))
				{
					if (endTile.costSoFar <= endTileCost)
						continue;
					
					closed.Remove(endTile);
				}
				else if(open.Contains(endTile))
				{
					if(endTile.costSoFar <= endTileCost)
						continue;
				}
				
				float endTileHeuristic = HeuristicEstimate(endTile, goal, heuristicWeight);
				endTile.costSoFar = endTileCost;
				endTile.parentTile = currentTile;
				endTile.estimatedTotalCost = endTileCost + endTileHeuristic;
				
				if (!open.Contains(endTile))
				{
					open.Push(endTile);
				}
			}
			
			closed.Push(currentTile);
			open.Remove(currentTile);
		}
		if (!currentTile.Equals(goal))
		{
			return new ArrayList();
		}
		else
		{
			return GetPath(currentTile);
		}
	}
	
	private static float HeuristicEstimate(Tile currentTile, Tile goal, float heuristicWeight)
	{
		return EuclideanDistance(currentTile.position, goal.position) * heuristicWeight;
	}
	
	private static float GetCost(Tile currentTile, Tile goal)
	{
		return EuclideanDistance(currentTile.position, goal.position);
	}
	
	private static ArrayList GetPath(Tile tile)
	{
		ArrayList path = new ArrayList();
		
		while (tile != null)
		{
			path.Add(tile);
			tile = tile.parentTile;
		}
		
		path.Reverse();
		return path;
	}
	
	public static float EuclideanDistance(Vector2 p1, Vector2 p2)
	{
		return (p1 - p2).magnitude;
	}
}