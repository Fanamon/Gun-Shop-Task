using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartSceneScript : MonoBehaviour
{
    [SerializeField] private Button _restartSceneButton;

    private void OnEnable()
    {
        _restartSceneButton.onClick.AddListener(OnRestartSceneButtonClick);
    }

    private void OnDisable()
    {
        _restartSceneButton.onClick.RemoveListener(OnRestartSceneButtonClick);
    }

    private void OnRestartSceneButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
