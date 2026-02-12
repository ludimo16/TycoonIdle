using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    //public variables - Define Gameplay
    public float BaseStoreCost;
    public float BaseStoreProfit;
    public float StoreTimer;
    public int StoreCount;
    public bool ManagerUnlocked;
    public float StoreMultiplier;
    public bool StoreUnlocked;
    public int StoreTimerDivision;
    float NextStoreCost;
    float CurrentTimer = 0;
    public bool StartTimer;

    void Start()
        {
            StartTimer = false;
        }

    public float GetCurrentTimer()
        {
            return CurrentTimer;
        }

    public void SetNextStoreCost(float amt)
        {
            NextStoreCost = amt;
        }
    public float GetNextStoreCost()
        {
            return NextStoreCost;
        }

    public float GetStoreTimer()
        {
            return StoreTimer;
        }

    void Update()
        {
            if (StartTimer)
                {
                    CurrentTimer += Time.deltaTime;
                    if (CurrentTimer >= StoreTimer)
                        {
                            gamemanager.instance.AddToBalance(BaseStoreProfit + StoreCount);
                            CurrentTimer = 0f;

                            if (!ManagerUnlocked)
                            {
                                StartTimer = false;
                            }
                        }
                }
        }

    public void BuyStore()
        {
            StoreCount = StoreCount +1;

            float Amt = -NextStoreCost;
            NextStoreCost = (BaseStoreCost * Mathf.Pow(StoreMultiplier,StoreCount));
            gamemanager.instance.AddToBalance(Amt);
        
            if (StoreCount % StoreTimerDivision ==0)
            {
                StoreTimer = StoreTimer / 2;
            }
        }

    public void OnStartTimer()
        {
            if (!StartTimer && StoreCount > 0)
            StartTimer = true;
        }
}