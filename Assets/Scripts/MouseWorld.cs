using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;
    
    [SerializeField] private LayerMask mousePlaneLayer;

    private void Awake()
    {
        instance = this;
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayer);

        return raycastHit.point;
    } 
}
