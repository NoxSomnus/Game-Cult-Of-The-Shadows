using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [Header("Controles para el Lerp y  el Damping mientras el player salta/cae")]
    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTime = 0.35f;

    public float _fallSpeedYDampingChangeThreshold = -15f;

    public bool IsLerpingYDamping { get; private set; }
    public  bool LerpedFromPlayerfalling {  get; set; }

    private Coroutine _lerpYPanCoroutine;

    private CinemachineFramingTransposer framingTransposer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }



    
}
