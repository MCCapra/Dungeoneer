using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public EncounterManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<EncounterManager>();
    }

    public void Clicked()
    {
        manager.ShowBaseMenu();
    }
}
