using Grid;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tiles
{
    public class Blocks : MonoBehaviour, IPointerClickHandler
    {
        public Vector2Int seque;
        public static Blocks Selected;


        public void Select()
        {
            transform.localScale = Vector3.one * 1.3f;
        }

        public void Unselect()
        {
            transform.localScale = Vector3.one * 1.5f; // Set it back to original size
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Selected != null)
            {
                Selected.Unselect();
                if (Selected != this)
                {
                    if (Vector2Int.Distance(Selected.seque, seque) == 1)
                    {
                        Debug.Log("Swapping tiles: " + Selected.seque + " with " + seque);
                        GridManager.Instance.SwapTiles(seque, Selected.seque);
                    }
                    Selected = this;
                    Select();
                }
                else
                {
                    Debug.Log("Unselecting previously selected tile.");
                    // If clicking the same tile, unselect it
                    Selected = null;
                }
            }
            else
            {
                Selected = this;
                Select();
            }

            GridManager.Instance.Create();
        }
    }
}
