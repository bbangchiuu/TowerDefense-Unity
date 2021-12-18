using System.Collections;
using UnityEngine;

public class MenuRankingUI : MonoBehaviour
{
    [SerializeField]
    PlayerRankingUI playerRankingUIPrefab;
    [SerializeField]
    RectTransform listingContainer;

    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void GetRankingScore()
    {
        canvas.enabled = true;
        StartCoroutine(GetRequest());
    }

    IEnumerator GetRequest()
    {
        WWW www = new WWW(Helper.base_url + Helper.GetRanking);
        yield return www;
        Ranking[] rankings = JsonHelper.getJsonArray<Ranking>(www.text);
        for (int i = 0; i < rankings.Length; i++)
        {
            PlayerRankingUI playerRankingUI = Instantiate(playerRankingUIPrefab, listingContainer);
            playerRankingUI.txtName.text = (i+1) + ". " + rankings[i].name;
            
            playerRankingUI.txtScore.text = rankings[i].score + "";
            listingContainer.sizeDelta = new Vector2(listingContainer.sizeDelta.x, listingContainer.sizeDelta.y + 110);
        }

        
    }

    public void CloseRankingUI()
    {
        canvas.enabled = false;
    }
}
