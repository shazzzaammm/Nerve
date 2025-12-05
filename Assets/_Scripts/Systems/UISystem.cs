using System;
using System.Collections;
using UnityEngine;

public class UISystem : Singleton<UISystem>
{
    [SerializeField] private GameObject pauseMenu, gameOverScreen;
    public bool paused { get; private set; } = false;
    public bool gameOver { get; private set; } = false;
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<PlayerKilledGA>(PlayerKilledPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<PlayerKilledGA>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private IEnumerator PlayerKilledPerformer(PlayerKilledGA playerKilledGA)
    {
        ActivateGameOverScreen();
        yield return null;
    }

    private void ActivateGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        gameOver = true;
    }

    public void TogglePauseMenu(bool enable)
    {
        pauseMenu.SetActive(enable);
        paused = enable;
    }

    public void TogglePauseMenu()
    {
        TogglePauseMenu(!paused);
    }
}
