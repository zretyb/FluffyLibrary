using FluffyLibrary.CameraManager;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FluffyApp.Core
{
    public class CameraManager : BaseCameraManager
    {
        private Camera _uiCamera;
        private UniversalAdditionalCameraData _uiCameraData;
        
        public Camera UiCamera => _uiCamera;

        public void Init()
        {
            CreateUiCamera();
        }
        
        private void CreateUiCamera()
        {
            var cameraGameObject = new GameObject("UICamera",
                typeof(Camera));
            cameraGameObject.transform.parent = transform;
            
            _uiCamera = cameraGameObject.GetComponent<Camera>();
            _uiCamera.cullingMask = LayerMask.GetMask("UI");
            _uiCamera.clearFlags = CameraClearFlags.SolidColor;
            _uiCamera.backgroundColor = Color.white;
            _uiCamera.allowDynamicResolution = true;
            _uiCamera.transform.localPosition = new Vector3(0f, -10000f, 0f);

            cameraGameObject.AddComponent<AudioListener>();
            
            _uiCameraData = _uiCamera.GetUniversalAdditionalCameraData();
            _uiCameraData.renderShadows = false;
            
            AddCamera(_uiCamera);
        }
    }
}
