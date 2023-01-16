// attached to obstacles

using UnityEngine;

public class SliderFront : MonoBehaviour
{
    float speed = 0.3f; 
    float range = 1f; 
    float zStart;
    int direction = 1;

    void Start() 
    {
        zStart = transform.position.z; 
    }

    /// keeps running this function - similar to Update but for bodies that obey physics 
    /// laws e.g. gravity
    void FixedUpdate() 
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime * direction);
        
        // to change the vector direction when the object center reaches the end limits
        // if slider goes too far to the front, the direction changes to the back
        // and vice versa
        if (transform.position.z < zStart) 
        {direction *= -1;}

        if (transform.position.z > (zStart + range))
        {direction *= -1;}
    }
}