using UnityEngine;

//Summary: allows building, selecting and moving turrets. 
//Daan Meijneken, 19/12/2023, BPW1 
public class TurretBuilder : MonoBehaviour
{
    [SerializeField] private Transform turrentPlacementPoint;
    
    //player
    private PlayerController player;
    private TurretManager turretList;
    
    //state variables
    private bool isHovering = false;
    private bool isHoldingTurret = false;
    private bool rbInputFlag = false;
    private GameObject turret;
    
    //turret
    private GameObject selectedTurret; //current turret being hovered over
    
    private GameObject turretInstance; //current turret being held
    private TurretInfo turretIController;
    private ScrapManager scrapManager;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        turretList = FindObjectOfType<TurretManager>();
        scrapManager = FindObjectOfType<ScrapManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && rbInputFlag && !isHovering)
        {
            if (turret != null)
            {
                pickupTurret(turret);
            }
        }
        
        
        if (isHoldingTurret)
        {
            HoldingUpdate();
            return;
        }
        
        if (isHovering)
        {
            HoverUpdate();
        }
    }

    public void SelectNewTurret(int i)
    {
        if (isHovering)
        {
            Destroy(selectedTurret);
            InstantiateTurretHover(turretList.turrets[i]);
        }
        else
        {
            isHovering = true; 
            InstantiateTurretHover(turretList.turrets[i]);
        }
    }
    
    private void InstantiateTurretHover(GameObject newObj)
    {
        selectedTurret = Instantiate(newObj, turrentPlacementPoint.position, turrentPlacementPoint.rotation);
        selectedTurret.GetComponent<TurretInfo>().SetColor(Color.cyan);
        
        //add transparent shader? 
    }
    
    private void pickupTurret(GameObject target)
    {
        isHoldingTurret = true;

        player.IsCarrying = true;

        turretInstance = target.transform.parent.gameObject;
        turretIController = turretInstance.GetComponent<TurretInfo>();
        turretIController.PickupTurret();
    }
    
    private bool CheckForObstacles()
    {
        RaycastHit2D hit = Physics2D.CircleCast(turrentPlacementPoint.position, 0.5f, Vector2.zero, 0f,
            LayerMask.GetMask("Obstacles"));
        if (hit.collider != null)
        {
            Debug.Log("Found wall");
            return false;
        }
        return true;
    }

    private void HoldingUpdate()
    {
        if (Input.GetMouseButton(0) && turretInstance != null && CheckForObstacles())
        {
            isHoldingTurret = false;

            player.IsCarrying = false;
            
            turretIController.DropTurret();
            turretInstance = null;
            turretIController = null;
        }
        else
        {
            turretInstance.transform.position = turrentPlacementPoint.position;
            turretInstance.transform.rotation = turrentPlacementPoint.rotation;
        }
    }
    private void HoverUpdate()
    {
        if (Input.GetMouseButtonDown(0) && CheckForObstacles())
        {
            if (scrapManager.CanAffordTurret())
            {
                selectedTurret.GetComponent<TurretInfo>().InitializeTurret();
                selectedTurret = null; 
                isHovering = false;
            }
            else
            {
                Destroy(selectedTurret);
                isHovering = false;
            }
        }
        else
        {
            if (selectedTurret != null)
            {
                selectedTurret.transform.position = turrentPlacementPoint.position;
                selectedTurret.transform.rotation = turrentPlacementPoint.rotation;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isHovering && other.gameObject.CompareTag("Turret"))
        {
            // bool inTurret
            //add outline shader
            Debug.Log("Found turret");

            rbInputFlag = true;
            turret = other.gameObject;
        }
        else
        {
            rbInputFlag = false; 
        }
    }
}
