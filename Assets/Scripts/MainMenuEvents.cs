using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _start_button;
    private Button _levels_button;

    private List<Button> _menuButtons = new List<Button>();

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();

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
        GlobalSceneManager.INSTANCE.TransitionToScene(2);
	}

    private void OnLevelsClick(ClickEvent evt)
    {
        Debug.Log("You Pressed the Levels Button");
        // TODO: Transition to some scene
    }

    private void OnAllButtonsClick(ClickEvent evt)
    {

        _audioSource.Play();
    }

}
