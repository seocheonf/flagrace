public enum ExistenceType
{
    TURN_PLAYER,
    OTHER_PLAYER,
    TRAP,
    PANEL,
}

public enum PanelType
// 패널 타입    
{
    EMPTY,
    EVENT,
    CHECKPOINT,
}

public enum PanelLine
// 해당 패널이 보드에서 어떤 라인에 있는가
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
}

public enum CardType
// 카드 타입
{
    CHARACTER,
    NEUTRAL,
}

public enum State
// 캐릭터 상태
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
// 이동 방법
{
    RUN,
    TELEPORT,
}
public enum MoveTool
// 이동 수단
{
    CARD,
    DICE,
    EFFECT,
}

public enum Timing
// 효과발동 조건을 체크하는 "때"
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
// 턴 진행 페이즈
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