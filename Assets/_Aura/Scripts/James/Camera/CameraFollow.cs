using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private SimpleMovement _playerMovement;

    [SerializeField] private float _turnTime = 0.5f;
    [SerializeField] private bool _isFacingRight;

    private Coroutine _turnCoroutine;

    private void Awake()
    {
        _playerMovement = _player.GetComponent<SimpleMovement>();
    }

    private void Start()
    {
        _isFacingRight = _playerMovement.isFacingRight;
    }

    private void Update()
    {
        transform.position = _player.position;
    }

    public void PlayerTurn()
    {
        if(_turnCoroutine != null)
            StopCoroutine(_turnCoroutine);

        _turnCoroutine = StartCoroutine(TurnLerp());
    }

    private IEnumerator TurnLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotation = DetermineRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;

        while(elapsedTime <_turnTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotation, (elapsedTime / _turnTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private float DetermineRotation()
    {
        _isFacingRight = !_isFacingRight;

        if (_isFacingRight)
        {
            return 0f;
        }
        else
        {
            return 180f;
        }
    }
}
