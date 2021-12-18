using UnityEngine;

public class testgraphic : MonoBehaviour
{
    public Mesh prefab;
    public Material material;
    public GameObject currentPrefab;
    GameObject current;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(current != null)
        {
            current.transform.position = MouseToTerrainPosition();
        }
    }

    public void Onclick()
    {
        Vector3 position = MouseToTerrainPosition();
        //Graphics.DrawMesh(prefab, position, Quaternion.identity, material, 0);
        current = Instantiate(currentPrefab, position, Quaternion.identity);
    }

    public static Vector3 MouseToTerrainPosition()
    {
        Vector3 position = Vector3.zero;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit info, 100, LayerMask.GetMask("Default")))
        {
            position = info.point;
            Debug.Log(position);
        }
        return position;
    }
}
