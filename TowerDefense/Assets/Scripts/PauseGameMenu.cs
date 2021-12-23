using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGameMenu : MonoBehaviour
{
    [SerializeField]
    GameObject buttonPause;
    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    Image icon;

    [SerializeField]
    Sprite iconPause, iconPlay;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        pauseMenu.SetActive(false);
        icon.sprite = iconPause;
    }

    public void OnClickPause()
    {
        Time.timeScale = 0;
        icon.sprite = iconPlay;
        pauseMenu.SetActive(true);
    }

    public void OnClickResume()
    {
        Time.timeScale = 1;
        icon.sprite = iconPause;
        pauseMenu.SetActive(false);
    }

    public void OnClickMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Helper.menuScene);
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnApplicationPause(bool pause)
    {
        OnClickPause();
    }
}
