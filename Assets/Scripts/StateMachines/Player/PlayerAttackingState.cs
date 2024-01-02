using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;
    private Attack attack;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }


    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();
        
        float NormalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if(NormalizedTime >= previousFrameTime && NormalizedTime < 1f)
        {
            if(NormalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }

            if(stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(NormalizedTime);
            }
        }

        else 
        {
            if(stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }

            else 
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }

        previousFrameTime = NormalizedTime;
    }

    public override void Exit()
    {

    }
    private void TryComboAttack(float NormalizedTime)
    {
        if(attack.ComboStateIndex == -1) { return;}

        if(NormalizedTime < attack.ComboAttackTime) { return;}
        
        stateMachine.SwitchState
        (
            new PlayerAttackingState
            (
                stateMachine,
                attack.ComboStateIndex
            )
        );
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);
        
        alreadyAppliedForce = true ;
    }


   
    
}

