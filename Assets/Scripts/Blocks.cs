using Grid;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tiles
{
    public class Blocks : MonoBehaviour, IPointerDownHandler
    {
        public Vector2Int seque;
        public static Blocks Selected;
        private Vector3 originalScale;

        private void Start()
        {
            originalScale = transform.localScale;
        }

        public void Select()
        {
            transform.localScale = Vector3.one * 1.3f;
            Debug.Log($"Vị trí chọn object: {seque}, tên của nó : {this.name}");

        }

        public void Unselect()
        {
            transform.localScale = originalScale; // Set it back to original size
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Input.touchCount <= 1)
            {
                if (Selected != null)
                {
                    HandleSelection();
                }
                else
                {
                    SelectBlock();
                }
            }
        }

        private void HandleSelection()
        {
            // Unselect the previously selected block
            Selected.Unselect();

            if (Selected != this)
            {
                // Perform actions based on the properties of the selected block
                if (Vector2Int.Distance(Selected.seque, seque) == 1)
                {
                    Debug.Log($"Đổi 2 vị trí là : {Selected.seque} .Tên của nó là : {Selected.name}  Và {seque} Tên của nó là : {this.name}");
                    GridManager.Instance.SwapTiles(seque, Selected.seque);
                }
                else
                {
                    Debug.Log("Không thể thay đổi 2 game object này");
                }
            }
            else
            {
                Debug.Log("Không thể thay đổi 2 game object này");
            }

            // Select the current block
            SelectBlock();
        }

        private void HandleBlockInteraction()
        {
            Tile selectedTile = Selected.gameObject.GetComponent<Tile>();
            Tile currentTile = this.gameObject.GetComponent<Tile>();
        }

        private void DestroyBlocks()
        {
            Destroy(Selected.gameObject);
            Destroy(this.gameObject);
            Selected = null;
        }

        private void SelectBlock()
        {
            Selected = this;
            Select();
        }
    }
}
