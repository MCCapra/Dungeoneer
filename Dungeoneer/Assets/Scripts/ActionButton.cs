using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private Action action;
    public EncounterManager manager;

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
}
