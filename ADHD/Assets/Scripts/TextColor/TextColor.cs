using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml.Serialization;
using System.Security.Cryptography;


public class TextColor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI meaningText;
    [SerializeField] private TextMeshProUGUI textColorText;
    public int points = 0;
    public int wrong = 0;
    private bool isMatching;
    List<string> colorNames = new List<string>{ "red", "green", "blue", "gray", "black", "yellow", "magenta", "cyan" };

    void Start()
    {
        GenerateColors();
    }

    void Update()
    {
    }

    private void GenerateColors()
    {
        int randomNumber = Random.Range(0, 2);
        isMatching = randomNumber == 0;

        if(isMatching)
        {
            string matchingColor = colorNames[Random.Range(0,8)];
            meaningText.text = matchingColor;
            meaningText.color = StringToColor(colorNames[Random.Range(0,8)]);
            textColorText.text = colorNames[Random.Range(0,8)];
            textColorText.color = StringToColor(matchingColor);
        }
        else
        {
            List<string> colors = new List<string>(colorNames);

            int notMatchingIndex = Random.Range(0,8);

            meaningText.text = colors[notMatchingIndex];
            colors.RemoveAt(notMatchingIndex);

            meaningText.color = StringToColor(colors[Random.Range(0,7)]);
            textColorText.text = colors[Random.Range(0,7)];
            textColorText.color = StringToColor(colors[Random.Range(0,7)]);
        }
    }

    Color StringToColor(string colorName)
    {
        switch (colorName.ToLower())
        {
            case "red":
                return Color.red;
            case "green":
                return Color.green;
            case "blue":
                return Color.blue;
            case "gray":
                return Color.gray;
            case "black":
                return Color.black;
            case "yellow":
                return Color.yellow;
            case "magenta":
                return Color.magenta;
            case "cyan":
                return Color.cyan;
            default:
                Debug.LogWarning("Unrecognized color name: " + colorName);
                return Color.clear;
        }
    }


    public void Negative()
    {
        if(isMatching)
        {
            wrong++;
            GenerateColors();
        }
        else
        {
            points++;
            GenerateColors();
        }
    }

    public void Positive()
    {
        if(isMatching)
        {
            points++;
            GenerateColors();
        }
        else
        {
            wrong++;
            GenerateColors();
        }
    }
}
