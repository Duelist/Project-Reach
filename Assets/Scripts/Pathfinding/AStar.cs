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
				return GetPath(currentTile);
			
			open.Remove(currentTile);
			closed.Push(currentTile);
			ArrayList neighbours = map.GetNeighbours(currentTile);
			for (int i = 0; i < neighbours.Count; i++)
			{
				Tile endTile = (Tile) neighbours[i];
				if (closed.Contains(endTile))
					continue;
				
				float endTileCost = endTile.costSoFar + GetCost(currentTile, endTile);
				
				if (!open.Contains(endTile))
					open.Push(endTile);
				else if (endTileCost >= endTile.costSoFar)
					continue;
				
				float endTileHeuristic = HeuristicEstimate(endTile, goal, heuristicWeight);
				endTile.costSoFar = endTileCost;
				endTile.parentTile = currentTile;
				endTile.estimatedTotalCost = endTileCost + endTileHeuristic;
			}
		}
		return new ArrayList();
	}
	
	private static float HeuristicEstimate(Tile currentTile, Tile goal, float heuristicWeight)
	{
		return EuclideanDistance(currentTile.tileObject.transform.position, goal.tileObject.transform.position) * heuristicWeight;
	}
	
	private static float GetCost(Tile currentTile, Tile goal)
	{
		return EuclideanDistance(currentTile.tileObject.transform.position, goal.tileObject.transform.position);
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