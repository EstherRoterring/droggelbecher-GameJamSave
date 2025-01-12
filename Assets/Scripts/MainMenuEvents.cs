using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    [SerializeField] public GameObject _levelMenu;

    private Button _start_button;
    private Button _levels_button;

    private List<Button> _menuButtons = new List<Button>();

    [SerializeField] AudioClip ButtonClick;

    private void Awake()
    {
        _levelMenu.SetActive(false);
        // _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();

        //_levelMenu = GameObject.Find("LevelMenu");

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();

        _start_button = _document.rootVisualElement.Q("StartGameButton") as Button;
        _start_button.RegisterCallback<ClickEvent>(OnStartGameClick);

        _levels_button = _document.rootVisualElement.Q("LevelsButton") as Button;
        _levels_button.RegisterCallback<ClickEvent>(OnLevelsClick);

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        _start_button.UnregisterCallback<ClickEvent>(OnStartGameClick);
        _levels_button.UnregisterCallback<ClickEvent>(OnLevelsClick);
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnStartGameClick(ClickEvent evt)
    {
        Debug.Log("You Pressed the Start Button");
        GlobalSceneManager.INSTANCE.ScheduleTransitionToScene(2);
        gameObject.SetActive(false);
    }

    private void OnLevelsClick(ClickEvent evt)
    {
        Debug.Log("You Pressed the Levels Button");
        _levelMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {

        SoundManager.Instance.Play(ButtonClick);
    }

}
