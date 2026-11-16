
using UnityEngine;
using TMPro;

public class DataManager : MonoBehaviour
{
    public int totalData = 0;
    public TMP_Text dataText;

    void Start()
    {
        UpdateUI();
    }

    public void AddDataFragments(int amount)
    {
        totalData += amount;
        UpdateUI();

        Debug.Log("Total Data: " + totalData);
    }

    void UpdateUI()
    {
        if (dataText != null)
        {
            dataText.text = "DATA: " + totalData;
        }
    }
}
