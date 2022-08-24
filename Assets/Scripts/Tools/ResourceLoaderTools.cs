using UnityEngine;

namespace Tools
{
  internal class ResourceLoaderTools : IResourceLoader
  {
    public Sprite LoadSprite(string path)
    {
      return Resources.Load<Sprite>(path);
    }

    public GameObject LoadPrefab(string path)
    {
      return Resources.Load<GameObject>(path);
    }
  }  
}

