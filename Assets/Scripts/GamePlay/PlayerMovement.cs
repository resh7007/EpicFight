using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private Animator Anim;
    private GameObject PlayerGO;
    private GameObject Opponent;
    private Vector3 OppPosition;
    private bool FacingLeft = false;
    private bool FacingRight = true;
    public int dir;
    public ParticleSystem heavyFistParticle;
    public ParticleSystem heavyKickParticle;
    public ParticleSystem lightKickParticle;
    public GameObject LeftRestrict;
    public GameObject RightRestrict;
    private void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        PlayerGO = transform.GetChild(0).gameObject; 
    }

    public MoveRestrict GetMoveRestrict()
    {
       return GetComponentInChildren<MoveRestrict>();
    }

    public void AssignAnOppponent(GameObject _opponent)
    { 
        Opponent = _opponent;
    }

    public void AssignParticles(ParticleSystem _heavyFistParticle, ParticleSystem _heavyKickParticle, ParticleSystem _lightKickParticle)
    {
        heavyFistParticle = _heavyFistParticle;
        heavyKickParticle = _heavyKickParticle;
        lightKickParticle = _lightKickParticle;
    }

    public ParticleSystem GetHeavyFistParticle()
    {
        return heavyFistParticle;
    }
    public ParticleSystem GetHeavyKickParticle()
    {
        return heavyKickParticle;
    }
    public ParticleSystem GetLightFistParticle()
    {
        return lightKickParticle;
    }
    
    public Animator GetAnim()
    {
        return Anim;
    }

    public bool GetFacingLeft ()
    {
        return FacingLeft;
    }
    public bool GetFacingRight ()
    {
        return FacingRight;
    }

    void Update()
    {
        GetOpponentPosition(); 
    }
    void GetOpponentPosition()
    {
        OppPosition = Opponent.transform.position;
        FlipToFaceOpponent();
    }

    void FlipToFaceOpponent()
    {
   
       if (OppPosition.x > PlayerGO.transform.position.x)
       { 
           StartCoroutine(FaceLeft());
       }
       else if (OppPosition.x < PlayerGO.transform.position.x)
       { 
           StartCoroutine(FaceRight());
       }
 
    }

    IEnumerator FaceLeft()
    {
        if (FacingLeft)
        {
            FacingLeft = false;
            FacingRight = true;
            dir = -1;
            yield return new WaitForSeconds(.15f);
            PlayerGO.transform.Rotate(0, -180, 0);
            Anim.SetLayerWeight(1, 0);
            
        }
    }
    IEnumerator FaceRight()
    {
        if (FacingRight)
        { 
            FacingLeft = true;
            FacingRight = false;
            dir = 1;
            yield return new WaitForSeconds(.15f);
            PlayerGO.transform.Rotate(0, 180, 0);
            Anim.SetLayerWeight(1, 1);

        }
    }

    public string GetOpponentTag()
    {
        return Opponent.tag;
    }

 
}
