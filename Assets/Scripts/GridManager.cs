using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GridManager : MonoBehaviour
{
    public List<GameObject> list;
    public GameObject[,] grid;
    public int rows;
    public int columns;
    public GameObject gridParent; // Parent GameObject with GridLayoutGroup
    public static GridManager instance;
    public GridLayoutGroup gridLayoutGroup;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        // Check if the parent GameObject has a GridLayoutGroup component
        gridLayoutGroup = gridParent.GetComponent<GridLayoutGroup>();
        if (gridLayoutGroup == null)
        {
            Debug.LogError("GridParent does not have a GridLayoutGroup component.");

        }
        // Calculate rows and columns based on half of the child count

        int childCount = gridParent.transform.childCount;
        gridLayoutGroup.constraintCount = Mathf.FloorToInt(Mathf.Sqrt(childCount));
        rows = Mathf.FloorToInt(Mathf.Sqrt(childCount));
        columns = Mathf.FloorToInt(Mathf.Sqrt(childCount));


        // Initialize the grid array
        grid = new GameObject[rows, columns];

        // Populate the grid array with existing child GameObjects
        PopulateGrid();
        StartCoroutine(Matchs());
    }
    private void PopulateGrid()
    {
        int childCount = gridParent.transform.childCount;
        int index = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (index < childCount)
                {
                    grid[i, j] = gridParent.transform.GetChild(index).gameObject;
                    grid[i, j].GetComponent<Blocks>().Position = new Vector2Int(i, j);
                    index++;
                }
            }
        }
        // Ensure grid is fully populated
        if (index < rows * columns)
        {
            Debug.LogWarning("Not enough child objects to fill the grid.");
        }
    }
    private HashSet<GameObject> found = new HashSet<GameObject>();
    bool test()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                GameObject a = GetGameObjectAt(x, y);
                if (a == null) continue;

                // Check row matches
                List<GameObject> rowMatchesTiles = new List<GameObject> { a };
                for (int i = 1; x + i < columns; i++)
                {
                    if (GetGameObjectAt(x + i, y).GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                    {
                        rowMatchesTiles.Add(GetGameObjectAt(x + i, y));
                    }
                    else break;
                }
                for (int i = 1; x - i >= 0; i++)
                {
                    if (GetGameObjectAt(x - i, y).GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                    {
                        rowMatchesTiles.Add(GetGameObjectAt(x - i, y));
                    }
                    else break;
                }

                if (rowMatchesTiles.Count >= 3)
                {
                    found.UnionWith(rowMatchesTiles);
                }

                // Check column matches
                List<GameObject> columnMatchesTiles = new List<GameObject> { a };
                for (int j = 1; y + j < rows; j++)
                {
                    if (GetGameObjectAt(x, y + j).GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                    {
                        columnMatchesTiles.Add(GetGameObjectAt(x, y + j));
                    }
                    else break;
                }
                for (int j = 1; y - j >= 0; j++)
                {
                    if (GetGameObjectAt(x, y - j).GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                    {
                        columnMatchesTiles.Add(GetGameObjectAt(x, y - j));
                    }
                    else break;
                }

                if (columnMatchesTiles.Count >= 3)
                {
                    found.UnionWith(columnMatchesTiles);
                }
            }
        }
        return found.Count > 3;
    }
    public IEnumerator Matchs()
    {
        while (true)
        {
            found.Clear();

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    GameObject a = GetGameObjectAt(x, y);
                    if (a == null) continue;

                    // Check row matches
                    List<GameObject> rowMatchesTiles = new List<GameObject> { a };
                    for (int i = 1; x + i < columns; i++)
                    {
                        if (GetGameObjectAt(x + i, y).GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                        {
                            rowMatchesTiles.Add(GetGameObjectAt(x + i, y));
                        }
                        else break;
                    }
                    for (int i = 1; x - i >= 0; i++)
                    {
                        if (GetGameObjectAt(x - i, y).GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                        {
                            rowMatchesTiles.Add(GetGameObjectAt(x - i, y));
                        }
                        else break;
                    }

                    if (rowMatchesTiles.Count >= 3)
                    {
                        found.UnionWith(rowMatchesTiles);
                    }

                    // Check column matches
                    List<GameObject> columnMatchesTiles = new List<GameObject> { a };
                    for (int j = 1; y + j < rows; j++)
                    {
                        if (GetGameObjectAt(x, y + j).GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                        {
                            columnMatchesTiles.Add(GetGameObjectAt(x, y + j));
                        }
                        else break;
                    }
                    for (int j = 1; y - j >= 0; j++)
                    {
                        if (GetGameObjectAt(x, y - j).GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                        {
                            columnMatchesTiles.Add(GetGameObjectAt(x, y - j));
                        }
                        else break;
                    }

                    if (columnMatchesTiles.Count >= 3)
                    {
                        found.UnionWith(columnMatchesTiles);
                    }
                }
            }

            // Animation for matched tiles
            foreach (var tile in found)
            {
                StartCoroutine(AnimateAndDestroyTile(tile));
            }
            // Wait for the animation to complete
            yield return new WaitForSeconds(1.5f);

            StartCoroutine(InitNull());

        }
    }

    IEnumerator AnimateAndDestroyTile(GameObject tile)
    {
        float duration = 1.5f;
        float elapsedTime = 0f;
        Vector3 initialScale = tile.transform.localScale;
        while (elapsedTime < duration)
        {
            tile.transform.Rotate(Vector3.left * Time.deltaTime * 5);
            tile.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        tile.SetActive(false);
    }

    IEnumerator InitNull()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {

                GameObject a = GetGameObjectAt(x, y);
                if (a == null) continue;

                // Check row matches
                List<GameObject> rowMatchesTiles = new List<GameObject> { a };
                for (int i = 1; x + i < columns; i++)
                {
                    if (GetGameObjectAt(x + i, y).GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                    {
                        rowMatchesTiles.Add(GetGameObjectAt(x + i, y));
                    }
                    else break;
                }
                for (int i = 1; x - i >= 0; i++)
                {
                    if (GetGameObjectAt(x - i, y).GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                    {
                        rowMatchesTiles.Add(GetGameObjectAt(x - i, y));
                    }
                    else break;
                }

                if (rowMatchesTiles.Count >= 3)
                {
                    found.UnionWith(rowMatchesTiles);
                }

                // Check column matches
                List<GameObject> columnMatchesTiles = new List<GameObject> { a };
                for (int j = 1; y + j < rows; j++)
                {
                    if (GetGameObjectAt(x, y + j).GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                    {
                        columnMatchesTiles.Add(GetGameObjectAt(x, y + j));
                    }
                    else break;
                }
                for (int j = 1; y - j >= 0; j++)
                {
                    if (GetGameObjectAt(x, y - j).GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                    {
                        columnMatchesTiles.Add(GetGameObjectAt(x, y - j));
                    }
                    else break;
                }

                if (columnMatchesTiles.Count >= 3)
                {
                    found.UnionWith(columnMatchesTiles);
                }

                foreach (var tile in found)
                {
                    StartCoroutine(AnimateAndDestroyTile(tile));
                }
                // Wait for the animation to complete
                yield return new WaitForSeconds(1.5f);

                if (GetGameObjectAt(x, y) == null || !GetGameObjectAt(x, y).activeSelf)
                {
                    int index = Random.Range(0, list.Count - (int)Mathf.Sqrt(rows * 2));
                    GameObject newTile = Instantiate(list[index], gridParent.transform);
                    newTile.transform.SetSiblingIndex(x * columns + y);
                    newTile.transform.localPosition = gridParent.transform.GetChild(x * columns + y).localPosition;
                    Destroy(GetGameObjectAt(x, y));
                    grid[x, y] = newTile;
                    grid[x, y].GetComponent<Blocks>().Position = new Vector2Int(x, y);
                }

            }
        }
        yield return null;
    }

    public void SwapTiles(Vector2Int pos1, Vector2Int pos2)
    {
        Debug.Log("check 2");

        // Lấy đối tượng ô tại vị trí chỉ định
        GameObject tile1 = GetGameObjectAt(pos1.x, pos1.y);
        GameObject tile2 = GetGameObjectAt(pos2.x, pos2.y);

        if (tile1 == null || tile2 == null)
        {
            Debug.LogError("One or both tiles are null. Aborting swap.");
            return;
        }

        // Hoán đổi các ô trong mảng lưới
        grid[pos1.x, pos1.y] = tile2;
        grid[pos2.x, pos2.y] = tile1;

        // Hoán đổi vị trí của các ô trong cảnh
        SwapTilePositions(tile1, tile2);

        // Kiểm tra điều kiện với hàm test() và khôi phục nếu không đạt
        if (!test())
        {
            // Hoán đổi lại các ô trong mảng lưới
            grid[pos1.x, pos1.y] = tile1;
            grid[pos2.x, pos2.y] = tile2;

            // Hoán đổi lại vị trí của các ô trong cảnh
            SwapTilePositions(tile1, tile2);
        }
    }

    private void SwapTilePositions(GameObject tile1, GameObject tile2)
    {
        // Hoán đổi vị trí của các ô trong cảnh
        Vector3 tempPosition = tile1.transform.localPosition;
        tile1.transform.localPosition = tile2.transform.localPosition;
        tile2.transform.localPosition = tempPosition;

        // Cập nhật thứ tự của các ô trong hệ thống phân cấp nếu cần thiết
        int tempIndex = tile1.transform.GetSiblingIndex();
        tile1.transform.SetSiblingIndex(tile2.transform.GetSiblingIndex());
        tile2.transform.SetSiblingIndex(tempIndex);
    }

    public GameObject GetGameObjectAt(int row, int col)
    {
        if (row < 0 || row >= rows || col < 0 || col >= columns)
        {
            Debug.LogError("Grid position out of bounds.");
            return null;
        }
        else if (grid[row, col] == null) return null;
        return grid[row, col];
    }
    public Vector2Int testGrid;
    float inittime = 0;
    void Update()
    {
        inittime += Time.deltaTime;
        if (inittime > 10)
        {
            if (test()) StartCoroutine(Matchs());
            inittime = 0;
        }

        //if (Input.GetMouseButtonDown(0)) // Example trigger
        //{

        //    GameObject obj4 = GetGameObjectAt(testGrid.x - 1, testGrid.y - 1);
        //    if (obj4 != null)
        //    {
        //        Debug.Log($"Found GameObject at ({testGrid.x},{testGrid.y}): " + obj4.name);
        //    }
        //}
    }
}
