using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GameMaster : MonoBehaviour
{
    // controlls how long a character moves toward any given target
    public float minTimerForNewPath = 3f;
    public float maxTimerForNewPath = 8f;

    // used to stop a patient
    public bool isBeingTalkedTo = false;

    // in case we get around to playing sounds on clicking button
    public AudioSource clickSound;
    public AudioSource chatterSound;

    // to open/close dialoge UI
    public GameObject interaction_UI;
    public GameObject conversationPartner;
    public GameObject playerCharacter;

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
