using UnityEngine;

namespace Tools
{
  internal interface IResourceLoader
  {
     Sprite LoadSprite(string path);
     GameObject LoadPrefab(string path);
  }
}

