using System.Collections;
using System.Collections.Generic;
using UnityEngine;




// This is data for each player's session

/*
[System.Serializable]
public class Pixel
{
	public float R;
	public float G;
	public float B;
}

[System.Serializable]
public class Texture
{
	[SerializeField]
	public List<Pixel> Pixels;
}
*/


[System.Serializable]
public class SessionData
{
	public int PlayerID;
	public int Score;
	public float Length;

	public string TimeStamp;

	[SerializeField]
	public List<Hit> HitLocations;

    [SerializeField]
    public WallData[] masks;

    [SerializeField]
    public List<SpatialData> spatialData;

    [SerializeField]
    public List<PickupData> pickupData;

}


[System.Serializable]
public class Hit
{
	public float U;
	public float V;
    public float temperature;
	public float pressure;
    public int wallID;

    public Hit(Vector2 uv, float pressure, float temp ,int wallID)
    {
		this.U = uv.x;
		this.V = uv.y;
		this.pressure = pressure;
        this.temperature = temp;
        this.wallID = wallID;
    }
}

[System.Serializable]
public class WallData
{
	public string combinedMasks;
    public int id;

    public WallData(string combined, int wallID)
    {
	    this.combinedMasks = combined;
	    this.id = wallID;
    }
}

[System.Serializable]
public class PickupData
{
    public int id;
    public string effected;
    public float effect;
    public string pickupTimestamp;

    public PickupData(int id, string effected, float effect, string stamp)
    {
        this.id = id;
        this.effected = effected;
        this.effect = effect;
        this.pickupTimestamp = stamp;
    }
}

[System.Serializable]
public class SpatialData
{
    public Vector3 position;
    public Vector3 velocity;
    public string time;

    public SpatialData(Vector3 pos, Vector3 vel, string time)
    {
        this.position = pos;
        this.velocity = vel;
        this.time = time;
    }
}

#region Participant Data

[System.Serializable]
public class ParticipantData
{
	public int ID;
	public int PIN;
	public string Password;
}

public class LoginOutputData
{
	public bool success;
	public string id;
}

public class ErrorOutputData
{
    public string msg;
    public bool success;
}
#endregion



#region Account and User

// NOT USING THIS CODE YET

[System.Serializable]
public class AccountData
{
	public string Email;
	public string Password;
}

[System.Serializable]
public class UserData
{
	// Account that this user is a child of
	public string AccountID;

    // Username for this user
    public string Username;

    // PIN required to access this user
    public int PIN;

}




#endregion

#region Image and Score


[System.Serializable]
public class ScoreInputData
{
	public string[] paths;
	public float[] weights;
}

[System.Serializable]
public class ScoreOutputData
{
	public float score;
	public bool success;
}

#endregion