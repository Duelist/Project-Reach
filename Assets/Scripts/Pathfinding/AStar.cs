using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar 
{
	private static List<Tile> open;
	private static List<Tile> closed;
	private static Tile[] cameFrom;
	private static float[] g_score;
	private static float[] h_score;
	private static float[] f_score;
	
	public static ArrayList Search(Tile start, Tile goal, Map map, float heuristicWeight)
	{
		open = new List<Tile>();
		closed = new List<Tile>();
		cameFrom = new Tile[(int)map.mapSize.x * (int)map.mapSize.y];
		
		g_score = new float[(int)map.mapSize.x * (int)map.mapSize.y];
		h_score = new float[(int)map.mapSize.x * (int)map.mapSize.y];
		f_score = new float[(int)map.mapSize.x * (int)map.mapSize.y];
		
		open.Add(start);
		g_score[start.id] = 0;
		h_score[start.id] = HeuristicEstimate(start,goal,heuristicWeight);
		f_score[start.id] = g_score[start.id] + h_score[start.id];
		
		Tile currentTile = null;
		
		while (open.Count != 0)
		{
			//Tile currentTile;
			if (open.Count > 0)
				currentTile = (Tile)open[0];
			else
				currentTile = null;
			
			if (currentTile == goal)
				return GetPath(currentTile);
			
			open.Remove(currentTile);
			closed.Add(currentTile);
			ArrayList neighbours = map.GetNeighbours(currentTile);
			Debug.Log(open.ToString());
			for (int i = 0; i < neighbours.Count; i++)
			{
				Tile endTile = (Tile) neighbours[i];
				if (closed.Contains(endTile))
					continue;
				
				float endTileCost = g_score[endTile.id] + GetCost(currentTile, endTile);
				
				if (!open.Contains(endTile))
					open.Add(endTile);
				else if (endTileCost >= g_score[endTile.id])
					continue;
				
				float endTileHeuristic = HeuristicEstimate(endTile, goal, heuristicWeight);
				g_score[endTile.id] = endTileCost;
				cameFrom[endTile.id] = currentTile;
				f_score[endTile.id] = endTileCost + endTileHeuristic;
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
			tile = cameFrom[tile.id];
		}
		
		path.Reverse();
		return path;
	}
	
	public static float EuclideanDistance(Vector2 p1, Vector2 p2)
	{
		return (p1 - p2).magnitude;
	}
}