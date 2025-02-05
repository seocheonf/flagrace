using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourEnd : Action
{
    // behaviour����� �ݵ�� �÷��̾ ������ ��.
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
