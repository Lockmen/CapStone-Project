using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARPlacementManager : MonoBehaviour
{
    ARRaycastManager m_ARRaycastManager;
    static List<ARRaycastHit> raycast_Hits = new List<ARRaycastHit>();

    public Camera aRCamera;
    public GameObject battleArenaGameobject;


    private void Awake()
    {
        m_ARRaycastManager = GetComponent<ARRaycastManager>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = aRCamera.ScreenPointToRay(centerOfScreen);

        if(m_ARRaycastManager.Raycast(ray, raycast_Hits, TrackableType.PlaneWithinPolygon))
        {

            // Intersection!
            Pose hitPose = raycast_Hits[0].pose;

            Vector3 positionToBePlaced = hitPose.position;  // 게임오브젝트를 터치 한 곳에 둔다.

            battleArenaGameobject.transform.position = positionToBePlaced;


        }
    }
}
