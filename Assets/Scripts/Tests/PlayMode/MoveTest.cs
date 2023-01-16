using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Tests
{
    [UnityTest]
    public IEnumerator ForwardTest() //test for the avatar moving forward
    {
        // Arrange
        GameObject playerObject = new GameObject();
        playerObject.AddComponent<Animator>();
        Player player = playerObject.AddComponent<Player>();

        Vector3 originalPosition = player.transform.position;

        // Act
        player.transform.Translate(0, 0, 1);

        // Assert
        Assert.AreNotEqual(originalPosition.z, player.transform.position.z);

        yield return null;
    }

    [UnityTest]
    public IEnumerator TurnTest() // test for turning the avatar
    {
        // Arrange
        GameObject playerObject = new GameObject();
        playerObject.AddComponent<Animator>();
        Player player = playerObject.AddComponent<Player>();

        Vector3 originalPosition = player.transform.position;

        // Act
        player.transform.Translate(0, 1, 0);

        // Assert
        Assert.AreNotEqual(originalPosition.y, player.transform.position.y);

        yield return null;
    }
}
