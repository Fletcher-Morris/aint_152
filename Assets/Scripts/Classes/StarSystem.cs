using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StarSystem
{
	public string systemName;
	public int systemDanger;
	public int systemTheme;
	public Vector2 systemMapPos;
	public bool visited;

	public List<Ship> enemyShips;
	public List<Asteroid> asteroids;

	public StarSystem()
	{
		systemName = "New Star System";
		systemDanger = 0;
		systemTheme = 0;
		systemMapPos = new Vector2 (0, 0);
		visited = false;

		enemyShips = new List<Ship>();
		asteroids = new List<Asteroid>();
	}
}