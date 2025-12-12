// Merge3 class
using System;
using System.Drawing;

// A Merge3 is a 1 dimensional Conveyor which connects three incoming
// Conveyors to 1 outgoing Conveyor.  The Merge3 decides which
// incoming Mobile to let enter the Merge3 intersection and controls
// the other incoming Mobile objects so that no collisions occur.
// The merge_space of a Merge3 is the amount of distance before the merge
// that the Mobile objects are kept while the intersection is occupied.
// If the Merge3 intersection is clear (no traffic) and only one incoming
// Conveyor has a Mobile object that is close to the merge, then this
// Mobile object is allowed into the merge without delay.  If both
// incoming Conveyors contain Mobile objects that are within the
// Merge3_space of the Merge3, then only one of these Mobile objects is
// let into the intersection at a time and the incoming Conveyor
// that is allowed to send its Mobile object into the intersection
// is alternated.  This Conveyor makes use of the Conveyor  parameter
// in spaceRear(Conveyor) since the Merge3 will want to know which of
// its incoming Conveyors is asking if there is space.  Depending on
// the situation, the Merge3 can report to the Conveyor that there is
// space, and thus the Conveyor should advance its Mobile object into
// the intersection, or that there there is another Mobile object about
// to enter the intersection, and thus the Conveyor should keep its
// Mobile objects back.  Note that spaceRear can return any value,
// positive or negative.  Returning a negative value prevents an
// incoming Conveyor from advancing its inbound Mobile objects within
// a distance of that value, thus keeping the intersection clear for
// the other incoming Conveyor to advance its Mobile objects.  This
// is controled by the spaceRear() method.

public class Merge3: Conveyor
{
	private double x, y;	// x and y coordinates of Merge
	private Conveyor in1;	// First incoming Conveyor
	private Conveyor in2;	// Second incoming Conveyor
	private Conveyor in3;	// Third incoming Conveyor
	private Conveyor cout;	// outgoing Conveyor

	private double merge_space; // The space that cars should leave before the merge point.

	private bool merge_one; // If merge_one is true, then it is in1's turn to
	private bool merge_two; 	// advance it's front Mobile into the intersection.
								// Else, it is in2's turn.

	private bool merging; // If merging is true, then rotate between in1, in2 and in3.

	// Construct a new Merge with coordinates (new_x, new_y) and
	// merge space new_merge_space.
	public Merge3(double new_x, double new_y, double new_merge_space, Scene scene): base(scene)
	{
		x = new_x;
		y = new_y;
		in1 = null;
		in2 = null;
		in3 = null;
		cout = null;
		merge_space = new_merge_space;
		merge_one = true;
		merge_two = false;
		merging = false;
	}


	// Construct a new Merge with incoming Conveyors new_in1,
	// and new_in2, outgoing Conveyors new_out, and with merge
	// space new_merge_space.
	public Merge3(Conveyor new_in1, Conveyor new_in2, Conveyor new_in3, Conveyor new_out,
				double new_merge_space, Scene scene): base(scene)
	{
		in1 = new_in1;
		in2 = new_in2;
		in3 = new_in3;
		cout = new_out;

		if (in1.OutX() != cout.InX()) throw new ArgumentException("Merge in1.OutX() != out.InX()");
		if (in1.OutY() != cout.InY()) throw new ArgumentException("Merge in1.OutY() != out.InY()");
		if (in2.OutX() == cout.InX()) throw new ArgumentException("Merge in2.OutX() == out.InX()");
		if (in2.OutY() == cout.InY()) throw new ArgumentException("Merge in2.OutY() == out.InY()");
		if (in3.OutX() == cout.InX()) throw new ArgumentException("Merge in3.OutX() == out.InX()");
		if (in3.OutY() == cout.InY()) throw new ArgumentException("Merge in3.OutY() == out.InY()");

		x = cout.InX();
		y = cout.InY();

		in1.AddOut(this);
		in2.AddOut(this);
		cout.AddIn(this);

		merge_space = new_merge_space;
		merge_one = true;
		merging = false;
	}

	// Return the x and y coordinates of this Merge.
	public override double InX() { return x; }
	public override double InY() { return y; }
	public override double OutX() { return x; }
	public override double OutY() { return y; }


	// Add set the incoming Conveyors.  If both incoming Conveyors
	// have already been set, then this method will produce undefined
	// behavior.
	public override void AddIn(Conveyor new_in)
	{
		if (in1 == null)
			in1 = new_in;
		else if (in2 == null)
			in2 = new_in;
		else
			in3 = new_in;
	}


	// Add the outgoing Conveyor.  If the outgoing Conveyor has
	// already been set, then this method will produce undefined
	// behavior.
	public override void AddOut(Conveyor new_out) { cout = new_out; }


	// Return the minimum of the space in front of the two incoming
	// Conveyors.
	public override double SpaceFront()
	{
		return Math.Min(in1.SpaceFront(), Math.Min(in2.SpaceFront(), in3.SpaceFront()));
	}

	// If it is the Conveyor me's turn to allows it's Mobile object to
	// enter the merge, then return the space in the rear of the
	// outgoing Conveyor.  Else return -merge_space, thus the incoming
	// Conveyor will not advance incoming Mobile objects closer than
	// merge_space before the merge.
	public override double SpaceRear(Conveyor me)
	{
		if(!merging ||
		   (merge_one && me == in1) ||
		   (!merge_one && me == in2)  )
			return cout.SpaceRear(this);
		else
			return -merge_space;
	}


	// Hand off the mobile object to the outgoing Conveyor with
	// residual speed residual_speed.  If both incoming Conveyors
	// have Mobile objects which are within merge_space of the
	// merge intersection, then alternate the incoming Conveyor
	// allowed to advance its Mobile object into the merge
	// intersection.
	public override void HandOff(Mobile mobile, double residual_speed)
	{
		//*-*-*-*//
		if (merge_one)	{
			merge_one = !merge_one;
			merge_two = !merge_two;
		}
		else if (merge_two)	{
			merge_one = !merge_one;
			merge_two = !merge_two;
		}
		//*-*-*-*//
		cout.HandOff(mobile, residual_speed);
	}


	// Set merging true if all incoming Conveyors have Mobile objects
	// ready to enter the Merge.  Set merge_one to true if in1's
	// Mobile object is closer than in2's.  Set merge_one false
	// otherwise.
	public override void Update()
	{
		merging = (in1.SpaceFront() <= merge_space) &&
			(in2.SpaceFront() <= merge_space)&&
			(in3.SpaceFront() <= merge_space);

		//*-*-*-*//
		if(!merging && merge_one)
			merge_one = in1.SpaceFront() < in2.SpaceFront();
		else if(!merging && merge_two)
			merge_two = in2.SpaceFront() < in3.SpaceFront();
		//*-*-*-*//
	}

	// A Merge is not painted
	public override void Draw(Graphics g){}
}
