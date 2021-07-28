using UnityEngine;
using ViewTo.Objects;

namespace ViewTo.Connector.Unity
{
  public class BlockerContentMono : ViewContentMono<BlockerContent>
  {

    protected override void SetValidContent(BlockerContent t)
    {
      Debug.Log("No content to set for blocker ");
    }
  }
}