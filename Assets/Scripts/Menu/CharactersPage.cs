using UnityEngine;
using System.Collections;

[RequireComponent(typeof (PageTransition))]
public class CharactersPage : MonoBehaviour {

    public PlayerSelector[] PlayerCards;

	void Update() {
        if (GetComponent<PageTransition>().IsTransitioning()) {
            return;
        }
        for (int inputIndex = 0; inputIndex < InputManager.NumberOfInputs; ++inputIndex) {
            if (InputManager.Instance.GetKeyUp(InputAlias.Cancel, inputIndex) && GameManager.Instance.IsInputAvailable(inputIndex)) {
                _deselectAllPlayers();
                GetComponent<PageTransition>().GoPrevious();
            }
            if (InputManager.Instance.GetKeyUp(InputAlias.Submit, inputIndex) &&
                !GameManager.Instance.IsInputAvailable(inputIndex) &&
                GameManager.Instance.GetNextPlayerIndex() > 1) {
                GetComponent<PageTransition>().GoNext();
            }
        }
        for (int playerIndex = 0; playerIndex < 5; ++playerIndex) {
            PlayerData player = GameManager.Instance.GetPlayer(playerIndex);
            PlayerSelector playerCard = PlayerCards[playerIndex];
            if (player != null) {
                if (InputManager.Instance.GetKeyUp(InputAlias.Cancel, player.InputIndex)) {
                    playerCard.DeselectPlayer();
                } else if (InputManager.Instance.GetKeyUp(InputAlias.Alt, player.InputIndex)) {
                    playerCard.ChangePlayerSkin();
                }
            } else {
                if (GameManager.Instance.GetNextPlayerIndex() == playerIndex) {
                    for (int inputIndex = 0; inputIndex < InputManager.NumberOfInputs; ++inputIndex) {
                        if (InputManager.Instance.GetKeyUp(InputAlias.Submit, inputIndex) && GameManager.Instance.IsInputAvailable(inputIndex)) {
                            playerCard.SelectPlayer(inputIndex);
                        }
                    }
                }
            }
        }
    }

    private void _deselectAllPlayers() {
        for (int inputIndex = 0; inputIndex < InputManager.NumberOfInputs; ++inputIndex) {
            PlayerCards[inputIndex].DeselectPlayer();
        }
    }
}
