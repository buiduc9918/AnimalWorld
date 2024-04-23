using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }
    public GameObject[,] Grid { get; private set; }
    private Sprite[] sprites = new Sprite[3];
    public Image request1, request2, request3;
    public List<Sprite> randomSprites;
    public GridManager gridManager;

    private void Awake()
    {
        Instance = this;
        request1 = GameObject.Find("cat").GetComponent<Image>();
        request2 = GameObject.Find("punny").GetComponent<Image>();
        request3 = GameObject.Find("pig").GetComponent<Image>();
    }

    private void Start()
    {
        Grid = gridManager.Grid;
        SetRequestSprites();
    }

    private void SetRequestSprites()
    {
        int index1, index2, index3;
        do
        {
            index1 = Random.Range(0, randomSprites.Count);
            index2 = Random.Range(0, randomSprites.Count);
            index3 = Random.Range(0, randomSprites.Count);
        } while (index1 == index2 || index2 == index3 || index1 == index3);
        request1.sprite = randomSprites[index1];
        sprites[0] = request1.sprite;
        request2.sprite = randomSprites[index2];
        sprites[1] = request2.sprite;
        request3.sprite = randomSprites[index3];
        sprites[2] = request3.sprite;
    }

}