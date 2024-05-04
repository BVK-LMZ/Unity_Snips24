using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class QuizManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _canvasMainText;
    [SerializeField] Question_ScriptableObject[] _questionScriptableObjects;
    [SerializeField] Button[] _answerButtons;

    private int _currentQuestionIndex = 0;
    private int _totalQuestions = 4; // Total number of questions
    private int _score = 0;
    private bool _answerSelected = false;
    private float _delayBeforeNextQuestion = 1.5f; // Delay before moving to the next question

    private void Start()
    {
        AskQuestion();
    }

    private void AskQuestion()
    {
        SetQuestion(_currentQuestionIndex);
        EnableAnswerButtons();
    }

    public void Clear()
    {
        _canvasMainText.text = "";
        for (int i = 0; i < _answerButtons.Length; i++)
        {
            _answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    private void SetQuestion(int questionIndex)
    {
        if (questionIndex >= 0 && questionIndex < _totalQuestions)
        {
            _canvasMainText.text = _questionScriptableObjects[questionIndex].GetQuestion();

            for (int i = 0; i < _answerButtons.Length; i++)
            {
                _answerButtons[i].interactable = false;
                _answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = _questionScriptableObjects[questionIndex].GetAnswer(i);
            }
        }
        else
        {
            Debug.LogWarning("Invalid question index: " + questionIndex);
        }
    }

    public void SelectAnswer(int answerIndex)
    {
        if (!_answerSelected)
        {
            Debug.Log("Player clicked button " + answerIndex);

            if (answerIndex == _questionScriptableObjects[_currentQuestionIndex].GetCorrectAnswerIndex())
            {
                Debug.Log("Correct!");
                _score++;
            }
            else
            {
                Debug.Log("Incorrect!");
            }

            DisableAnswerButtons();
            _answerSelected = true;
            Invoke("NextQuestion", _delayBeforeNextQuestion); // Move to the next question after a delay
        }
    }

    private void EnableAnswerButtons()
    {
        for (int i = 0; i < _answerButtons.Length; i++)
        {
            _answerButtons[i].interactable = true;
        }
    }

    private void DisableAnswerButtons()
    {
        for (int i = 0; i < _answerButtons.Length; i++)
        {
            _answerButtons[i].interactable = false;
        }
    }

    private void NextQuestion()
    {
        _currentQuestionIndex++;
        _answerSelected = false;
        Clear();
        if (_currentQuestionIndex < _totalQuestions)
        {
            AskQuestion();
        }
        else
        {
            Debug.Log("Quiz finished! Score: " + _score);

           if ( _score >= 4)
            {
                //load the win scene
                SceneManager.LoadScene("EndGame");

            }
            else
            {
                //load the main scene
                SceneManager.LoadScene("MainMenu");

            }
        }
    }
}
