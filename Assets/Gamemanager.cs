using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
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
        skill[0].id = 1;
        skill[0].description = "you can destroy one row";
        skill[1] = new ItemData();
        skill[1].id = 2;
        skill[1].description = "you can destroy one column";
        skill[2] = new ItemData();
        skill[2].id = 3;
        skill[2].description = "you can destroy one row, one column";
        skill[3] = new ItemData();
        skill[3].id = 4;
        skill[3].description = "you can destroy 1 row, 2 columns";
        ProductData[] product = new ProductData[4];
        product[0] = new ProductData();
        product[0].id = 1;
        product[0].cost = 10;
        product[1] = new ProductData();
        product[1].id = 2;
        product[1].cost = 10;
        product[2] = new ProductData();
        product[2].id = 3;
        product[2].cost = 10;
        product[3] = new ProductData();
        product[3].id = 4;
        product[3].cost = 10;

        PlayerData[] playerDatas = new PlayerData[1];
        playerDatas[0] = new PlayerData();
        playerDatas[0].name = "admin";
        playerDatas[0].idpc = 10102001;
        playerDatas[0].item_1 = 1;
        playerDatas[0].item_2 = 1;
        playerDatas[0].item_3 = 1;
        playerDatas[0].item_4 = 1;
        playerDatas[0].score = 120;
        playerDatas[0].money = 200;
        playerDatas[0].level = 1;

        SavePlayerDataToXML(playerDatas[0], "player_info" + 0 + ".xml");
        PlayerData[] loadedPlayerDatas = new PlayerData[1];
        loadedPlayerDatas[0] = LoadPlayerDataFromXML("player_info" + 0 + ".xml");
        Debug.Log("name :" + loadedPlayerDatas[0].name);
        Debug.Log("idpc :" + loadedPlayerDatas[0].idpc.ToString());
        Debug.Log("item-1 :" + loadedPlayerDatas[0].item_1.ToString());
        Debug.Log("item-2 :" + loadedPlayerDatas[0].item_2.ToString());
        Debug.Log("item-3 :" + loadedPlayerDatas[0].item_3.ToString());
        Debug.Log("item-4 :" + loadedPlayerDatas[0].item_4.ToString());

        Debug.Log("score :" + loadedPlayerDatas[0].score.ToString());
        Debug.Log("money :" + loadedPlayerDatas[0].money.ToString());
        Debug.Log("level :" + loadedPlayerDatas[0].level.ToString());


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
            Debug.Log("id: " + loadedItemData[i].id.ToString());
            Debug.Log("Description: " + loadedItemData[i].description);
        }

        for (int i = 0; i < product.Length; i++)
        {
            SaveProductDataToXML(product[i], "product_info" + i + ".xml");
        }
        ProductData[] loadedProductData = new ProductData[4];
        for (int i = 0; i < 4; i++)
        {
            loadedProductData[i] = LoadProductDataFromXML("product_info" + i + ".xml");
        }
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("id: " + loadedProductData[i].id.ToString());
            Debug.Log("cost: " + loadedProductData[i].cost.ToString());
        }
        StartCoroutine(Laydulieu());
        StartCoroutine(InsertDataIntoTableCoroutine());

        StartCoroutine(DeleteDuplicateItemsCoroutine());


    }

    [System.Serializable]
    public class ProductData
    {
        public int id;
        public int cost;
    }
    public class ItemData
    {
        public int id;
        public string description;
    }
    public class PlayerData
    {
        public string name;
        public int idpc;
        public int item_1;
        public int item_2;
        public int item_3;
        public int item_4;
        public int score;
        public int money;
        public int level;
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

    IEnumerator Laydulieu()
    {
        string connStr = "server=localhost;user=root;database=AnimalWorld;port=3306;password=";
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Debug.Log("Connecting to MySQL...");
            conn.Open();

            string sql = "SELECT idpc FROM pc_info";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                int pcCount = Convert.ToInt32(result);
                Debug.Log("Number of PCs in the database is: " + pcCount);
            }
            else
            {
                Debug.Log("No PC found in the database.");
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Error connecting to MySQL: " + ex.Message);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        Debug.Log("Done.");
        yield break; // Not necessary to yield further
    }


    private string connectionString = "server=localhost;user=user;database=AnimalWorld;port=3306;password=vungoimora";

    IEnumerator InsertDataIntoTableCoroutine()
    {
        yield return new WaitForSeconds(1f); // Đợi 1 giây trước khi thực hiện truy vấn

        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO store_market (item, cost) VALUES (9, 10)";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Debug.Log("Rows affected: " + rowsAffected);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Error inserting data: " + ex.Message);
        }
    }
    IEnumerator DeleteDuplicateItemsCoroutine()
    {
        yield return new WaitForSeconds(1f); // Chờ 1 giây trước khi thực hiện truy vấn

        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Xóa các mục trùng lặp từ bảng store_market dựa trên cột item
                string sql = "DELETE t1 FROM store_market t1 " +
                             "INNER JOIN store_market t2 " +
                             "WHERE t1.id > t2.id AND t1.item = t2.item";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Debug.Log("Rows affected: " + rowsAffected);
                    Debug.Log("đã xóa chùng lặp");

                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error deleting duplicate items: " + ex.Message);
        }
    }
}

