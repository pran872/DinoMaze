// attached to coins to implement coin collection system

using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Update() 
    {
        transform.Rotate(0, 0.01f, 0); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            QuestionManager.coinsCollected++;
            PlayerPrefs.SetInt("NumberOfCoins", QuestionManager.coinsCollected); 
            // stores Player preferences between game sessions
            // this function is helpful when playing multiple levels to help store the number of coins collected in all games

            Destroy(this.gameObject);
        }
    }
}