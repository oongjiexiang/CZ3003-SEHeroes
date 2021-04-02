using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 7.0f;
    public bool moveable;
    
    float horizontal;
    float vertical;

    // need to pass from API
    private string username = ProgramStateController.username;
    private string characterType = ProgramStateController.characterType;

    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if(characterType.Equals("RedWarrior"))
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/RedWarrior");
        else if(characterType.Equals("Magician"))
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Magician");
        else if(characterType.Equals("Bowman"))
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Bowman");
        else if(characterType.Equals("Swordman"))
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Swordman");
    }

    // Update is called once per frame
    void Update()
    {
        if(moveable){
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            
            Vector2 move = new Vector2(horizontal, vertical);
            
            if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize();
            }
            
            animator.SetFloat("Look X", lookDirection.x);
            animator.SetFloat("Look Y", lookDirection.y);
            animator.SetFloat("Speed", move.magnitude);
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C key is pressed!");
            animator.Play("Attack");
        }
    }
    
    void FixedUpdate()
    {
        if(rigidbody2d) {
            if(moveable){
                Vector2 position = rigidbody2d.position;
                position.x = position.x + speed * horizontal * Time.deltaTime;
                position.y = position.y + speed * vertical * Time.deltaTime;

                rigidbody2d.MovePosition(position);
            }
        }
    }
}
