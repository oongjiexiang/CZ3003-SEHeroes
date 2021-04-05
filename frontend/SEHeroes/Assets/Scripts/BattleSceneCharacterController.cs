using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneCharacterController : MonoBehaviour
{   
    private string character = ProgramStateController.characterType;

    public static Dictionary<string, object> warrior = new Dictionary<string, object>();
    public static Dictionary<string, object> magician = new Dictionary<string, object>();
    public static Dictionary<string, object> bowman = new Dictionary<string, object>();
    public static Dictionary<string, object> swordman = new Dictionary<string, object>();

    public float maxHealth = 5.0f;
    public float health { get { return currentHealth; }}
    float currentHealth;

    Animator animator;
    
    // Start is called before the first frame update
    void Awake() {
        warrior.Add("Health", 20);
        warrior.Add("Damage", 3);
        warrior.Add("Hit", 2);
        warrior.Add("HealChance", 0f);
        warrior.Add("Crit", 0.10f);
        warrior.Add("CritDamage", 5);

        magician.Add("Health", 20);
        magician.Add("Damage", 2);
        magician.Add("Hit", 2);
        // Heal Chance = 35%
        magician.Add("HealChance", 0.35f);
        magician.Add("Crit", 0f);
        magician.Add("CritDamage", 0);

        bowman.Add("Health", 20);
        bowman.Add("Damage", 2);
        bowman.Add("Hit", 2);
        // Critical Chance = 35%
        bowman.Add("HealChance", 0f);
        bowman.Add("Crit", 0.35f);
        bowman.Add("CritDamage", 4);


        swordman.Add("Health", 20);
        swordman.Add("Damage", 2);
        swordman.Add("Hit", 1);
        swordman.Add("HealChance", 0f);
        swordman.Add("Crit", 0f);
        swordman.Add("CritDamage", 0);
        Debug.Log("Done dict");
    }
    void Start() {
        animator = GetComponent<Animator>();   
        if(character.Equals("Warrior"))
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/RedWarrior");
        else if(character.Equals("Magician"))
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Magician");
        else if(character.Equals("Bowman"))
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Bowman");
        else if(character.Equals("Swordman"))
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Swordman");

    }
}
