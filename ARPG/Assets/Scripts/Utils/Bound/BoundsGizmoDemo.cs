using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; // Handles.* 사용 (에디터 전용)
#endif

public class BoundsGizmoDemo : MonoBehaviour
{
    [Header("Sphere")]
    public bool showSphere = true;
    public Vector3 sphereCenter = Vector3.zero; // 월드 기준 오프셋
    public float sphereRadius = 1f;
    public bool showSphereAABB = true;

    [Header("Capsule (A-B 축)")]
    public bool showCapsule = true;
    public Transform capA;          // 캡슐 한쪽 끝(반구 중심)
    public Transform capB;          // 캡슐 다른쪽 끝(반구 중심)
    public float capsuleRadius = 0.5f;
    public bool showCapsuleAABB = true;

    [Header("Colors")]
    public Color colorTrue = Color.green;                     // 실제 도형
    public Color colorAABB = new Color(1f, 1f, 0f, 1f);       // Bounds(AABB)

    // --- AABB 유틸 ---
    public static Bounds FromSphere(Vector3 center, float radius)
        => new Bounds(center, Vector3.one * (radius * 2f));

    public static Bounds FromCapsule(Vector3 a, Vector3 b, float radius)
    {
        var box = new Bounds(a, Vector3.zero);
        box.Encapsulate(b);
        box.Expand(radius * 2f); // 모든 축으로 반지름만큼 확장(양쪽 합 2r)
        return box;
    }

    private void OnDrawGizmos()
    {
        // 기준을 오브젝트 위치로 잡고 싶으면 여기에 transform.position 더하기
        // (원하면 주석 해제)
        // var worldOffset = transform.position;
        var worldOffset = Vector3.zero;

        // ---- Sphere ----
        if (showSphere)
        {
            Vector3 c = worldOffset + sphereCenter;

            // 실제 구
            Gizmos.color = colorTrue;
            Gizmos.DrawWireSphere(c, sphereRadius);

            // AABB (Bounds)
            if (showSphereAABB)
            {
                Bounds b = FromSphere(c, sphereRadius);
                Gizmos.color = colorAABB;
                Gizmos.DrawWireCube(b.center, b.size);
            }
        }

        // ---- Capsule ----
        if (showCapsule && capA != null && capB != null)
        {
            Vector3 a = capA.position;
            Vector3 b = capB.position;
            Vector3 mid = (a + b) * 0.5f;
            Vector3 axis = b - a;
            float len = axis.magnitude;
            Quaternion rot = len > 1e-5f ? Quaternion.FromToRotation(Vector3.up, axis) : Quaternion.identity;

            // 실제 캡슐
            #if UNITY_EDITOR
            Handles.color = colorTrue;
            // totalHeight = 직선부 길이 + 2 * 반지름
            float totalHeight = len + 2f * capsuleRadius;
            //Handles.DrawWireCube(mid, rot, capsuleRadius, totalHeight);
            #else
            // 런타임 전용 대체 표현(간단): 양 끝 구 + 대략적인 연결선
            Gizmos.color = colorTrue;
            Gizmos.DrawWireSphere(a, capsuleRadius);
            Gizmos.DrawWireSphere(b, capsuleRadius);
            Gizmos.DrawLine(a + (Vector3.right * capsuleRadius), b + (Vector3.right * capsuleRadius));
            Gizmos.DrawLine(a - (Vector3.right * capsuleRadius), b - (Vector3.right * capsuleRadius));
            Gizmos.DrawLine(a + (Vector3.forward * capsuleRadius), b + (Vector3.forward * capsuleRadius));
            Gizmos.DrawLine(a - (Vector3.forward * capsuleRadius), b - (Vector3.forward * capsuleRadius));
            #endif

            // AABB (Bounds)
            if (showCapsuleAABB)
            {
                Bounds bb = FromCapsule(a, b, capsuleRadius);
                Gizmos.color = colorAABB;
                Gizmos.DrawWireCube(bb.center, bb.size);
            }
        }
    }
}
