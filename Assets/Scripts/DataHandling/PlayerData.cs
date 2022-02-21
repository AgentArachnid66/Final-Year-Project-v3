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
public class Hit
{
	public float U;
	public float V;

	public float pressure;
    public int wallID;

	public Hit(Vector2 uv, float pressure, int wallID)
    {
		this.U = uv.x;
		this.V = uv.y;
		this.pressure = pressure;
        this.wallID = wallID;
    }
}

[System.Serializable]
public class SessionData
{
	// PlayerID = AccountID + UserID
	public int PlayerID;
	public int Score;
	public float Length;

	public string TimeStamp;

	[SerializeField]
	public List<Hit> HitLocations;

	[SerializeField]
	public string mask;
}

#region Participant Data

[System.Serializable]
public class ParticipantData
{
	public int ID;
	public int PIN;
}

public class LoginOutputData
{
	public bool success;
	public string id;
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
public class ImagePathData
{
	public string renderTarget;
	public string ntf;
	public string stt;
	public string mix;
}

[System.Serializable]
public class ImageWeightData
{
	public float renderTarget;
	public float ntf;
	public float stt;
	public float mix;
}

[System.Serializable]
public class ScoreInputData
{
	public ImagePathData images;
	public ImageWeightData weights;
}

[System.Serializable]
public class ScoreOutputData
{
	public float score;
	public bool success;
}

#endregion