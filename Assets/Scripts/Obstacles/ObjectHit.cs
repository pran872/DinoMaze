/// attached to the obstacles and front and back wall pieces

using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) 
    {
        // to disregard the already bumped into pieces
        if (other.gameObject.tag == "Player")
        {
            GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(0, 62, 89); 
            // using a lighter shade of red to prevent any visual disturbances

            gameObject.tag = "Hit";  
        }
    }
}
