using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LevelMenuScript : MonoBehaviour
{
    private UIDocument _document;

    private Button _back_button;

    [SerializeField] public GameObject _mainMenu;

    private List<Button> _menuButtons = new List<Button>();
    private List<Button> _levelButtons = new List<Button>();
    private Dictionary<Button, EventCallback<ClickEvent>> _buttonCallbacks = new Dictionary<Button, EventCallback<ClickEvent>>();

    [SerializeField] AudioClip ButtonClick;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        // _mainMenu = GameObject.Find("MainMenu");

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();

        _levelButtons.Add(_document.rootVisualElement.Q("Tutorial") as Button);

        _back_button = _document.rootVisualElement.Q("Back") as Button;
        _back_button.RegisterCallback<ClickEvent>(OnBackClick);

        // find level Buttons
        for (int i = 1; i <= 5; i++)
        {
            _levelButtons.Add(_document.rootVisualElement.Q(i.ToString()) as Button);
        }

        // register level Buttons and store callbacks
        for (int i = 0; i < _levelButtons.Count; i++)
        {
            int levelIndex = i + 2; // Capture the current loop variable
            var callback = new EventCallback<ClickEvent>(evt => OnLevelClick(evt, levelIndex));
            _levelButtons[i].RegisterCallback(callback);
            _buttonCallbacks[_levelButtons[i]] = callback;
        }

        // all buttons
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }

        //_start_button = _document.rootVisualElement.Q("StartGameButton") as Button;
        //_start_button.RegisterCallback<ClickEvent>(OnStartGameClick);
    }

    private void OnDisable()
    {
        // Unregister back button callback
        if (_back_button != null)
        {
            _back_button.UnregisterCallback<ClickEvent>(OnBackClick);
        }

        // Unregister level button callbacks
        foreach (var kvp in _buttonCallbacks)
        {
            kvp.Key.UnregisterCallback(kvp.Value);
        }
        _buttonCallbacks.Clear();

        // Unregister general menu button callbacks
        foreach (var button in _menuButtons)
        {
            button.UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnLevelClick(ClickEvent evt, int levelIndex)
    {
        Debug.Log($"Level {levelIndex} clicked");
        GlobalSceneManager.INSTANCE.ScheduleTransitionToScene(levelIndex);
    }

    private void OnBackClick(ClickEvent evt)
    {
        Debug.Log("You Pressed the Back Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {
        SoundManager.Instance.Play(ButtonClick);
    }
}