using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FriendBattlePlayerController : MonoBehaviourPun {
    
    Animator animator;
    private string character = ProgramStateController.characterType;
    private void Start() {
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