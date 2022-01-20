using UnityEngine;
using TMPro;

public class GameplayScreen : Screen
{
    [SerializeField] GameMode _gameMode;
    [SerializeField] GameSession _gameSession;

    [Header("Input Text Components")]
    [SerializeField] TextMeshProUGUI _moveForwardText;
    [SerializeField] TextMeshProUGUI _rotateLeftText;
    [SerializeField] TextMeshProUGUI _rotateRightText;
    [SerializeField] TextMeshProUGUI _singleShootAttackText;
    [SerializeField] TextMeshProUGUI _tripleShootAttackText;

    [Header("Time Text Components")]
    [SerializeField] TextMeshProUGUI _timeRemainingText;
    [SerializeField] TextMeshProUGUI _singleShootCoolDownText;
    [SerializeField] TextMeshProUGUI _tripleShootCoolDownText;

    PlayerController _playerController;
    PlayerInputs _playerInputs;

    private void Start() 
    {
        _playerController = _gameMode.GetPlayerController();
        _playerInputs = _playerController.GetPlayerInputs();
    }

    void Update() => UpdateTimeTexts();
    void LateUpdate()  => UpdateInputsTexts();

    void UpdateTimeTexts()
    {
        _timeRemainingText.text = $"Time Remaining : {_gameSession.ReamingTime()}";
        _singleShootCoolDownText.text = $"Frontal Attack CoolDown : {_playerController.GetFrontalShootCoolDown()}";
        _tripleShootCoolDownText.text = $"Side Attack CoolDown : {_playerController.GetSideShootCoolDown()}";
    }

    void UpdateInputsTexts()
    {
        if(_playerInputs == null)
            return;

        _moveForwardText.text = $"Move Forward: {_playerInputs.ForwardKey}";  
        _rotateLeftText.text = $"Rotate Left : {_playerInputs.RotateLeftKey}";
        _rotateRightText.text = $"Rotate Right : {_playerInputs.RotateRightKey}";
        _singleShootAttackText.text = $"Single Shoot Attack : {_playerInputs.FrontalShoot}";
        _tripleShootAttackText.text = $"Triple Shoot Attack : {_playerInputs.SideShoot}";
    }
}