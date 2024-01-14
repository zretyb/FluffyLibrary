using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FluffyLibrary.CameraManager
{
    public class BaseCameraManager : BaseManager
    {
        private readonly List<Camera> _cameras = new();

        public Camera BaseCamera { get; private set; }

        public void AddCamera(Camera cam)
        {
            if (_cameras.Contains(cam)) return;

            _cameras.Add(cam);

            UpdateCameraStack();
        }

        public void InsertCamera(int index, Camera cam, bool updateCameraStack = true)
        {
            if (_cameras.Contains(cam)) return;

            _cameras.Insert(index, cam);

            if (updateCameraStack) UpdateCameraStack();
        }

        public void RemoveCamera(Camera cam, bool updateCameraStack = true)
        {
            if (_cameras.Count == 0)
            {
                return;
            }

            if (!_cameras.Contains(cam)) return;
            
            if (BaseCamera != cam)
            {
                RemoveFromCameraStack(BaseCamera, cam);
            }

            SetAsOverlayCamera(cam);

            _cameras.Remove(cam);

            if (updateCameraStack) UpdateCameraStack();
        }

        private void UpdateCameraStack()
        {
            if (_cameras.Count == 0) return;

            var newBaseCamera = _cameras.First();

            if (BaseCamera == newBaseCamera) return;

            foreach (var cam in _cameras)
                if (cam == newBaseCamera)
                {
                    SetAsBaseCamera(cam);
                }
                else
                {
                    SetAsOverlayCamera(cam);
                    AddToCameraStack(newBaseCamera, cam);
                }
        }

        private void SetAsBaseCamera(Camera value)
        {
            var camData = value.GetUniversalAdditionalCameraData();
            camData.renderType = CameraRenderType.Base;
            camData.cameraStack.Clear();

            BaseCamera = value;
        }

        private static void SetAsOverlayCamera(Camera value)
        {
            var camData = value.GetUniversalAdditionalCameraData();
            camData.renderType = CameraRenderType.Overlay;
        }

        private static void AddToCameraStack(Camera baseCamera, Camera overlayCamera)
        {
            var camData = baseCamera.GetUniversalAdditionalCameraData();
            camData.cameraStack.Add(overlayCamera);
        }

        private static void RemoveFromCameraStack(Camera baseCamera, Camera overlayCamera)
        {
            var camData = baseCamera.GetUniversalAdditionalCameraData();
            camData.cameraStack.Remove(overlayCamera);
        }
    }
}