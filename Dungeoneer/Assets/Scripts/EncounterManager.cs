using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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


    // Start is called before the first frame update
    void Start()
    {
        actionStack = new Stack<GameObject>();
        UpdateUI();
        Initiative();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartTurn(Entity ent)
    {
        actor = ent;
        inProgress = null;
        ShowBaseMenu();
        currentMenu = MenuState.Base;

        baseMenu.transform.GetChild(5).GetComponent<Text>().text = ent.GetComponent<Entity>().e_name + "'s Turn";

        if(actor is Enemy)
        {
            DeclareAction(actor.basicAttack);
            ChooseTarget(Random.Range(0, allies.Count));
        }
    }

    public void DeclareAction(Action a)
    {
        inProgress = a;

        actor.target = null; // should be deprecated when a taunt status effect is added

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
        if (index < allies.Count)
        {
            t = allies[index].GetComponent<Character>();
        }
        else if (index < allies.Count + enemies.Count)
        {
            t = enemies[index - allies.Count].GetComponent<Enemy>();
        }
        else
        {
            // explode
        }

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
                inProgress.AppliedEffect.OnEndOfTurn(actor, actor);
                break;
            case Action.TargetingType.Single:
                inProgress.AppliedEffect.OnEndOfTurn(actor, actor.target);
                break;
            case Action.TargetingType.All:
                // TODO: apply to all enemies
                if(inProgress.TargetEnemy)
                {
                    for(int i = 0; i < enemies.Count; i++)
                    {
                        inProgress.AppliedEffect.OnEndOfTurn(actor, enemies[i].GetComponent<Enemy>());
                    }
                }
                else
                {
                    foreach (GameObject e in allies)
                    {
                        inProgress.AppliedEffect.OnEndOfTurn(actor, e.GetComponent<Entity>());
                    }
                }
                break;
            default:
                break;
        }

        actor.OnEndOfTurn();
        // update UI
        UpdateUI();


        if (actionStack.Count > 0)
        {
            StartTurn(actionStack.Pop().GetComponent<Entity>());
        }
        else
        {
            Initiative();
        }
    }

    public void ShowBaseMenu()
    {
        targetMenu.SetActive(false);
        skillMenu.SetActive(false);
        baseMenu.SetActive(true);
        currentMenu = MenuState.Base;
        baseMenu.transform.GetChild(0).GetComponent<ActionButton>().SetAction(actor.basicAttack); // attack button
    }
    public void ShowTargetMenu(bool targetEnemies)
    {
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
        targetMenu.SetActive(false);
        skillMenu.SetActive(true);
        baseMenu.SetActive(false);
        currentMenu = MenuState.Skills;

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

    private void Initiative()
    {
        List<Entity> holder = new List<Entity>();


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
                Destroy(enemies[i]);
                enemies.Remove(enemies[i]);
                return;
            }
            // Debug.Log(enemies[i].name);
            GameObject enemy = GameObject.Find("Canvas/Panel/" + enemies[i].name + "/HPbar/bar1");
            Debug.Log(enemy);
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
}
