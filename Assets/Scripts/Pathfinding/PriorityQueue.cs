using UnityEngine;
using System.Collections;

public class PriorityQueue
{
	private ArrayList tiles;
	
	public PriorityQueue()
	{
		tiles = new ArrayList();
	}
	
	public ArrayList GetTiles()
	{
		return tiles;
	}
	
	public int GetLength()
	{
		return tiles.Count;
	}
	
	public Tile Front()
	{
		if (tiles.Count > 0)
			return (Tile) tiles[0];
		else
			return null;
	}
	
	public int Push(Tile tile)
	{
		tiles.Add(tile);
		tiles.Sort();
		return tiles.Count;
	}
	
	public Tile Pop()
	{
		if (tiles.Count == 0)
			return null;
		Tile tile = (Tile) tiles[0];
		tiles.RemoveAt(0);
		return tile;
	}
	
	public bool Contains(System.Object tile)
	{
		return tiles.Contains(tile);
	}
	
	public void Remove(Tile tile)
	{
		tiles.Remove(tile);
		tiles.Sort();
	}
	
	public int Compare(Tile t1, Tile t2)
	{
		if (t1.estimatedTotalCost < t2.estimatedTotalCost)
			return -1;
		else if (t1.estimatedTotalCost > t2.estimatedTotalCost)
			return 1;
		else
			return 0;
	}
}