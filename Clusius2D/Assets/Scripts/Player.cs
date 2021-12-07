using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{  
    [SerializeField] Camera _Camera;

    void Update()
    {
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.touches[0];
            Vector3 pointClickedOnScreen = touch.position;
            RaycastHit hit;
            Ray ray = _Camera.ScreenPointToRay(pointClickedOnScreen);

            if (EventSystem.current.IsPointerOverGameObject(0))
            {              
                return;
            }

            if (Physics.Raycast(ray, out hit) && hit.collider.GetComponent<PlantStateMachine>())
            {
                if (touch.phase == TouchPhase.Began)
                {
                    FarmManager.ThisPlantIsTouched(hit.collider.GetComponent<PlantStateMachine>().GetID);
                }
            }
            else { /*FarmManager.TogglePlantInformation();*/ }            
        }      
    }
}
