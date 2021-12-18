using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager instance;

    public PlayerManager playerManager;
    public bool isStartGame = false;

    [SerializeField]
    public SpawnEnemies spawnEnemies;
    [SerializeField]
    TowerPlaceConfirmation towerPlaceConfirmation;
    [SerializeField]
    public TowerControllerUI TowerControllerUI;
    [SerializeField]
    public PauseGameMenu pauseGameMenu;
    [SerializeField]
    public GameOverMenu gameOverMenu;

    [SerializeField]
    Camera mainCamera;

    //set tower radius tower ghost
    GameObject m_RadiusVisualizers;
    public GameObject radiusVisualizerPrefab;
    public float radiusVisualizerHeight = 0.02f;
    //end set tower radius tower ghost

    [HideInInspector]
    public MyTower currentTower;
    [HideInInspector]
    public SpawnTowerBtn buttonBuildCurrent;
    [HideInInspector]
    public MyNodePlace nodePlaceCurrent;

    //[SerializeField]
    //public LayerMask layerPlacementAreas, layerPlacementTower, layerEnviroment, layerPostionTower;
    [HideInInspector]
    public bool isBuildingTower = false;
    //public bool isMoveTower = false;
    //public bool towerPlaceValid = false;

    //public bool isClickConfirm = false;

    [SerializeField]
    ParticleSystem buidPfx;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Exitsts");
            return;
        }
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    public void GameOver(int currentScore)
    {
        Time.timeScale = 0;
        gameOverMenu.GameOver(currentScore);
    }

    public void OnClickStartGame()
    {
        isStartGame = true;
        spawnEnemies.gameObject.SetActive(true);
        pauseGameMenu.gameObject.SetActive(true);
        //playerManager.UpdateHealth(-10);
    }

    public void SelectNodePlace(MyNodePlace myNodePlace)
    {
        nodePlaceCurrent = myNodePlace;
        isBuildingTower = true;
        Vector3 getBuildPosition = nodePlaceCurrent.transform.position + new Vector3(0, 0.25f, 0);
        //Tạo tower base
        currentTower = Instantiate(currentTower, getBuildPosition, Quaternion.identity);

        if (currentTower == null)
        {
            return;
        }

        //tạo tower ghost để xác nhận build hay không
        currentTower.towerGhost = Instantiate(currentTower.towerGhost, currentTower.transform.position, currentTower.transform.rotation);
        currentTower.towerGhost.transform.SetParent(currentTower.transform);

        m_RadiusVisualizers = Instantiate(radiusVisualizerPrefab);
        GameObject radiusVisualizer = m_RadiusVisualizers;
        radiusVisualizer.SetActive(true);
        radiusVisualizer.transform.SetParent(currentTower.towerGhost.transform);
        radiusVisualizer.transform.localPosition = new Vector3(0, radiusVisualizerHeight, 0);
        radiusVisualizer.transform.localScale = Vector3.one * 8 * 2.0f;

        var visualizerRenderer = radiusVisualizer.GetComponent<Renderer>();
        if (visualizerRenderer != null)
        {
            visualizerRenderer.material.color = new Color(0f, 1f, 1f, 0.098f);
        }

        //mở Ui xác nhận build
        towerPlaceConfirmation.ActiveUI();
    }

    //nhận dữ liệu -> sang bên MyNodePlace để chọn vị trí build
    public void SetTowerToBuild(MyTower tower, SpawnTowerBtn spawnTowerBtn)
    {
        if(buttonBuildCurrent != null)
        {
            buttonBuildCurrent.ResetButton();
        }
        buttonBuildCurrent = spawnTowerBtn;
        currentTower = tower;
    }

    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
   
    #region Setup Tower
    //Xác nhận build tower
    public void ConfirmTowerPlace()
    {
        isBuildingTower = false;

        currentTower.SetUpTower(nodePlaceCurrent);

        buttonBuildCurrent.ResetButton();
        Instantiate(buidPfx, currentTower.transform.position, Quaternion.identity);
        playerManager.UpdateEnergy(-currentTower.towerLevels[0].inforTower.enegry);

        buttonBuildCurrent = null;
        currentTower = null;
        nodePlaceCurrent = null;
    }

    public void CloseSetUpTower()
    {
        isBuildingTower = false;
        DestroyImmediate(currentTower.gameObject);
        buttonBuildCurrent.ResetButton();
        buttonBuildCurrent = null;
        nodePlaceCurrent.ResetNodePlace();
        nodePlaceCurrent = null;
    }
    #endregion

    #region ko dung nua
    //void CheckPalceTower()
    //{
    //    bool checkPlacementAreas = CheckPlacementAreas();
    //    bool checkPostionTower = currentTower.GetComponentInChildren<TowerGhost>().checkPositionTower;

    //    if (checkPlacementAreas && !checkPostionTower)
    //    {
    //        currentTower.GetComponentInChildren<TowerGhost>().SetMaterialTowerGhost();
    //        towerPlaceValid = true;
    //    }
    //    else
    //    {
    //        towerPlaceValid = false;
    //        currentTower.GetComponentInChildren<TowerGhost>().SetMaterialTowerGhostInvalid();
    //    }
    //}

    //void MoveTowerGhost()
    //{
    //    if (currentTower != null && isBuilding)
    //    {
    //        if (isMoveTower)
    //        {
    //            currentTower.transform.position = MouseToTerrainPosition();
    //        }
    //        CheckPalceTower();
    //    }
    //}

    //public Vector3 MouseToTerrainPosition()
    //{
    //    Vector3 position = Vector3.zero;
    //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit info, 100, layerEnviroment))
    //    {
    //        position = info.point;
    //    }

    //    return position;
    //}

    //public bool CheckPlacementAreas()
    //{
    //    return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit info, 100, layerPlacementAreas);
    //}

    //public bool CheckPositionTower()
    //{
    //    return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit info, 100, layerPlacementTower);
    //}

    //public void SetupRadiusVisualizers(MyTower tower)
    //{
    //    currentTower = Instantiate(tower, MouseToTerrainPosition(), Quaternion.identity);

    //    isBuilding = true;
    //    isMoveTower = true;

    //    currentTower.towerGhost = Instantiate(currentTower.inforTower.towerGhost, currentTower.transform.position, currentTower.transform.rotation);
    //    currentTower.towerGhost.transform.SetParent(currentTower.transform);

    //    m_RadiusVisualizers = Instantiate(radiusVisualizerPrefab);
    //    GameObject radiusVisualizer = m_RadiusVisualizers;
    //    radiusVisualizer.SetActive(true);
    //    radiusVisualizer.transform.SetParent(currentTower.towerGhost.transform);
    //    radiusVisualizer.transform.localPosition = new Vector3(0, radiusVisualizerHeight, 0);
    //    radiusVisualizer.transform.localScale = Vector3.one * 8 * 2.0f;

    //    var visualizerRenderer = radiusVisualizer.GetComponent<Renderer>();
    //    if (visualizerRenderer != null)
    //    {
    //        visualizerRenderer.material.color = new Color(0f, 1f, 1f, 0.098f);
    //    }

    //    TowerPlaceConfirmation.instance.ActiveUI();
    //}
    #endregion
}
