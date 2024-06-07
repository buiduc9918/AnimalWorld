using Grid;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tiles
{
    public class Blocks : MonoBehaviour, IPointerDownHandler
    {
        public Vector2Int seque;
        public static Blocks Selected;
        Vector3 vector3;
        private void Start() => vector3 = transform.localScale;
        public void Select()
        {
            transform.localScale = Vector3.one * 1.3f;
            Debug.Log($"Vị trí chọn object: {seque}, tên của nó : {this.name}");
        }
        public void Unselect() => transform.localScale = vector3; // Set it back to original size
        public void OnPointerDown(PointerEventData eventData)
        {
            if (Input.touchCount > 0 && Input.touchCount <= 1)
            {
                Touch touch = Input.GetTouch(0);
                if (Selected != null)
                {
                    Selected.Unselect();
                    if (Selected != this)
                    {
                        if (Vector2Int.Distance(Selected.seque, seque) == 1)
                        {
                            Debug.Log(" Đổi 2 vị trí là : " + Selected.seque + " .Tên của nó là : " + Selected.name + "  Và " + seque + " Tên của nó là : " + this.name);
                            GridManager.Instance.SwapTiles(seque, Selected.seque);
                        }
                        else
                        {
                            Selected = this;
                            Select();
                        }
                    }
                    else
                    {
                        Debug.Log("Không thể thay đổi 2 game object này");
                        Selected = this;
                        Select();
                    }
                }
                else
                {
                    Selected = this;
                    Select();
                }
            }
        }
    }
}
