// BoatCrossing class
using System;
using System.Drawing;

// A BoatCrossing is a 1 dimensional Conveyor which connects two incoming
// Conveyors to 1 outgoing Conveyor.  The BoatCrossing decides which
// incoming Mobile to let enter the BoatCrossing intersection and controls
// the other incoming Mobile objects so that no collisions occur.
// The boat_space of a BoatCrossing is the amount of distance before the BoatCrossing
// that the Mobile objects are kept while the intersection is occupied.
// If the BoatCrossing intersection is clear (no traffic) and only one incoming
// Conveyor has a Mobile object that is close to the BoatCrossing, then this
// Mobile object is allowed into the BoatCrossing without delay.  If both
// incoming Conveyors contain Mobile objects that are within the
// boat_space of the BoatCrossing, then only one of these Mobile objects is
// let into the intersection at a time and the incoming Conveyor
// that is allowed to send its Mobile object into the intersection
// is alternated.  This Conveyor makes use of the Conveyor  parameter
// in spaceRear(Conveyor) since the BoatCrossing will want to know which of
// its incoming Conveyors is asking if there is space.  Depending on
// the situation, the BoatCrossing can report to the Conveyor that there is
// space, and thus the Conveyor should advance its Mobile object into
// the intersection, or that there there is another Mobile object about
// to enter the intersection, and thus the Conveyor should keep its
// Mobile objects back.  Note that spaceRear can return any value,
// positive or negative.  Returning a negative value prevents an
// incoming Conveyor from advancing its inbound Mobile objects within
// a distance of that value, thus keeping the intersection clear for
// the other incoming Conveyor to advance its Mobile objects.  This
// is controled by the spaceRear() method.

public class BoatCrossing: Conveyor
{
	private double x, y;	// x and y coordinates of BoatCrossing
	private Conveyor bin;	// incoming River
	private Conveyor cin;	// Second incoming Conveyor
	private Conveyor bout;	// outgoing River
	private Conveyor cout;	// outgoing Conveyor

	private double boat_space; // The space that cars should leave before the BoatCrossing point.

	private bool aBoatIsComing; // If aBoatIsComing, then hold all traffic on cin

	// Construct a new BoatCrossing with coordinates (new_x, new_y) and
	// BoatCrossing space new_boat_space.
	public BoatCrossing(double new_x, double new_y, double new_boat_space, Scene scene): base(scene)
	{
		x = new_x;
		y = new_y;
		bin = null;
		cin = null;
		bout = null;
		cout = null;
		boat_space = new_boat_space;
		aBoatIsComing = false;
	}


	// Construct a new BoatCrossing with incoming Conveyors new_in1,
	// and new_in2, outgoing Conveyors new_out, and with BoatCrossing
	// space new_boat_space.
	public BoatCrossing(Conveyor new_bin, Conveyor new_cin, Conveyor new_bout, Conveyor new_cout,
				double new_boat_space, Scene scene): base(scene)
	{
		bin = new_bin;
		cin = new_cin;
		bout = new_bout;
		cout = new_cout;

		if (cin.OutX() != cout.InX()) throw new ArgumentException("BoatCrossing in1.OutX() != out.InX()");
		if (cin.OutY() != cout.InY()) throw new ArgumentException("BoatCrossing in1.OutY() != out.InY()");
		if (bin.OutX() == cout.InX()) throw new ArgumentException("BoatCrossing in2.OutX() == out.InX()");
		if (bin.OutY() == cout.InY()) throw new ArgumentException("BoatCrossing in2.OutY() == out.InY()");

		x = cout.InX();
		y = cout.InY();

		bin.AddOut(this);
		cin.AddOut(this);
		bout.AddIn(this);
		cout.AddIn(this);

		boat_space = new_boat_space;
		aBoatIsComing = false;
	}

	// Return the x and y coordinates of this BoatCrossing.
	public override double InX() { return x; }
	public override double InY() { return y; }
	public override double OutX() { return x; }
	public override double OutY() { return y; }

	// Add set the incoming Conveyors.  If both incoming Conveyors
	// have already been set, then this method will produce undefined
	// behavior.
	public override void AddIn(Conveyor new_in)	{
		if (cin == null && !(new_in is River))
			cin = new_in;

		if (bin == null && new_in is River)
			bin = new_in;
	}

	// Add the outgoing Conveyors.  If the outgoing Conveyors have
	// already been set, then this method will produce undefined
	// behavior.
	public override void AddOut(Conveyor new_out) {
		if (cout == null && !(new_out is River))
			cout = new_out;

		if (bout == null && new_out is River)
			bout = new_out;
	}

	// Return the minimum of the space in front of the two incoming
	// Conveyors.
	public override double SpaceFront()	{
		return Math.Min(cin.SpaceFront(), bin.SpaceFront());
	}

	// If it is the Conveyor me's turn to allows it's Mobile object to
	// enter the BoatCrossing, then return the space in the rear of the
	// outgoing Conveyor.  Else return -boat_space, thus the incoming
	// Conveyor will not advance incoming Mobile objects closer than
	// boat_space before the BoatCrossing.
	public override double SpaceRear(Conveyor me)	{
		if(aBoatIsComing && me == cin)
			return -boat_space;
		else if (aBoatIsComing && me == bin)
			return bout.SpaceRear(this);
		else if (me == bin)
			return bout.SpaceRear(this);
		else
			return cout.SpaceRear(this);

//				if(!merging ||
//			(merge_one && me == in1) ||
//			(!merge_one && me == in2)  )
//			return cout.SpaceRear(this);
//		else
//			return -merge_space;
	}

	// Hand off the mobile object to the outgoing Conveyor with
	// residual speed residual_speed.  If both incoming Conveyors
	// have Mobile objects which are within boat_space of the
	// BoatCrossing intersection, then stop the Road and the Train objects
	// are allowed to advance its Mobile object into the BoatCrossing
	// intersection.
	public override void HandOff(Mobile mobile, double residual_speed)	{
		if (aBoatIsComing)
			bout.HandOff(mobile, residual_speed);
		else
			cout.HandOff(mobile, residual_speed);
	}

	// Set aBoatIsComing true if a River has Mobile objects
	// ready to enter the BoatCrossing.  Set BoatCrossing_one false
	// otherwise.
	public override void Update()	{
		aBoatIsComing = (bin.SpaceFront() <= boat_space);
		((BridgeRoad)cin).setPaint(aBoatIsComing);
		((BridgeRoad)cout).setPaint(aBoatIsComing);
	}

	// A BoatCrossing is not painted
	public override void Draw(Graphics g){}
}
