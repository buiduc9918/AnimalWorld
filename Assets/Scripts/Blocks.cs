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

        private void Awake()
        {
            Selected = this;
        }

        public void Select()
        {
            transform.localScale = Vector3.one * 1.3f;
        }
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

                    GridManager.Instance.SwapTiles(Selected.seque, seque);
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
