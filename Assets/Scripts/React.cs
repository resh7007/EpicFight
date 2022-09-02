using UnityEngine;

public class React : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator Anim;
    private AudioSource MyPlayer;
    private bool isKnockedOut = false;
    private void Awake()=> playerMovement = GetComponent<PlayerMovement>();
    
    private void Start()
    {
        Anim = playerMovement.GetAnim();
        MyPlayer = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // if(transform.CompareTag("player1"))
        // {
        //     if (Save.Player1Health <= 0 && !isKnockedOut)
        //     { 
        //         isKnockedOut = true;
        //        transform.GetComponent<BoxCollider>().enabled = false;
        //     }
        //     
        // }
        // else if(transform.CompareTag("player2"))
        // {
        //     if (Save.Player2Health <= 0  && !isKnockedOut)
        //     { 
        //         Anim.SetTrigger("KnockedOut");
        //         isKnockedOut = true;
        //
        //     }
        //     
        // }
        
        
        if (other.gameObject.CompareTag("KickLight"))
        {
            Anim.SetTrigger("HeadReact");
            MyPlayer.clip =  MyPlayer.GetComponent<AudioPlayer>().audioClip[0];
            MyPlayer.Play();
        }
        else if (other.gameObject.CompareTag("KickHeavy"))
        {
            Anim.SetTrigger("BigReact");
            MyPlayer.clip =  MyPlayer.GetComponent<AudioPlayer>().audioClip[1];
            MyPlayer.Play();
        }  
        else if(other.gameObject.CompareTag("FistLight"))
        {
            Anim.SetTrigger("HeadReact");
            MyPlayer.clip =  MyPlayer.GetComponent<AudioPlayer>().audioClip[2];
            MyPlayer.Play();
        }   
        else if (other.gameObject.CompareTag("FistHeavy"))
        {
            Anim.SetTrigger("BigReact");
            MyPlayer.clip =  MyPlayer.GetComponent<AudioPlayer>().audioClip[3];
            MyPlayer.Play();
        }

    }
}
