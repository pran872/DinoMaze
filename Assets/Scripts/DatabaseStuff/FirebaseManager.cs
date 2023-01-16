///Manages Firebase Data

using Firebase; using Firebase.Auth; using Firebase.Database;
using System.Collections; using System;
using UnityEngine;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    [Header("Firebase")] //Groups Firebase variables together in the inspector
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public FirebaseDatabase datab;

    [Header("Login")] //Login variables
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText; //message for incorrect login
    public TMP_Text confirmLoginText; //for successful login

    [Header("Register")] //Register and User variables
    public TMP_InputField usernameField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText; //message for incorrect registration
    public TMP_InputField ageField;
    public TMP_Dropdown cpTypeField;
    public TMP_Dropdown gmfcField;
    public TMP_Dropdown accountTypeField;

    [Header("Player")] //Current player variables
    public TMP_Text UsernameCurrField;

    [Header("ResetPassword")] //Reset password variables
    public TMP_InputField emailPasswordField;


    ///runs when the program begins
    void Awake() {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }
    
    ///Connects to Firebase Authentication and Firebase Database
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Authentication");

        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        //Set the database instance object
        datab = FirebaseDatabase.DefaultInstance;
    }

    ///Clears all login fields on login UI
    public void ClearLoginFields()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
        warningLoginText.text = "";
        confirmLoginText.text = "";
    }

    ///Clears all register fields on register UI
    public void ClearRegisterFields()
    {
        ageField.text = "";
        cpTypeField.value = 0;
        gmfcField.value = 0;
        accountTypeField.value = 0;
        usernameField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
        warningRegisterText.text = "";
    }

    ///Clears all player fields on player UI
    public void ClearPlayerFields()
    {
        UsernameCurrField.text = "";
    }

    ///Called from the Login button on Login UI
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }

    ///Called from the Register button on Register UI
    public void RegisterButton()
    {        
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameField.text));
    }

    ///Called from the Sign Out button on Player UI
    ///signs the current user out and changes screen to login UI
    public void SignOutButton()
    {
        auth.SignOut(); 
        UIManagerComp.instance.LoginScreen();
        ClearPlayerFields();
    }

    ///Called from the Forgot Password button on Reset Password UI
    public void ForgotPasswordButton()
    {
        //Call the forgot password coroutine however this method is currently incomplete
        StartCoroutine(ForgotPassword(emailPasswordField.text));
    }

    ///Checks if registration variables, username, password, and age, are appropriate
    ///Returns check_result - true if successful, false otherwise with warning messages
    private bool ValidationChecks(string _username)
    { 
        //initialising check_result 
        bool check_result = true;

        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
            check_result = false;
        }
        else if(passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
            check_result = false;
        }
        else
        {
            //Although the ageField can only take in integers as input, there is placeholder text 'Age'
            //which can lead to an exception if not changed - this needs to be caught
            try {
                int age_text = int.Parse(ageField.text);
                if (age_text>100 | age_text<=1)
                {
                    //If the age is greater than 100 or less than 2 show warning
                    warningRegisterText.text = "Invalid Age";
                    check_result = false;
                }
            }
            catch (Exception) { //i.e. 'Age'
                    Debug.Log("Caught");
            }            
        }
        return check_result;
    }

   ///Logs you in if account is existing and details are correct
    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the Firebase task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        //Error handling
        if (LoginTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            //Corresponding error message displayed on screen
            warningLoginText.text = message;
        }

        //No errors with logging in
        else
        {
            //User is now logged in
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged in"; //displayed on Login UI

            //Calls the loaduserdata coroutine
            StartCoroutine(LoadUserData());
            yield return new WaitForSeconds(1); //waits one second for 'logged in' message to show
            UIManagerComp.instance.PlayerScreen(); //Change to player UI
            
            ClearLoginFields();            
        }
    }

    ///Registers user
    private IEnumerator Register(string _email, string _password, string _username)
    {
        //Calls the ValidationChecks method and returns its result
        bool check_result = ValidationChecks(_username);

        //If validation checks are successful
        if (check_result) 
        {
            //Call the Firebase auth signup function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                    case AuthError.InvalidEmail:
                        message = "Invalid Email";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                User = RegisterTask.Result;
                Debug.Log("User has been created");

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile{DisplayName = _username};

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        ClearLoginFields();    
                        UIManagerComp.instance.LoginScreen();                  

                        //Sends username and email of currently registered user to DatabaseRegistration
                        //to upload data onto the FirebaseDatabase
                        StartCoroutine(DatabaseRegistration(User, _email, _username));
                    }
                }
            }
        }
    }

    ///Uploads user data from registration onto FirebaseDatabase
    private IEnumerator DatabaseRegistration(Firebase.Auth.FirebaseUser _user, string _email, string _username)
    {

        //student account
        if (accountTypeField.value==0) 
        {
            //Although the ageField can only take in integers, there is placeholder text 'Age'
            //which can lead to an exception if not changed which needs to be caught
            try {
                int age_text = int.Parse(ageField.text);
                var age_data = datab.RootReference.Child("users").Child(User.UserId).Child("age").SetValueAsync(age_text);
            }
            catch (Exception) {
                Debug.Log("Caught");
                //If no age is provided, N/A is sent to the database for the age field
                var age_data = datab.RootReference.Child("users").Child(User.UserId).Child("age").SetValueAsync("N/A");
            }

            //Get the option selected on the dropdown menus
            string cpType_text = cpTypeField.options[cpTypeField.value].text;
            string gmfc_text = gmfcField.options[gmfcField.value].text;

            //If no option is selected, N/A is sent to the database for that field
            if (cpType_text == "CP Type")
            {cpType_text = "N/A";}
            if (gmfc_text == "GMFC Level")
            {gmfc_text = "N/A";}

            var cpType_data = datab.RootReference.Child("users").Child(User.UserId).Child("cp type").SetValueAsync(cpType_text);
            var gmfc_data = datab.RootReference.Child("users").Child(User.UserId).Child("gmfc").SetValueAsync(gmfc_text);
        } 
        
        //Sends username to database
        var username_data = datab.RootReference.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);
        yield return new WaitUntil(predicate: () => username_data.IsCompleted);

        //Sends emailid to database
        var email_data = datab.RootReference.Child("users").Child(User.UserId).Child("emailid").SetValueAsync(_email);
        yield return new WaitUntil(predicate: () => email_data.IsCompleted);
        
        //Sends account type to database
        string acc_text = accountTypeField.options[accountTypeField.value].text;
        var account_data = datab.RootReference.Child("users").Child(User.UserId).Child("type").SetValueAsync(acc_text);
        yield return new WaitUntil(predicate: () => account_data.IsCompleted);
        
        ClearRegisterFields();
    } 

    ///Loads user infromation such as username from the database after logging in
    private IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        var DBTask = datab.RootReference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            //The username is displayed on the Player UI screen
            UsernameCurrField.text = snapshot.Child("username").Value.ToString();
        }
    }

    
    ///Incomplete method to send email instructions for resetting password
    private IEnumerator ForgotPassword(string _email)
    {
        Debug.Log("in Enumerator");
        yield return 0; 
    }
    
}
