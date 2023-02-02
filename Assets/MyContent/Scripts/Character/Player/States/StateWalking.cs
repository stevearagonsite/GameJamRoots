using FP;
using UnityEngine;

public class StateWalking: IState {
    private Player _player;
    private EventFSM<Player.PlayerStates> _fsm;
    public StateWalking(Player player, EventFSM<Player.PlayerStates> fsm) {
        _player = player;
        _fsm = fsm;
    }
    
    public void OnEnter() {
        _player.animator.SetBool(Constants.CharacterConstants.ANIMATOR_IS_JUMPING, false);
    }
    
    public void OnUpdate() {
        _player.horizontalMove = _player.playerController.horizontalMove * _player.runSpeed;
        
        _player.animator.SetFloat(
            Constants.CharacterConstants.ANIMATOR_SPEED, 
            Mathf.Abs(_player.playerController.horizontalMove)
        );
        
        if (_player.playerController.keyDownJump) {
            _fsm.Feed(Player.PlayerStates.JUMPING);
            return;
        }
        if (_player.playerController.keyDownAttack) {
            _fsm.Feed(Player.PlayerStates.ATTACK);
            return;
        }
        if (_player.playerController.keyDownHook) {
            _fsm.Feed(Player.PlayerStates.HOOKING);
            return;
        }
        if (_player.horizontalMove == 0) {
            _fsm.Feed(Player.PlayerStates.IDLE);
            return;
        }
        if (_player.playerController.keyDownCrouch) {
            _fsm.Feed(Player.PlayerStates.CROUCHING);
        }
    }
    
    public void OnExit() {
        // Do something when exiting this state
    }
}