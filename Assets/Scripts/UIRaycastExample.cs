using System.Collections.Generic;
using Tiles;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Raycaster
{
    public class UIRaycastExample : MonoBehaviour
    {
        [SerializeField] private GameObject startUIObject;
        public Canvas canvas;
        [SerializeField] private GraphicRaycaster graphicRaycaster;
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private PointerEventData pointerEventData;

        void Start()
        {
            graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
            eventSystem = EventSystem.current;
        }

        GameObject FindUIObjectAlongTouchDirection(GameObject startObject, Vector2 touchPosition)
        {
            RectTransform rectTransform = startObject.GetComponent<RectTransform>();
            Vector2 startScreenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, rectTransform.position);
            Vector2 direction = (touchPosition - startScreenPoint).normalized;

            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = startScreenPoint;

            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, results);

            results.RemoveAll(r => r.gameObject == startObject);

            foreach (RaycastResult result in results)
            {
                RectTransform resultRectTransform = result.gameObject.GetComponent<RectTransform>();
                Vector2 resultScreenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, resultRectTransform.position);
                Vector2 toResult = resultScreenPoint - startScreenPoint;

                if (Vector2.Dot(direction, toResult.normalized) > 0.9f)
                {
                    if (result.gameObject.GetComponent<Blocks>() != null)
                        return result.gameObject;
                }
            }

            return null;
        }

        GameObject FindUIObjectAlongMouseDirection(GameObject startObject)
        {
            RectTransform rectTransform = startObject.GetComponent<RectTransform>();
            Vector2 startScreenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, rectTransform.position);
            Vector2 mouseScreenPoint = Input.mousePosition;
            Vector2 direction = (mouseScreenPoint - startScreenPoint).normalized;

            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = startScreenPoint;

            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, results);

            results.RemoveAll(r => r.gameObject == startObject);

            foreach (RaycastResult result in results)
            {
                Vector2 resultScreenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, result.gameObject.GetComponent<RectTransform>().position);
                Vector2 toResult = resultScreenPoint - startScreenPoint;

                if (Vector2.Dot(direction, toResult.normalized) > 0.9f)
                {
                    return result.gameObject;
                }
            }

            return null;
        }

        public void HighlightTile(GameObject tile)
        {
            Image img = tile.GetComponent<Image>();
            if (img != null)
            {
                img.color = Color.yellow;
            }
        }
    }
}
