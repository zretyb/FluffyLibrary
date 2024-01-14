using FluffyApp.Core;
using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace FluffyApp.Util
{
    public class RectTransformUtil
    {
        public static Vector2 GetRectTransformMousePosition(RectTransform rect, Vector2 offset = new Vector2())
        {
            var touchPosition = Touch.activeTouches.Count > 0 ? Touch.activeTouches[0].screenPosition : Vector2.zero;
            touchPosition += offset;
            var uiCamera = FluffyApp.GetManager<CameraManager>().UiCamera;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, touchPosition, uiCamera, out var localPoint);
            return localPoint;
        }

        public static void SetRectTransformPositionToMousePosition(RectTransform target, RectTransform holder,
            Vector2 offset = new Vector2())
        {
            var localPosition = GetRectTransformMousePosition(holder, offset);
            target.localPosition = localPosition;
        }
    }
}
