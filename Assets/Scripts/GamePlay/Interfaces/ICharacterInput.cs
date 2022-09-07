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
     public void Win();
     public bool enabled {get; set;}




 }
