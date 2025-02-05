public enum ExistenceType
{
    TURN_PLAYER,
    OTHER_PLAYER,
    TRAP,
    PANEL,
}

public enum PanelType
// �г� Ÿ��    
{
    EMPTY,
    EVENT,
    CHECKPOINT,
}

public enum PanelLine
// �ش� �г��� ���忡�� � ���ο� �ִ°�
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
}

public enum CardType
// ī�� Ÿ��
{
    CHARACTER,
    NEUTRAL,
}

public enum State
// ĳ���� ����
{
    NORMAL,
    MOVE,
    STUN,
    FAST,
    SLOW,
    TRAP_PROTECTED,
    PROTECTED,
    JUST_MOVE,

    LENGTH
}

public enum MovePath
// �̵� ���
{
    RUN,
    TELEPORT,
}
public enum MoveTool
// �̵� ����
{
    CARD,
    DICE,
    EFFECT,
}

public enum Timing
// ȿ���ߵ� ������ üũ�ϴ� "��"
{
    CARD_USE_START,
    CARD_USE_END,
    CARD_MOVE_BEFORE,
    CARD_MOVE_AFTER,
    DICE_MOVE_BEFORE,
    DICE_MOVE_AFTER,
    EFFECT_MOVE_BEFORE,
    EFFECT_MOVE_AFTER,
    EFFECT_USE_END,
    MOVE_ONESTEP_BEFORE,
    MOVE_ONESTEP_AFTER,
    MOVE_ONESTEP_FAIL,
    MOVE_JUST_BEFORE,
    TURN_END,
}

public enum Phase
// �� ���� ������
{
    TURN_START,
    DRAW,
    BEHAVIOUR,
    TURN_END,
}


/*
public enum AnimationType
{
    ROLL_DICE,
    SET_PLAYER,
    MOVE_RUN_PLAYER,
    MOVE_TELEPORT_PLAYER
}
*/