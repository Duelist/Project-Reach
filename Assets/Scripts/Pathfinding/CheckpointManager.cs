using UnityEngine;
using System.Collections;

public class CheckpointManager
{
    private struct Checkpoint
    {
        private GameObject checkpoint;
        private float minDistance;
        
        public Checkpoint(GameObject checkpoint, float minDistance)
        {
            this.checkpoint = checkpoint;
            this.minDistance = minDistance;
        }
    }
    
    private ArrayList checkpoints;
    
    public CheckpointManager()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("cp");
        float minDistance = 2.0f;
        
        for (int i = 0; i < checkpoints.Length; i++)
        {
            Checkpoint new_checkpoint = new Checkpoint(checkpoints[i],minDistance);
            this.checkpoints.Add(new_checkpoint);
        }
    }
}