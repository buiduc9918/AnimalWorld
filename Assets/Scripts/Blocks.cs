using Grid;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tiles
{
    public class Blocks : MonoBehaviour, IPointerClickHandler
    {
        public Vector2Int seque;
        public static Blocks Selected;
        public void Awake()
        {
            if (Selected == null)
            {
                Selected = this;
            }
        }
        public void Select()
        {
            transform.localScale = Vector3.one * 1.3f;
        }

        public void Unselect()
        {
            transform.localScale = Vector3.one * 1.5f;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //{
            //    if (eventData.pointerClick.gameObject.GetComponent<Tile>().number == 1)
            //    {
            //        GridManager.Instance.cheat1(eventData.pointerClick.gameObject.GetComponent<Blocks>().seque.x);
            //        Destroy(gameObject);
            //        GridManager.Instance.Create();
            //    }
            //    if (eventData.pointerClick.gameObject.GetComponent<Tile>().number == 2)
            //    {
            //        GridManager.Instance.cheat2(eventData.pointerClick.gameObject.GetComponent<Blocks>().seque.y);
            //        Destroy(gameObject);
            //        GridManager.Instance.Create();

            //    }
            if (Selected != null)
            {
                Selected.Unselect();
                if (Selected != this)
                {
                    if (Vector2Int.Distance(Selected.seque, seque) == 1)
                    {
                        GridManager.Instance.SwapTiles(seque, Selected.seque);
                    }
                    {
                        Selected = this;
                        Select();
                    }
                }
                if (eventData.pointerClick.gameObject.GetComponent<Tile>().number == 1)
                {
                    GridManager.Instance.cheat1(eventData.pointerClick.gameObject.GetComponent<Blocks>().seque.x);
                    Destroy(gameObject);
                    GridManager.Instance.Create();
                }
                if (eventData.pointerClick.gameObject.GetComponent<Tile>().number == 2)
                {
                    GridManager.Instance.cheat2(eventData.pointerClick.gameObject.GetComponent<Blocks>().seque.y);
                    Destroy(gameObject);
                    GridManager.Instance.Create();

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
