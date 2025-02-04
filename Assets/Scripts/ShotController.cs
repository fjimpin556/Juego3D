using UnityEngine;

public class ShotController : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    Vector3 mouseWorldPosition = Vector3.zero;
    [SerializeField] private Transform spawnBulletPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 screenCenter = new Vector2 (Screen.width/2f, Screen.height/2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(bullet, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
        }


    }
}
