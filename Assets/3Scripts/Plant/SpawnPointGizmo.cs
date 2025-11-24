using UnityEngine;

public class SpawnPointGizmo : MonoBehaviour
{
    // Gizmo의 색상을 설정합니다.
    public Color gizmoColor = Color.red;
    // Gizmo의 크기를 설정합니다.
    public float gizmoSize = 0.3f;

    // Gizmos를 그리는 함수입니다.
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor; // Gizmo의 색상을 설정합니다.
        Gizmos.DrawSphere(transform.position, gizmoSize); // Gizmo를 구 형태로 그립니다.
    }
}
