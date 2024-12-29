using UnityEngine;
using UnityEngine.UI;

public class InputFieldColorizer : MonoBehaviour
{
    public InputField xpCoinInputField;
    public InputField currencyInputField;
    public Dropdown currencyDropdown;

    private int minValue = 500;
    private int maxValue = 900000000;
    private Color defaultColor;
    private Color redColor = Color.red;

    void Start()
    {
        defaultColor = xpCoinInputField.textComponent.color;
        xpCoinInputField.onValueChanged.AddListener(CheckXPCoinInputValue);
        xpCoinInputField.contentType = InputField.ContentType.IntegerNumber;
        xpCoinInputField.characterLimit = 9;

        currencyInputField.onValueChanged.AddListener(CheckCurrencyInputValue);
        currencyInputField.contentType = InputField.ContentType.DecimalNumber;
        currencyInputField.characterLimit = 12;

        currencyDropdown.onValueChanged.AddListener(HandleCurrencyDropdownValueChanged);
    }

    void HandleCurrencyDropdownValueChanged(int dropdownIndex)
    {
        if (int.TryParse(xpCoinInputField.text, out int xpCoinAmount))
        {
            ConvertCurrency(dropdownIndex, xpCoinAmount);
        }
        else if (float.TryParse(currencyInputField.text, out float currencyAmount))
        {
            ConvertCurrency(dropdownIndex, currencyAmount, true);
        }
    }

    void CheckXPCoinInputValue(string input)
    {
        if (int.TryParse(input, out int value))
        {
            if (value < minValue || value > maxValue)
            {
                xpCoinInputField.textComponent.color = redColor;
            }
            else
            {
                xpCoinInputField.textComponent.color = defaultColor;
                ConvertCurrency(currencyDropdown.value, value);
            }
        }
        else
        {
            xpCoinInputField.textComponent.color = redColor;
        }
    }

    void CheckCurrencyInputValue(string input)
    {
        if (float.TryParse(input, out float value))
        {
            ConvertCurrency(currencyDropdown.value, value, true);
        }
    }

    void ConvertCurrency(int dropdownIndex, float amount, bool isCurrencyInput = false)
    {
        float convertedAmount = 0f;
        string currencySymbol = "";

        switch (dropdownIndex)
        {
            case 0: // USD
                currencySymbol = "$";
                if (isCurrencyInput)
                {
                    convertedAmount = amount / 0.075f;
                    xpCoinInputField.text = convertedAmount.ToString("0");
                }
                else
                {
                    convertedAmount = amount * 0.075f;
                    currencyInputField.text = convertedAmount.ToString("0.00");
                }
                break;
            case 1: // EUR
                currencySymbol = "€";
                if (isCurrencyInput)
                {
                    convertedAmount = amount / 0.071f;
                    xpCoinInputField.text = convertedAmount.ToString("0");
                }
                else
                {
                    convertedAmount = amount * 0.071f;
                    currencyInputField.text = convertedAmount.ToString("0.00");
                }
                break;
            case 2: // GBP
                currencySymbol = "£";
                if (isCurrencyInput)
                {
                    convertedAmount = amount / 0.062f;
                    xpCoinInputField.text = convertedAmount.ToString("0");
                }
                else
                {
                    convertedAmount = amount * 0.062f;
                    currencyInputField.text = convertedAmount.ToString("0.00");
                }
                break;
        }
    }
}