using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    public static CameraManager instance;

    [SerializeField] private CinemachineVirtualCamera[] _allVirtualCameras;

    [Header("Controles para el Lerp y  el Damping mientras el player salta/cae")]
    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTime = 0.35f;

    public float _fallSpeedYDampingChangeThreshold = -15f;

    public bool IsLerpingYDamping { get; private set; }
    public  bool LerpedFromPlayerfalling {  get; set; }

    public Coroutine _lerpYPanCoroutine;

    public CinemachineVirtualCamera _currentCamera;
    public CinemachineFramingTransposer _framingTransposer;

    public float _normYPanAmount;
    private void Awake()
    {
        Debug.Log("Awake");
        if (instance == null)
        {
            Debug.Log("instancia");

            instance = this;
        }
        for (int i = 0; i < _allVirtualCameras.Length; i++)
        {
            if (_allVirtualCameras[i].enabled)
            {
                Debug.Log("cosas con la camara");

                //set the current active camera 
                _currentCamera = _allVirtualCameras[i];

                //set the framing transposer 
                _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();    

            }
        }

        //set the YDamping amount so it's based on the inspector value
        _normYPanAmount = _framingTransposer.m_YDamping;
    }

    #region Lerp the Y Damping

    public void LerpYDamping(bool isPlayerFalling)
    {
        _lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }    

    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;
        Debug.Log("corutina        lerp");

        // grab the starting damping amount
        float startDampAmount = _framingTransposer.m_YDamping;
        float endDampAmount = 0f;

        //determine the en damping amount
        if (isPlayerFalling)
        {
            Debug.Log("estoy cayendo");

            endDampAmount = _fallPanAmount;
            LerpedFromPlayerfalling = true;
        }
        else
        {
            Debug.Log(" no estoy cayendo");

            endDampAmount = _normYPanAmount;
        }

        //lerp the pan amount
        float elapsedTime = 0f;
        while (elapsedTime < _fallYPanTime)
        {
            Debug.Log("hace cosas en el while");
            elapsedTime += Time.deltaTime;
            
            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount,( elapsedTime / _fallYPanTime));
            _framingTransposer.m_YDamping = lerpedPanAmount;

            yield return null;
        }


        IsLerpingYDamping = false;
    }
    #endregion
}
