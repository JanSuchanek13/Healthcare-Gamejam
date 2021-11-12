using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsPatient : MonoBehaviour
{
    bool isIll = false;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        coroutine = Needs(60f);
        StartCoroutine(coroutine);
    }

    private IEnumerator Needs(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (isIll == false)
        {
            isIll = true;

        }
    }

}
