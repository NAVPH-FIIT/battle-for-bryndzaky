using Bryndzaky.Units.Player;
using UnityEngine;

namespace Bryndzaky.General.Common
{
  public interface IInteractable
  {
    void ExecuteAction();
    public void OnTriggerEnter2D(Collider2D other);
    public void OnTriggerExit2D(Collider2D other);
  }
}