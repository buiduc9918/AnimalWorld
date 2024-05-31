using Grid;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tiles
{
    public class Blocks : MonoBehaviour, IPointerDownHandler
    {
        public Vector2Int seque;
        private Vector3 local;

        public static Blocks Selected;
        private Tile tile;
        private void Awake()
        {
            Selected = this;

        }
        private void Start()
        {
            tile = GetComponent<Tile>();
        }
        public void Select()
        {
            transform.localScale = Vector3.one * 1.3f;
            MoveMouse();
            if (tile.number == 1)
            {
                GridManager.instance.destroy44(seque);
            }
            if (tile.number == 3)
            {
                GridManager.instance.destroy55(seque);
            }
        }

        public void Unselect()
        {
            local = transform.position;
            transform.localScale = Vector3.one * 1.5f;
            MoveMouse2();
        }

        private void SaveClick()
        {
            Selected = this;
            Select();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (Input.GetMouseButton(0))
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
        IEnumerator MoveMouse()
        {
            float duration = 1.5f;
            float elapsedTime = 0f;
            Vector3 initialPosition = transform.position;
            Vector3 targetPosition = Input.mousePosition;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            // Ensure the object reaches the exact target position
            transform.position = targetPosition;
        }
        IEnumerator MoveMouse2()
        {
            float duration = 1.5f;
            float elapsedTime = 0f;
            Vector3 initialPosition = transform.position;
            Vector3 targetPosition = local;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            // Ensure the object reaches the exact target position
            transform.position = targetPosition;
        }
    }
}
