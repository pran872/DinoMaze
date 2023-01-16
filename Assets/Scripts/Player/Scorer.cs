// attached to the Player (dino) to track the number of times the dino hits an obstacle
// it also asks a question every three bumps as a "penalty"

using UnityEngine;

public class Scorer : MonoBehaviour
{
    GameObject QM; 
    QuestionManager qManager;
    GameObject Mover; 
    public static int hits = 0;

    private void Start() 
    {
        // instantiating QuestionManager Script
        QM = GameObject.Find("QuestionManager");
        qManager = QM.GetComponent<QuestionManager>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.transform.tag == "Obstacle")
        {
            hits++;
            Debug.Log("You've bumped into an obstacle this/these many times: " + hits);

            if (hits % 3 == 0) // to process "penalty" for every 3 hits by the user
            {
                // dont allow dino to move
                Player.canMove = false;

                // accesses questionmanager script to display question
                QuestionManager.askQuestion = true;
                qManager.GenerateQuestions();
            }
        }
    }
}