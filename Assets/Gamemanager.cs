using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Gamemanager Instance;
    public UIManager UIcontroller;
    public ScreenManager ScreenManager;
    public GameObject Uimanager;
    public GameObject screenmanager;
    public Notification Notification;
    public int IdGame = 10102001;
    public void Awake()
    {
        if (Instance != null) DestroyImmediate(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if (UIcontroller != null) UIcontroller = GetComponent<UIManager>();
        if (ScreenManager != null) ScreenManager = screenmanager.GetComponent<ScreenManager>();
    }

    public void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
    void Start()
    {
        //product_info.xml
        if (Uimanager != null) Uimanager.SetActive(true);
        if (screenmanager != null) screenmanager.SetActive(true);
        if (UIcontroller != null) UIcontroller.enabled = true;
        if (ScreenManager != null) ScreenManager.enabled = true;
        if (Notification != null) Notification.enabled = true;

        // Đã tạo các item có chức năng đặc biệt 

        ItemData[] skill = new ItemData[4];
        skill[0] = new ItemData(); // Khởi tạo các phần tử của mảng
        skill[0].name = "skill 1";
        skill[0].description = "you can destroy one row";
        skill[0].number = 1;
        skill[1] = new ItemData();
        skill[1].name = "skill 2";
        skill[1].description = "you can destroy one column";
        skill[1].number = 1;
        skill[2] = new ItemData();
        skill[2].name = "skill 3";
        skill[2].description = "you can destroy one row, one column";
        skill[2].number = 1;
        skill[3] = new ItemData();
        skill[3].name = "skill 4";
        skill[3].description = "you can destroy 3 rows, 3 columns";
        skill[3].number = 1;

        for (int i = 0; i < skill.Length; i++)
        {
            SaveItemDataToXML(skill[i], "item_info" + i + ".xml");
        }
        ItemData[] loadedItemData = new ItemData[4];
        for (int i = 0; i < 4; i++)
        {
            loadedItemData[i] = LoadItemDataFromXML("item_info" + i + ".xml");
        }
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("Name: " + loadedItemData[i].name);
            Debug.Log("Description: " + loadedItemData[i].description);
            Debug.Log("Number: " + loadedItemData[i].number);
        }
        StartCoroutine(Laydulieu());
    }

    [System.Serializable]
    public class ProductData
    {
        public string name;
        public string description;
        public int number;
    }
    public class ItemData
    {
        public string name;
        public string description;
        public int number;
    }
    public class PlayerData
    {
        public string name;
        public string description;
        public int number;
        public int level;
        public ItemData[] itemDatas;
        public string[] friend;
    }

    void SaveItemDataToXML(ItemData itemData, string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemData));
        FileStream stream = new FileStream(filePath, FileMode.Create);
        serializer.Serialize(stream, itemData);
        stream.Close();
    }
    ItemData LoadItemDataFromXML(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemData));
        FileStream stream = new FileStream(filePath, FileMode.Open);
        ItemData itemData = serializer.Deserialize(stream) as ItemData;
        stream.Close();
        return itemData;
    }
    void SaveProductDataToXML(ProductData productData, string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ProductData));
        FileStream stream = new FileStream(filePath, FileMode.Create);
        serializer.Serialize(stream, productData);
        stream.Close();
    }

    ProductData LoadProductDataFromXML(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ProductData));
        FileStream stream = new FileStream(filePath, FileMode.Open);
        ProductData productData = serializer.Deserialize(stream) as ProductData;
        stream.Close();
        return productData;
    }
    void SavePlayerDataToXML(PlayerData playerData, string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
        FileStream stream = new FileStream(filePath, FileMode.Create);
        serializer.Serialize(stream, playerData);
        stream.Close();
    }

    PlayerData LoadPlayerDataFromXML(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
        FileStream stream = new FileStream(filePath, FileMode.Open);
        PlayerData playerData = serializer.Deserialize(stream) as PlayerData;
        stream.Close();
        return playerData;
    }
    // Thiet lap qua trinh khoi tao data base 
    IEnumerator Laydulieu()
    {
        WWWForm form = new WWWForm();
        form.AddField("idPc", IdGame.ToString());
        WWW www = new WWW("http://localhost/AnimalWorld/item_info.php");
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("Connected");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        }
        else
        {
            Debug.Log("Disconnectted! " + " " + www.text);
        }
    }
    public void Vertifire()
    {

    }
}

