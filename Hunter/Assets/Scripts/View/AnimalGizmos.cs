using UnityEngine;

public class AnimalGizmos : MonoBehaviour
{
    [SerializeField]
    private float _bodySeekRadius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _bodySeekRadius);
    }
}
