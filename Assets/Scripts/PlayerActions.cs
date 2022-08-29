using System;
using System.Collections;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    [SerializeField] private float JumpSpeed;
    [SerializeField] private float FlipSpeed=.3f;

    private GameObject Player1;
    [SerializeField] private bool HeavyMoving = false;
    [SerializeField] private float PunchSlideAmount =.5f;
    private PlayerMovement playerMovement;
    private AudioSource MyPlayer;
 
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
 
    }

    public void JumpUp()
    {
        Player1.transform.Translate(0,JumpSpeed,0);
    }
    public void FlipUp()
    {
        Player1.transform.Translate(0,FlipSpeed,0); 
    }
    public void FlipBack()
    {
        Player1.transform.Translate(0,FlipSpeed,0); 
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
