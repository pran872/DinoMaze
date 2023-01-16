/// for unit testing the player movement

/*using NUnit.Framework;
using UnityEngine;

public class PlayerTests
{
    Player player;

    [SetUp]
    public void Setup()
    {
        // create a new player object for each test
        player = new Player();
        player.speed = 10;
        player.rotateKey = 30;
    }

    [Test]
    public void MovePlayerKeys_InputValid_PlayerMoves()
    {
        // Arrange
        Vector3 originalPosition = player.transform.position;

        // Act
        Input.SetAxis("Vertical", 1);
        Input.SetAxis("Horizontal", 1);
        player.MovePlayerKeys();

        // Assert
        Assert.AreNotEqual(originalPosition, player.transform.position);
    }

    [Test]
    public void MovePlayerKeys_InputInvalid_PlayerDoesNotMove()
    {
        // Arrange
        Vector3 originalPosition = player.transform.position;

        // Act
        Input.SetAxis("Vertical", 0);
        Input.SetAxis("Horizontal", 0);
        player.MovePlayerKeys();

        // Assert
        Assert.AreEqual(originalPosition, player.transform.position);
    }

    [Test]
    public void MovePlayerKeys_InputOutOfRange_PlayerDoesNotMove()
    {
        // Arrange
        Vector3 originalPosition = player.transform.position;

        // Act
        Input.SetAxis("Vertical", 2);
        Input.SetAxis("Horizontal", -2);
        player.MovePlayerKeys();

        // Assert
        Assert.AreEqual(originalPosition, player.transform.position);
    }
}*/