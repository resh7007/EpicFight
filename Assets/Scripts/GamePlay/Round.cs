using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Round : MonoBehaviour
{
    [SerializeField] private GameObject RoundText;
    [SerializeField] private GameObject FightText;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClipRounds;
    [SerializeField] private AudioClip _audioClipFight;
    [SerializeField] private float PauseTime = 1.0f;

 
    void Start()
    {
        RoundText.gameObject.SetActive(false);
        FightText.gameObject.SetActive(false);
        SetRoundNumber(1);
    }

     public void SetRoundNumber(int roundNum)
    {
        StartCoroutine(RoundSet(roundNum));

    }

    IEnumerator RoundSet(int roundNum)
    {
        yield return new WaitForSeconds(.4f);
        RoundText.gameObject.SetActive(true);
        RoundText.GetComponent<TMP_Text>().text="Round "+roundNum;
        _audioSource.clip=_audioClipRounds[roundNum-1];
        _audioSource.Play();
        yield return new WaitForSeconds(PauseTime);
        RoundText.gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
        FightText.gameObject.SetActive(true);
        _audioSource.clip=_audioClipFight;
        _audioSource.Play();
        yield return new WaitForSeconds(PauseTime);
        FightText.gameObject.SetActive(false);
        Save.TimeOut = false;

    }
 
}
