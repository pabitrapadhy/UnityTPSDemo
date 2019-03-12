using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 300f;
    [SerializeField]
    private float turnSpeed = 10f;
    
    private CharacterController character;
    private Animator animator;
    
    private void Start()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        var horizontalAxis = Input.GetAxis("Horizontal");
        var verticalAxis = Input.GetAxis("Vertical");
        
        // movement direction
        var movement = new Vector3(horizontalAxis, 0, verticalAxis);

        // get the character controller for simple movement
        character.SimpleMove(movement * Time.deltaTime * speed);
        
        // set the look direction
        if (movement.magnitude > 0)
        {
            // set the look direction
            var lookDirection = Quaternion.LookRotation(movement);
            
            // slowly change the direction
            transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, 
                Time.deltaTime * turnSpeed);    
        }
        
        // set the blend animation idle <-> run
        animator.SetFloat("Speed", movement.magnitude);
    }
}
