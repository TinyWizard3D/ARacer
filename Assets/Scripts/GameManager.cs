using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public event EventHandler<OnCarSpawnedEventArgs> OnCarSpawned;
    public class OnCarSpawnedEventArgs : EventArgs
    {
        public ICarControl car;
    }

    public event EventHandler OnMeshingRenderChange;
    
    public static GameManager Instance { get; private set; }

    [SerializeField] private MessagesUI MessagesUI;
    [SerializeField] public GameObject car;
    [SerializeField] private Button createCarButton;
    private GameObject currentCar;
    private ICarControl curCarICarControl;

    private bool hideMesh = true;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        createCarButton.onClick.AddListener(CreateCar);
    }


    public void CreateCar()
    {
        Ray CheckCollisionRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(CheckCollisionRay, out hit) && hit.collider != null)
        {
            if (currentCar != null)
            {
                Destroy(currentCar);
            }

            currentCar = Instantiate(car, new Vector3 (hit.point.x, hit.point.y + 0.2f, hit.point.z), Quaternion.identity);
            curCarICarControl = currentCar.GetComponent<ICarControl>();

            if (curCarICarControl != null) {
                OnCarSpawned?.Invoke(this, new OnCarSpawnedEventArgs {
                    car = curCarICarControl
                });
            }  
        }
        else
        {
            MessagesUI.PlayNoColliderWarning();
        }
    }

    public void ToggleHideMesh()
    {
        hideMesh = !hideMesh;
        OnMeshingRenderChange?.Invoke( this, EventArgs.Empty );
    }

    public bool getMeshRenderState()
    {
        return hideMesh;
    }
}
