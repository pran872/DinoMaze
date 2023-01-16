// attached to obstacles
// spinner rotates the object continuously

using UnityEngine;

public class Spinner : MonoBehaviour
{
    float yAngle = 0.4f;
    
    void Update()
    {
       transform.Rotate(0, yAngle, 0); 
    }
}