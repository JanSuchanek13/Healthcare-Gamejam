using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsPatient : MonoBehaviour
{
    bool needActive = false;
    int needStrength = -1;
    string[] needsList = new string[] {"Food", "Medic", "Bath" }; //Array aller needs
    public string currentNeed;
    Transform NeedsSign;

    private IEnumerator coroutine;
    

    // Start is called before the first frame update
    void Start()
    {
        coroutine = Needs(Random.Range(10f, 11f)); //zeit festlegen in der needs entstehen
        StartCoroutine(coroutine);
        NeedsSign = transform.Find("NeedsSign");
        
    }

    private void Update()
    {
        if (NeedsSign.gameObject.activeSelf == true)
        {
            switch (needStrength) // in Abhängikeit von der needsStrength verfärbt sich das needSign
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
        
        if (relieve == currentNeed)
        {
            Debug.Log("hit");
            needStrength = -1;
            needActive = false;
            NeedsSign.gameObject.SetActive(false);
        }

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

                //zufällige Need verteilen aus Needlist
                int randomNeed = Random.Range(0, needsList.Length -2);
                currentNeed = needsList[randomNeed];
                Debug.Log(currentNeed);

                needStrength++;

            }
            else
            {
                needStrength++;
                
            }
        }


    }

}
