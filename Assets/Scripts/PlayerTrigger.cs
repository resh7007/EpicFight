using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
        private Collider _collider;
        private ActionsInput _actionsInput;
        private string opponentTag;
        [SerializeField] private float DamageAmount = .1f;
        [SerializeField] private bool EmitFX = false;
        private ParticleSystem hitParticles;
        private void Awake()
        {
                _actionsInput = transform.root.GetChild(0).GetComponent<ActionsInput>();
                _collider = GetComponent<BoxCollider>();
                if (transform.root.CompareTag("player1"))
                        opponentTag = "player2";
                if (transform.root.CompareTag("player2"))
                        opponentTag = "player1";

                hitParticles = transform.root.GetComponent<PlayerMovement>().GetHitParticle();
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
                        ShowParticles();
                }
        }

        void ShowParticles()
        {
                if(!EmitFX) return;
                hitParticles.Play();
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
