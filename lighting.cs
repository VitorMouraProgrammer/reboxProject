using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighting : MonoBehaviour {

    public GameObject imglighting;
    private bool boolLight;

    void Start()
    {
        boolLight = false;
        imglighting.SetActive(false);
    }

    public void doLighting()
    {
        boolLight = !boolLight;

        imglighting.SetActive(boolLight);
    }
}
