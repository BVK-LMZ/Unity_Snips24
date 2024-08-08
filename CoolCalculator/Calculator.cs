
using UnityEngine.UI;

public class Calculator : MonoBehaviour
{
    [Header("Display")]
    public TMP_InputField _CalculateInputField;

    [Header("Buttons")]
    public Button _calculate;
    public Button _clear;

    public Button _button_1;
    public Button _button_2;
    public Button _button_3;
    public Button _button_4;
    public Button _button_5;
    public Button _button_6;
    public Button _button_7;
    public Button _button_8;
    public Button _button_9;

    [Header("Operations")]
    public Button _add;
    public Button _subtract;
    public Button _multiply;
    public Button _divide;

    [Header("Values")]
    private string _inputString_1 = "";
    private string _inputString_2 = "";
    private string currentOperation = "";
    private bool isSecondInput = false;

    private void Start()
    {
        Clear();

        // Add listeners to digit buttons
        _button_1.onClick.AddListener(() => HandleNumberInput("1"));
        _button_2.onClick.AddListener(() => HandleNumberInput("2"));
        _button_3.onClick.AddListener(() => HandleNumberInput("3"));
        _button_4.onClick.AddListener(() => HandleNumberInput("4"));
        _button_5.onClick.AddListener(() => HandleNumberInput("5"));
        _button_6.onClick.AddListener(() => HandleNumberInput("6"));
        _button_7.onClick.AddListener(() => HandleNumberInput("7"));
        _button_8.onClick.AddListener(() => HandleNumberInput("8"));
        _button_9.onClick.AddListener(() => HandleNumberInput("9"));

        // Add listeners to operation buttons
        _add.onClick.AddListener(() => SetOperation("Add"));
        _subtract.onClick.AddListener(() => SetOperation("Subtract"));
        _multiply.onClick.AddListener(() => SetOperation("Multiply"));
        _divide.onClick.AddListener(() => SetOperation("Divide"));

        // Add listeners to calculate and clear buttons
        _calculate.onClick.AddListener(Calculate);
        _clear.onClick.AddListener(Clear);
    }

    private void HandleNumberInput(string number)
    {
        if (!isSecondInput)
        {
            _inputString_1 += number;
            UpdateInputField(_inputString_1);
        }
        else
        {
            _inputString_2 += number;
            UpdateInputField(_inputString_2);
        }
    }

    private void SetOperation(string operation)
    {
        if (!string.IsNullOrEmpty(_inputString_1) && string.IsNullOrEmpty(_inputString_2))
        {
            currentOperation = operation;
            isSecondInput = true; // Ready for the second number
        }
    }

    private void Calculate()
    {
        if (!string.IsNullOrEmpty(_inputString_1) && !string.IsNullOrEmpty(_inputString_2) && !string.IsNullOrEmpty(currentOperation))
        {
            int inputValue_1 = int.Parse(_inputString_1);
            int inputValue_2 = int.Parse(_inputString_2);
            int result = 0;

            switch (currentOperation)
            {
                case "Add":
                    result = inputValue_1 + inputValue_2;
                    break;
                case "Subtract":
                    result = inputValue_1 - inputValue_2;
                    break;
                case "Multiply":
                    result = inputValue_1 * inputValue_2;
                    break;
                case "Divide":
                    if (inputValue_2 != 0)
                    {
                        result = inputValue_1 / inputValue_2;
                    }
                    else
                    {
                        UpdateInputField("Error");
                        ResetInputs();
                        return;
                    }
                    break;
                default:
                    UpdateInputField("Error");
                    ResetInputs();
                    return;
            }

            UpdateInputField(result.ToString());
            ResetInputs();
        }
    }

    private void Clear()
    {
        ResetInputs();
        UpdateInputField("0");
    }

    private void ResetInputs()
    {
        _inputString_1 = "";
        _inputString_2 = "";
        currentOperation = "";
        isSecondInput = false;
    }

    private void UpdateInputField(string text)
    {
        _CalculateInputField.text = text;
    }
}
