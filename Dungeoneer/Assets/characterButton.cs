using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterButton : MonoBehaviour
{
    public GameObject character;
    public PlayerProfile playerProfile;

    public List<GameObject> characterImgs;
    public GameObject changePanel;

    public cpManager manager;

    // Start is called before the first frame update
    void Start()
    {
        playerProfile = GameObject.Find("PlayerProfile").GetComponent<PlayerProfile>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParty()
    {
        playerProfile.party[manager.changeIndex] = character;
        characterImgs[manager.changeIndex].GetComponent<Image>().sprite = character.GetComponent<Character>().icon;
        changePanel.SetActive(false);
    }
}
