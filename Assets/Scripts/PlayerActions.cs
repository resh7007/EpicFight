using System;
using System.Collections;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    [SerializeField] private float JumpSpeed;
    [SerializeField] private float FlipHight=.3f;
    [SerializeField] private float FlipForwardDist=1.3f;
    [SerializeField] private bool HeavyMoving = false;
    [SerializeField] private float PunchSlideAmount =.5f;
    private GameObject Player1;
    private PlayerMovement playerMovement;
    private AudioSource MyPlayer;
    
    [SerializeField] private float HeavyReactAmount = 3f;
    private bool HeavyReact = false;
    private void Awake()
    {
        Player1 = transform.parent.gameObject;
        playerMovement =Player1.GetComponent<PlayerMovement>();
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
        Player1.transform.Translate(0,JumpSpeed,0);
    }
  

    IEnumerator Forward()
    {
        Player1.transform.Translate(0,FlipHight,0);
        yield return new WaitForSeconds(.15f);
        Player1.transform.Translate(FlipForwardDist,0,0);

    }
    IEnumerator Backward()
    {
        Player1.transform.Translate(0,FlipHight,0);
       
         yield return new WaitForSeconds(.15f);
        Player1.transform.Translate(-FlipForwardDist,0,0);

    }
    void HeavyPunchSlide()
    {
        if (!HeavyMoving) return;
        
        if(playerMovement.GetFacingRight())
            Player1.transform.Translate(PunchSlideAmount*Time.deltaTime,0,0);
        if(playerMovement.GetFacingLeft())
            Player1.transform.Translate(-PunchSlideAmount*Time.deltaTime,0,0);
        
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
            Player1.transform.Translate(-HeavyReactAmount*Time.deltaTime,0,0);
        if(playerMovement.GetFacingLeft())
            Player1.transform.Translate(HeavyReactAmount*Time.deltaTime,0,0);
        
    }
    IEnumerator HeavySlide()
    {
        HeavyReact = true;
        yield return new WaitForSeconds(.05f);
        HeavyReact = false;
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
