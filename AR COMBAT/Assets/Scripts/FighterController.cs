using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterController : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform enemyTarget;
    static Animator anim;

    public static bool mvBack = false;
    public static bool mvFwrd = false;
    public static bool IsAttack = false;
    public static FighterController instance;

    public Slider PlayerHB;

    public int PLayerHealth = 100;

    public BoxCollider[] C;

    private Vector3 direction;

    public AudioClip[] Clip;
    AudioSource Audio;

    private Vector3 PlayerPosition;


    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerSetterFOrBoxCollider(false);
        Audio = GetComponent<AudioSource>();
        PlayerPosition = transform.position;
    }

    void PlayAudio(int AudioIndex) {

        Audio.clip = Clip[AudioIndex];
        Audio.Play();

    }


    public void PlayerSetterFOrBoxCollider(bool State) {
        C[0].enabled = State;
        C[1].enabled = State;

    }

    // Update is called once per frame
    void Update()

    {

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("fight_idle"))
        {
            direction = enemyTarget.position - this.transform.position;

            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), .3f);

        }








        if (anim.GetCurrentAnimatorStateInfo(0).IsName("fight_idle")) {

            IsAttack = false;
            PlayerSetterFOrBoxCollider(false);
        }

        if (GameController.AllowMovement == true)
        {
            if (IsAttack == false)
            {

                PlayerSetterFOrBoxCollider(false);
                if (mvBack == true)
                {
                    anim.SetTrigger("wkBACK");
                    anim.ResetTrigger("Idle");
                    anim.ResetTrigger("wkFWRD");


                }
                else if (mvFwrd == true)
                {
                    anim.SetTrigger("wkFWRD");
                    anim.ResetTrigger("Idle");
                    anim.ResetTrigger("wkBACK");


                }
                else
                {

                    anim.SetTrigger("Idle");
                    anim.ResetTrigger("wkFWRD");
                    anim.ResetTrigger("wkBACK");


                }
            }

            else if (IsAttack == true)
            {

                PlayerSetterFOrBoxCollider(true);

            }
        }




    }
    
    public void Punch() {
        if (WalkBackController.MB == false && WalkFwrdController.MF==false)
        {
            IsAttack = true;

            anim.ResetTrigger("Idle");
            anim.SetTrigger("Punch");
            PlayAudio(0);
            Debug.Log(WalkBackController.MB);
            Debug.Log(WalkFwrdController.MF);

        }

    }

    public void Kick()
    {
        if (WalkBackController.MB == false && WalkFwrdController.MF == false )
        {
            IsAttack = true;

            anim.ResetTrigger("Idle");
            anim.SetTrigger("Kick");
            PlayAudio(1);
            Debug.Log(WalkBackController.MB);
            Debug.Log(WalkFwrdController.MF);
        }
    }

    public void React()
    {


        //IsAttack = true;
        PlayerSetterFOrBoxCollider(false);
        PLayerHealth = PLayerHealth - 10;

        PlayerHB.value = PLayerHealth;

        if (PLayerHealth < 10)
        {
            

            EnemyController.instance.EnemySetterFOrBoxCollider(false);
            PlayerKnockOut();
            PlayAudio(3);
           
            
        }
        else {

            anim.ResetTrigger("Idle");
            anim.SetTrigger("React");
            PlayAudio(2);
        }


    }

    public void PlayerKnockOut() {

        PLayerHealth = 100;
        PlayerHB.value = 100;

        GameController.AllowMovement = false;

        GameController.instance.EnemyScoreUpdate();
        
        

        PlayerSetterFOrBoxCollider(false);
        EnemyController.instance.EnemySetterFOrBoxCollider(false); 
        
        
        anim.SetTrigger("KnockOut");
       
        GameController.instance.Rounds();
        if (GameController.instance.EnemyScore == 2)
        {
            GameController.instance.DoReset();
            resetCharacters();


        }
        else
        {
            StartCoroutine(resetCharacters());

        }
        GameController.instance.OnScreenPoinPupdate();
    }
    IEnumerator resetCharacters()
    {


        PLayerHealth = 100;
        PlayerHB.value = 100;

        yield return new WaitForSeconds(4);
        
        GameObject[] TheClone = GameObject.FindGameObjectsWithTag("Player");
        Transform t = TheClone[5].GetComponent<Transform>();
        

        anim.SetTrigger("Idle");
        anim.ResetTrigger("KnockOut");
        t.position = PlayerPosition;
        t.position = new Vector3(t.position.x, 0.00001f, t.position.z);

         
        GameController.AllowMovement = true;
    }


}
