 using System.Collections;

 interface ICharacterInput
 {
     public void SetWalkRight(bool value);

     public void SetWalkLeft(bool value);
     public bool GetIsInBlock();
     public void SetCanWalkRight(bool canWalk);
     public void SetCanWalkLeft(bool canWalk);
     public bool GetCanWalkRight();
     public bool GetCanWalkLeft();
     public void Lose();
     public IEnumerator Win();
     public bool enabled {get; set;}
     public string GetPlayerName();
     public void ResetPlayer();



 }
