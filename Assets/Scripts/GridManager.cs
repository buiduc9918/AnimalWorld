using System.Collections;
using System.Collections.Generic;
using Tiles;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace Grid
{
    public class GridManager : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI m_TextMeshPro;
        private int _score;

        public List<GameObject> list;
        public List<GameObject> exspecial;
        [SerializeField] private List<Vector3> locationTiles;
        public GameObject[,] grid;
        public int rows = 4;
        public int columns = 5;
        public static GridManager Instance;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        public float Distance = 120;

        #region START


        private void Awake()
        {
            Instance = this;
        }
        Vector3 positionOffset;

        public GameObject GetGameObjectAt(int row, int col)
        {
            Create();
            if (row < 0 || row >= rows || col < 0 || col >= columns)
            {
                return null;
            }
            return grid[row, col];
        }
        void Start()
        {
            locationTiles = new List<Vector3>();
            positionOffset = new Vector3(-200, 0, 0) + transform.position - new Vector3(columns * Distance / 2.0f, rows * Distance / 2.0f, 0);
            gridLayoutGroup = GetComponent<GridLayoutGroup>();
            grid = new GameObject[rows, columns];
            _score = 0;
            Create();
            if (FOUND().Count > 0)
                StartCoroutine(RemoveMatchesCoroutine(FOUND()));
        }

        public int Score
        {
            set
            {
                _score = value;
            }
            get
            {
                if (_score > 300000 && _score < 454000)
                {
                    m_TextMeshPro.text = $"Deliciuos";
                }
                else m_TextMeshPro.text = $"{_score}";
                return _score;
            }
        }
        //  Score++;
        private IEnumerator RemoveMatchesCoroutine(HashSet<GameObject> matches)
        {
            foreach (var tile in matches)
                StartCoroutine(AnimateAndDestroyTileSimple(tile));
            yield return new WaitForSeconds(1f); // wait for animations to complete
            Create();
            HashSet<GameObject> newMatches = FOUND();
            if (newMatches.Count > 0)
                StartCoroutine(RemoveMatchesCoroutine(newMatches));
        }
        private IEnumerator AnimateAndDestroyTileSimple(GameObject tile)
        {
            float duration = 1.2f;
            float elapsedTime = 0f;
            Vector3 initialScale = tile.transform.localScale;
            while (elapsedTime < duration)
            {
                if (tile != null)
                {
                    tile.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 180);
                    tile.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, elapsedTime / duration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }
            if (tile != null)
            {
                Destroy(tile);
            }
        }
        #endregion
        #region FOUND


        private HashSet<GameObject> FOUND()
        {
            HashSet<GameObject> found = new HashSet<GameObject>();

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    GameObject a = GetGameObjectAt(x, y);
                    if (a == null) continue;
                    List<GameObject> rowMatchesTiles = new List<GameObject> { a };
                    for (int i = 1; x + i < columns; i++)
                    {
                        if (GetGameObjectAt(x + i, y)?.GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                        {
                            rowMatchesTiles.Add(GetGameObjectAt(x + i, y));
                        }
                        else break;
                    }
                    for (int i = 1; x - i >= 0; i++)
                    {
                        if (GetGameObjectAt(x - i, y)?.GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                        {
                            rowMatchesTiles.Add(GetGameObjectAt(x - i, y));
                        }
                        else break;
                    }
                    if (rowMatchesTiles.Count >= 3)
                    {
                        found.UnionWith(rowMatchesTiles);
                    }
                    List<GameObject> columnMatchesTiles = new List<GameObject> { a };
                    for (int j = 1; y + j < rows; j++)
                    {
                        if (GetGameObjectAt(x, y + j)?.GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                        {
                            columnMatchesTiles.Add(GetGameObjectAt(x, y + j));
                        }
                        else break;
                    }
                    for (int j = 1; y - j >= 0; j++)
                    {
                        if (GetGameObjectAt(x, y - j)?.GetComponent<Tile>().number == a.GetComponent<Tile>().number)
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
            Score += 10000;
            return found;
        }
        #endregion
        #region BUFF

        public IEnumerator destroy44(Vector2Int x)
        {
            HashSet<GameObject> tiles = new HashSet<GameObject>();
            for (int i = 0; i < columns; i++)
                tiles.Add(GetGameObjectAt(x.x, i));
            foreach (var tile in tiles)
                StartCoroutine(AnimateAndDestroyTileSimple(tile));
            Create();
            HashSet<GameObject> newMatches = FOUND();
            if (newMatches.Count > 0)
                StartCoroutine(RemoveMatchesCoroutine(newMatches));
            yield return null;
        }
        public IEnumerator destroy55(Vector2Int y)
        {
            HashSet<GameObject> tiles = new HashSet<GameObject>();
            for (int i = 0; i < rows; i++)
                tiles.Add(GetGameObjectAt(i, y.y));
            foreach (var tile in tiles)
                StartCoroutine(AnimateAndDestroyTileSimple(tile));
            Create();
            HashSet<GameObject> newMatches = FOUND();
            if (newMatches.Count > 0)
                StartCoroutine(RemoveMatchesCoroutine(newMatches));
            yield return null;
        }

        #endregion
        #region INIT


        private enum xet
        {
            khoitao,
            lon,
            bang,
            nho
        }
        [SerializeField] private xet a;

        private void Create()
        {
            int test = transform.childCount;
            if (test > rows * columns)
                a = xet.lon;
            else if (test == rows * columns)
                a = xet.bang;
            else if (test < rows * columns && test > 0)
                a = xet.nho;
            else if (test == 0)
                a = xet.khoitao;
            Create(a);
        }
        private void Create(xet a)
        {
            switch (a)
            {
                // Case xet.lon has been commented out

                case xet.nho:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            GameObject l = grid[i, j];
                            if (l != null)
                                continue;

                            // Create a new list of GameObjects to choose from
                            List<GameObject> k = new List<GameObject>(list);

                            // Remove GameObjects that would create a sequence of three in a row vertically
                            if (i > 1 && grid[i - 1, j] != null && grid[i - 2, j] != null &&
                                grid[i - 1, j].GetComponent<Tile>().number == grid[i - 2, j].GetComponent<Tile>().number)
                            {
                                k.Remove(grid[i - 1, j]);
                            }

                            // Remove GameObjects that would create a sequence of three in a row horizontally
                            if (j > 1 && grid[i, j - 1] != null && grid[i, j - 2] != null &&
                                grid[i, j - 1].GetComponent<Tile>().number == grid[i, j - 2].GetComponent<Tile>().number)
                            {
                                k.Remove(grid[i, j - 1]);
                            }

                            if (k.Count == 0)
                                k = new List<GameObject>(list); // Reset to full list if all objects are removed

                            // Instantiate a new GameObject from the list
                            GameObject newObject = Instantiate(k[Random.Range(0, k.Count)]);
                            grid[i, j] = newObject;
                            newObject.transform.parent = transform;
                            newObject.GetComponent<Blocks>().seque = new Vector2Int(i, j);
                            newObject.transform.position = new Vector3(2 * j * Distance, 2 * i * Distance, 0) + positionOffset;
                        }
                    }
                    break;

                // Case xet.bang has been commented out

                case xet.khoitao:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            List<GameObject> k = new List<GameObject>(list);

                            // Remove GameObjects that would create a sequence of three in a row vertically
                            if (i > 1 && grid[i - 1, j] != null && grid[i - 2, j] != null &&
                                grid[i - 1, j].GetComponent<Tile>().number == grid[i - 2, j].GetComponent<Tile>().number)
                            {
                                k.Remove(grid[i - 1, j]);
                            }

                            // Remove GameObjects that would create a sequence of three in a row horizontally
                            if (j > 1 && grid[i, j - 1] != null && grid[i, j - 2] != null &&
                                grid[i, j - 1].GetComponent<Tile>().number == grid[i, j - 2].GetComponent<Tile>().number)
                            {
                                k.Remove(grid[i, j - 1]);
                            }

                            if (k.Count == 0)
                                k = new List<GameObject>(list); // Reset to full list if all objects are removed

                            // Instantiate a new GameObject from the list
                            GameObject newObject = Instantiate(k[Random.Range(0, k.Count)]);
                            grid[i, j] = newObject;
                            newObject.transform.parent = transform;
                            newObject.GetComponent<Blocks>().seque = new Vector2Int(i, j);
                            newObject.transform.position = new Vector3(2 * j * Distance, 2 * i * Distance, 0) + positionOffset;
                        }
                    }
                    break;
                case xet.lon:
                    break;
                case xet.bang:
                    break;
            }
        }
        #region Creat Old
        /*
        private void Create(xet a)
        {
            switch (a)
            {
                case xet.lon:
                    if (transform.GetChild(rows * columns) != null)
                        DestroyImmediate(transform.GetChild(transform.childCount - 1).gameObject);
                    break;

                case xet.nho:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            int index = i * columns + j;
                            GameObject l = grid[i, j];
                            if (index < transform.childCount && transform.GetChild(index) != null)
                                continue;
                            else
                            {
                                List<GameObject> k = new List<GameObject>(list);
                                if (i > 1 && grid[i - 1, j] != null && grid[i - 2, j] != null && grid[i - 1, j].GetComponent<Tile>().number == grid[i - 2, j].GetComponent<Tile>().number)
                                    k.Remove(grid[i - 1, j]);
                                if (j > 1 && grid[i, j - 1] != null && grid[i, j - 2] != null && grid[i, j - 1].GetComponent<Tile>().number == grid[i, j - 2].GetComponent<Tile>().number)
                                    k.Remove(grid[i, j - 1]);
                                if (k.Count == 0)
                                    k = new List<GameObject>(list); // If k is empty, reset to the full list to avoid errors
                                GameObject newObject = Instantiate(k[Random.Range(0, k.Count)]);
                                grid[i, j] = newObject;// O TRONG LUOI TUONG UNG
                                newObject.transform.SetParent(transform);
                                newObject.GetComponent<Blocks>().seque = new Vector2Int(i, j);// XAC DINH THEO O
                                newObject.transform.position = new Vector3(2 * j * (Distance), 2 * i * Distance, 0) + positionOffset;                                                        
                                newObject.transform.position = locationTiles[index];// XAC DINH DUNG THEO VI TRI O
                            }
                        }
                    }
                    break;

                case xet.bang:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            int index = i * columns + j;
                            GameObject existingObject = transform.GetChild(index).gameObject;
                            grid[i, j] = existingObject;
                            existingObject.transform.SetParent(transform);
                            existingObject.GetComponent<Blocks>().seque = new Vector2Int(i, j);
                            existingObject.transform.position = new Vector3(2 * j * (Distance), 2 * i * Distance, 0) + positionOffset= locationTiles[index];//XAC DINH THEO VI TRI O
                        }
                    }
                    break;
                case xet.khoitao:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            int index = i * columns + j;
                            List<GameObject> k = new List<GameObject>(list);
                            if (i > 1 && grid[i - 1, j] != null && grid[i - 2, j] != null && grid[i - 1, j].GetComponent<Tile>().number == grid[i - 2, j].GetComponent<Tile>().number)
                                k.Remove(grid[i - 1, j]);
                            if (j > 1 && grid[i, j - 1] != null && grid[i, j - 2] != null && grid[i, j - 1].GetComponent<Tile>().number == grid[i, j - 2].GetComponent<Tile>().number)
                                k.Remove(grid[i, j - 1]);
                            if (k.Count == 0)
                                k = new List<GameObject>(list); // If k is empty, reset to the full list to avoid errors
                            GameObject newObject = Instantiate(k[Random.Range(0, k.Count)]);
                            grid[i, j] = newObject;
                            newObject.transform.SetParent(transform);
                            newObject.GetComponent<Blocks>().seque = new Vector2Int(i, j);
                            newObject.transform.position = new Vector3(2 * j * (Distance), 2 * i * Distance, 0) + positionOffset;
                            locationTiles.Add(newObject.transform.position);
                        }
                    }
                    break;
            }

        }
        */
        #endregion
        #endregion
        #region SWAP
        public void SwapTiles(Vector2Int pos1, Vector2Int pos2)
        {
            GameObject tile1 = GetGameObjectAt(pos1.x, pos1.y);
            GameObject tile2 = GetGameObjectAt(pos2.x, pos2.y);
            if (tile1 == null || tile2 == null) return;
            grid[pos1.x, pos1.y] = tile2;
            grid[pos2.x, pos2.y] = tile1;
            SwapTilePositions(tile1, tile2);
            HashSet<GameObject> matches = FOUND();
            if (matches.Count > 0)
                StartCoroutine(RemoveMatchesCoroutine(matches));
            else
            {
                grid[pos1.x, pos1.y] = tile1;
                grid[pos2.x, pos2.y] = tile2;
                SwapTilePositions(tile1, tile2);
            }
        }

        private void SwapTilePositions(GameObject tile1, GameObject tile2)
        {
            Vector3 tempPosition = tile1.transform.localPosition;
            tile1.transform.localPosition = tile2.transform.localPosition;
            tile2.transform.localPosition = tempPosition;
            int tempIndex = tile1.transform.GetSiblingIndex();
            tile1.transform.SetSiblingIndex(tile2.transform.GetSiblingIndex());
            tile2.transform.SetSiblingIndex(tempIndex);
        }
        #endregion
        //IEnumerator MoveMouse(GameObject a, Vector3 targetPosition)
        //{
        //    if (a == null)
        //    {
        //        Debug.LogError("GameObject is null!");
        //        yield break;
        //    }

        //    float duration = 0.3f;
        //    float elapsedTime = 0f;
        //    Vector3 initialPosition = a.transform.position;

        //    while (elapsedTime < duration)
        //    {
        //        float t = elapsedTime / duration;
        //        a.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
        //        elapsedTime += Time.deltaTime;
        //        yield return null;
        //    }

        //    // Ensure the object reaches the exact target position
        //    a.transform.position = targetPosition;
        //}

    }
}
