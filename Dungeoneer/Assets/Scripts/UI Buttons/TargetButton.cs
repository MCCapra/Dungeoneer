using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetButton : MonoBehaviour
{
    [SerializeField] private int index;
    public EncounterManager manager;
    private void Awake()
    {
        index = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<EncounterManager>();
    }

    public void SetIndex(int val)
    {
        index = val;
    }
    public void Clicked()
    {
        manager.ChooseTarget(index);
    }
}
