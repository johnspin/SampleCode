// Router class
using System;
using System.Drawing;

// A Router connects one conveyor to two conveyors

public class Router: Conveyor
{
	private double x, y;	// x and y coordinates of Relay
	private Conveyor cin;	// incomming Conveyor
	private Conveyor out1, out2; // outgoing Conveyors

	// Construct a new Router at coordinates (new_x, new_y).
	public Router(double new_x, double new_y, Scene scene): base(scene)
	{
		x = new_x;
		y = new_y;
		cin = null;
		out1 = null;
		out2 = null;
	}

	// Construct a new Relay with incoming Conveyor new_in,
	// and outgoing Conveyor new_out.
	public Router(Conveyor new_in, Conveyor new_out1, Conveyor new_out2, Scene scene): base(scene)
	{
		cin = new_in;
		out1 = new_out1;
		out2 = new_out2;

		if(cin.OutX() != out1.InX())
			throw new ArgumentException("Router: cin.OutX() != out1.InX()");
		if(cin.OutY() != out1.InY())
			throw new ArgumentException("Router: cin.OutY() != out1.InY()");
		if(cin.OutX() != out2.InX())
			throw new ArgumentException("Router: cin.OutX() != out2.InX()");
		if(cin.OutY() != out2.InY())
			throw new ArgumentException("Router: in.OutY() != out2.InY()");

		x = out1.InX();
		y = out1.InY();

		cin.AddOut(this);
		out1.AddIn(this);
		out2.AddIn(this);
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

	// Add an outgoing conveyor to the router
	// If the outgoing Conveyors have
	// already been added, then this method will produce undefined
	// behavior.
	public override void AddOut(Conveyor new_out)
	{
		if (out1==null)
			out1 = new_out;
		else
			out2 = new_out;
	}

	// Return the space in front of the incoming Conveyor.
	public override double SpaceFront()
	{
		return cin.SpaceFront();
	}

	// Return the minimum space in the rear of the outgoing Conveyor.
	public override double SpaceRear(Conveyor me)
	{
		return Math.Min(out1.SpaceRear(this),out2.SpaceRear(this));
	}

	// Hand off mobile to the outgoing Conveyors with residual
	// speed residual_speed. Choice between 1 and 2 is random.
	public override void HandOff(Mobile mobile, double residual_speed)
	{
		if (new Random().NextDouble() < 0.5)
			out1.HandOff(mobile, residual_speed);
		else
			out2.HandOff(mobile, residual_speed);
	}

	// Do nothing.
	public override void Update(){}

	// Do nothing.
	public override void Draw(Graphics g){}
}
