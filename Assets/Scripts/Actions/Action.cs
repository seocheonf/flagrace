using UnityEngine;

public abstract class Action
{
    protected Player player;
    //행동을 하는 주체
    public abstract bool DoAction();
    //선택한 행동에 대한 작업 수행. 명령의 함수

    public Player GetPlayer()
    {
        return player;
    }
    public void SetPlayer(Player player)
    {
        this.player = player;
    }
}
