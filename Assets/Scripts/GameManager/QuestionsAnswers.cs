// attached to QuestionManager Script -- creates a new type of object

[System.Serializable]

public class QuestionsAnswers
{
    public string Question;
    public string[] Answers; // to create a serializable a list of answer options, in our case 4
    public int CorrectAnswer; // stores the index of the correct option of each question
}
