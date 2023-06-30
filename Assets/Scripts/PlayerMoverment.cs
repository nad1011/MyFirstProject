using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMoverment : MonoBehaviour
{
    [SerializeField] private CharacterController2D Controller;
    [SerializeField] private Animator animator;
    [SerializeField] private float runSpeed;

    float horizontalMove = 0f;
    bool jump = false;
    bool roll = false;

    private State state;
    private enum State {
        Normal,
        Roll
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Normal;  
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case State.Normal:
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
                animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

                if (Input.GetKeyDown(KeyCode.W)) {
                    jump = true;
                    animator.SetBool("IsJumping", true);
                }
                if (Input.GetKeyDown(KeyCode.Space) && !jump) {
                    roll = true;
                }
                break;
        }
    }

    public void OnLanding () {
        animator.SetBool("IsJumping", false);
    }

    private void FixedUpdate() {
        Controller.Move(horizontalMove * Time.fixedDeltaTime,ref roll, jump);
        jump = false;
    }
}
