// This script is used to create confetti particle effects and trigger 
// the game over function when the player collides with the "Finish Platform" (GameObject Stop).

using UnityEngine;

public class Confetti : MonoBehaviour
{
    public GameObject confettiParticles;
    GameObject QM; 
    public QuestionManager qManager;

    private void Start() 
    {
        // instantiating QuestionManager Script variable
        QM = GameObject.Find("QuestionManager");
        qManager = QM.GetComponent<QuestionManager>();
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        // the confetti effect will occur ten times before the game over screen is displayed
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject ob = Instantiate(confettiParticles);
                Destroy(ob, 5f);
            }

            qManager.GameOver();
        }
    }
}
