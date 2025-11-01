using UnityEngine;

public class PlayerMoveState : EntityState {
    public PlayerMoveState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {

    }

    public override void Update()
    {
        base.Update();

        //player.dustRunning.Play();


        if (player.moveInput.x == 0 && player.moveInput.y == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.moveInput.y != 0 && player.moveInput.x == 0)
        {
            stateMachine.ChangeState(player.moveState);
        }

        if (player.moveInput.x != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }

        if (player.moveInput.y > 0) 
        {
            stateMachine.ChangeState(player.up_moveState);
        }

        if (player.moveInput.y < 0)
        {
            stateMachine.ChangeState(player.down_moveState);
        }



        player.SetVelocity(player.moveInput.x * player.moveSpeed, player.moveInput.y * player.moveSpeed);


    }

}
