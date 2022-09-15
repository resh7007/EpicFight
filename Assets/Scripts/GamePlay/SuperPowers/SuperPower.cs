using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPower : MonoBehaviour
{
    protected int Dir;
    protected string opponent;


    public void SetDir(int _dir)
    {
        Dir = _dir;
    }
    public void SetOpponent(string _opponent)
    {
        opponent = _opponent;
    }
}
