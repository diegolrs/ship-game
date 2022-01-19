using UnityEngine;
using TMPro;

public class GameplayScreen : Screen
{
    [SerializeField] GameMode _gameMode;
    [SerializeField] TextMeshProUGUI _moveForwardText;
    [SerializeField] TextMeshProUGUI _rotateLeftText;
    [SerializeField] TextMeshProUGUI _rotateRightText;
    [SerializeField] TextMeshProUGUI _singleShootAttackText;
    [SerializeField] TextMeshProUGUI _tripleShootAttackText;

    PlayerInputs _playerInputs;

    private void Start() 
    {
        _playerInputs = _gameMode.GetPlayerController().GetPlayerInputs();
    }

    void LateUpdate() 
    {
        UpdateInputsText();
    }

    void UpdateInputsText()
    {
        if(_playerInputs == null)
            return;

        _moveForwardText.text = $"Move Forward: {_playerInputs.ForwardKey}";  
        _rotateLeftText.text = $"Rotate Left : {_playerInputs.RotateLeftKey}";
        _rotateRightText.text = $"Rotate Right : {_playerInputs.RotateRightKey}";
        _singleShootAttackText.text = $"Single Shoot Attack : {_playerInputs.SingleShoot}";
        _tripleShootAttackText.text = $"Triple Shoot Attack : {_playerInputs.TripleShoot}";
    }
}