using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverControl : MonoBehaviour
{
    Vector2 mousePosition;
    //RaycastHit[] raycastHits;

    RaycastHit2D[] raycastAllHit2D;

    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MouseOverUpdate();
    }

    void MouseOverUpdate()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.z -= mainCamera.transform.position.z;

        //raycastHits = Physics.RaycastAll(ray);


        raycastAllHit2D = Physics2D.RaycastAll(ray.origin, ray.direction);
        //Debug.DrawRay(mousePosition, transform.forward, Color.red, 0.3f);
        //Debug.Log(mousePosition);
        //Debug.Log(raycastHits.Length);
        //Debug.Log(raycastAllHit2D.Length);
        


        foreach (RaycastHit2D raycastHit2D in raycastAllHit2D)
        {
            //Debug.Log(raycastHit2D.transform.tag);
            SendGameObjectToTarget(raycastHit2D.transform.gameObject);
        }

    }

    void SendGameObjectToTarget(GameObject targetGameObject)
    {

        ControllerManager.ControllerManagerInstance.ShowPanelInfoFromBoard(targetGameObject);

    }
}
