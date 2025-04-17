using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private GameObject _target;
    [SerializeField] private float _rotTime;
    [SerializeField] private float _moveTime;

    public void Init(GameObject target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;

        float mouseX = Input.GetAxis("MouseX");
        float mouseY = Input.GetAxis("MouseY");

        Quaternion qua = Quaternion.Euler(mouseY, mouseX, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, _moveTime);

        Vector3 dest = _target.transform.position + _offset;
        transform.position = dest;
    }
}