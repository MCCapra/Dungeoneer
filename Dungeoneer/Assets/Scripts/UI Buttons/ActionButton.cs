using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private Action action;
    public EncounterManager manager;
    [SerializeField] private GameObject skillInfo;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<EncounterManager>();
    }

    public void SetAction(Action a)
    {
        action = a;
    }
    public void Clicked()
    {
        manager.DeclareAction(action);
    }

    public void UpdateSkillInfo()
    {
        skillInfo.GetComponent<Text>().text = action.description;
    }

}
