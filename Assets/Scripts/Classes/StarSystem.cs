using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StarSystem
{
	public string systemName;
	public int systemDanger;
	public Color systemColour;
	public int systemTheme;
	public Vector2 systemMapPos;
	public bool visited;

	public List<Ship> enemyShips;
	public List<Asteroid> asteroids;
    public List<WorldObject> objects;

	public StarSystem()
	{
		systemName = "New Star System";
		systemDanger = 0;
		systemColour = new Color (11, 17, 27);
		systemTheme = 0;
		systemMapPos = new Vector2 (0, 0);
		visited = false;

		enemyShips = new List<Ship>();
		asteroids = new List<Asteroid>();
        objects = new List<WorldObject>();
	}
}