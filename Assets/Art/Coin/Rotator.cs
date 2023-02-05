using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    #region .: Public Vars :.
    #endregion

    #region .: Public Events :.
    #endregion

    #region .: Private Vars :.
    [SerializeField] private bool _rotateOnStart;
    [SerializeField] private bool _x;
    [SerializeField] private float _rotationSpeedX;
    [SerializeField] private bool _backwardsX;
    [Tooltip("Swaps between backwards and towards interval in seconds.")]
    [SerializeField] private float _swapBackwardsIntervalX;

    [SerializeField] private bool _y;
    [SerializeField] private float _rotationSpeedY;
    [SerializeField] private bool _backwardsY;
    [Tooltip("Swaps between backwards and towards interval in seconds.")]
    [SerializeField] private float _swapBackwardsIntervalY;


    [SerializeField] private bool _z;
    [SerializeField] private float _rotationSpeedZ;
    [SerializeField] private bool _backwardsZ;
    [Tooltip("Swaps between backwards and towards interval in seconds.")]
    [SerializeField] private float _swapBackwardsIntervalZ;


    [SerializeField] private Space _rotationSpace;

    private Coroutine _swapBackwardsX;
    private Coroutine _swapBackwardsY;
    private Coroutine _swapBackwardsZ;

    #endregion

    #region .: Public Methods :.
    public void Start()
    {
        enabled = _rotateOnStart;
        if (_swapBackwardsIntervalX > 0)
            _swapBackwardsX = StartCoroutine(SwapBackwardsX(_swapBackwardsIntervalX));
        if (_swapBackwardsIntervalY > 0)
            _swapBackwardsY = StartCoroutine(SwapBackwardsY(_swapBackwardsIntervalY));
        if (_swapBackwardsIntervalZ > 0)
            _swapBackwardsZ = StartCoroutine(SwapBackwardsZ(_swapBackwardsIntervalZ));
    }
    public void Stop()
    {
        enabled = false;
        StopAllCoroutines();
    }

    #endregion

    #region .: Private Methods :.

    private void Update()
    {
        int backwardsX = _backwardsX ? -1 : 1;
        int backwardsY = _backwardsY ? -1 : 1;
        int backwardsZ = _backwardsZ ? -1 : 1;
        transform.Rotate(_x ? _rotationSpeedX * backwardsX * Time.deltaTime : 0, _y ? _rotationSpeedY * backwardsY * Time.deltaTime : 0, _z ? _rotationSpeedZ * backwardsZ * Time.deltaTime : 0);
    }

    private IEnumerator SwapBackwardsX(float seconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
            _backwardsX = !_backwardsX;
        }
    }
    private IEnumerator SwapBackwardsY(float seconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
            _backwardsY = !_backwardsY;
        }
    }
    private IEnumerator SwapBackwardsZ(float seconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
            _backwardsZ = !_backwardsZ;
        }
    }

    #endregion
}

