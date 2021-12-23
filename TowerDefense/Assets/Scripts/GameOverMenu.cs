using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public GameObject panelSaveScore;
    public GameObject textError;
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
        textError.SetActive(false);
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

        UnityWebRequest www = UnityWebRequest.Post(Helper.base_url + Helper.AddPlayer, form);
        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success)
        {
            OnClickMainMenu();
        }
        else
        {
            Debug.Log(www.error);
            textError.SetActive(true);
        }
        
    }

    public void ClosePanelSaveScore()
    {
        panelSaveScore.gameObject.SetActive(false);
        btnSaveScore.enabled = true;
        textError.SetActive(false);
    }
}
