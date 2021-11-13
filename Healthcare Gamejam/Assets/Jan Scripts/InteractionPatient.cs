using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPatient : MonoBehaviour
{
    [SerializeField] GameObject InteractionUI;
    [SerializeField] TextMeshProUGUI InteractionUI_Name;
    [SerializeField] TextMeshProUGUI InteractionUI_Text;

    /*private void Start()
    {
        InteractionUI = GameObject.Find("Interaction_UI");
        InteractionUI_Name = GameObject.Find("Name_UI").GetComponent<TextMeshProUGUI>();
    }*/



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Patient"))
        {
            other.transform.parent.gameObject.GetComponent<MoveRandomly>().StopAndTalk(gameObject.transform.position);

            //Debug.Log("Needs recognized");
            InteractionUI.SetActive(true);

            //ask for name and set them
            GameObject parent = other.transform.parent.gameObject;
            string parentName = parent.name;
            InteractionUI_Name.text = parentName;

            //ask for Need and set them
            string currentNeed = other.transform.parent.gameObject.GetComponent<NeedsPatient>().currentNeed;
            InteractionUI_Text.text = currentNeed;


            if (other.transform.parent.gameObject.GetComponent<NeedsPatient>().compareNeeds("Food"))
            {

            }
            other.transform.parent.gameObject.GetComponent<NeedsPatient>().clearNeeds("Food"); //gib relieve an patient
            
        }
    }

    

    private void OnTriggerExit(Collider other)
    {
        InteractionUI.SetActive(false); //UI ausschalten wenn man sich von Patienten entfernt
    }
}
