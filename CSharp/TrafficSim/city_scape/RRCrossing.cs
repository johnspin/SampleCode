// RRCrossing class
using System;
using System.Drawing;

// A RRCrossing is a 1 dimensional Conveyor which connects two incoming
// Conveyors to 1 outgoing Conveyor.  The RRCrossing decides which
// incoming Mobile to let enter the RRCrossing intersection and controls
// the other incoming Mobile objects so that no collisions occur.
// The train_space of a RRCrossing is the amount of distance before the RRCrossing
// that the Mobile objects are kept while the intersection is occupied.
// If the RRCrossing intersection is clear (no traffic) and only one incoming
// Conveyor has a Mobile object that is close to the RRCrossing, then this
// Mobile object is allowed into the RRCrossing without delay.  If both
// incoming Conveyors contain Mobile objects that are within the
// train_space of the RRCrossing, then only one of these Mobile objects is
// let into the intersection at a time and the incoming Conveyor
// that is allowed to send its Mobile object into the intersection
// is alternated.  This Conveyor makes use of the Conveyor  parameter
// in spaceRear(Conveyor) since the RRCrossing will want to know which of
// its incoming Conveyors is asking if there is space.  Depending on
// the situation, the RRCrossing can report to the Conveyor that there is
// space, and thus the Conveyor should advance its Mobile object into
// the intersection, or that there there is another Mobile object about
// to enter the intersection, and thus the Conveyor should keep its
// Mobile objects back.  Note that spaceRear can return any value,
// positive or negative.  Returning a negative value prevents an
// incoming Conveyor from advancing its inbound Mobile objects within
// a distance of that value, thus keeping the intersection clear for
// the other incoming Conveyor to advance its Mobile objects.  This
// is controled by the spaceRear() method.

public class RRCrossing: Conveyor
{
	private double x, y;	// x and y coordinates of RRCrossing
	private Conveyor rrin;	// incoming RRTrack
	private Conveyor cin;	// Second incoming Conveyor
	private Conveyor rrout;	// outgoing RRTrack
	private Conveyor cout;	// outgoing Conveyor

	private double train_space; // The space that cars should leave before the RRCrossing point.

	private bool aTrainIsComing; // If aTrainIsComing, then hold all traffic on cin

	// Construct a new RRCrossing with coordinates (new_x, new_y) and
	// RRCrossing space new_train_space.
	public RRCrossing(double new_x, double new_y, double new_train_space, Scene scene): base(scene)
	{
		x = new_x;
		y = new_y;
		rrin = null;
		cin = null;
		rrout = null;
		cout = null;
		train_space = new_train_space;
		aTrainIsComing = false;
	}


	// Construct a new RRCrossing with incoming Conveyors new_in1,
	// and new_in2, outgoing Conveyors new_out, and with RRCrossing
	// space new_train_space.
	public RRCrossing(Conveyor new_rrin, Conveyor new_cin, Conveyor new_rrout, Conveyor new_cout,
				double new_train_space, Scene scene): base(scene)
	{
		rrin = new_rrin;
		cin = new_cin;
		rrout = new_rrout;
		cout = new_cout;

		if (cin.OutX() != cout.InX()) throw new ArgumentException("RRCrossing in1.OutX() != out.InX()");
		if (cin.OutY() != cout.InY()) throw new ArgumentException("RRCrossing in1.OutY() != out.InY()");
		if (rrin.OutX() == cout.InX()) throw new ArgumentException("RRCrossing in2.OutX() == out.InX()");
		if (rrin.OutY() == cout.InY()) throw new ArgumentException("RRCrossing in2.OutY() == out.InY()");

		x = cout.InX();
		y = cout.InY();

		rrin.AddOut(this);
		cin.AddOut(this);
		rrout.AddIn(this);
		cout.AddIn(this);

		train_space = new_train_space;
		aTrainIsComing = false;
	}

	// Return the x and y coordinates of this RRCrossing.
	public override double InX() { return x; }
	public override double InY() { return y; }
	public override double OutX() { return x; }
	public override double OutY() { return y; }

	// Add set the incoming Conveyors.  If both incoming Conveyors
	// have already been set, then this method will produce undefined
	// behavior.
	public override void AddIn(Conveyor new_in)	{
		if (cin == null && !(new_in is RRTrack))
			cin = new_in;

		if (rrin == null && new_in is RRTrack)
			rrin = new_in;
	}

	// Add the outgoing Conveyors.  If the outgoing Conveyors have
	// already been set, then this method will produce undefined
	// behavior.
	public override void AddOut(Conveyor new_out) {
		if (cout == null && !(new_out is RRTrack))
			cout = new_out;

		if (rrout == null && new_out is RRTrack)
			rrout = new_out;
	}

	// Return the minimum of the space in front of the two incoming
	// Conveyors.
	public override double SpaceFront()	{
		return Math.Min(cin.SpaceFront(), rrin.SpaceFront());
	}

	// If it is the Conveyor me's turn to allows it's Mobile object to
	// enter the RRCrossing, then return the space in the rear of the
	// outgoing Conveyor.  Else return -train_space, thus the incoming
	// Conveyor will not advance incoming Mobile objects closer than
	// train_space before the RRCrossing.
	public override double SpaceRear(Conveyor me)	{
		if(aTrainIsComing && me == cin)
			return -train_space;
		else if (aTrainIsComing && me == rrin)
			return rrout.SpaceRear(this);
		else if (me == rrin)
			return rrout.SpaceRear(this);
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
	// have Mobile objects which are within train_space of the
	// RRCrossing intersection, then stop the Road and the Train objects
	// are allowed to advance its Mobile object into the RRCrossing
	// intersection.
	public override void HandOff(Mobile mobile, double residual_speed)	{
		if (aTrainIsComing)
			rrout.HandOff(mobile, residual_speed);
		else
			cout.HandOff(mobile, residual_speed);
	}

	// Set aTrainIsComing true if a RRTRack has Mobile objects
	// ready to enter the RRCrossing.  Set RRCrossing_one false
	// otherwise.
	public override void Update()	{
		aTrainIsComing = (rrin.SpaceFront() <= train_space);

	}

	// A RRCrossing is not painted
	public override void Draw(Graphics g){}
}
