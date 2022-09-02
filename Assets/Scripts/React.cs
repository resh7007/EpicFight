using UnityEngine;

public class React : MonoBehaviour
{ 
    private PlayerMovement playerMovement;
    private Animator Anim;
    private AudioSource MyPlayer;
    private ICharacterInput _characterInput;
    private void Awake()=> playerMovement = GetComponent<PlayerMovement>();
    
    private void Start()
    {
        Anim = playerMovement.GetAnim();
        MyPlayer = GetComponent<AudioSource>(); 
        _characterInput = GetComponent<ICharacterInput>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_characterInput.GetIsInBlock())
            return;

        if (other.gameObject.CompareTag("KickLight"))
        {
            Anim.SetTrigger("HeadReact");
            MyPlayer.clip = MyPlayer.GetComponent<AudioPlayer>().audioClip[0];
            MyPlayer.Play();
        }
        else if (other.gameObject.CompareTag("KickHeavy"))
        {
            Anim.SetTrigger("BigReact");
            MyPlayer.clip = MyPlayer.GetComponent<AudioPlayer>().audioClip[1];
            MyPlayer.Play();
        }
        else if (other.gameObject.CompareTag("FistLight"))
        {
            Anim.SetTrigger("HeadReact");
            MyPlayer.clip = MyPlayer.GetComponent<AudioPlayer>().audioClip[2];
            MyPlayer.Play();
        }
        else if (other.gameObject.CompareTag("FistHeavy"))
        {
            Anim.SetTrigger("BigReact");
            MyPlayer.clip = MyPlayer.GetComponent<AudioPlayer>().audioClip[3];
            MyPlayer.Play();
        }
         
    }
}
