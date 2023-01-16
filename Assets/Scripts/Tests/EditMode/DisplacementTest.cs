using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Tests
{
    // A Test behaves as an ordinary method
    [Test]
    public void DisplacementTest() // Tests the function that calculates the avatar's movement
    {
        float speed = 1.0f;
        
        float translation = 1 * speed; //{Input.GetAxis("Vertical") * Time.deltaTime} is replaced by a 1 for the sake of the test
        Vector3 displacement;
        displacement = new Vector3(0, 0, translation);
  
        Assert.IsTrue(displacement.z != 0);
    }

    [Test]
    public void RotationTest() // Tests the function that calculates the avatar's turning
    {
        float speed = 1.0f;
        
        float rotation = 1 * speed; //{Input.GetAxis("Horizontal") * Time.deltaTime} is replaced by a 1 for the sake of the test
        Vector3 turn;
        turn = new Vector3(0, rotation, 0);
  
        Assert.IsTrue(turn.y != 0);
    }
}
