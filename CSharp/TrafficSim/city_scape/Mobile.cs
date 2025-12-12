// Mobile class


// A Mobile object is an object which is mobile.  It can be move
// about the Scene on a Conveyor.  Examples are: Cars, Trucks,
// Buses, Bicyclists, Pedestrians, Helicopters, etc.

public abstract class Mobile: SceneElement
{
	protected double x, y; // x and y coordinates of Mobile object
	protected double length; // length of Mobile object (including space
							// in front and behind of the object)
	protected double max_speed; // Maximum speed of Mobile object
	protected double vx, vy; // Unit vector in direction of movement.
							// vx is the x coordinate of this unit vector,
							// and vy is the y coordinate.

	protected bool has_moved; // True if the Mobile has already been moved
								// in the current update.  This would be set
								// to true if a Conduit handed off this Mobile
								// to another Conduit, and is used by Conduit
								// to ensure that a "double update" on this
								// Mobile does not occur.  has_moved can be reset
								// to false when this Mobile is updated since
								// the call to this Mobile's update is certain
								// to occur after the Conduit that it is on has
								// updated since this Mobile is at a higher layer.

	// Construct a new Mobile with center coordinates (x, y),
	// length length, and maximum speed max_speed.
	public Mobile(double x, double y, double length, double max_speed, Scene scene): base(scene)
	{
		this.x = x;
		this.y = y;
		this.length = length;
		this.max_speed = max_speed;
	}

	// Return the x and y coordinates of this Mobile object.
	public double GetX() { return x; }
	public double GetY() { return y; }


	// Return the length of this Mobile object
	public double GetLength() { return length; }

	// Return the maximum speed of this Mobile object
	public double GetMaxSpeed() { return max_speed; }

	// Set of the position of this Mobile object to the
	// coordinates (x, y).
	public void SetPosition(double x, double y)
	{
		this.x = x;
		this.y = y;
	}

	// Translate the position of this Mobile object by the
	// vector [dx dy].  This simple adds dx to the current
	// x coordinate of this Mobile object, and adds dy to
	// the current y coordinate.
	public void Translate(double dx, double dy)
	{
		x = x + dx;
		y = y + dy;
	}

	// Set the direction of this Mobile object such that it
	// is headed in direction angle.  The units of angle is
	// Radians.
	public virtual void SetDirection(double angle)
	{
		vx = RotateX(1, 0, angle);
		vy = RotateY(1, 0, angle);
	}

	// Return true if the Mobile has been moved during this update.
	// Return false otherwise.
	public bool Moved() { return has_moved; }

	// Set has_moved to new_has_moved;
	public void Moved(bool new_has_moved) { has_moved = new_has_moved; }

	// Move this Mobile object in the direction that it is
	// currently headed by a distance of distance.
	public void Move(double distance)
	{
		Translate(distance*vx, distance*vy);
	}

	// Set moved to false
	public override void Update()
	{
		Moved(false);
	}
}

