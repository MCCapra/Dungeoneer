﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
/*
 * Michael Capra
 * Encounter Manager: Manages combat for the dungeon crawl
 * 2/11/2020
 */

enum Phases
{
    Action,
    Damage,
    Cleanup
}
class HI : IComparer<Entity>
{
    public int Compare(Entity x, Entity y)
    {
        if (x.speed == y.speed)
        {
            return 0;
        }

        return x.speed.CompareTo(y.speed);
    }
}


public class EncounterManager : MonoBehaviour
{
    [Header("Menu GameObjects")]
    [SerializeField] private GameObject baseMenu;
    [SerializeField] private GameObject targetMenu;
    [SerializeField] private GameObject skillMenu;
    [SerializeField] private GameObject infoPanel;


    private enum MenuState { Base, Skills, Target, Other }
    private MenuState currentMenu = MenuState.Base;

    [Header("Entities")]
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<GameObject> allies; //Player party

    [SerializeField] private Stack<GameObject> actionStack; //This is will be determined by going through every gameobject in play, getting their speed, and then adding them to this.
    private bool endofturn;

    private Entity actor;
    private Action inProgress;

    public List<Vector3> enemyPositions; //positions for enemies on canvas
    public List<Vector3> allyPositions; //positions for allies on canvas


    [SerializeField] private List<GameObject> hpBars;

    [SerializeField] private List<GameObject> enemyHpBars;

    [SerializeField] private List<GameObject> possibleEnemies;

    [SerializeField] private GameObject statusList;
    [SerializeField] private GameObject statList;
    [SerializeField] private GameObject charImg;
    [SerializeField] private GameObject charName;

    [SerializeField] private GameObject skillInfoTxt;



    // Start is called before the first frame update
    void Start()
    {
        actionStack = new Stack<GameObject>();

        foreach(GameObject a in allies)
        {
            a.GetComponent<Entity>().OnSpawn();
        }
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTurn(Entity ent)
    {
        actor = ent;
        inProgress = null;

        if(actor.silenced)
        {
            baseMenu.transform.GetChild(1).GetComponent<Button>().interactable = false;
        }

        if (actor is Enemy)
        {
            if(actor.silenced)
            {
                DeclareAction(actor.basicAttack);
                ChooseTarget(Random.Range(0, allies.Count));
            }
            else
            {
                DeclareAction(actor.gameObject.GetComponent<Enemy>().chooseAction());
                ChooseTarget(Random.Range(0, allies.Count));
            }
        }
        else
        {
            ShowBaseMenu();
            currentMenu = MenuState.Base;

            Debug.Log(ent.e_name + ": " + ent.hitpoints);

            baseMenu.transform.GetChild(3).GetComponent<Text>().text = ent.GetComponent<Entity>().e_name + "'s Turn";
        }
    }

    public void DeclareAction(Action a)
    {
        inProgress = a;

        if(!actor.taunted) actor.target = null; // should be deprecated when a taunt status effect is added

        switch (a.TargetType)
        {
            case Action.TargetingType.Single:
                //Toggle correct menu
                ShowTargetMenu(a.TargetEnemy); //change this to be set by the action chosen
                break;
            case Action.TargetingType.None:
            case Action.TargetingType.All:
                ResolveTurn();
                break;
            default:
                break;
        }
    }

    public void ChooseTarget(int index)
    {
        Entity t = null;

        t = actor.target;

        if (index < allies.Count && actor.target == null)
        {
            t = allies[index].GetComponent<Character>();
        }
        else if (index < allies.Count + enemies.Count && actor.target == null)
        {
            t = enemies[index - allies.Count].GetComponent<Enemy>();
        }
        else
        {
            // explode
        }

        //last check just in case
        if (t != null)
        {
            actor.target = t;
            ResolveTurn();
        }
    }

    public void ResolveTurn()
    {
        switch (inProgress.TargetType)
        {
            case Action.TargetingType.None:
                inProgress.AppliedEffect.OnEffectApplied(actor, actor);
                actor.ApplyEffect(inProgress.AppliedEffect);
                break;
            case Action.TargetingType.Single:
                inProgress.AppliedEffect.OnEffectApplied(actor, actor.target);
                actor.target.ApplyEffect(inProgress.AppliedEffect);
                break;
            case Action.TargetingType.All:
                // TODO: apply to all enemies
                if(inProgress.TargetEnemy)
                {
                    for(int i = 0; i < enemies.Count; i++)
                    {
                        inProgress.AppliedEffect.OnEffectApplied(actor, enemies[i].GetComponent<Enemy>());
                        enemies[i].GetComponent<Enemy>().ApplyEffect(inProgress.AppliedEffect);
                    }
                }
                else
                {
                    foreach (GameObject e in allies)
                    {
                        inProgress.AppliedEffect.OnEffectApplied(actor, e.GetComponent<Character>());
                        e.GetComponent<Enemy>().ApplyEffect(inProgress.AppliedEffect);
                    }
                }
                break;
            default:
                break;
        }


        actor.OnEndOfTurn();

        // update UI
        UpdateUI();

        baseMenu.transform.GetChild(1).GetComponent<Button>().interactable = true;

        if (enemies.Count <= 0)
        {
            Respawn();
        }
        
        //Skip over stunned and dead people
        while(actionStack.Peek().GetComponent<Entity>().hitpoints <= 0 || actionStack.Peek().GetComponent<Entity>().stunned)
        {
            if(actionStack.Peek().GetComponent<Entity>().hitpoints <= 0 && actionStack.Peek().GetComponent<Entity>() is Enemy)
            {
                GameObject.Find("Player Profile").GetComponent<PlayerProfile>().gold += actionStack.Peek().GetComponent<Enemy>().GiveGold();
            }

            actionStack.Pop();
        }


        //Start next turn
        if(actionStack.Count <= 0)
        {
            Initiative();
        }
        else
        {
            StartTurn(actionStack.Pop().GetComponent<Entity>());
        }
    }

    public void ShowBaseMenu()
    {
        CloseInfo();
        targetMenu.SetActive(false);
        skillMenu.SetActive(false);
        baseMenu.SetActive(true);
        currentMenu = MenuState.Base;
        baseMenu.transform.GetChild(0).GetComponent<ActionButton>().SetAction(actor.basicAttack); // attack button
    }
    public void ShowTargetMenu(bool targetEnemies)
    {
        CloseInfo();
        targetMenu.SetActive(true);
        skillMenu.SetActive(false);
        baseMenu.SetActive(false);
        currentMenu = MenuState.Target;

        List<GameObject> potentialTargets;
        int firstIndex = 0;
        if (targetEnemies)
        {
            potentialTargets = enemies;
            firstIndex = allies.Count;
        }
        else
        {
            potentialTargets = allies;
        }

        for (int i = 0; i < targetMenu.transform.childCount - 2; i++)
        {
            if (i >= potentialTargets.Count)
            {
                // disable the button if it doesn't correlate to something
                targetMenu.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                targetMenu.transform.GetChild(i).gameObject.SetActive(true);

                GameObject obj = potentialTargets[i];

                Entity e = obj.GetComponent<Entity>();
                TargetButton button = targetMenu.transform.GetChild(i).GetComponent<TargetButton>();
                button.GetComponentInChildren<Text>().text = e.e_name;
                button.SetIndex(firstIndex + i);
            }
        }
    }

    public void ShowSkillMenu()
    {
        CloseInfo();
        targetMenu.SetActive(false);
        skillMenu.SetActive(true);
        baseMenu.SetActive(false);
        currentMenu = MenuState.Skills;

        skillInfoTxt.GetComponent<Text>().text = "";

        for(int i = 0; i < skillMenu.transform.childCount - 3; i++)
        {
            if(i >= actor.skills.Count)
            {
                skillMenu.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                skillMenu.transform.GetChild(i).gameObject.SetActive(true);

                Action a = actor.skills[i];

                ActionButton button = skillMenu.transform.GetChild(i).GetComponent<ActionButton>();
                button.GetComponentInChildren<Text>().text = a.a_name;
                button.SetAction(a);
            }
        }
    }

    public void ShowInfo()
    {
        infoPanel.SetActive(true);
        charName.GetComponent<Text>().text = actor.e_name;
        charImg.GetComponent<Image>().sprite = actor.icon;
        statList.GetComponent<Text>().text = "Stats\nHP: " + actor.hitpoints + "/" + actor.maxHitpoints + "\nATK: " + actor.attack + "\nDEF: " + actor.defense + "\nM.ATK: " + actor.magic + "\nM.DEF: " + actor.magDefense + "\nSPD: " + actor.speed;
        statusList.GetComponent<Text>().text = "";
        /*for (int i = 0; i < 4; i++)
        {
            for(int j = 0; j < actor.StatusList[j].Count; j++)
            {
                statusList.GetComponent<Text>().text = actor.StatusList[i][j];
            }
            
        }*/
    }

    public void CloseInfo()
    {
        infoPanel.SetActive(false);
    }

    private void Initiative()
    {
        List<Entity> holder = new List<Entity>();
        actionStack.Clear();

        foreach (GameObject ally in allies)
        {
            holder.Add(ally.GetComponent<Character>());
        }

        foreach (GameObject enemy in enemies)
        {
            holder.Add(enemy.GetComponent<Enemy>());
        }
        //sort by initiative
        HI sorter = new HI();

        holder.Sort(sorter);

        foreach (Entity e in holder)
        {
            actionStack.Push(e.gameObject);
        }

        //Debug.Log(actionStack.Peek().name);

        StartTurn(actionStack.Pop().GetComponent<Entity>());
    }

    private void UpdateUI()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy e = enemies[i].GetComponent<Enemy>();
            if (e.hitpoints <= 0)
            {
                GameObject eN = enemies[i];
               
                Debug.Log(enemies.Remove(enemies[i]));
                Destroy(eN);
                enemyHpBars.Remove(enemyHpBars[i]);
                return;
            }
            // Debug.Log(enemies[i].name);
            GameObject enemy = enemyHpBars[i];//GameObject.Find("Canvas/Panel/" + enemies[i].name + "/HPbar/bar1");
            enemy.GetComponent<RectTransform>().localScale = new Vector3((float)e.hitpoints / e.maxHitpoints, 1, 1);
        }

        for (int i = 0; i < allies.Count; i++)
        {
            Character a = allies[i].GetComponent<Character>();
            hpBars[i].transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3((float)a.hitpoints / a.maxHitpoints, 1, 1);

            //update names if needed
            hpBars[i].transform.GetComponentInChildren<Text>().text = a.e_name;
        }
    }

    private void Respawn()
    {
        List<GameObject> newEnemies = new List<GameObject>();
        List<GameObject> newEnemyBars = new List<GameObject>();

        enemies.Clear();
        for(int i = 0; i < 4; i++)
        {
            int randNum = Random.Range(0, possibleEnemies.Count-1);
            newEnemies.Add(GameObject.Instantiate(possibleEnemies[randNum], GameObject.Find("Canvas/Panel").transform));
            newEnemies[i].GetComponent<RectTransform>().localPosition = enemyPositions[i];
            newEnemies[i].GetComponent<Entity>().OnSpawn();
            newEnemyBars.Add(newEnemies[i].transform.GetChild(0).GetChild(0).gameObject);

        }
        enemies = newEnemies;
        enemyHpBars = newEnemyBars;

        actionStack.Clear();

        Initiative();
        UpdateUI();
    }
}
