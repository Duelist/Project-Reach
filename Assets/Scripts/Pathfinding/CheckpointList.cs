using UnityEngine;
using System.Collections;

public class CheckpointList
{
    private struct Checkpoint
    {
        private GameObject checkpoint;
        private float minDistance;
        
        public Checkpoint(GameObject checkpoint, float minDistance)
        {
            this.checkpoint = checkpoint;
            this.minDistance = minDistance;
			Debug.Log(checkpoint);
        }
		
		public GameObject GetCheckpoint (){
			return checkpoint;
		}
		
		public float GetMinDistance (){
			return minDistance;
		}
    }
    
    private ArrayList checkpoints;
    
    public CheckpointList(string prefix, int numberOfCheckpoints)
    {
        float minDistance = 1.0f;
        checkpoints = new ArrayList ();
        for (int i = 0; i < numberOfCheckpoints; i++)
        {
			Debug.Log(prefix+(i+1));
            Checkpoint new_checkpoint = new Checkpoint(GameObject.FindWithTag(prefix+(i+1)),minDistance);
            this.checkpoints.Add(new_checkpoint);
        }
    }
    
    public ArrayList GetList(){
        return checkpoints;
    }
	
	public GameObject GetCpHead (){
		if (checkpoints.Count > 0){
			return ((Checkpoint)checkpoints[0]).GetCheckpoint();
		}
		return null;
	}
	
	public void RemoveHead (){
		checkpoints.RemoveAt(0);
	}
	
	public float GetMinDistance (){
		return ((Checkpoint)checkpoints[0]).GetMinDistance();
	}
}