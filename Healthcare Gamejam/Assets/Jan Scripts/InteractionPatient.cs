using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPatient : MonoBehaviour
{
    [SerializeField] GameObject InteractionUI;
    [SerializeField] TextMeshProUGUI InteractionUI_Name;

    /*private void Start()
    {
        InteractionUI = GameObject.Find("Interaction_UI");
        InteractionUI_Name = GameObject.Find("Name_UI").GetComponent<TextMeshProUGUI>();
    }*/


    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Patient"))
        {
            //Debug.Log("Needs recognized");
            InteractionUI.SetActive(true);

            //ask for name and set them
            GameObject parent = other.transform.parent.gameObject;
            string parentName = parent.name;
            InteractionUI_Name.text = parentName;

            other.transform.parent.gameObject.GetComponent<NeedsPatient>().clearNeeds("Food");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InteractionUI.SetActive(false); //UI ausschalten wenn man sich von Patienten entfernt
    }
}
