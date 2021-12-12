using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class FarmManager
{    
    public static event Action<string> PlayerNeedToSelectAPlant;   
    
    private static Dictionary<string, PlantStateTD> _PlantInScene = new Dictionary<string, PlantStateTD>();

    private static GameObject plantInformationUI;

    private static PlayerInventory inventory;

    public static void AttachUIComponent(GameObject UIcomponent)
    {
        plantInformationUI = UIcomponent;         
    }    

    public static void PlayerNeedsToSelectPlant(string plantID)
    {
        if (plantID != null)
        {
            PlayerNeedToSelectAPlant(plantID);
        }
        else { Debug.LogFormat("FARMMANAGER.PLAYERNEEDSTOSELECTPLANT(): Something is null! PlantID: {0}", plantID); }
    }

    public static void PlantIsSelected(string plantID, Seed selectedPlant)
    {     
        if (plantID != null && selectedPlant != null)
        {           
            _PlantInScene[plantID].PlantedSeed = selectedPlant;

            //Execute StateMachine Tick so Behaviour is Switched to Growing. Needs to be done here because PlayerNeedToSelectAPlant is an Event. 
            _PlantInScene[plantID].ExecuteBehaviourOnClick();
        }
        else { Debug.LogFormat("FARMMANAGER.SELECTEDPLANT(): Something is null! PlantID: {0}, SelectedPlant: {1}.", plantID, selectedPlant); }
    }

    public static void AddMeToManager(PlantStateTD plantToAdd)
    {
        if (plantToAdd.GetComponent<PlantStateTD>() && plantToAdd.GetPlantId != null)
        {
            _PlantInScene.Add(plantToAdd.GetPlantId, plantToAdd);
            Debug.Log("ADDED PLANT: " + plantToAdd.GetPlantId);
        }
        else { Debug.LogFormat("FARMMANAGER.ADDMETOMANAGER(): Tried to add {0} to PlantsInSceneArray while this is not a plant", plantToAdd.name); }
    }  

    public static void ThisPlantIsTouched(string plantID)
    {
        if (plantID != null && _PlantInScene.ContainsKey(plantID))
        {           
            _PlantInScene[plantID].ExecuteBehaviourOnClick();
        }
        else { Debug.LogFormat("FARMMANAGER.THISPLANTISTOUCHED: PlantID is null or PlantID isn't found in Dictionary! PlantID: {0}", plantID); }
    }      
    
    public static void ShowPlantData(string plantID)
    {
        if (!plantInformationUI.activeSelf)
        {
            TogglePlantInformation();
        }

        if (plantID != null && plantInformationUI != null)
        {            
            SetPlantData(_PlantInScene[plantID]);
        }
        else { Debug.LogFormat("FARMMANAGER.SHOWPLANTDATA: Something is null! PlantID: {0}, UIComponent: {1}", plantID, plantInformationUI.name);    }
    }   

    public static void TogglePlantInformation()
    {
        plantInformationUI.SetActive(!plantInformationUI.activeSelf);
    }

    public static bool AddProduceToInventory(Produce produce)
    {
        if(inventory == null)
        {
            inventory = PlayerInventory.GetPlayerInventory();
        }

        bool succesfullyAdded = inventory.AddToFirstEmptySlot(produce);

        return succesfullyAdded;
    }

    public static void RemoveItemFromInventory(Item item)
    {
        if (inventory == null)
        {
            inventory = PlayerInventory.GetPlayerInventory();
        }       
    }

    private static void SetPlantData(PlantStateTD currentPlant)
    {
        PlantInformationUI informationUI =  plantInformationUI.GetComponent<PlantInformationUI>();       

        informationUI.SetTimerScript(currentPlant.Timer);
        informationUI.SetPlantName(currentPlant.PlantedSeed.TypeOfProduce.name); 

        plantInformationUI.transform.position = new Vector3(currentPlant.transform.position.x,
            currentPlant.transform.position.y + 175, currentPlant.transform.position.z);
    }

}
