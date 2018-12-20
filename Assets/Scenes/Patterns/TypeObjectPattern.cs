using UnityEngine;

public class TypeObjectPattern : MonoBehaviour
{
    private void Start()
    {
        Breed troll = new Breed(null, 25, "Troll hit");
        Breed trollArch = new Breed(troll, 0, "Empty");
        Breed trollWizard = new Breed(troll, 1000, "Knife");

        Monster trollMonster = troll.NewMonster();
        trollMonster.ShowAtack();
    }
}

public class Breed
{
    private int health;
    private string atack;
    private Breed _parent;

    public Breed(Breed parent, int health, string atack)
    {
        this.health = health;
        this.atack = atack;
        _parent = null;

        if (parent != null)
        {
            _parent = parent;
            if (health == 0)
            {
                health = parent.GetHealth();
            }

            if (atack == null)
            {
                atack = parent.GetAtack();
            }
        }
    }

    public Monster NewMonster()
    {
        return new Monster(this);
    }

    public int GetHealth()
    {
        return health;
    }

    public string GetAtack()
    {
        return atack;
    }
}

public class Monster
{
    private int _health;
    private string _atack;
    private Breed _breed;

    public Monster(Breed breed)
    {
        _health = breed.GetHealth();
        _atack = breed.GetAtack();
        _breed = breed;
    }

    public string GetAtack()
    {
        return _atack;
    }

    public int GetHealth()
    {
        return _health;
    }

    public void ShowAtack()
    {
        Debug.Log(_atack);
    }
        
}