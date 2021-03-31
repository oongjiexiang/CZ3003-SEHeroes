using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneCharacterController : MonoBehaviour
{   
    
    private string world;
    private string section;
    private string level;
    private string character;

    Dictionary<string, object> redwarrior = new Dictionary<string, object>();
    Dictionary<string, object> magician = new Dictionary<string, object>();
    Dictionary<string, object> bowman = new Dictionary<string, object>();
    Dictionary<string, object> swordman = new Dictionary<string, object>();

    public float maxHealth = 5.0f;
    public float health { get { return currentHealth; }}
    float currentHealth;

    Animator animator;
    
    // Start is called before the first frame update
    void Start() {
        world = ProgramStateController.world;
        section = ProgramStateController.section;
        level = ProgramStateController.level;
        character = ProgramStateController.characterType;

        if(world!=null) {
            if(world.Contains("Forest"))
                GameObject.FindGameObjectWithTag("ForestMusic").GetComponent<MusicController>().StopMusic();
            // else if(world.Contains("Village"))
            //     GameObject.FindGameObjectWithTag("VillageMusic").GetComponent<MusicController>().StopMusic();
            // else if(world.Contains("Snowland"))
            //     GameObject.FindGameObjectWithTag("SnowlandMusic").GetComponent<MusicController>().StopMusic();
            // else if(world.Contains("Desert"))                
            //     GameObject.FindGameObjectWithTag("DesertMusic").GetComponent<MusicController>().StopMusic();
            // else if(world.Contains("Ashland"))    
            //     GameObject.FindGameObjectWithTag("AshlandMusic").GetComponent<MusicController>().StopMusic();
        }


        animator = GetComponent<Animator>();   
        if(character.Equals("RedWarrior"))
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/RedWarrior");
        else if(character.Equals("Magician"))
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Magician");
        else if(character.Equals("Bowman"))
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Bowman");
        else if(character.Equals("Swordman"))
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Swordman");

        redwarrior.Add("Health", 5);
        redwarrior.Add("Damage", 1.5);
        redwarrior.Add("Hit", 0.75);

        magician.Add("Health", 5);
        magician.Add("Damage", 1);
        magician.Add("Hit", 1);
        // Heal Chance = 35%
        magician.Add("HealChance", 35);

        bowman.Add("Health", 5);
        bowman.Add("Damage", 1);
        bowman.Add("Hit", 1);
        // Critical Chance = 35%
        bowman.Add("Crit", 35);
        bowman.Add("CritDamage", 2);

        swordman.Add("Health", 5);
        swordman.Add("Damage", 1);
        swordman.Add("Hit", 0.5);
        
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    
    void FixedUpdate()
    {

    }

    public void Attack() {
        animator.Play("CorrectAttack");
    }

    public void ChangeHealth(float amount)
    {
        if (amount < 0)
        {
            animator.Play("Hit");
        }
        
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
