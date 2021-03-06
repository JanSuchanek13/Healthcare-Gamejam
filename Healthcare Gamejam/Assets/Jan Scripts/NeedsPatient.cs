using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsPatient : MonoBehaviour
{
    // var for Need
    bool needActive = false;
    int needStrength = -1;
    public string[] needsList = new string[] {"Food", "Medic", "Bath" }; //Array aller needs
    public string currentNeed;
    public string needText = "Ich brauche Ruhe";
    Transform NeedsSign;

    // var for MainTask
    public string mainTask;
    public bool taskAnnounced = false;
    public int sessionsLeft = 3;

    //cooldown
    bool cooldownRunning;

    //gamemaster
    GameObject Gamemaster;

    private IEnumerator coroutine;
    private IEnumerator coroutine2;



    // Start is called before the first frame update
    void Start()
    {
        needText = "Ich brauche Ruhe";

        Gamemaster = GameObject.Find("GAME_MASTER");

        //activate Maintask
        mainTask = "Ich m?chte mein linkes Sprunggelenk wieder voll belasten k?nnen ^^";


        //activate Need
        coroutine = Needs(Random.Range(20f, 40f)); //zeit festlegen in der needs entstehen
        StartCoroutine(coroutine);
        NeedsSign = transform.Find("NeedsSign");
        

    }

    private void Update()
    {
        // enable follow button only for bath or if cooldown is not running
        if (currentNeed == "Bath" || !cooldownRunning)
        {
            Gamemaster.GetComponent<GameMaster>().followEnabled = true;
        }

        if (NeedsSign.gameObject.activeSelf == true)
        {
            switch (needStrength) // in Abh?ngikeit von der needsStrength verf?rbt sich das needSign
            {
                case 0:
                    
                    NeedsSign.GetComponent<MeshRenderer>().material.color = Color.green;
                    break;

                case 1:
                    
                    NeedsSign.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    break;

                case 2:
                    
                    NeedsSign.GetComponent<MeshRenderer>().material.color = Color.red;
                    break;

                case 3:
                    Destroy(gameObject);
                    break;

                default:
                    Debug.Log("Fehler");
                    break;
            }
        }
    }

    public void clearNeeds(string relieve)
    {
        if (relieve == "Trigger_Reha")
        {
            sessionsLeft--;
            Gamemaster.GetComponent<GameMaster>().gainHearts();
            Gamemaster.GetComponent<GameMaster>().Reha_Success();
            Gamemaster.GetComponent<GameMaster>().UpdatePatient_1(gameObject.name, mainTask, " weitere Reha-Termine. Sieht schon richtig gut aus!", sessionsLeft);
            Debug.Log("eine session geschafft");
            if (sessionsLeft == 0)
            {
                StartCoroutine(coroutine2);
                Gamemaster.GetComponent<GameMaster>().gainHearts();
                
            }
        }
        if (relieve == currentNeed)
        {
            
            Debug.Log("hit");
            needStrength = -1;
            needActive = false;
            NeedsSign.gameObject.SetActive(false);

            switch (relieve)
            {
                case "Food":

                    Gamemaster.GetComponent<GameMaster>().Kitchen_Fetched();
                    break;

                case "Medic":

                    Gamemaster.GetComponent<GameMaster>().Apotheke_Fetched();
                    break;

                case "Bath":

                    Gamemaster.GetComponent<GameMaster>().Toilet_Success();
                    break;

                default:
                    Debug.Log("Fehler");
                    break;
            }
            needText = "Ich brauche Ruhe";
        }
        gameObject.GetComponent<FollowOtherCharacter>().StopFollowing();
    }

    public bool compareNeeds(string relieve)
    {
        return relieve == currentNeed;
    }

    private IEnumerator Needs(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            if (needActive == false)
            {
                needActive = true;
                NeedsSign.gameObject.SetActive(true);

                //zuf?llige Need verteilen aus Needlist
                int randomNeed = Random.Range(0, needsList.Length);
                currentNeed = needsList[randomNeed];
                Debug.Log(currentNeed);
                needStrength++;

                switch (currentNeed)
                {
                    case "Food":

                        needText = "K?nnte ich ein Glas Wasser haben?";
                        break;

                    case "Medic":

                        needText = "Mein Fu? tut weh";
                        break;

                    case "Bath":

                        needText = "Ich muss zur Toilette";
                        break;

                    default:
                        Debug.Log("Fehler");
                        break;
                }

                /*if (currentNeed == "Bath" || !cooldownRunning)
                {
                    Gamemaster.GetComponent<GameMaster>().followEnabled = true;
                }*/

            }
            else
            {
                needStrength++;
                
            }
        }


    }

    private IEnumerator Countdown()
    {

        cooldownRunning = true;
        yield return new WaitForSeconds(20.0f);
        cooldownRunning = false;

    }

}
