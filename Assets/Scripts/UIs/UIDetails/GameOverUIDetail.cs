using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIDetail : MonoBehaviour
{
    [SerializeField]
    Button gameOverButton;
    [SerializeField]
    Animator gameOverAnimator;
    [SerializeField]
    RuntimeAnimatorController animatorController;

    private void Awake()
    {
        gameOverButton.onClick.AddListener(() => {ControllerManager.ControllerManagerInstance.GameOverNextScene(); });
        gameOverAnimator.runtimeAnimatorController = animatorController;
    }

    public void StartGameOverAnimation(int characterIndex)
    {
        gameOverAnimator.SetInteger("Character", characterIndex);
    }

}
