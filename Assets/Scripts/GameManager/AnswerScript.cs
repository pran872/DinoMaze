/// attached to the Option Buttons
/// checks whether the right answer index is selected or not

using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    GameObject QM; 
    public QuestionManager qManager;
    private void Start() 
    {
        QM = GameObject.Find("QuestionManager");
        qManager = QM.GetComponent<QuestionManager>();
    }
       
    public void Answer()
    {
        if (isCorrect)
        {
            qManager.CorrectAnswerSelected();
        }
        else
        {
            qManager.WrongAnswerSelected();
        }
    }
}
