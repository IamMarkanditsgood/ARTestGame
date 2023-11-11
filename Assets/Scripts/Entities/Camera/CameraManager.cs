using System;
using System.Collections.Generic;
using Entities.Numbers.Data;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static UnityEngine.Screen;

namespace Entities.Camera
{
    public class CameraManager : MonoBehaviour
    {
        [Header("Put your planeMarker here")]
        [SerializeField] private GameObject _planeMarkerPrefab;
        [SerializeField] private GameObject _scene;
        [SerializeField] private ARRaycastManager _arRaycastManagerScript;

        private Vector2 _touchPosition;
        private bool _isGameStarted;
        
        public event Action OnPlay;
        

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            ShowMarker();
        }

        private void Initialize()
        {
            _planeMarkerPrefab.SetActive(false);
        }

        private  List<ARRaycastHit> ShootRayCastToPlanes()
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            _arRaycastManagerScript.Raycast(new Vector2(width / 2, height / 2), hits, TrackableType.Planes);
            return hits;
        }
        
        private void ShowMarker()
        {
            if (!_isGameStarted)
            {
                List<ARRaycastHit> hits = ShootRayCastToPlanes();
                if (hits.Count > 0)
                {
                    _planeMarkerPrefab.transform.position = hits[0].pose.position;
                    _planeMarkerPrefab.SetActive(true);
                }

                CheckTouch();
            }
        }

        private void CheckTouch()
        {
            List<ARRaycastHit> hits = ShootRayCastToPlanes();
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                OnPlay?.Invoke();
                _scene.transform.position = hits[0].pose.position;
                _scene.transform.LookAt(_arRaycastManagerScript.transform);
                _scene.SetActive(true);
                _isGameStarted = true;
            }
        }
    }
}
