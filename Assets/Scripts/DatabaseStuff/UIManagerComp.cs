///Manages moving between UIs

using UnityEngine;

public class UIManagerComp : MonoBehaviour
{
    public static UIManagerComp instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject playerUI;
    public GameObject passwordUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    ///Turn off all screens/UIs
    public void ClearScreen() 
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        playerUI.SetActive(false);
        passwordUI.SetActive(false);
    }

    ///Called upon back button on register UI
    ///Called upon sign out button on player UI
    ///Called upon main menu back button on scene 'Menu'
    public void LoginScreen() 
    {
        ClearScreen();
        loginUI.SetActive(true);
    }

    ///Called upon sign up button on login UI
    public void RegisterScreen() 
    {
        ClearScreen();
        registerUI.SetActive(true);
    }

    ///Called from FirebaseManager.instance.LoginButton() upon successful login
    public void PlayerScreen() 
    {
        ClearScreen();
        playerUI.SetActive(true);
    }

    ///Called from Forgot password button on Login UI
    public void ForgotPasswordScreen() 
    {
        ClearScreen();
        passwordUI.SetActive(true);
    }
}