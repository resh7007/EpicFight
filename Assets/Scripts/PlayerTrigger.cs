using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{ 
        private Collider _collider;

        private ActionsInput _actionsInput;
        private string opponentTag;

        private void Awake()
        {
                _actionsInput = transform.root.GetChild(0).GetComponent<ActionsInput>();
                _collider = GetComponent<BoxCollider>();
                if (transform.root.CompareTag("player1"))
                        opponentTag = "player2";
                if (transform.root.CompareTag("player2"))
                        opponentTag = "player1";
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
                }
        }
}
