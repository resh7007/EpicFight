using UnityEngine;
using UnityEngine.Serialization;

public class PlayerTrigger : MonoBehaviour
{
        private Collider _collider;
        private ActionsInput _actionsInput;
        private string opponentTag;
        [SerializeField] private float DamageAmount = .1f;
        [SerializeField] private bool EmitHeavyKickFX = false;
        [SerializeField] private bool EmitFistFX = false;
        [SerializeField] private bool EmitLightKickFX = false;

        private ParticleSystem heavyFistParticle;
        private ParticleSystem heavyKickParticle;
        private ParticleSystem lightKickParticle;


        private void Awake()
        {
                Transform rootObj = transform.root;
                _actionsInput = rootObj.GetChild(0).GetComponent<ActionsInput>();
                _collider = GetComponent<BoxCollider>();
                if (transform.root.CompareTag("player1"))
                        opponentTag = "player2";
                if (transform.root.CompareTag("player2"))
                        opponentTag = "player1";

                heavyFistParticle = rootObj.GetComponent<PlayerMovement>().GetHeavyFistParticle();
                heavyKickParticle = rootObj.GetComponent<PlayerMovement>().GetHeavyKickParticle();
                lightKickParticle =  rootObj.GetComponent<PlayerMovement>().GetLightFistParticle();
        }

        private void Update()
        { 
                _collider.enabled = !_actionsInput.GetHit(); 
           
        }

        private void OnTriggerEnter(Collider other)
        { 
                if (other.gameObject.CompareTag(opponentTag))
                {
                        _actionsInput.SetHit(true);
                        DamagePlayer();
                        ShowHeavyKickParticles();
                        ShowHeavyFistParticles();
                        ShowLightKickParticles();
                }
        }

        void ShowHeavyKickParticles()
        {
                if(!EmitHeavyKickFX) return;
                heavyKickParticle.Play();
                Time.timeScale = .3f;
        }
        void ShowHeavyFistParticles()
        {
                if(!EmitFistFX) return;
                heavyFistParticle.Play();
        }
        void ShowLightKickParticles()
        {
              //  if(!EmitLightKickFX) return;
              //  lightKickParticle.Play();
        }
        void DamagePlayer()
        {
                if (transform.root.CompareTag("player1"))
                {
                        Save.ReducePlayer2Health(DamageAmount);
                        if(Save.Player2Timer < 2.0f)
                                Save.Player2Timer += 2.0f;
                }

                if (transform.root.CompareTag("player2"))
                {
                        Save.ReducePlayer1Health(DamageAmount);
                        if (Save.Player1Timer < 2.0f)
                                Save.Player1Timer += 2.0f;
                }


        }
}
