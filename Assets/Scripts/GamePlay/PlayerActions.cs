using System;
using System.Collections;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    [SerializeField] private float JumpSpeed=2;
    [SerializeField] private float FlipHight=.3f; 
    [SerializeField] private bool HeavyMoving = false;
    [SerializeField] private float PunchSlideAmount =.5f;
    private GameObject Player;
    private PlayerMovement playerMovement;
    private AudioSource MyPlayer; 
    [SerializeField] private float HeavyReactAmount = 3f;
    private bool HeavyReact = false;
    private Rigidbody rb;
    private bool FlyingJump = false;
    public Character character;

    private void Awake()
    {
        Player = transform.parent.gameObject;
        playerMovement =Player.GetComponent<PlayerMovement>();
        rb = Player.GetComponent<Rigidbody>();
    }
    
    private void Start()
    { 
        MyPlayer = GetComponent<AudioSource>();

    }
    private void Update()
    {
        HeavyPunchSlide();
        HeavyReactionSlide();
    }

    public void JumpUp()
    {
    Player.transform.Translate(0,JumpSpeed,0); 

    }
  

    IEnumerator Forward()
    { 
        
        Player.transform.Translate(0,FlipHight,0);
        FlyingJump = true;
        yield return new WaitForSeconds(.15f);  
 

    }
    IEnumerator Backward()
    {  
        Player.transform.Translate(0,FlipHight,0);
        FlyingJump = true;
 
        yield return new WaitForSeconds(.15f); 
  
    }

    public void IdleSpeed()
    {
        FlyingJump = false;

    }

    public bool GetFlyingJump()
    {
        return FlyingJump;
    }

    void HeavyPunchSlide()
    {
        if (!HeavyMoving) return;
        
        if(playerMovement.GetFacingRight())
            Player.transform.Translate(PunchSlideAmount*Time.deltaTime,0,0);
          

        if(playerMovement.GetFacingLeft())
            Player.transform.Translate(-PunchSlideAmount*Time.deltaTime,0,0); 

        
    }

    IEnumerator PunchSlide()
    {
        HeavyMoving = true;
        yield return new WaitForSeconds(.05f);
        HeavyMoving = false;
    }
    
    void HeavyReactionSlide()
    {
        if (!HeavyReact) return;
        
        if(playerMovement.GetFacingRight())
           Player.transform.Translate(-HeavyReactAmount*Time.deltaTime,0,0); 
        if(playerMovement.GetFacingLeft())
           Player.transform.Translate(HeavyReactAmount*Time.deltaTime,0,0); 
        
    }
    IEnumerator HeavySlide()
    {
        HeavyReact = true;
        yield return new WaitForSeconds(.05f);
        HeavyReact = false; 
        SetLightKick();


    }

    void SetLightKick()
    {
        if (TryGetComponent(out ActionsInputAI actionsInputAI))
        {
            actionsInputAI.SetAttackNumber();
        }

    }
    public void PunchWooshSound()
    {
        MyPlayer.clip = MyPlayer.GetComponent<AudioPlayer>().audioClip[0];
        MyPlayer.Play();
    }
    
    public void KickWooshSound()
    {
        MyPlayer.clip = MyPlayer.GetComponent<AudioPlayer>().audioClip[1];
        MyPlayer.Play();
    }

}
