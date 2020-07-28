using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  
    public static bool AllowMovement = false;
    private bool Played321 = false;

    public static GameController instance;
    public GameObject FlashButton;
    public GameObject CameraButton;
    public GameObject PlayerScoreOnScreen;
    public GameObject EnemyScoreOnScreen;
    public GameObject BackButton;
    public GameObject ForwardButton;
    public GameObject PunchButtor;
    public GameObject KickButton;
    public AudioClip[] Clip;
    private AudioSource Audio;

    public int PlayerScore = 0;
    public int EnemyScore = 0;

    public GameObject[] Points;
    public static int Round = 0;

    


    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    private void PlayAudio(int AudioIndex) {

        Audio.clip = Clip[AudioIndex];
        Audio.Play();


    }

    public void DoReset() {
        if (PlayerScore == 2) {
            PlayAudio(6);

        }
        else {
            PlayAudio(5);

        }
        FighterController.instance.PlayerHB.value = 100;
        FighterController.instance.PLayerHealth = 100;

        EnemyController.instance.EnemyHB.value = 100;
        EnemyController.instance.EnemyHealth = 100;

        PlayerScore = 0;
        EnemyScore = 0;

       

        StartCoroutine(ResartGame());
    }

    IEnumerator ResartGame() {
        yield return new WaitForSeconds(4.5f);
        Points[0].SetActive(false);
        Points[1].SetActive(false);
        Points[2].SetActive(false);
        Points[3].SetActive(false);
        AllowMovement = true;
        StartCoroutine(ResartRoundAudio());

    }

    IEnumerator ResartRoundAudio() {
        yield return new WaitForSeconds(1f);
        PlayAudio(0); 


    }


    // Update is called once per frame
    void Update()
    {
        if (Played321 == false) {
            if (DefaultTrackableEventHandler.TrueFals == true) {
                FlashButton.SetActive(false);
                CameraButton.SetActive(false);
                PlayerScoreOnScreen.SetActive(true);
                EnemyScoreOnScreen.SetActive(true);
                BackButton.SetActive(true);
                ForwardButton.SetActive(true);
                PunchButtor.SetActive(true);
                KickButton.SetActive(true);
                Played321 = true;
                StartCoroutine(Round1());
            }




        }


        
    }

    public void PlayerScoreUpdate() {

        PlayerScore++;

    }

    public void EnemyScoreUpdate() {

        EnemyScore++;
        //Debug.Log(EnemyScore);
    }

    IEnumerator Round1() {
        PlayAudio(0);
        yield return new WaitForSeconds(.2f);
        StartCoroutine(PrepareYourSelf());

    }

    IEnumerator PrepareYourSelf()
    {

        yield return new WaitForSeconds(1.2f);
        PlayAudio(1);
        StartCoroutine(Start231());


    }
    IEnumerator Start231()
    {

        yield return new WaitForSeconds(2f);
        PlayAudio(2);
        StartCoroutine(AllowEnemyMovement());


    }
    IEnumerator AllowEnemyMovement()
    {

        yield return new WaitForSeconds(5f);
        AllowMovement = true;


    }

    public void OnScreenPoinPupdate() {
        if (PlayerScore == 1)
        {
            Points[0].SetActive(true);
        }
        else if (PlayerScore == 2)
        {
            Points[1].SetActive(true);
        }

        if (EnemyScore == 1)
        {
            Debug.Log(EnemyScore);
            Points[2].SetActive(true);
        }
        else if (EnemyScore == 2) {

            Debug.Log(EnemyScore);
            Points[3].SetActive(true);

        }
    }

    public void Rounds() {
        Round = PlayerScore + EnemyScore;
        if (Round == 1)
        {
            PlayAudio(3);

        }

        if (Round == 2 && PlayerScore != 2 && EnemyScore != 2) {

            PlayAudio(4);
           

        }
}



}
