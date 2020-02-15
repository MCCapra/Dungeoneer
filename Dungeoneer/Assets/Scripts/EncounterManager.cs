using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class HI : IComparer<Entity>
{
    public int Compare(Entity x, Entity y)
    {
        if(x.speed == y.speed)
        {
            return 0;
        }

        return x.speed.CompareTo(y.speed);
    }
}


public class EncounterManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<GameObject> allies; //Player party

    [SerializeField] private Stack<GameObject> actionStack; //This is will be determined by going through every gameobject in play, getting their speed, and then adding them to this.
    private bool endofturn;
    /*
     * Michael Capra
     * Encounter Manager: Manages combat for the dungeon crawl
     * 2/11/2020
     */
    // Start is called before the first frame update
    void Start()
    {
        Initiative();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initiative()
    {
        List<Entity> holder = new List<Entity>();

        foreach(GameObject ally in allies)
        {
            holder.Add(ally.GetComponent<Character>());
        }

        foreach(GameObject enemy in enemies)
        {
            holder.Add(enemy.GetComponent<Enemy>());
        }
        //sort by initiative
        HI sorter = new HI();

        holder.Sort(sorter);

        holder.Reverse();
        foreach(Entity e in holder)
        {
            actionStack.Push(e.gameObject);
        }
    }
}
