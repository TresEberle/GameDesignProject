using Unity.VisualScripting;
using UnityEngine;

public abstract class EntityState {
    protected StateMachine stateMachine;
    protected string animBoolName;
    protected Player player;

    protected Animator animator;
    protected Rigidbody2D rb;
    protected PlayerInputSet input;

    protected float stateTimer;


    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;


        animator = player.animator;
        rb = player.rb;
        input = player.input;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        if (input.Player.Quest.WasPressedThisFrame()) {
            //Open Quest System 
            Debug.Log("QUEST TAB");
        }

        if (input.Player.MenuPage.WasPressedThisFrame())
        {
            //Open Menu Page System 
            player.isInMenu = !player.isInMenu;
            player.MenuPanel.SetActive(player.isInMenu);

        }

    }


    public virtual void Exit()
    {
        animator.SetBool(animBoolName, false);


    }

    //public bool canDash()
    //{
    //    if (player.WallDetected)
    //    {
    //        return false;
    //    }

    //    if (stateMachine.currentState == player.dashState)
    //    {
    //        return false;
    //    }

    //    return true;
    //}


}
