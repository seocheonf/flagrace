using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum TitleState
{
    Input,
    FadeIn,
    FadeOut,
    Scene,
    Rest
}

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager SceneManagerInstance;

    [SerializeField]
    Image fade;
    Color fadeColor;
    [SerializeField]
    SpriteRenderer backGround;
    [SerializeField]
    float fadeDuration;
    float currentFadeTime = 0;

    TitleState titleState;


    private void Awake()
    {
        if (SceneManagerInstance == null)
        {
            SceneManagerInstance = this;
            DontDestroyOnLoad(this.gameObject);
            Initialize();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Initialize()
    {
        int randomImageIndex = Random.Range(1, 13);

        string path = $"Prefabs/BackGrounds/{randomImageIndex}";

        Sprite imageSprite = Resources.Load<Sprite>(path);

        backGround.sprite = imageSprite;

        fadeColor = new Color(0, 0, 0, 0);
        titleState = TitleState.Input;

        wantScene = "MainGame";
    }

    private void Update()
    {
        switch (titleState)
        {
            case TitleState.Input:
                InputProcess();
                break;
            case TitleState.FadeIn:
                FadeInProcess();
                break;
            case TitleState.FadeOut:
                FadeOutProcess();
                break;
            case TitleState.Scene:
                ChangeScene();
                break;
            case TitleState.Rest:
                break;
        }
    }

    void InputProcess()
    {
        if (Input.anyKeyDown)
        {
            titleState = TitleState.FadeIn;
        }
    }

    void FadeInProcess()
    {
        currentFadeTime += Time.deltaTime;
        fadeColor.a = (currentFadeTime / fadeDuration);
        fade.color = fadeColor;

        if(fadeColor.a >= 1)
        {
            currentFadeTime = 0;
            titleState = TitleState.Scene;
        }
    }

    void FadeOutProcess()
    {
        currentFadeTime += Time.deltaTime;
        fadeColor.a = 1 - (currentFadeTime / fadeDuration);
        fade.color = fadeColor;

        if (fadeColor.a <= 0)
        {
            if (wantScene == "Title")
            {
                wantScene = "MainGame";
                titleState = TitleState.Input;
            }
            else
                titleState = TitleState.Rest;

            currentFadeTime = 0;
        }
    }


    string wantScene;

    public void StartChangeScene(string wantScene)
    {
        this.wantScene = wantScene;
        titleState = TitleState.FadeIn;
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(wantScene);
        titleState = TitleState.FadeOut;
    }

}
