using TMPro;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public delegate void UpdateBalance();
    public static event UpdateBalance OnUpdateBalance;
    public static gamemanager instance;
    float CurrentBalance = 0;
    public string CompanyName;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
        {
            if (OnUpdateBalance != null)
                {
                    OnUpdateBalance();
                }
        }

    // Update is called once per frame
    void Update()
        {
            
        }

    void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

    public void AddToBalance (float amt)
        {
            CurrentBalance += amt;
            if (OnUpdateBalance != null)
            {
                OnUpdateBalance();
            }
        }

    public bool CanBuy(float AmtToSpend)
        {
            if (AmtToSpend > CurrentBalance)
                return false;
            else
                return true;
        }

    public float GetCurrentBalance()
        {
            return CurrentBalance;
        }
}
