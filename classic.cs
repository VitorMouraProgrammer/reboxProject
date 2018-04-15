using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class classic : MonoBehaviour {

    //Menu
    public GameObject canvasPlay;
    public GameObject canvasSettings;
    public GameObject canvasPutz;
    public GameObject canvasSpecial;
    public GameObject canvasSpecial2;

    //Start variables
    public static bool startGame;
    public static int movementations;
    public static int deadCount;
    public GameObject controls;
    public AudioSource gameMusic;
    

    //Transforms
    public Transform rebox;
    public Transform reboxLate;
    public Transform cameraRot;

    //Recorder
    private int[] movement;
    private int steps;

    //Things o/
    public Material rightMat;
    public Text Score;
    public Text levelNameTxt;
    public Text levelNameInt;

    //Audio
    public AudioClip[] soundEffects;
    private int clipPlay;
    public AudioSource playerAudio;
    public AudioSource wrong;
    public AudioSource right;
    public AudioSource rightEnd;

    //Rotations
    private Vector3 rotLeft = new Vector3(0, -90, 0);
    private Vector3 rotRight = new Vector3(0, 90, 0);
    private Vector3 rotUp = new Vector3(-90, 0, 0);
    private Vector3 rotDown = new Vector3(90, 0, 0);
    private Vector3 rotStart = new Vector3(0, 0, 0);

    //HighScore
    public static int highScore = 0;
    public Text highScoreTxt;

    //Repeater
    private float repeatTime;
    public bool isRepeating;
    private int repeatAux;
    private bool cameraBack;
    private float cameraTime;

    //ChangePlayer
    public Animator you;
    public Animator box;   

    //efeitos
    public Animator cameraAnim;
    public ParticleSystem tremorPart;
    public AudioSource tremorAud;
    public AudioClip[] efeitosClip;

    //Tutorial
    public GameObject info;
    

    void Start()
    {
        //ads.showBanner();
        cameraBack = false;
        cameraTime = 3;
        deadCount = 0;

        rightMat.color = Color.black;
        movementations = 0;
        repeatAux = 0;
        repeatTime = 0.8f;
        isRepeating = false;
        startGame = false;
        controls.SetActive(false);

        movement = new int[100];
        steps = 0;
        highScore = PlayerPrefs.GetInt("highScore");
        levelName(highScore);
        highScoreTxt.text = ("HighScore:" +" " + highScore.ToString());
    }

	void Update () {
        rebox.rotation = Quaternion.Lerp(rebox.rotation, reboxLate.rotation, Time.deltaTime * 5);

        if (cameraBack)
        {
            cameraTime -= Time.deltaTime;
            cameraRot.rotation = Quaternion.Lerp(cameraRot.rotation, Quaternion.identity, Time.deltaTime * 5);

            if(cameraTime <= 0)
            {
                cameraBack = false;
            }
        }

        if (startGame)
        {
            if (movementations == steps)
            {
                Invoke("isRepeat", 0.7f);
                rightEnd.Play();
                if (steps > highScore)
                {
                    levelName(steps);
                }
                box.Play("boxFadeIn");
                you.Play("fadeOut");
                Score.text = steps.ToString();
                movementations = 0;
            }

            //REPETIDOR
            if (isRepeating)
            {
                controls.SetActive(false);
                repeatTime -= Time.deltaTime;

                if (repeatTime <= 0)
                {
                    rightMat.color = Color.yellow;
                    repeat(movement[repeatAux]);
                    repeatAux += 1;

                    if (steps == 8)
                    {
                        zigzag();
                    }

                    if (steps == 5)
                    {
                        tremor();
                    }

                    if(repeatAux == steps)
                    {
                        repeatAux = 0;
                        Invoke("change", 0.8f);
                        Invoke("canPlay", 2);
                        isRepeating = false;
                    }
                    repeatTime = 0.8f;
                }
            }
        }
    }

    public void levelName(int scores)
    {
        print("foi chamado" +scores);
        if(scores < 5)
        {
            levelNameTxt.text = ("Noob");
            levelNameInt.text = ("Next > 5!");
        }

        else if(scores >= 5 && scores < 10)
        {
            levelNameTxt.text = ("Normal");
            levelNameInt.text = ("Next > 10!");
        }

        else if(scores >= 10 && scores < 15)
        {
            levelNameTxt.text = ("Good");
            levelNameInt.text = ("Next > 15!");
        }

        else if (scores >= 15 && scores < 20)
        {
            levelNameTxt.text = ("Great");
            levelNameInt.text = ("Next > 20!");
        }

        else if (scores >= 20 && scores < 25)
        {
            levelNameTxt.text = ("Perfect");
            levelNameInt.text = ("Next > 25!");
        }

        else if(scores >= 25 && scores < 30)
        {
            levelNameTxt.text = ("God");
            levelNameInt.text = ("Next > 30!");
        }

        else if(scores >= 30 && scores < 50)
        {
            levelNameTxt.text = ("Hacker");
            levelNameInt.text = ("Next > 50!");
        }

        else if(scores >= 50)
        {
            levelNameTxt.text = ("Developer");
            levelNameInt.text = ("Congratulations!");
        }
    }

    public void isRepeat()
    {
        isRepeating = true;
    }
    
    public void tremor()
    {
        cameraAnim.Play("cameraTremor");
        tremorAud.clip = efeitosClip[1];
        tremorAud.Play();
        tremorPart.Play();
    }

    public void zigzag()
    {
        cameraAnim.Play("cameraDrunk");
        tremorAud.clip = efeitosClip[0];
        tremorAud.Play();
    }

    public void helper()
    {
        /*if (PlayerPrefs.GetInt("highScore") <= 5 && steps <= 5)
        {
                if (movement[steps - movementations - 1] == 1)
                {
                    helpAnim.Play("helperRight");
                }

                if (movement[steps - movementations - 1] == 2)
                {
                    helpAnim.Play("helperDown");
                }

                if (movement[steps - movementations - 1] == 3)
                {
                    helpAnim.Play("helperLeft");
                }

                if (movement[steps - movementations - 1] == 4)
                {
                    helpAnim.Play("helperUp");
                }
        } */
    }

    public void canPlay()
    {
        controls.SetActive(true);
        helper();
        rightMat.color = Color.green;
        you.Play("fadeIn");
        box.Play("boxFadeOut");
        
    }

    public void startGamePlay()
    {
        canvasPlay.SetActive(false);
        canvasSettings.SetActive(false);
        canvasPutz.SetActive(false);
        canvasSpecial.SetActive(false);
        canvasSpecial2.SetActive(false);
        info.SetActive(false);
        startGame = true;
        steps = 0;
        Score.text = steps.ToString();
        rightMat.color = Color.yellow;
        box.Play("boxFadeIn");
        Invoke("canPlay", 1);
        change();
    }

    public void playAgain()
    {
        canvasPlay.SetActive(true);
        canvasSettings.SetActive(true);
        canvasPutz.SetActive(true);
        canvasSpecial.SetActive(true);
        canvasSpecial2.SetActive(true);
        info.SetActive(true);
        controls.SetActive(false);
        startGame = false;
        steps -= 1;
        you.Play("fadeOut");
        box.Play("boxFadeOut");
        if (steps > highScore)
        {
            PlayerPrefs.SetInt("highScore", steps);
            highScoreTxt.text = ("HighScore:" + " " + PlayerPrefs.GetInt("highScore").ToString());
        }

        movementations = 0;
        steps = 0;
        reboxLate.rotation = Quaternion.identity;
        cameraTime = 3;
        cameraBack = true;
        //Invoke("adCount", 0.5f);
    }

    void adCount()
    {
        deadCount += 1;
        //ads.showInerstitial();
    }

        void audioMove()
    {
        //AUDIO PART
        clipPlay = Random.Range(1, 4);
        playerAudio.clip = soundEffects[clipPlay - 1];
        playerAudio.Play();
    }

    public void change()
    {
        int side = Random.Range(1, 5);
        movement[steps] = side;
        steps += 1;

        if (side == 1) // Left
        {
            audioMove();
            reboxLate.Rotate(rotLeft, Space.World);
        }

        if (side == 2) //Up
        {
            audioMove();
            reboxLate.Rotate(rotUp, Space.World);
        }

        if (side == 3) //Right
        {
            audioMove();
            reboxLate.Rotate(rotRight, Space.World);
        }

        if (side == 4) //Down
        {
            audioMove();
            reboxLate.Rotate(rotDown, Space.World);
        }
    } 

    public void repeat(int repeater)
    {
        if (repeater == 1) // Left
        {
            audioMove();
            reboxLate.Rotate(rotLeft, Space.World);
        }

        if (repeater == 2) //Up
        {
            audioMove();
            reboxLate.Rotate(rotUp, Space.World);
        }

        if (repeater == 3) //Right
        {
            audioMove();
            reboxLate.Rotate(rotRight, Space.World);
        }

        if (repeater == 4) //Down
        {
            audioMove();
            reboxLate.Rotate(rotDown, Space.World);
        }
    }

    public void turnLeft()
    {
        audioMove();
        reboxLate.Rotate(rotLeft, Space.World);
        movementations += 1;

        if (movement[steps - movementations] == 3)
        {
            isRight(true);
            right.Play();
        }

        else
        {
            isRight(false);
        }
        helper();
    }

    public void turnRight()
    {
        audioMove();
        reboxLate.Rotate(rotRight, Space.World);
        movementations += 1;

        if (movement[steps - movementations] == 1)
        {
            isRight(true);
            right.Play();
        }

        else
        {
            isRight(false);
        }
        helper();
    }

    public void turnUp()
    {
        audioMove();
        reboxLate.Rotate(rotUp, Space.World);
        movementations += 1;

        if (movement[steps - movementations] == 4)
        {
            isRight(true);
            right.Play();
        }

        else
        {
            isRight(false);
        }
        helper();
    }

    public void turnDown()
    {
        audioMove();
        reboxLate.Rotate(rotDown, Space.World);
        movementations += 1;

        if (movement[steps - movementations] == 2)
        {
            isRight(true);
            right.Play();
        }

        else
        {
            isRight(false);
        }
        helper();
    }

    void isRight(bool isTrue)
    {
        if (isTrue)
        {
            rightMat.color = Color.green;
        }
        if (!isTrue)
        {
            wrong.Play();
            rightMat.color = Color.red;
            playAgain();
        }
    }

    public void menuManipulation(int side)
    {
        if (side == 1)
        {
            audioMove();
            reboxLate.Rotate(rotLeft, Space.World);
        }
        if (side == 2)
        {
            audioMove();
            reboxLate.Rotate(rotUp, Space.World);
        }
        if (side == 3)
        {
            audioMove();
            reboxLate.Rotate(rotRight, Space.World);
        }
        if (side == 4)
        {
            audioMove();
            reboxLate.Rotate(rotDown, Space.World);
        }
    }
}
