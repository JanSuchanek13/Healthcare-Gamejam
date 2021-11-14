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
    public bool followEnabled = false;
    bool zoomInOnPlayer = false;

    // to open/close dialoge UI
    public GameObject interaction_UI;
    public GameObject conversationPartner;
    public GameObject playerCharacter;

    // winning condition
    public int hearts;

    // in case we get around to playing sounds on clicking button
    public AudioSource clickSound;
    public AudioSource chatterSound;
    public AudioSource victorySound;

    public void gainHearts()
    {
        hearts++;
        Debug.Log("nr of hearzs " + hearts);
        if(hearts >= 3)
        {
            Debug.Log("you gathered " + hearts + " hearts! congrats!");

            // should stop all characters and let them idle
            isBeingTalkedTo = true;
            zoomInOnPlayer = true;
            //GameObject mainCam = GameObject.Find("Main Camera");
            //mainCam.GetComponent<CameraZoom>().
            /*    if (Camera.main.fieldOfView > maxZoom)
            {
                Camera.main.fieldOfView -= zoomSpeed * Time.deltaTime;
            }
            if (Camera.main.orthographicSize >= 1)
            {
                Camera.main.orthographicSize -= 0.5f;
            }*/

            // play victory sound (if we have one)
            if (victorySound)
            {
                victorySound.Play();
            }
        }
    }
    private void FixedUpdate()
    {
        //if (zoomInOnPlayer && Camera.main.fieldOfView > maxZoom)
        //{

        //}
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
