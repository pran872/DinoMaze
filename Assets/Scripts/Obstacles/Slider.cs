// attached to obstacles

using UnityEngine;

public class Slider : MonoBehaviour
{
    float speed = 0.5f; 
    float range = 0.75f; 
    float xStart;
    int direction = 1;

    void Start() 
    {
        xStart = transform.position.x;
    }

    /// keeps running this function - similar to Update but for bodies that obey physics 
    /// laws e.g. gravity
    void FixedUpdate() 
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime * direction);
        
        // to change the vector direction when the object center reaches the end limits
        // if slider goes too far to the left, the direction changes to the right
        // and vice versa
        if (transform.position.x < xStart)
        {direction *= -1;}

        if  (transform.position.x > (xStart + range))
        {direction *= -1;}
    }
}