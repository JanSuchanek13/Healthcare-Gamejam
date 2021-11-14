using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class GameMaster : MonoBehaviour
{
    // controlls how long a character moves toward any given target
    public float minTimerForNewPath = 3f;
    public float maxTimerForNewPath = 8f;

    // used to stop a patient
    public bool isBeingTalkedTo = false;
    public bool followEnabled = false;
    //bool zoomInOnPlayer = false;

    // to open/close dialoge UI
    public GameObject interaction_UI;
    public GameObject victoryBanner_UI;
    public GameObject follow_Button;
    public GameObject conversationPartner;
    public GameObject playerCharacter;

    // Klemmbrett UI
    public GameObject klemmbrett_UI;
    public bool hasPatient_1 = false;
    //
    public GameObject patient_1_Overview;
    public TextMeshProUGUI patient_1_Name;
    public TextMeshProUGUI patient_1_Ziel;
    public TextMeshProUGUI patient_1_Weg;
    public TextMeshProUGUI patient_1_verbleibendeTermine;

    // winning condition
    public int hearts;

    // in case we get around to playing sounds on clicking button
    public AudioSource clickSound;
    public AudioSource chatterSound;
    public AudioSource victorySound;
    public AudioSource errorSound;

    public void UpdatePatient_1(string name, string goal, string weg, int verbleibendeTermine)
    {
        hasPatient_1 = true;
        patient_1_Name.text = name;
        patient_1_Ziel.text = goal;
        patient_1_Weg.text = weg;
        patient_1_verbleibendeTermine.text = verbleibendeTermine.ToString();
    }
    public void gainHearts()
    {
        hearts++;
        Debug.Log("nr of hearts " + hearts);
        if(hearts >= 3)
        {
            Debug.Log("you gathered " + hearts + " hearts! congrats!");
            // should stop all characters and let them idle
            isBeingTalkedTo = true;
            if (victorySound)
            {
                victorySound.Play();
            }
            if (victoryBanner_UI)
            {
                victoryBanner_UI.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (!followEnabled)
        {
            follow_Button.SetActive(false);
        }
    }
    bool klemmbrettIsOpened = false;
    public void Klippbrett()
    {
        if (!klemmbrettIsOpened)
        {
            klemmbrettIsOpened = true;
            klemmbrett_UI.SetActive(true);
            if (hasPatient_1)
            {
                patient_1_Overview.SetActive(true);
            }
        }else
        {
            klemmbrett_UI.SetActive(false);
            klemmbrettIsOpened = false;
        }
    }
    public void Continue()
    {
        StartCoroutine(ContinueSounds());
        isBeingTalkedTo = false;
        interaction_UI.SetActive(false);
    }
    public void FollowMe()
    {
        StartCoroutine(ContinueSounds());
        conversationPartner.GetComponent<FollowOtherCharacter>().Follow(playerCharacter);
        isBeingTalkedTo = true;
        interaction_UI.SetActive(false);
        /*if (followEnabled)
        {
            StartCoroutine(ContinueSounds());
            conversationPartner.GetComponent<FollowOtherCharacter>().Follow(playerCharacter);
            isBeingTalkedTo = true;
            interaction_UI.SetActive(false);
        }else if (errorSound)
        {
            errorSound.Play();
        }*/
    }

    IEnumerator ContinueSounds()
    {
        if (clickSound)
        {
            clickSound.Play();
        }
        yield return new WaitForSeconds(.5f);
        if (chatterSound != null)
        {
            chatterSound.Play();
        }
    }
}
