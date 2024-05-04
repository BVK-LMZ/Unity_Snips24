using UnityEngine;

[CreateAssetMenu(menuName = "Quiz/Question")]
public class Question_ScriptableObject : ScriptableObject
{
    [SerializeField] private string question = "Enter new question text here";
    [SerializeField] private string[] answers;
    [SerializeField] private int correctAnswerIndex;

    // Get the question text.
    public string GetQuestion()
    {
        return question;
    }

    public string GetAnswer(int index)
    {

            return answers[index];
       
    }

    // Get the index of the correct answer.
    // Returns:
    //   The index of the correct answer.
    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
}
