using UnityEngine;
using UnityEngine.UI;

namespace HellRoad.External
{
    public class ShowMapTextureView : MonoBehaviour, ISetMiniMapPosition
    {
        [SerializeField] MapGenerator mapGenerator = null;
        [SerializeField] RawImage mapTexView = null;
        [SerializeField] float mapScale = 3;
        [SerializeField] float tileSize = 32;

        private void Awake()
        {
            mapGenerator.OnEndGenerate += (tiles) =>
            {
                Texture2D mapTex = new TilesToMiniMapTex().CreateTexture(tiles);
                mapTexView.texture = mapTex;
                mapTexView.SetNativeSize();
                mapTexView.rectTransform.sizeDelta *= mapScale;
            };
        }

        void ISetMiniMapPosition.SetPosition(Vector2 pos)
        {
            transform.localPosition = -pos / tileSize * mapScale;
        }
    }
}