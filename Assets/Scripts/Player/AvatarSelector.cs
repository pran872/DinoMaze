/// helps buy and change avatars
/// this is to implement the reward system of the game

using UnityEngine; 
using UnityEngine.UI;
using TMPro;

public class AvatarSelector : MonoBehaviour
{
    public int selectedCharacter;
    public GameObject[] skins;
    public Avatar[] characters; 
    public Button unlockButton;
    public TextMeshProUGUI coinsText;

    private void Awake()
    {
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        foreach (GameObject player in skins)
            player.SetActive(false);

        skins[selectedCharacter].SetActive(true);

        foreach(Avatar a in characters)
        {
            // to check the price of the avatars
            if (a.price == 0)
                a.isUnlocked = true;
            else
            {
                a.isUnlocked = PlayerPrefs.GetInt(a.name, 0) == 0 ? false : true;
                // storing the name of the avatar  and default value of 0 (false) as the reference keys
                // shorter form of if-else statement, if the condition "PlayerPrefs.GetInt(a.name, 0) == 0" is true, 
                // then the variable a.isUnlocked will be assigned false
            }
        }

        UpdateUI();
    }

    /// displays next avatar upon right arrow button click
    public void ChangeNext()
    {
        // hide the current avatar and increment the variable to select the next one in sequence
        skins[selectedCharacter].SetActive(false);
        selectedCharacter++;

        // checking for the number of the avatars
        // if we reach the end of the list, the varible resets and we start again
        if (selectedCharacter == skins.Length)
            selectedCharacter = 0;

        skins[selectedCharacter].SetActive(true);

        // updating the Player Prefs memory value
        if(characters[selectedCharacter].isUnlocked)
        {
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        }

        UpdateUI();
    }

    /// displays previous avatar upon left arrow button click
    public void ChangePrevious()
    {
        skins[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter == -1)
            selectedCharacter = skins.Length -1;

        skins[selectedCharacter].SetActive(true);
        
        if (characters[selectedCharacter].isUnlocked)
        {
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        }
        
        UpdateUI();
    }

    /// sets the variables and stores them to access throughout the game
    /// this is useful when playing with different avatars (future improvement)
    public void Unlock()
    {
        int coins = PlayerPrefs.GetInt("NumberOfCoins", 0); // accessing total number of coins from the game
        int price = characters[selectedCharacter].price;

        PlayerPrefs.SetInt("NumberOfCoins", coins - price); // updates the coin total after buying the avatar
        PlayerPrefs.SetInt(characters[selectedCharacter].name, 1);
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter); // update variable to play with the selected character

        characters[selectedCharacter].isUnlocked = true;

        UpdateUI();
    }

    /// accesses the 'Interactable' function of the button
    public void UpdateUI()
    {
        coinsText.text = "Coins: " + PlayerPrefs.GetInt("NumberOfCoins", 0);

        // disables or enables the button
        if (characters[selectedCharacter].isUnlocked == true)
            {
                unlockButton.gameObject.SetActive(false);
                // hides the button if the avatar has already been bought
            }

        else
        {
            unlockButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price: " + characters[selectedCharacter].price;

            // checks whether the number of coins collected by the user are enough to buy a new avatar
            if (PlayerPrefs.GetInt("NumberOfCoins", 0) < characters[selectedCharacter].price)
            {
                unlockButton.gameObject.SetActive(true);
                unlockButton.interactable = false;
                // we can see the button but not click on it
            }
            else
            {
                unlockButton.gameObject.SetActive(true);
                unlockButton.interactable = true;
                // we can see the button and interact with it
            }
        }
    }
}