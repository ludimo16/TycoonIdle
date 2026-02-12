using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using TMPro;

public class LoadGameData : MonoBehaviour
{
    public delegate void LoadDataComplete();
    public static event LoadDataComplete OnLoadDataComplete;
    public TextAsset GameData;
    public GameObject StorePrefab;
    public GameObject StorePanel;

        public void Start()
            {
                LoadData();
                if (OnLoadDataComplete != null)
                OnLoadDataComplete();
            }

    public void LoadData()
        {
            //Create XNL Document to hold game data
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(GameData.text);

            //Load Game Manager Data
            LoadGameManagerData(xmlDoc);

            //Load the Stores
            LoadStores(xmlDoc);
            }

            public void LoadGameManagerData(XmlDocument xmlDoc)
                {
                    //Load Game Manager Info
                    //StartingBalance
                    float StartingBalance = float.Parse(xmlDoc.GetElementsByTagName("StartingBalance")[0].InnerText);
                    gamemanager.instance.AddToBalance(StartingBalance);

                    string CompanyName = xmlDoc.GetElementsByTagName("CompanyName")[0].InnerText;
                    gamemanager.instance.CompanyName = CompanyName;
                }
    public void LoadStores(XmlDocument xmlDoc)
        {
            XmlNodeList StoreList = xmlDoc.GetElementsByTagName("store");
            foreach (XmlNode StoreInfo in StoreList)
                {
                   // Load Stores Nodes
                    LoadStoreNodes(StoreInfo);
                }
        }

    public void SetStoreObj(Store storeobj, XmlNode StoreNode, GameObject NewStore)
    {
          if (StoreNode.Name == "name")
                {
                    TMP_Text StoreText = NewStore.transform.Find("StoreNameText").GetComponent<TMP_Text>();
                    StoreText.text = StoreNode.InnerText;
                }
            if (StoreNode.Name == "image")
                {
                    Sprite newSprite = Resources.Load<Sprite>(StoreNode.InnerText);
                    Image StoreImage = NewStore.transform.Find("ImageButtonClick").GetComponent<Image>();
                    StoreImage.sprite = newSprite;
                }

            if (StoreNode.Name == "BaseStoreProfit")
                {
                    storeobj.BaseStoreProfit = float.Parse(StoreNode.InnerText);
                }
            if (StoreNode.Name == "BaseStoreCost")
                {
                    storeobj.BaseStoreCost = float.Parse(StoreNode.InnerText);
                }

            if (StoreNode.Name == "StoreTimer")
                {
                    storeobj.StoreTimer = float.Parse(StoreNode.InnerText);
                }

            if (StoreNode.Name == "StoreMultiplier")
                {
                    storeobj.StoreMultiplier = float.Parse(StoreNode.InnerText);
                }
            if (StoreNode.Name == "StoreTimerDivision")
                {
                    storeobj.StoreTimerDivision = int.Parse(StoreNode.InnerText);
                }
            if (StoreNode.Name == "StoreCount")
                {
                    storeobj.StoreCount = int.Parse(StoreNode.InnerText);
                }
    }

    public void LoadStoreNodes(XmlNode StoreInfo)
        {
            GameObject NewStore = (GameObject)Instantiate(StorePrefab);
            Store storeobj = NewStore.GetComponent<Store>();

            XmlNodeList StoreNodes = StoreInfo.ChildNodes;
            foreach (XmlNode StoreNode in StoreNodes)
                {
                    SetStoreObj(storeobj, StoreNode, NewStore);
                }
            //Setup Store Next Cost
            storeobj.SetNextStoreCost(storeobj.BaseStoreCost);
            //Connect our Store to the parent panel
            NewStore.transform.SetParent(StorePanel.transform);
        }
    }
