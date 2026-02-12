using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIStore : MonoBehaviour
{
    public TMP_Text StoreCountText;
    
    public Slider ProgressSlider;
    public TMP_Text BuyButtonText;
    public Button BuyButton;

    public Store Store;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnEnable()
        {
            gamemanager.OnUpdateBalance += UpdateUI;
            LoadGameData.OnLoadDataComplete += UpdateUI;
        }

    void OnDisable()
        {
            gamemanager.OnUpdateBalance -= UpdateUI;
            LoadGameData.OnLoadDataComplete += UpdateUI;
        }     

    void Awake()
        {
            Store = transform.GetComponent<Store>();
        }

    void Start()
        {
            StoreCountText.text = Store.StoreCount.ToString();
        }

    // Update is called once per frame
    void Update()
        {
            ProgressSlider.value = Mathf.Clamp01(Store.GetCurrentTimer() / Store.GetStoreTimer());    
        }

    public void UpdateUI()
        {
            //Hide Panel until you can afford the store.
            CanvasGroup cg = this.transform.GetComponent<CanvasGroup>();
            if (!Store.StoreUnlocked && !gamemanager.instance.CanBuy(Store.GetNextStoreCost()))
                {
                    cg.interactable = false;
                    cg.alpha = 0;
                }
            else
                {
                    cg.interactable = true;
                    cg.alpha = 1;
                    Store.StoreUnlocked = true;
                }
                //Update Button until you can afford the store.
                if (gamemanager.instance.CanBuy (Store.GetNextStoreCost()))
                    {
                        BuyButton.interactable = true;
                    }
                else
                    {
                    BuyButton.interactable =false;
                    }
                BuyButtonText.text = "Buy " + Store.GetNextStoreCost().ToString("C2");
        }

        public void BuyStoreOnClick()
            {
                if (!gamemanager.instance.CanBuy (Store.GetNextStoreCost()))
                return;
                Store.BuyStore();
                StoreCountText.text = Store.StoreCount.ToString();
                UpdateUI();
            }

        public void OnTimerClick()
            {
                Store.OnStartTimer();
            }
}
