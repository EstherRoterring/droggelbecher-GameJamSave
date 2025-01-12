using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class PuzzleSceneSwitch : MonoBehaviour
{
    private UIDocument _document;

    private Button _start_button;
    private Button _levels_button;

    private List<Button> _menuButtons = new List<Button>();

    [SerializeField] AudioClip ButtonClick;

    private void Awake()
    {
        // _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();

        _start_button = _document.rootVisualElement.Q("next") as Button;
        _start_button.RegisterCallback<ClickEvent>(OnStartGameClick);

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnDisable()
    {
        _start_button.UnregisterCallback<ClickEvent>(OnStartGameClick);
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnStartGameClick(ClickEvent evt)
    {
        Debug.Log("You Pressed the Button");
        GlobalSceneManager.INSTANCE.ScheduleTransitionToScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }


    private void OnAllButtonsClick(ClickEvent evt)
    {

        SoundManager.Instance.Play(ButtonClick);
    }

}
