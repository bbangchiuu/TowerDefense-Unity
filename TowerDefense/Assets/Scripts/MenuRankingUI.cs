using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MenuRankingUI : MonoBehaviour
{
    [SerializeField]
    PlayerRankingUI playerRankingUIPrefab;
    [SerializeField]
    RectTransform listingContainer;
    [SerializeField]
    GameObject textError;

    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        textError.SetActive(false);
    }

    public void GetRankingScore()
    {
        canvas.enabled = true;
        textError.SetActive(false);
        StartCoroutine(GetRequest());
    }

    IEnumerator GetRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get(Helper.base_url + Helper.GetRanking);
        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.Success)
        {
            Ranking[] rankings = JsonHelper.getJsonArray<Ranking>(www.downloadHandler.text);
            for (int i = 0; i < rankings.Length; i++)
            {
                PlayerRankingUI playerRankingUI = Instantiate(playerRankingUIPrefab, listingContainer);
                playerRankingUI.txtName.text = (i + 1) + ". " + rankings[i].name;

                playerRankingUI.txtScore.text = rankings[i].score + "";
                listingContainer.sizeDelta = new Vector2(listingContainer.sizeDelta.x, listingContainer.sizeDelta.y + 110);
            }
        }
        else
        {
            Debug.Log(www.error);
            textError.SetActive(true);
        }
    }

    public void CloseRankingUI()
    {
        canvas.enabled = false;
    }
}
