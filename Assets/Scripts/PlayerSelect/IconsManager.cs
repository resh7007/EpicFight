using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IconsManager : MonoBehaviour
{
    private List<Button> btns = new List<Button>();

    private void Awake()
    {
        AssignPlayerSelectButtonIds(); 
    }

    void AssignPlayerSelectButtonIds()
    {
        Transform child;
        for (int i = 0; i < transform.childCount; i++)
        {  
            child = transform.GetChild(i);
            for (int j = 0; j <child.childCount;j++)
            {

                child.GetChild(j).GetComponent<PlayerSelectButton>().buttonId = i*child.childCount +j;
                child.GetChild(j).name = (i*child.childCount +j).ToString();
                btns.Add(child.GetChild(j).GetComponent<Button>());
            }
        }
    }

    public List<Button> GetSelectionButtons()
    {
        return btns;
    }

}
