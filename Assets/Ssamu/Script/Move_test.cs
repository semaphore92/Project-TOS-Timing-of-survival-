using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_test : MonoBehaviour
{
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(moveX, moveY, 0f);

        transform.Translate(moveVector.normalized*Time.deltaTime*5f);

        if (moveX!=0||moveY!=0)
        {
            animator.SetFloat("RunState", 0.5f );
        }
        else
        {
            animator.SetFloat("RunState", 0 );
        }
       
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Attack");
        }
    }
}
