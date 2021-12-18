using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public GameObject panelSaveScore;
    public Text textScore;

    public int playerScore;
    public string playerName;
    public Button btnSaveScore;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        ClosePanelSaveScore();
        btnSaveScore.enabled = true;
    }

    public void GameOver(int currentScore)
    {
        playerScore = currentScore;
        textScore.text = playerScore + "";
        gameObject.SetActive(true);
    }

    public void OnClickMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Helper.menuScene);
    }

    public void ChangePlayerName(string currentName)
    {
        playerName = currentName;
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveScore()
    {
        if(playerName.Length == 0)
        {
            return;
        }
        btnSaveScore.enabled = false;
        StartCoroutine(PostRequest(playerName, playerScore));
    }

    public void ActivePanelSaveScore()
    {
        panelSaveScore.SetActive(true);
    }

    IEnumerator PostRequest(string name, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("score", score);
        WWW www = new WWW(Helper.base_url + Helper.AddPlayer, form);
        yield return www;
        OnClickMainMenu();
    }

    public void ClosePanelSaveScore()
    {
        panelSaveScore.gameObject.SetActive(false);
    }
}
