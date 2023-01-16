/// attached to QuestionManager (Empty GameObject)

using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    [Header("Variables")]
    public static bool askQuestion = false;
    public int currentQuestion; 
    public static int coinsCollected;
    public static Vector3 lastCheckpointPos = new Vector3(-5, 0.05f, -5);
    
    [Header("CanvasPanels")]
    public GameObject QuestionPanel; // question-answer panel
    public GameObject GOPanel; // game over panel
    public GameObject pauseMenuScreen; // pause menu

    [Header("GameObjects")]
    public List<QuestionsAnswers> QnA; // access the database of question and answers 
    public GameObject[] options; // used for assigning the different options of answers

    [Header("CanvasTextBoxes")]
    public Text QuestionText; 
    public Text CoinsCollectedText; 
    public Text Paused;
    public TMP_Text WrongAnswer; 
    public TextMeshProUGUI coinsText;

    private void Awake() 
    {
        coinsCollected = PlayerPrefs.GetInt("NumberOfCoins", 0); 
        // helps store coin count in between game sessions so that the user doesn't loose the number of coins collected
        // applicable for a multi-level game application (future improvement)
        // 0 is the default value, if the previous value is somehow lost

        WrongAnswer.text = "";
        GOPanel.SetActive(false); // turns off the game over screen
    }       
    
    void Update()
    {
        coinsText.text = coinsCollected.ToString();

        // if dino hits 3 obstacle the question panel is switched on
        if (askQuestion) {QuestionPanel.SetActive(true);}
        else {QuestionPanel.SetActive(false);}
    }

    /// method to access the question database
    public void GenerateQuestions()
    {
        // choose a random question to display from the database
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionText.text = QnA[currentQuestion].Question;

            AnswerSet(); 
        }
        // if you run out of questions then game over
        else
        {
            GameOver();
        }
    }

    /// assign the array of answers to each button
    void AnswerSet()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i+1) 
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true; 
                // compare the indicies with isCorrect to get the correct answer
            }
        }
    }

    /// process for when the correct option button is clicked
    public void CorrectAnswerSelected()
    {
        askQuestion = false;
        QnA.RemoveAt(currentQuestion); // to remove the already asked questions
        WrongAnswer.text = ""; // should be empty as the correct answer is selected
        
        GetMazeScene();
    }

    /// process for when the wrong option button is clicked
    public void WrongAnswerSelected()
    {
        WrongAnswer.text = "Oops! Try Again!";
    }

    /// to continue playing the game
    public void GetMazeScene()
    {
        // user begins at last known checkpoint 
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckpointPos;
        Player.canMove = true;
    }

    /// process for when the correct option button is clicked
    public void GameOver()
    {
        QuestionPanel.SetActive(false);
        GOPanel.SetActive(true);
        CoinsCollectedText.text = "GAME OVER!\nCoins Collected: " + coinsCollected;
    }

    /// to play the game again when the replay button is clicked 
    public void ReplayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the game

        // Re-setting variables 
        Scorer.hits = 0;
        lastCheckpointPos = new Vector3(-5, 0.05f, -5);
        askQuestion = false;
        Player.canMove = true;
    }

    /// pauses the game and allows the user to go back to the Main Menu
    public void PauseGame()
    {
        Time.timeScale = 0; 
        // this is helpful if the game had a timer (future improvement)
        // however, this is not one of the user requirements as they just want to promote movement and learning
        // and not put any limitations

        pauseMenuScreen.SetActive(true);
        Player.canMove = false;
        Paused.text = "GAME PAUSED\nCoins Collected: " + coinsCollected;
    }

    /// You can start playing the game again
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
        Player.canMove = true;
    }

    // method linked to the button that allows the user to go back to the main menu
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}