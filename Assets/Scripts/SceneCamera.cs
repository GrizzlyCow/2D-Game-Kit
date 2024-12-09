using Unity.Cinemachine;
using UnityEngine;

public class SceneCamera : MonoBehaviour
{

    [SerializeField] private CinemachineCamera cinemachineCamera;
    
    void Start()
    {
        cinemachineCamera.Follow = NewPlayer.Instance.transform;
    }

    
    void Update()
    {
        
    }
}
