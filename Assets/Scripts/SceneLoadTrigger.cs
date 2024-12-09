using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private string loadSceneString;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == NewPlayer.Instance.gameObject)
        {
            SceneManager.LoadScene(loadSceneString);
            NewPlayer.Instance.transform.position = GameObject.Find("Spawn Location").transform.position;
        }
    }
}
