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
        public List<GameObject> exspecial;//thêm các object dặc biệt

        [SerializeField] private List<Vector3> locationTiles;
        public GameObject[,] grid;
        public int rows = 7;
        public int columns = 7;
        public static GridManager Instance;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        public float Distance = 120;
        Vector3 positionOffset;
        #region SINH RA OBJECT CHEAT
        private IEnumerator RemoveMatchesCoroutine2(List<GameObject> matches)
        {
            foreach (var tile in matches)
            {
                StartCoroutine(AnimateAndDestroyTileSimple(tile));
            }
            yield return new WaitForSeconds(2.0f); // wait for animations to complete
            List<GameObject> p = FOUND();// xoát them còn match hay không
            if (p.Count > 0)
            {
                int result = ngang_doc_honhop_giandon(p);

                switch (result)
                {
                    case 0: // giản đơn
                        StartCoroutine(RemoveMatchesCoroutine(p));
                        break;
                    case 1: // hỗn hợp
                        StartCoroutine(RemoveMatchesCoroutine2(p));
                        Create2();
                        Create();
                        List<GameObject> p2 = FOUND();
                        if (p2.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p2));
                        }
                        break;
                    case 2:
                        StartCoroutine(RemoveMatchesCoroutine3(p));
                        Create3();
                        Create();
                        List<GameObject> p3 = FOUND();
                        if (p3.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p3));
                        }
                        break;
                    case 3: // dọc
                        StartCoroutine(RemoveMatchesCoroutine4(p));
                        Create4();
                        Create();
                        List<GameObject> p4 = FOUND();
                        if (p4.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p4));
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public void Create2()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (GetGameObjectAt(i, j) == null)
                    {
                        GameObject newObject = Instantiate(exspecial[0]);
                        grid[i, j] = newObject;
                        newObject.transform.SetParent(transform);
                        newObject.GetComponent<Blocks>().seque = new Vector2Int(i, j);
                        newObject.transform.position = new Vector3(2 * j * Distance, 2 * i * Distance, 0) + positionOffset;
                        break;
                    }
                }
            }
        }
        private IEnumerator RemoveMatchesCoroutine3(List<GameObject> matches)
        {
            foreach (var tile in matches)
            {
                StartCoroutine(AnimateAndDestroyTileSimple(tile));
            }
            yield return new WaitForSeconds(2.0f); // wait for animations to complete
            List<GameObject> p = FOUND();// xoát them còn match hay không
            if (p.Count > 0)
            {
                int result = ngang_doc_honhop_giandon(p);

                switch (result)
                {
                    case 0: // giản đơn
                        StartCoroutine(RemoveMatchesCoroutine(p));
                        break;
                    case 1: // hỗn hợp
                        StartCoroutine(RemoveMatchesCoroutine2(p));
                        Create2();
                        Create();
                        List<GameObject> p2 = FOUND();
                        if (p2.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p2));
                        }
                        break;
                    case 2:
                        StartCoroutine(RemoveMatchesCoroutine3(p));
                        Create3();
                        Create();
                        List<GameObject> p3 = FOUND();
                        if (p3.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p3));
                        }
                        break;
                    case 3: // dọc
                        StartCoroutine(RemoveMatchesCoroutine4(p));
                        Create4();
                        Create();
                        List<GameObject> p4 = FOUND();
                        if (p4.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p4));
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public void Create3()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (GetGameObjectAt(i, j) == null)
                    {
                        GameObject newObject = Instantiate(exspecial[1]);
                        grid[i, j] = newObject;
                        newObject.transform.SetParent(transform);
                        newObject.GetComponent<Blocks>().seque = new Vector2Int(i, j);
                        newObject.transform.position = new Vector3(2 * j * Distance, 2 * i * Distance, 0) + positionOffset;
                        break;
                    }
                }
            }
        }
        private IEnumerator RemoveMatchesCoroutine4(List<GameObject> matches)
        {
            foreach (var tile in matches)
            {
                StartCoroutine(AnimateAndDestroyTileSimple(tile));
            }
            yield return new WaitForSeconds(2.0f); // wait for animations to complete
            List<GameObject> p = FOUND();// xoát them còn match hay không
            if (p.Count > 0)
            {
                int result = ngang_doc_honhop_giandon(p);

                switch (result)
                {
                    case 0: // giản đơn
                        StartCoroutine(RemoveMatchesCoroutine(p));
                        break;
                    case 1: // hỗn hợp
                        StartCoroutine(RemoveMatchesCoroutine2(p));
                        Create2();
                        Create();
                        List<GameObject> p2 = FOUND();
                        if (p2.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p2));
                        }
                        break;
                    case 2:
                        StartCoroutine(RemoveMatchesCoroutine3(p));
                        Create3();
                        Create();
                        List<GameObject> p3 = FOUND();
                        if (p3.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p3));
                        }
                        break;
                    case 3: // dọc
                        StartCoroutine(RemoveMatchesCoroutine4(p));
                        Create4();
                        Create();
                        List<GameObject> p4 = FOUND();
                        if (p4.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p4));
                        }
                        break;
                    default:
                        // Handle unexpected cases if necessary
                        break;
                }
            }
        }
        public void Create4()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (GetGameObjectAt(i, j) == null)
                    {
                        GameObject newObject = Instantiate(exspecial[2]);
                        grid[i, j] = newObject;
                        newObject.transform.SetParent(transform);
                        newObject.GetComponent<Blocks>().seque = new Vector2Int(i, j);
                        newObject.transform.position = new Vector3(2 * j * Distance, 2 * i * Distance, 0) + positionOffset;
                        break;
                    }
                }
            }
        }
        #endregion
        #region START
        private void Awake()
        {
            Instance = this;
        }
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
            positionOffset = new Vector3(-300, 0, 0) + transform.position - new Vector3(columns * Distance / 2.0f, rows * Distance / 2.0f, 0);
            gridLayoutGroup = GetComponent<GridLayoutGroup>();
            grid = new GameObject[rows, columns];
            _score = 0;
            Create();
            List<GameObject> p = FOUND();// xoát them còn match hay không
            if (p.Count > 0)
            {
                int result = ngang_doc_honhop_giandon(p);

                switch (result)
                {
                    case 0: // giản đơn
                        StartCoroutine(RemoveMatchesCoroutine(p));
                        break;
                    case 1: // hỗn hợp
                        StartCoroutine(RemoveMatchesCoroutine2(p));
                        Create2();
                        Create();
                        List<GameObject> p2 = FOUND();
                        if (p2.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p2));
                        }
                        break;
                    case 2:
                        // ngang
                        StartCoroutine(RemoveMatchesCoroutine3(p));
                        Create3();
                        Create();
                        List<GameObject> p3 = FOUND();
                        if (p3.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p3));
                        }
                        break;
                    case 3: // dọc
                        StartCoroutine(RemoveMatchesCoroutine4(p));
                        Create4();
                        Create();
                        List<GameObject> p4 = FOUND();
                        if (p4.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p4));
                        }
                        break;
                    default:
                        break;
                }
            }
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
        private IEnumerator RemoveMatchesCoroutine(List<GameObject> matches)
        {
            foreach (var tile in matches)
            {
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(AnimateAndDestroyTileSimple(tile));
            }
            yield return new WaitForSeconds(2.0f); // wait for animations to complete
            Create();
            List<GameObject> p = FOUND();// xoát them còn match hay không
            if (p.Count > 0)
            {
                int result = ngang_doc_honhop_giandon(p);

                switch (result)
                {
                    case 0: // giản đơn
                        StartCoroutine(RemoveMatchesCoroutine(p));
                        break;
                    case 1: // hỗn hợp
                        StartCoroutine(RemoveMatchesCoroutine2(p));
                        Create2();
                        Create();
                        List<GameObject> p2 = FOUND();
                        if (p2.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p2));
                        }
                        break;
                    case 2:
                        StartCoroutine(RemoveMatchesCoroutine3(p));
                        Create3();
                        Create();
                        List<GameObject> p3 = FOUND();
                        if (p3.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p3));
                        }
                        break;
                    case 3: // dọc
                        StartCoroutine(RemoveMatchesCoroutine4(p));
                        Create4();
                        Create();
                        List<GameObject> p4 = FOUND();
                        if (p4.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p4));
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private IEnumerator AnimateAndDestroyTileSimple(GameObject tile)
        {
            float duration = 1.5f;
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
        private List<GameObject> FOUND()
        {
            List<GameObject> found = new List<GameObject>();
            HashSet<Vector2Int> checkedPositions = new HashSet<Vector2Int>();
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    Vector2Int currentPosition = new Vector2Int(x, y);
                    if (checkedPositions.Contains(currentPosition)) continue;
                    GameObject a = GetGameObjectAt(x, y);
                    if (a == null || a.GetComponent<Tile>() == null) continue;
                    List<GameObject> rowMatchesTiles = new List<GameObject> { a };
                    for (int i = 1; x + i < rows; i++)
                    {
                        GameObject b = GetGameObjectAt(x + i, y);
                        if (b != null && b.GetComponent<Tile>() != null && b.GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                        {
                            rowMatchesTiles.Add(b);
                            checkedPositions.Add(new Vector2Int(x + i, y)); // Add to checked positions
                        }
                        else break; // Exit the loop if the numbers do not match
                    }
                    for (int i = 1; x - i >= 0; i++)
                    {
                        GameObject b = GetGameObjectAt(x - i, y);
                        if (b != null && b.GetComponent<Tile>() != null && b.GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                        {
                            rowMatchesTiles.Add(b);
                            checkedPositions.Add(new Vector2Int(x - i, y)); // Add to checked positions
                        }
                        else break; // Exit the loop if the numbers do not match
                    }
                    if (rowMatchesTiles.Count >= 3)
                    {
                        found.AddRange(rowMatchesTiles);
                        foreach (GameObject go in rowMatchesTiles)
                        {
                            Vector2Int pos = new Vector2Int((int)go.transform.position.x, (int)go.transform.position.y);
                            checkedPositions.Add(pos); // Add all matched positions to checked positions
                        }
                    }
                    List<GameObject> columnMatchesTiles = new List<GameObject> { a };
                    for (int j = 1; y + j < columns; j++)
                    {
                        GameObject b = GetGameObjectAt(x, y + j);
                        if (b != null && b.GetComponent<Tile>() != null && b.GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                        {
                            columnMatchesTiles.Add(b);
                            checkedPositions.Add(new Vector2Int(x, y + j)); // Add to checked positions
                        }
                        else break;
                    }
                    for (int j = 1; y - j >= 0; j++)
                    {
                        GameObject b = GetGameObjectAt(x, y - j);
                        if (b != null && b.GetComponent<Tile>() != null && b.GetComponent<Tile>().number == a.GetComponent<Tile>().number)
                        {
                            columnMatchesTiles.Add(b);
                            checkedPositions.Add(new Vector2Int(x, y - j)); // Add to checked positions
                        }
                        else break;
                    }
                    if (columnMatchesTiles.Count >= 3)
                    {
                        found.AddRange(columnMatchesTiles);
                        foreach (GameObject go in columnMatchesTiles)
                        {
                            Vector2Int pos = new Vector2Int((int)go.transform.position.x, (int)go.transform.position.y);
                            checkedPositions.Add(pos); // Add all matched positions to checked positions
                        }
                    }
                    checkedPositions.Add(currentPosition); // Add the current position to checked positions
                }
            }
            return found;
        }
        public int ngang_doc_honhop_giandon(List<GameObject> tiles)
        {
            List<Vector2Int> positions = new List<Vector2Int>();
            foreach (var tile in tiles)
            {
                positions.Add(tile.gameObject.GetComponent<Blocks>().seque);
            }
            int ngang = 0;
            int doc = 0;
            for (int i = 0; i < tiles.Count; i++)
            {
                for (int j = i + 1; j < tiles.Count; j++)
                {
                    int result = xetngangdoc(positions[i], positions[j]);
                    if (result == 0) // Same column
                    {
                        doc++;
                    }
                    else if (result == 1) // Same row
                    {
                        ngang++;
                    }
                }
            }
            if (ngang >= 3 && doc >= 3)
            {
                Debug.Log("Ăn hỗn hợp");
                return 1; // Mixed
            }
            else if (ngang > 3 && doc < 3)
            {
                Debug.Log("Ăn ngang ");
                return 2; // Horizontal
            }
            else if (ngang < 3 && doc > 3)
            {
                Debug.Log("Ăn dọc ");
                return 3; // Vertical
            }
            else
            {
                Debug.Log("Ăn 3 bình thường");
                return 0; // Simple
            }
        }
        public int xetngangdoc(Vector2Int a, Vector2Int b)
        {
            if (a.x == b.x && a.y != b.y) return 1; // ngang
            if (a.x != b.x && a.y == b.y) return 0; // doc
            return 2; // Neither
        }
        private List<GameObject> FOUNDCUNGOBJECT(GameObject a, int rows, int columns)
        {
            List<GameObject> found = new List<GameObject>() { a };
            Tile aTile = a.GetComponent<Tile>();
            if (aTile == null) return found; // Nếu a không có thành phần Tile, trả về danh sách chỉ chứa a

            int targetNumber = aTile.number;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    GameObject b = GetGameObjectAt(i, j);
                    if (b != null)
                    {
                        Tile bTile = b.GetComponent<Tile>();
                        if (bTile != null && bTile.number == targetNumber)
                        {
                            found.Add(b);
                        }
                    }
                }
            }
            return found;
        }
        private List<GameObject> FOUNDNGANG(int x)
        {
            List<GameObject> found = new List<GameObject>();
            HashSet<Vector2Int> checkedPositions = new HashSet<Vector2Int>();

            for (int y = 0; y < columns; y++)
            {
                Vector2Int currentPosition = new Vector2Int(x, y);
                if (checkedPositions.Contains(currentPosition)) continue;

                GameObject a = GetGameObjectAt(x, y);
                if (a == null || a.GetComponent<Tile>() == null) continue;

                List<GameObject> columnMatchesTiles = new List<GameObject> { a };
                checkedPositions.Add(currentPosition);
                for (int j = 1; y + j < columns; j++)
                {
                    GameObject b = GetGameObjectAt(x, y + j);
                    Vector2Int newPosition = new Vector2Int(x, y + j);
                    if (b != null && b.GetComponent<Tile>() != null && !checkedPositions.Contains(newPosition))
                    {
                        columnMatchesTiles.Add(b);
                        checkedPositions.Add(newPosition);
                    }
                    else break;
                }
                for (int j = 1; y - j >= 0; j++)
                {
                    GameObject b = GetGameObjectAt(x, y - j);
                    Vector2Int newPosition = new Vector2Int(x, y - j);
                    if (b != null && b.GetComponent<Tile>() != null && !checkedPositions.Contains(newPosition))
                    {
                        columnMatchesTiles.Add(b);
                        checkedPositions.Add(newPosition);
                    }
                    else break;
                }

                found.AddRange(columnMatchesTiles);
            }
            return found;
        }
        private List<GameObject> FOUNDDOC(int y)
        {
            List<GameObject> found = new List<GameObject>();
            HashSet<Vector2Int> checkedPositions = new HashSet<Vector2Int>();

            for (int x = 0; x < rows; x++)
            {
                Vector2Int currentPosition = new Vector2Int(x, y);
                if (checkedPositions.Contains(currentPosition)) continue;

                GameObject a = GetGameObjectAt(x, y);
                if (a == null || a.GetComponent<Tile>() == null) continue;

                List<GameObject> rowMatchesTiles = new List<GameObject> { a };
                checkedPositions.Add(currentPosition);
                for (int j = 1; x + j < rows; j++)
                {
                    GameObject b = GetGameObjectAt(x + j, y);
                    Vector2Int newPosition = new Vector2Int(x + j, y);
                    if (b != null && b.GetComponent<Tile>() != null && !checkedPositions.Contains(newPosition))
                    {
                        rowMatchesTiles.Add(b);
                        checkedPositions.Add(newPosition);
                    }
                    else break;
                }
                for (int j = 1; x - j >= 0; j++)
                {
                    GameObject b = GetGameObjectAt(x - j, y);
                    Vector2Int newPosition = new Vector2Int(x - j, y);
                    if (b != null && b.GetComponent<Tile>() != null && !checkedPositions.Contains(newPosition))
                    {
                        rowMatchesTiles.Add(b);
                        checkedPositions.Add(newPosition);
                    }
                    else break;
                }

                found.AddRange(rowMatchesTiles);
            }
            return found;
        }


        #endregion
        #region INIT
        public void Create()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    GameObject l = grid[i, j];
                    if (l != null)
                    {
                        grid[i, j] = l;
                        l.transform.SetParent(transform);
                        l.GetComponent<Blocks>().seque = new Vector2Int(i, j);
                        l.transform.position = new Vector3(2 * j * Distance, 2 * i * Distance, 0) + positionOffset;
                    }
                    else
                    {
                        List<GameObject> k = new List<GameObject>(list);
                        if (i > 1 && grid[i - 1, j] != null && grid[i - 2, j] != null &&
                            grid[i - 1, j].GetComponent<Tile>().number == grid[i - 2, j].GetComponent<Tile>().number)
                        {
                            k.Remove(grid[i - 1, j]);
                        }
                        if (j > 1 && grid[i, j - 1] != null && grid[i, j - 2] != null &&
                            grid[i, j - 1].GetComponent<Tile>().number == grid[i, j - 2].GetComponent<Tile>().number)
                        {
                            k.Remove(grid[i, j - 1]);
                        }
                        if (k.Count == 0)
                        {
                            k = new List<GameObject>(list); // Reset to full list if all objects are removed
                        }
                        l = Instantiate(k[Random.Range(0, k.Count)]);
                        grid[i, j] = l;
                        l.transform.SetParent(transform);
                        l.GetComponent<Blocks>().seque = new Vector2Int(i, j);
                        l.transform.position = new Vector3(2 * j * Distance, 2 * i * Distance, 0) + positionOffset;
                    }
                }
            }
        }
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
            List<GameObject> matches = FOUND();
            grid[pos1.x, pos1.y] = tile1;
            grid[pos2.x, pos2.y] = tile2;
            SwapTilePositions(tile1, tile2);
            if (matches != null && matches.Count > 0)
            {
                grid[pos1.x, pos1.y] = tile2;
                grid[pos2.x, pos2.y] = tile1;
                SwapTilePositions(tile1, tile2);
                int result = ngang_doc_honhop_giandon(matches);
                switch (result)
                {
                    case 0: // giản đơn
                        StartCoroutine(RemoveMatchesCoroutine(matches));
                        break;
                    case 1: // hỗn hợp
                        StartCoroutine(RemoveMatchesCoroutine2(matches));
                        Create2();
                        Create();
                        List<GameObject> p2 = FOUND();
                        if (p2.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p2));
                        }
                        break;
                    case 2:
                        StartCoroutine(RemoveMatchesCoroutine3(matches));
                        Create3();
                        Create();
                        List<GameObject> p3 = FOUND();
                        if (p3.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p3));
                        }
                        break;
                    case 3: // dọc
                        StartCoroutine(RemoveMatchesCoroutine4(matches));
                        Create4();
                        Create();
                        List<GameObject> p4 = FOUND();
                        if (p4.Count > 0)
                        {
                            StartCoroutine(RemoveMatchesCoroutine(p4));
                        }
                        break;
                    default:
                        break;
                }
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
    }
}

#region FOUND CU
//  Score++;
//private IEnumerator RemoveMatchesCoroutine(HashSet<GameObject> matches)
//{
//    foreach (var tile in matches)
//    {
//        StartCoroutine(AnimateAndDestroyTileSimple(tile));
//    }
//    yield return new WaitForSeconds(3.0f); // wait for animations to complete
//    Create();
//    HashSet<GameObject> newMatches = FOUND();
//    if (newMatches.Count > 0)
//        StartCoroutine(RemoveMatchesCoroutine(newMatches));
//}
//private HashSet<GameObject> FOUND()
//{
//    HashSet<GameObject> found = new HashSet<GameObject>();

//    for (int x = 0; x < rows; x++)
//    {
//        for (int y = 0; y < columns; y++)
//        {
//            GameObject a = GetGameObjectAt(x, y);
//            if (a == null) continue;
//            List<GameObject> rowMatchesTiles = new List<GameObject> { a };
//            for (int i = 1; x + i < rows; i++)
//            {
//                if (GetGameObjectAt(x + i, y)?.GetComponent<Tile>().number == a.GetComponent<Tile>().number)
//                {
//                    rowMatchesTiles.Add(GetGameObjectAt(x + i, y));
//                }
//                else break;
//            }
//            for (int i = 1; x - i >= 0; i++)
//            {
//                if (GetGameObjectAt(x - i, y)?.GetComponent<Tile>().number == a.GetComponent<Tile>().number)
//                {
//                    rowMatchesTiles.Add(GetGameObjectAt(x - i, y));
//                }
//                else break;
//            }
//            if (rowMatchesTiles.Count >= 3)
//            {
//                found.UnionWith(rowMatchesTiles);
//            }
//            List<GameObject> columnMatchesTiles = new List<GameObject> { a };
//            for (int j = 1; y + j < columns; j++)
//            {
//                if (GetGameObjectAt(x, y + j)?.GetComponent<Tile>().number == a.GetComponent<Tile>().number)
//                {
//                    columnMatchesTiles.Add(GetGameObjectAt(x, y + j));
//                }
//                else break;
//            }
//            for (int j = 1; y - j >= 0; j++)
//            {
//                if (GetGameObjectAt(x, y - j)?.GetComponent<Tile>().number == a.GetComponent<Tile>().number)
//                {
//                    columnMatchesTiles.Add(GetGameObjectAt(x, y - j));
//                }
//                else break;
//            }
//            if (columnMatchesTiles.Count >= 3)
//            {
//                found.UnionWith(columnMatchesTiles);
//            }
//        }
//    }
//    return found;
//}
#endregion
#region Creat Old
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

/*
    [SerializeField] private xet a;
 * 
 * private enum xet
    {
        khoitao,
        lon,
        bang,
        nho
    }
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
//public void SwapTiles(Vector2Int pos1, Vector2Int pos2)
//{
//    GameObject tile1 = GetGameObjectAt(pos1.x, pos1.y);
//    GameObject tile2 = GetGameObjectAt(pos2.x, pos2.y);
//    if (tile1 == null || tile2 == null) return;
//    grid[pos1.x, pos1.y] = tile2;
//    grid[pos2.x, pos2.y] = tile1;
//    SwapTilePositions(tile1, tile2);
//    HashSet<GameObject> matches = FOUND();
//    if (matches.Count > 0)
//        StartCoroutine(RemoveMatchesCoroutine(matches));
//    else
//    {
//        grid[pos1.x, pos1.y] = tile1;
//        grid[pos2.x, pos2.y] = tile2;
//        SwapTilePositions(tile1, tile2);
//    }
//}
#endregion