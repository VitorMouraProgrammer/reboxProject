using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightColor : MonoBehaviour {

    public Light pointLight;
    private float colorTime;


    private Color greenColor = Color.green;
    private Color blueColor = Color.blue;
    private Color redColor = Color.red;
    private Color yellowColor = Color.yellow;
    private Color cyanColor = Color.cyan;
    private Color magentaColor = Color.magenta;
    public Color randomColor;

    private void Start()
    {
        randomColorPick();
        colorTime = 5;
    }

    void Update ()
    {
        if (classic.startGame)
        {
            colorTime -= Time.deltaTime;

            if (colorTime > 0)
            {
                pointLight.color = Color.Lerp(pointLight.color, randomColor, Time.deltaTime * 0.1f);
            }

            else
            {
                randomColorPick();
                colorTime = 10;
            }
        }
    }

    void randomColorPick()
    {
        int random = Random.Range(1, 7);

        if (random == 1)
            randomColor = greenColor;
        if (random == 2)
            randomColor = blueColor;
        if (random == 3)
            randomColor = redColor;
        if (random == 4)
            randomColor = yellowColor;
        if (random == 5)
            randomColor = cyanColor;
        if (random == 6)
            randomColor = magentaColor;
    }
}
