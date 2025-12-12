// Relay class
using System;
using System.Drawing;

// A Relay is simple an anchor point used to connect other Conveyors.
// A Relay can be thought of as a Point, where a Conduit is a Line.

public class Relay: Conveyor
{
	private double x, y;	// x and y coordinates of Relay
	private Conveyor cin;	// incomming Conveyor
	private Conveyor cout;	// outgoing Conveyor

	// Construct a new Relay at coordinates (new_x, new_y).
	public Relay(double new_x, double new_y, Scene scene): base(scene)
	{
		x = new_x;
		y = new_y;
		cin = null;
		cout = null;
	}

	// Construct a new Relay with incoming Conveyor new_in,
	// and outgoing Conveyor new_out.
	public Relay(Conveyor new_in, Conveyor new_out, Scene scene): base(scene)
	{
		cin = new_in;
		cout = new_out;

		if (cin.OutX() != cout.InX())
			throw new ArgumentException("Relay constructor: in.OutX()!=out.InX()");
		if (cin.OutY()!=cout.InY())
			throw new ArgumentException("Realy constructor: in.OutY()!=out.InY()");

		x = cout.InX();
		y = cout.InY();

		cin.AddOut(this);
		cout.AddIn(this);
	}

	// Return the x and y coordinates of this Relay.
	public override double InX() { return x; }
	public override double InY() { return y; }
	public override double OutX() { return x; }
	public override double OutY() { return y; }


	// Add the incoming Conveyor.  If the incoming Conveyor has
	// already been added, then this method will produce undefined
	// behavior.
	public override void AddIn(Conveyor new_in) { cin = new_in; }

	// Add the outgoing Conveyor.  If the outgoing Conveyor has
	// already been added, then this method will produce undefined
	// behavior.
	public override void AddOut(Conveyor new_out) { cout = new_out; }

	// Return the space in front of the incoming Conveyor.
	public override double SpaceFront()
	{
		return cin.SpaceFront();
	}

	// Return the space in the rear of the outgoing Conveyor.
	public override double SpaceRear(Conveyor me)
	{
		return cout.SpaceRear(this);
	}

	// Hand off mobile to the outgoing Conveyor with residual
	// speed residual_speed.
	public override void HandOff(Mobile mobile, double residual_speed)
	{
		cout.HandOff(mobile, residual_speed);
	}

	// Do nothing.
	public override void Update(){}

	// Do nothing.
	public override void Draw(Graphics g){}
}
