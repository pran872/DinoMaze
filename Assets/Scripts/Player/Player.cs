/// attached to the Player (dino)
// the game can be played with W,A,S,D or arrow keys and a Joystick
// the script contains code for both but is currently using the keys method

using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public static bool canMove = true;
    float speed = 1.0f;
    float rotateKey = 75.0f; 
    float rotationSpeed = 20.0f;    
    Rigidbody rigidbody; 
    Animator anim;

    private void Start() 
    {
        anim = this.GetComponent<Animator>(); // can use trigger values (bool or float) to turn animation on or off
        rigidbody = GetComponent<Rigidbody>();
        PrintInstructions();
    }

    void Update()
    {
        if (canMove)
        {
            //MovePlayerControls(); // controls the avatar movements using a joystick
            MovePlayerKeys(); // controls the avatar movements using keys
        }
    }

    /// display these instructions before the game begins as further improvement 
    void PrintInstructions() {
        Debug.Log("Welcome to the game.");
        Debug.Log("Move your player with WASD or arrow keys.");
        Debug.Log("Collect coins to get gifts later :)");
        Debug.Log("Don't hit the walls or obstacles! For every three obstacles you bump into, you will be asked a question.");
        Debug.Log("Answer correctly to continue.");
    }

    /// This method helps control the avatar (dino) with keys
    void MovePlayerKeys() 
    {
        try
        {
            float translation = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            float rotation = Input.GetAxis("Horizontal") * Time.deltaTime * rotateKey; 
            // Time.deltaTime for frame rate independance 
            // That is, processing power of a device shouldn't affect the game object's speed

            transform.Translate(0, 0, translation); // forward and backward movement
            transform.Rotate(0, rotation, 0); // y-axis rotation; x and z are frozen under rigidbody component

            DinoAnimator(translation); // calls DinoAnimator method for movement
        }

        catch (System.ArgumentException e) 
        {
            Debug.LogError(e.Message);
        }
    }

    /// This method helps control the avatar (dino) with a joystick        
    void MovePlayerControls()
    {
        try
        {
            float translation = CrossPlatformInputManager.GetAxis("Vertical") * speed * Time.deltaTime;
            float rotation = CrossPlatformInputManager.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

            transform.Translate(0, 0, translation); 
            transform.Rotate(0, rotation, 0); 

            DinoAnimator(translation); // calls DinoAnimator method for movement
        }

        catch (System.ArgumentException e) 
        {
            Debug.LogError(e.Message);
        }
    }

    void DinoAnimator(float translation)
    {
        if (translation > 0) // translation greater than 0, move forward
        {
            anim.SetBool("isRunning", true); // turns on running animation
            anim.SetFloat("speed", 0.01f);
        }
        else if (translation < 0) // or else go back
        {
            anim.SetBool("isRunning", true);
            anim.SetFloat("speed", -0.01f); // diff between forward and backward movement is the sign
        }
        else // otherwise translation = 0 --> no movement
        {
            anim.SetBool("isRunning", false);
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            anim.SetTrigger("jump"); // click on 'Jump' button to access the jump animation when using joystick
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("jump"); // press space key to access the jump animation when using keys
        }
    }
}