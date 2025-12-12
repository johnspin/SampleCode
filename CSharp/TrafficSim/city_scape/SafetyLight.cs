// SafetyLight class
using System;
using System.Drawing;

// Another example of a 1 dimensional Conveyor
public class SafetyLight: Conveyor
{
	private double x, y;	// x and y coordinates of Relay
	private Conveyor cin;	// incomming Conveyor
	private Conveyor cout;	// outgoing Conveyor
	private int timer;      // timer for the SafetyLight
	private Color color; // light color (red,yellow,green)
	private double bridge_space;	// space required in front of bridge for light to be on

	// Set the light color according to the timer value
	private void setColor()
	{
		if (timer<20)
			color = Color.Green;
		else if(timer<30)
			color = Color.Yellow;
		else
			color = Color.Red;
	}

	// Construct a new Relay at coordinates (new_x, new_y).
	public SafetyLight(double new_x, double new_y, int new_timer, int new_layer, double new_bridge_space, Scene scene)
		: this(new_x, new_y, new_timer, new_bridge_space, scene)	{
		layer = new_layer;
	}

	public SafetyLight(double new_x, double new_y, int new_timer, double new_bridge_space, Scene scene): base(scene)	{
		x = new_x;
		y = new_y;
		cin = null;
		cout = null;
		timer = new_timer;
		bridge_space = new_bridge_space;
		setColor();
	}

	// Construct a new Relay with incoming Conveyor new_in,
	// and outgoing Conveyor new_out.
	public SafetyLight(Conveyor new_in, Conveyor new_out, int new_layer, double new_bridge_space, Scene scene)
		: this(new_in, new_out, new_bridge_space, scene) {
		layer = new_layer;
	}

	public SafetyLight(Conveyor new_in, Conveyor new_out, double new_bridge_space, Scene scene): base(scene)	{
		cin = new_in;
		cout = new_out;

		if (cin.OutX() != cout.InX())
			throw new ArgumentException("SafetyLight: in.OutX() != out.InX()");
		if (cin.OutY() != cout.InY())
			throw new ArgumentException("SafetyLight: in.OutY() != out.InY()");

		x = cout.InX();
		y = cout.InY();

		cin.AddOut(this);
		cout.AddIn(this);

		timer = 0;
		bridge_space = new_bridge_space;
		setColor();
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
		// If the SafetyLight is green
		if (color==Color.Green)
		{
			// let a vehicle go through only if the intersection is clear
			// if (out.spaceRear(this)<out.distance(out.inX(),out.inY(),out.outX(),out.outY()))
			// return -1;
			//  else
			return cout.SpaceRear(this);
		}
		else
			// return a negative value so that no vehicle is allowed through
			return -1;
	}

	// Hand off mobile to the outgoing Conveyor with residual
	// speed residual_speed.
	public override void HandOff(Mobile mobile, double residual_speed)
	{
		cout.HandOff(mobile, residual_speed);
	}

	// Update the SafetyLight timer and color
	public override void Update() {
		if (cout.SpaceRear(cout) > bridge_space)	{
			timer++;
			if (timer>50)
				timer = 0;
		}
		else	{
			if (timer<20)
				timer = 20;
			else
				timer++;
		}
		setColor();
	}

	// Display the SafetyLight
	public override void Draw(Graphics g)
	{
		g.FillEllipse(new SolidBrush(color),(int)x-5,(int)y-5,10,10);
	}
}
