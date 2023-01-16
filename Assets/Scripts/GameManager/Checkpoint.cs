// attached to the checkpoint circles to store the last known position of dino before a question is asked

using UnityEngine;

public class Checkpoint : MonoBehaviour
{    
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            // updating the last known position of Dino (Player)
            QuestionManager.lastCheckpointPos = transform.position;
                      
            gameObject.SetActive(false);
        }
    }
}