using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Detect : MonoBehaviour, IDragHandler
{
    private GameObject beginObject;
    private GameObject endObject;
    private PointerEventData pointerEventData;
    private List<RaycastResult> danhSachKetQuaRaycast;
    private List<GameObject> both;
    Vector2Int a, b;

    public void OnBeginDrag(PointerEventData eventData)
    {
        pointerEventData = eventData;
    }
    public void OnDrag(PointerEventData eventData)
    {
        pointerEventData = eventData;
        Vector2 currentPosition = eventData.position;
        pointerEventData.position = RectTransformUtility.WorldToScreenPoint(null, currentPosition);
        danhSachKetQuaRaycast.Clear();
        EventSystem.current.RaycastAll(pointerEventData, danhSachKetQuaRaycast);
        foreach (RaycastResult ketQua in danhSachKetQuaRaycast)
        {
            Debug.Log("Trúng: " + ketQua.gameObject.name);
            if (ketQua.gameObject.GetComponent<Tile>() != null)
            {
                both.Add(ketQua.gameObject);
            }
        }
        both[0].transform.position = Input.mousePosition;
        beginObject = both[0].gameObject;
        endObject = both[1].gameObject;
        a = FindInGrid(both[0]);
        b = FindInGrid(both[1]);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GridManager.instance.SwapTiles(a, b);
        SwapTiles(a, b);
        Debug.Log("Kết thúc kéo");
    }
    Vector2Int FindInGrid(GameObject a)
    {
        for (int i = 0; i < GridManager.instance.rows; i++)
        {
            for (int j = 0; j < GridManager.instance.columns; j++)
            {
                if (a == GridManager.instance.GetGameObjectAt(i, j))
                {
                    Debug.Log(i.ToString() + " " + j.ToString());
                    return new Vector2Int(i, j);
                }
            }
        }
        return new Vector2Int(-1, -1); // Not found
    }
    void Start()
    {
        danhSachKetQuaRaycast = new List<RaycastResult>();
        both = new List<GameObject>();

    }
    public void SwapTiles(Vector2Int pos1, Vector2Int pos2)
    {
        // Ensure the positions are within the grid bounds
        if (pos1.x < 0 || pos1.x >= GridManager.instance.rows || pos1.y < 0 || pos1.y >= GridManager.instance.columns ||
            pos2.x < 0 || pos2.x >= GridManager.instance.rows || pos2.y < 0 || pos2.y >= GridManager.instance.columns)
        {
            Debug.LogError("One or both grid positions are out of bounds.");
            return;
        }

        // Get the tiles at the specified positions
        GameObject tile1 = GridManager.instance.GetGameObjectAt(pos1.x, pos1.y);
        GameObject tile2 = GridManager.instance.GetGameObjectAt(pos2.x, pos2.y);

        // Swap the tiles in the grid array
        GridManager.instance.grid[pos1.x, pos1.y] = tile2;
        GridManager.instance.grid[pos2.x, pos2.y] = tile1;

        // Swap the positions of the tiles in the scene
        Vector3 tempPosition = tile1.transform.localPosition;
        tile1.transform.localPosition = tile2.transform.localPosition;
        tile2.transform.localPosition = tempPosition;

        // Update the sibling index in the hierarchy if necessary
        int tempIndex = tile1.transform.GetSiblingIndex();
        tile1.transform.SetSiblingIndex(tile2.transform.GetSiblingIndex());
        tile2.transform.SetSiblingIndex(tempIndex);
    }
}