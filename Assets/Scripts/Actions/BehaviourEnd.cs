using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourEnd : Action
{
    // behaviour종료는 반드시 플레이어를 가져야 함.
    public BehaviourEnd(Player player)
    {
        this.player = player;
    }

    public override bool DoAction()
    {
        player.DoBehaviourEnd();
        return true;
    }
}
