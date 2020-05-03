using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class cpManager : MonoBehaviour
{
    public PlayerProfile playerProfile;
    public GameObject priestButton;
    public GameObject rogueButton;
    public GameObject thaumaturgeButton;
    public GameObject warriorButton;
    public GameObject demonkinButton;
    public GameObject paladinButton;
    public GameObject contentObj;

    public List<GameObject> characterImgs;

    public GameObject changePanel;

    public int changeIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerProfile = GameObject.Find("PlayerProfile").GetComponent<PlayerProfile>();
        for (int i = 0; i < playerProfile.party.Count; i++)
        {
            characterImgs[i].GetComponent<Image>().sprite = playerProfile.party[i].GetComponent<Character>().icon;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void goToFight()
    {
        Debug.Log("click");
        SceneManager.LoadScene("fightingScene");
    }

    public void openChange()
    {
        changePanel.SetActive(true);
    }

    public void SetIndex0()
    {
        changeIndex = 0;
    }
    public void SetIndex1()
    {
        changeIndex = 1;
    }
    public void SetIndex2()
    {
        changeIndex = 2;
    }
    public void SetIndex3()
    {
        changeIndex = 3;
    }
}