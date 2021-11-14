using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class GameMaster : MonoBehaviour
{
    // used for anythink pitch-modding
    public float minPitch = 1f;
    public float maxPitch = 2f;

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
    public AudioSource[] arrayOfChatter_Sounds;
    public AudioSource click_Sound;
    public AudioSource victory_Sound;
    public AudioSource reha_Sound;
    public AudioSource toilet_Sound;
    public AudioSource kitchen_Sound;
    public AudioSource apotheke_Sound;
    public AudioSource background_Music;

    //public AudioSource errorSound;

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
            if (victory_Sound)
            {
                victory_Sound.Play();
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
            Time.timeScale = 0.0f;
            if (hasPatient_1)
            {
                patient_1_Overview.SetActive(true);
            }
        }else
        {
            Time.timeScale = 1f;
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
    public void RandomChatterNoise()
    {
        AudioSource randomChatterSound = arrayOfChatter_Sounds[Random.Range(0, arrayOfChatter_Sounds.Length)];
        randomChatterSound.pitch = Random.Range(minPitch, maxPitch);
        randomChatterSound.Play();
    }


    IEnumerator ContinueSounds()
    {
        if (click_Sound)
        {
            click_Sound.Play();
        }
        yield return new WaitForSeconds(.5f);
        if (arrayOfChatter_Sounds != null)
        {
            RandomChatterNoise();
        }
    }

    public void Reha_Success()
    {
        reha_Sound.pitch = Random.Range(minPitch, maxPitch);
        reha_Sound.Play();
        Invoke("Victory_Sound", .5f); // success delayed
        //victory_Sound.Play();
    }
    public void Toilet_Success()
    {
        toilet_Sound.pitch = Random.Range(minPitch, maxPitch);
        toilet_Sound.Play();
        Invoke("Victory_Sound", .5f); // success delayed
        //victory_Sound.Play();
    }
    public void Kitchen_Fetched()
    {
        kitchen_Sound.pitch = Random.Range(minPitch, maxPitch);
        kitchen_Sound.Play();
    }
    public void Apotheke_Fetched()
    {
        apotheke_Sound.pitch = Random.Range(minPitch, maxPitch);
        apotheke_Sound.Play();
        //RandomChatterNoise(); // talk to pharma
        Invoke("RandomChatterNoise", .5f); // talking delayed
        Invoke("RandomChatterNoise", 1f); // response delayed
    }
    public void Victory_Sound()
    {
        victory_Sound.Play();
    }
}
