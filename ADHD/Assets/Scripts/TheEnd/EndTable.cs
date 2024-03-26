using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class SimpleTable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    private List<string> column1Data;
    private List<int> column2Data;

    void Start()
    {
        GlobalManager globalManagerInstance = FindObjectOfType<GlobalManager>();
        if(globalManagerInstance)
        {
            column1Data = globalManagerInstance.GetScenes().ToList();
            column2Data = globalManagerInstance.GetPoints().ToList();
            Destroy(globalManagerInstance.gameObject);

            string tableText = GenerateTable(column1Data, column2Data);
            textMeshProUGUI.text = tableText;
        }
    }

    string GenerateTable(List<string> column1, List<int> column2)
    {
        string tableText = "";

        int numRows = Mathf.Min(column1.Count, column2.Count);

        for (int row = 0; row < numRows; row++)
        {
            tableText += column1[row] + "\t" + column2[row];

            if (row < numRows - 1)
                tableText += "\n";
        }

        return tableText;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
