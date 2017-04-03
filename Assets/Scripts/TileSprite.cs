using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class TileSprite : MonoBehaviour {

	//define what atributes a tile has
	// ...

	public TileSprite() {
		
		// initialize a default tile
		// ...

	}

    public Sprite TileImage { get; set; }

	//public TileSprite( //constructor with your chosen attributes
	//						) {
	//	//set attribute values
	//	//...
	//}
}
