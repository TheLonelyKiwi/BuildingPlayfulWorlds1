using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayercarry
{
    private Rigidbody2D rb;
    private CursorTracker ct;
    private Vector3 mousePos;
    
    //inputs
    public Vector2 MovementInput { get; private set;}
    
    //status bools
    public bool IsMoving { get; set; }
    public bool IsCarrying { get; set; }
    
    //Statemachine variables
    public PlayerStateMachine Statemachine;
    
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerCarryIdle carryIdleState;
    public PlayerCarryMove carryMoveState;
    public PlayerDeadState deadState;

    private void Awake()
    {
        //initializes StateMachine and states
        Statemachine = new PlayerStateMachine();
        
        idleState = new PlayerIdleState(this, Statemachine);
        moveState = new PlayerMoveState(this, Statemachine);
        carryIdleState = new PlayerCarryIdle(this, Statemachine);
        carryMoveState = new PlayerCarryMove(this, Statemachine);
        deadState = new PlayerDeadState(this, Statemachine);
    }
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ct = FindObjectOfType<CursorTracker>();
        
        //starts StateMachine
        Statemachine.StartMachine(idleState);
    }

    private void FixedUpdate()
    {
        RotatePlayer(mousePos);
        
        Statemachine.currentState.UpdateState();
    }

    private void Update()
    {
        mousePos = ct.GetMousePos();
        
        //handles input
        MovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (MovementInput.x != 0 || MovementInput.y != 0)
        {
            SetIsMoving(true);
        }
        else
        {
            SetIsMoving(false);
        }
    }

    public void PickedUpTurret()
    {
        IsCarrying = true; 
    }
    
    public void PlacedTurret()
    {
        IsCarrying = false;
    }
    
    public void MovePlayer(Vector2 movement, float desiredSpeed) //moves player, gets called from active state.
    {
        rb.MovePosition(rb.position + movement * desiredSpeed * Time.deltaTime);
    }

    private void RotatePlayer(Vector2 rotation) //rotates player towards mouse rotation, gets called in Update method
    {
        Vector2 lookDir = rotation - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetIsDead()
    {
        Statemachine.ChangeState(deadState);
    }
    
    public void SetIsMoving(bool isMoving)
    {
        IsMoving = isMoving;
    }

    public void SetIsCarrying(bool isCarrying)
    {
        IsCarrying = isCarrying;
    }
}
