using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 3f, -4f);

    [SerializeField] private float _sensitive = 2.5f;
    [SerializeField] private float _smoothSpeed = 10f;

    [SerializeField] private float _minAngle = -30f;
    [SerializeField] private float _maxAngle = 45f;

    [SerializeField] private float _yaw;            //좌우 회전
    [SerializeField] private float _pitch;          //상하 회전

    public void Init(Transform target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;

        _yaw += Input.GetAxis("Mouse X") * _sensitive;
        _pitch += Input.GetAxis("Mouse Y") * _sensitive;

        _pitch = Mathf.Clamp(_pitch, _minAngle, _maxAngle);

        Quaternion rot = Quaternion.Euler(_pitch, _yaw, 0f);
        Vector3 offset = rot * new Vector3(0f, 0f, _offset.z);
        Vector3 caculatedPos = _target.position + offset + Vector3.up * _offset.y;
        Vector3 dir = caculatedPos - _target.position;

        Vector3 destPos = caculatedPos;
        if (Physics.Raycast(_target.position, dir.normalized, out RaycastHit hit, _offset.z) == true)
            destPos = hit.point;
        
        transform.position = Vector3.Lerp(transform.position, destPos, _smoothSpeed * Time.deltaTime);

        transform.LookAt(_target, Vector3.up);
    }
}