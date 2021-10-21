using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellRoad.External
{
    public class Ornament : MonoBehaviour
    {
        [SerializeField] SpriteRenderer[] spriteRenderers = null;

        public void Initalize(int x, int y, Vector2 cellSize, int sortingLayerID)
        {
            float posX = cellSize.x * x;
            float posY = cellSize.y * y;
            Vector2 position = new Vector2(posX, posY);
            transform.position = position;

            for (int i = 0; i < spriteRenderers.Length; i++) 
                spriteRenderers[i].sortingLayerID = sortingLayerID;
        }
    }
}