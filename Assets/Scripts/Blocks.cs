using Grid;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tiles
{
    public class Blocks : MonoBehaviour, IPointerDownHandler
    {
        public Vector2Int seque;
        private Vector3 localPosition;

        public static Blocks Selected { get; private set; }
        private Tile tile;

        private void Awake()
        {
            tile = GetComponent<Tile>();
        }

        public void Select()
        {
            transform.localScale = Vector3.one * 1.3f;
        }

        //private void GoAhead()
        //{
        //    int rows = GridManager.instance.rows;
        //    int cols = GridManager.instance.columns;

        //    int newRow = (seque.y == 0) ? seque.x - 1 : seque.x;
        //    int newCol = (seque.y == 0) ? cols - 1 : seque.y - 1;

        //    if (GridManager.instance.GetGameObjectAt(newRow, newCol) == null)
        //    {
        //        MoveToNewPosition(newRow, newCol);
        //    }

        //    int existingObjectsCount = CountExistingObjects(newRow, newCol, rows, cols);
        //    if (existingObjectsCount < 0)
        //    {
        //        GridManager.instance.Creat(existingObjectsCount < 0);
        //    }
        //}

        //private void MoveToNewPosition(int newRow, int newCol)
        //{
        //    GameObject go = gameObject;
        //    GridManager.instance.grid[newRow, newCol] = go;
        //    seque = new Vector2Int(newRow, newCol);
        //    go.transform.position = GridManager.instance.pointTiles(newRow, newCol);
        //}

        //private int CountExistingObjects(int startRow, int startCol, int rows, int cols)
        //{
        //    int count = 0;
        //    for (int i = 0; i < startRow; i++)
        //    {
        //        for (int j = 0; j < startCol; j++)
        //        {
        //            if (GridManager.instance.grid[i, j] == null)
        //            {
        //                MoveToNewPosition(i, j);
        //            }
        //        }
        //    }
        //    for (int i = startRow; i < rows; i++)
        //    {
        //        for (int j = startCol; j < cols; j++)
        //        {
        //            if (GridManager.instance.grid[i, j] != null)
        //            {
        //                count++;
        //            }
        //        }
        //    }
        //    return count;
        //}

        public void Unselect()
        {
            transform.localScale = Vector3.one * 1.5f;
        }

        private void SaveClick()
        {
            Selected = this;
            Select();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                HandleLeftClick();
            }
        }

        private void HandleLeftClick()
        {
            if (Selected != null)
            {
                if (Selected == this)
                    return;

                Selected.Unselect();

                if (Vector2Int.Distance(Selected.seque, seque) == 1)
                {
                    GridManager.instance.SwapTiles(Selected.seque, seque);
                    Selected = this;
                    Select();
                }
                else
                {
                    SaveClick();
                }
            }
            else
            {
                SaveClick();
            }
        }
    }
}

//if (index < transform.childCount && transform.GetChild(index) != null)
//    continue;
//else
//{
//    List<GameObject> k = new List<GameObject>(GridManager.instance.list);
//    if (i > 1 && GridManager.instance.grid[i - 1, j] != null && GridManager.instance.grid[i - 2, j] != null && GridManager.instance.grid[i - 1, j].GetComponent<Tile>().number == GridManager.instance.grid[i - 2, j].GetComponent<Tile>().number)
//        k.Remove(GridManager.instance.grid[i - 1, j]);
//    if (j > 1 && GridManager.instance.grid[i, j - 1] != null && GridManager.instance.grid[i, j - 2] != null && GridManager.instance.grid[i, j - 1].GetComponent<Tile>().number == GridManager.instance.grid[i, j - 2].GetComponent<Tile>().number)
//        k.Remove(GridManager.instance.grid[i, j - 1]);
//    if (k.Count == 0)
//        k = new List<GameObject>(GridManager.instance.list); // If k is empty, reset to the full list to avoid errors
//    GameObject newObject = Instantiate(k[Random.Range(0, k.Count)]);
//    GridManager.instance.grid[i, j] = newObject;
//    newObject.transform.SetParent(transform);
//    newObject.GetComponent<Blocks>().seque = new Vector2Int(i, j);
//    newObject.transform.parent = transform;
//    StartCoroutine(MoveMouse(GridManager.instance.pointTiles(i, j)));
//    newObject.transform.position = GridManager.instance.pointTiles(i, j);
//}