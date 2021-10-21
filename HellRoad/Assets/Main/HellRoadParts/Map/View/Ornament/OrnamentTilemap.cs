using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellRoad.External
{
	public class OrnamentTilemap : MonoBehaviour
	{
		[SerializeField] string sortingLayerName = "BackOrnament";

		private Vector2 cellSize;
		private List<GameObject> ornaments = new List<GameObject>();

		public void Initalize()
		{
			ClearAllOrnaments();
			cellSize = transform.parent.GetComponent<Grid>().cellSize;
		}

		public Ornament SetOrnament(Ornament prefab, int x, int y)
		{
			Ornament ornament = Instantiate(prefab, transform);
			ornament.Initalize(x, y, cellSize, SortingLayer.NameToID(sortingLayerName));
			ornaments.Add(ornament.gameObject);
			return ornament;
		}

		public void ClearAllOrnaments()
		{
			ornaments.ForEach(x => Destroy(x));
			ornaments = new List<GameObject>();
		}
	}
}