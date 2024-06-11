using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;


namespace GameManager
{

    public class Gamemanager : MonoBehaviour
    {
        public static Gamemanager Instance;
        public GameObject Table;
        public List<GameObject> childs;
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            childs = new List<GameObject>();
        }
        void Start()
        {

            // childs = Table.GetComponentInChildren<GameObject>().gameObject;
            //product_info.xml
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
    }
}
