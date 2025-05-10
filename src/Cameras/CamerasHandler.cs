using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using Board;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Clase encargada de manejar cuando se activan y desactivan las camaras, inicializarlas y jugar con ellas. 
/// </summary>
public class CamerasHandler : MonoBehaviour
{
    [Header("POST PROCESSING")]
    [SerializeField] private Volume volume;
    private DepthOfField depthOfField;

    [Header("BOARD REFERENCE")]
    [Space(10)]
    [SerializeField] private BoardController board;
    [SerializeField] private TurnController turn;

    [Header("CAMERA SETTINGS")]
    [Space(10)]
    [SerializeField] private float fieldOfView = 60f;
    [SerializeField] private int maxPriority;
    [Space(5)]
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Vector3 trackedOffsetPosition;

    [Header("SPECIFIC CAMERAS")]
    [Space(10)]
    [SerializeField] private CinemachineVirtualCamera mOverheadViewCamera;

    
    private GameObject parentCameras; // lugar donde se meter�n todas las camaras que se creen
    [HideInInspector] public List<GameObject> cameras; // listado de camaras generadas

    private void OnEnable()
    {
        //board.OnStartMove += ChangeViewCamera;
        turn.OnChangeTurn += ChangeViewCamera;
        board.OnEnterEvent += ActivateOverheadCameraEvent;
        //board.OnPlayerRouteChoice += ActivateOverheadCameraPlayer;
    }

    private void OnDisable()
    {
        //board.OnStartMove -= ChangeViewCamera;
        turn.OnChangeTurn -= ChangeViewCamera;
        board.OnEnterEvent -= ActivateOverheadCameraEvent;
        //board.OnPlayerRouteChoice -= ActivateOverheadCameraPlayer;
    }

    private void Start()
    {
        Camera.main.GetUniversalAdditionalCameraData().renderPostProcessing = PlayerPrefs.GetInt("PostProcessing") == 1;
        CreateCameras();
    }

    /// <summary>
    /// Funcion encargada de crear virtual cameras de cinemachine por cada jugador instanciado en la partida
    /// </summary>
    private void CreateCameras()
    {
        ushort nCameras = board.GetCountPlayers(); 

        parentCameras = new GameObject("Cameras");

        GameObject cameraTemplate = new GameObject();
        
        // se settea la rotacion inicial de dicha camara a una indicada desde el inspector
        cameraTemplate.transform.eulerAngles = rotation;

        // recorremos una vez por cada jugador instanciado en partida para poder crear de forma independiente un virtual camera por cada uno
        for (int i = 0; i < nCameras; i++)
        {
            GameObject newCamera = Instantiate(cameraTemplate);
            newCamera.name = "VC - Player " + (i + 1);

            // asignar un virtual camera al objeto creado
            CinemachineVirtualCamera cameraSettings = newCamera.AddComponent<CinemachineVirtualCamera>();

            // settear la prioridad. para evitar problemas de prioridad, hacemos que la primera camara creada tenga mas que las demas
            cameraSettings.Priority = (maxPriority < nCameras ? nCameras : maxPriority) - i;

            // settear el target que seguira la camara
            cameraSettings.Follow = board.GetPlayer(i).transform;
            
            // settear el target que mirara la camara
            cameraSettings.LookAt = board.GetPlayer(i).transform;

            // settear el field of view de preferencia para cada camara
            cameraSettings.m_Lens.FieldOfView = fieldOfView;

            cameraSettings.AddCinemachineComponent<CinemachineHardLockToTarget>();
            
            CinemachineCameraOffset cinemachineCameraOffset = newCamera.AddComponent<CinemachineCameraOffset>();
            
            cinemachineCameraOffset.m_Offset = trackedOffsetPosition;

            newCamera.transform.SetParent(parentCameras.transform);
        }

        // una vez creadas las camaras, las insertamos en un objeto vacio para tenerlas organizadas en el inspector
        foreach (Transform camera in parentCameras.transform)
        {
            cameras.Add(camera.gameObject);
        }
    }

    /// <summary>
    /// Funci�n encargada de cambiar de camara activa. Esto normalmente ocurrira cuando se cambie de turno
    /// </summary>
    private void ChangeViewCamera()
    {
        Debug.Log("CAMBIO CAMARA PARA EL SIGUIENTE JUGADOR");

        int indexToShow = board.GetNextPlayerIndex();

        ResetCameras();

        cameras[indexToShow].SetActive(true);
    }

    /// <summary>
    /// Funcion encargada de resetear todas las camaras, es decir, desactiva todas las camaras para luego en otro contexto,
    /// poder activar una en especifico
    /// </summary>
    private void ResetCameras()
    {
        foreach (GameObject camera in cameras)
        {
            camera.SetActive(false);
        }

        SetDepthOfField(false);
    }

    /// <summary>
    /// Funcion encargada de activar de forma directa la camara que muestra una vista desde arriba del mapa
    /// </summary>
    /// <param name="player"></param>
    private void ActivateOverheadCameraPlayer(Player player)
    {
        ResetCameras();

        mOverheadViewCamera.Follow = player.gameObject.transform;
        mOverheadViewCamera.gameObject.SetActive(true);
    }

    private void ActivateOverheadCameraEvent()
    {
        ResetCameras();

        mOverheadViewCamera.gameObject.SetActive(true);
        SetDepthOfField(true);
    }

    private void SetDepthOfField(bool active)
    {
        if (!volume.profile.TryGet(out depthOfField)) return;

        depthOfField.active = active;
    }
}
