using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text CurrentBalanceText;
    public TMP_Text CompanyNameText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
        {
        }

    // Update is called once per frame
    void Update()
    {
    }

    void OnEnable()
        {
            gamemanager.OnUpdateBalance += UpdateUI;
            LoadGameData.OnLoadDataComplete += UpdateUI;
        }

    void OnDisable()
        {
            gamemanager.OnUpdateBalance -= UpdateUI;
            LoadGameData.OnLoadDataComplete -= UpdateUI;
        }     
    public void UpdateUI()
        {
            CurrentBalanceText.text = gamemanager.instance.GetCurrentBalance().ToString("C2");
            CompanyNameText.text = gamemanager.instance.CompanyName;
        }
}
