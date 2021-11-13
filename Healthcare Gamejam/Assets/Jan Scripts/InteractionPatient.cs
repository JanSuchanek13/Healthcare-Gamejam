using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPatient : MonoBehaviour
{
    [SerializeField] GameObject InteractionUI;
    [SerializeField] TextMeshProUGUI InteractionUI_Name;
    [SerializeField] TextMeshProUGUI InteractionUI_Text;

    GameObject Gamemaster;
    

    string currentRelieve;
    bool patientNearby = false;
    

    private void Start()
    {
        Gamemaster = GameObject.Find("GAME_MASTER");
        
        
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Patient") && Gamemaster.GetComponent<GameMaster>().isBeingTalkedTo == false) //interaction with patient
        {

            if (other.transform.parent.gameObject.GetComponent<NeedsPatient>().taskAnnounced)
            {
                // determine GO of the patient who triggered the interaction
                GameObject otherChar = other.transform.parent.gameObject;
                // hand over GO of the patient to GameMaster
                Gamemaster.GetComponent<GameMaster>().conversationPartner = otherChar;
                //Debug.Log(Gamemaster.GetComponent<GameMaster>().conversationPartner + " was handed over");

                patientNearby = true;
                other.transform.parent.gameObject.GetComponent<MoveRandomly>().StopAndTalk(gameObject.transform.position);
                gameObject.GetComponent<CharacterController>().StopPlayerToTalk(otherChar.transform.position);

                //Debug.Log("Needs recognized");
                InteractionUI.SetActive(true);

                //ask for picture and set it
                

                //ask for name and set it
                GameObject parent = other.transform.parent.gameObject;
                string parentName = parent.name;
                InteractionUI_Name.text = parentName;

                //ask for Need and set it
                string currentNeed = other.transform.parent.gameObject.GetComponent<NeedsPatient>().currentNeed;
                InteractionUI_Text.text = currentNeed;


                if (other.transform.parent.gameObject.GetComponent<NeedsPatient>().compareNeeds(currentRelieve))    // ask if the Player has the right relieve for the Need
                {
                    other.transform.parent.gameObject.GetComponent<NeedsPatient>().clearNeeds(currentRelieve); //gib relieve an patient
                    currentRelieve = null;
                }
            }
            else
            {
                InteractionUI.SetActive(true);
                InteractionUI_Text.text = other.transform.parent.gameObject.GetComponent<NeedsPatient>().mainTask;
                other.transform.parent.gameObject.GetComponent<NeedsPatient>().taskAnnounced = true;
            }

        }
        else if (other.gameObject.CompareTag("Relieve")) //take relieve
        {
            if (other.name == "Food" | other.name == "Medic")
            {
                currentRelieve = other.name;
                Debug.Log(currentRelieve);
            }
            else
            {
                if (patientNearby)
                {
                    currentRelieve = other.name;
                    Debug.Log(currentRelieve);
                }
            }
        }
    }

    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Patient"))
        {
            patientNearby = false;
            InteractionUI.SetActive(false); //UI ausschalten wenn man sich von Patienten entfernt
        }
    }
}
