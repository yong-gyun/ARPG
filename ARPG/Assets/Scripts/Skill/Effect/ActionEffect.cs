using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public class ActionEffect : BaseEffect
{
    [SerializeField] private GameObject _target;

    [Header("스케일 변경")]
    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private float _scaleDuration;
    [SerializeField] private int _scaleDelay;
    [SerializeField] private bool _scaleAction;

    [Header("포지션 변경")]
    [SerializeField] private Define.DirType _moveDir;
    [SerializeField] private Define.UpDirType _upDir;
    [SerializeField] private float _distance;
    [SerializeField] private float _upPower;
    [SerializeField] private float _moveDuration;
    [SerializeField] private int _moveDelay;
    [SerializeField] private bool _moveAction;

    [Header("로테이션 변경")]
    [SerializeField] private Vector3 _targetRot;
    [SerializeField] private float _rotDuration;
    [SerializeField] private int _rotDelay;
    [SerializeField] private bool _rotAction;

    //n초후에 시작

    public override void PlayAction()
    {
        if (_moveAction == true)
            MoveAction();

        if (_scaleAction == true)
            ScaleAction();

        if (_rotAction == true)
            RotationAction();
    }

    private async void MoveAction()
    {
        await UniTask.Delay(_moveDelay);

        if (_target == null)
        {
            GameObject target = Owner.gameObject;
            var cc = target.GetComponent<CharacterController>();
            if (cc == null)
                return;

            Vector3 dir = Vector3.zero;
            if (_moveDir != Define.DirType.None)
                dir += target.GetLocalDir(_moveDir);

            if (_upDir == Define.UpDirType.None)
                dir.y = 0f;
            else
                dir.y = _upDir == Define.UpDirType.Up ? 1f : -1f;

            var speed = _distance / _moveDuration;
            var upPower = _upPower / _moveDuration;

            var currentTime = 0f;
            while (currentTime <= _moveDuration)
            {
                Vector3 motion = new Vector3(dir.x * speed, dir.y * upPower, dir.z * speed);
                cc.Move(motion * Time.deltaTime);
                currentTime += Time.deltaTime;
                await UniTask.Yield();
            }
        }
        else
        {

        }
    }

    private async void ScaleAction()
    {
        GameObject target = _target == null ? Owner.gameObject : _target;
        if (target == null)
            return;

        await UniTask.Delay(_scaleDelay);
        target.transform.DOScale(_targetScale, _scaleDuration);
    }

    private async void RotationAction()
    {
        GameObject target = _target == null ? Owner.gameObject : _target;
        if (target == null)
            return;

        await UniTask.Delay(_rotDelay);
        target.transform.DORotate(_targetRot, _rotDuration);
    }
}
