using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Transform enemy;
    private Vector3 direction1;
    public static Animator anim1;
    public static EnemyController instance;

    public Slider EnemyHB;

    public BoxCollider[] C;

    public int EnemyHealth = 100;

    public AudioClip[] Clip;
    AudioSource Audio;
    private Vector3 EnemyPosition;

    public void EnemySetterFOrBoxCollider(bool State)
    {
        C[0].enabled = State;
        C[1].enabled = State;

    }



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        anim1 = GetComponent<Animator>();
        EnemySetterFOrBoxCollider(false);
        Audio = GetComponent<AudioSource>();
        EnemyPosition = transform.position;
    }

    void PlayAudio(int AudioIndex)
    {

        Audio.clip = Clip[AudioIndex];
        Audio.Play();

    }


    // Update is called once per frame
    void Update()
    {


        if (anim1.GetCurrentAnimatorStateInfo(0).IsName("fight_idleCopy"))
        {
            direction1 = enemy.position - this.transform.position;

            direction1.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction1), .3f);

            
        }

      // Debug.Log(direction1.magnitude);

        if (direction1.magnitude > 13f && GameController.AllowMovement == true)
        {
            anim1.SetTrigger("WalkFRWD");
            EnemySetterFOrBoxCollider(false);
            Audio.Stop();
        }
        else {
             anim1.ResetTrigger("WalkFRWD");
            
             }

        if (direction1.magnitude < 13f && direction1.magnitude > 7f && GameController.AllowMovement == true)
        {


            EnemySetterFOrBoxCollider(true);
            if ( !Audio.isPlaying && !anim1.GetCurrentAnimatorStateInfo(0).IsName("roundhouse_kick 2") )
            {

                anim1.SetTrigger("KickEnemy");
                PlayAudio(1);

            }
        }
        else {

            anim1.ResetTrigger("KickEnemy");
        }
        if (direction1.magnitude < 6f && direction1.magnitude > 4f && GameController.AllowMovement == true)
        {


            EnemySetterFOrBoxCollider(true);
            if (!Audio.isPlaying && !anim1.GetCurrentAnimatorStateInfo(0).IsName("cross_punch"))
            {

                anim1.SetTrigger("Punch");
                PlayAudio(0);

            }

        }
        else
        {

            anim1.ResetTrigger("Punch");
        }
        if (direction1.magnitude > 0f && direction1.magnitude < 4f && GameController.AllowMovement == true)
        {

            anim1.SetTrigger("WalkBACK");
            EnemySetterFOrBoxCollider(false);
            Audio.Stop();
        }
        else
        {
            
            anim1.ResetTrigger("WalkBACK");
        }



    }

    public void EnemyReact() {

        EnemySetterFOrBoxCollider(false);
        EnemyHealth = EnemyHealth - 10;

        EnemyHB.value = EnemyHealth;

        if (EnemyHealth < 10)
        {
            

            FighterController.instance.PlayerSetterFOrBoxCollider(false);
            EnemyKnockOut();
            PlayAudio(3);
           
            
        }
        else
        {
            anim1.SetTrigger("React");
       
            PlayAudio(2);
        }
    }

    public void EnemyKnockOut() {
        EnemyHealth = 100;
        EnemyHB.value = 100;

        GameController.AllowMovement = false;

        GameController.instance.PlayerScoreUpdate();
        GameController.instance.OnScreenPoinPupdate();

        FighterController.instance.PlayerSetterFOrBoxCollider(false);
        EnemySetterFOrBoxCollider(false);



        anim1.SetTrigger("KnockOut");
        EnemySetterFOrBoxCollider(false);
        
        GameController.instance.Rounds();

        if (GameController.instance.PlayerScore == 2)
        {
            GameController.instance.DoReset();
            resetCharacters();
        }
        else {

            StartCoroutine(resetCharacters());

        }
        GameController.instance.OnScreenPoinPupdate();
    }
    IEnumerator resetCharacters() {
        yield return new WaitForSeconds(4);

        EnemyHealth = 100;
        EnemyHB.value = 100;

        GameObject[] TheClone = GameObject.FindGameObjectsWithTag("Enemy");
        Transform t = TheClone[4].GetComponent<Transform>();
        

        t.position = EnemyPosition;
        t.position = new Vector3(t.position.x, 0.004f, t.position.z);
        GameController.AllowMovement = true;
    }

}
