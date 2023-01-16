// attached to obstacles
// cylinder translates the object up and down continuously

using UnityEngine;

public class Cylinder : MonoBehaviour
{
    [SerializeField] float waitingTime = 0f;
    [SerializeField] float speed = 0.4f;
    [SerializeField] float range = 0.5f;
    float yStart;
    int dir = 1;

    void Start() 
    {
        yStart = transform.position.y;
    }

    void FixedUpdate() 
    {
        if (Time.time > waitingTime)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime * dir);

            // to change the vector direction when the object center reaches the end limits
            if (transform.position.y < yStart || transform.position.y > yStart + range)
            { dir *= -1;}
        }
    }
}