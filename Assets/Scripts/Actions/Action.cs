using UnityEngine;

public abstract class Action
{
    protected Player player;
    //�ൿ�� �ϴ� ��ü
    public abstract bool DoAction();
    //������ �ൿ�� ���� �۾� ����. ����� �Լ�

    public Player GetPlayer()
    {
        return player;
    }
    public void SetPlayer(Player player)
    {
        this.player = player;
    }
}
