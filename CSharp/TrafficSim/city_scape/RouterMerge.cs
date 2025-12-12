// RouterMerge class
using System;
using System.Drawing;

//A RouterMerge will signal a crossing road to stop for an incoming train.
//It will be used to provide right of way for a train.

public class RouterMerge: Conveyor
{
	private double x, y;	// x and y coordinates of Merge
	private Conveyor in1;	// First incoming Conveyor
	private Conveyor in2;	// Second incoming Conveyor
	private Conveyor out1;	// outgoing Conveyor
	private Conveyor out2;	// outgoing Conveyor

	private double merge_space; // The space that cars should leave before the merge point.

	private bool merge_one; // If merge_one is true, then it is in1's turn to
								// advance it's front Mobile into the intersection.
								// Else, it is in2's turn.

	private bool merging; // If merging is true, then alternate between in1
							// and in2.

	// Construct a new Merge with coordinates (new_x, new_y) and
	// merge space new_merge_space.
	public RouterMerge(double new_x, double new_y, double new_merge_space, Scene scene): base(scene)
	{
		x = new_x;
		y = new_y;
		in1 = null;
		in2 = null;
		out1 = null;
		out2 = null;
		merge_space = new_merge_space;
		merge_one = true;
		merging = false;
	}


	// Construct a new Merge with incoming Conveyors new_in1,
	// and new_in2, outgoing Conveyors new_out, and with merge
	// space new_merge_space.
	public RouterMerge(Conveyor new_in1, Conveyor new_in2, Conveyor new_out1, Conveyor new_out2,
				double new_merge_space, Scene scene): base(scene)
	{
		in1 = new_in1;
		in2 = new_in2;
		out1 = new_out1;
		out2 = new_out2;

		if (in1.OutX() != out1.InX()) throw new ArgumentException("Merge in1.OutX() != out1.InX()");
		if (in1.OutY() != out1.InY()) throw new ArgumentException("Merge in1.OutY() != out1.InY()");
		if (in2.OutX() == out2.InX()) throw new ArgumentException("Merge in2.OutX() == out2.InX()");
		if (in2.OutY() == out2.InY()) throw new ArgumentException("Merge in2.OutY() == out2.InY()");

		x = out1.InX();
		y = out1.InY();

		in1.AddOut(this);
		in2.AddOut(this);
		out1.AddIn(this);
		out2.AddIn(this);

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
		if(in1 == null)
			in1 = new_in;
		else
			in2 = new_in;
	}


	// Add the outgoing Conveyor.  If the outgoing Conveyor has
	// already been set, then this method will produce undefined
	// behavior.
	public override void AddOut(Conveyor new_out) {
		
		if (out1==null)
			out1 = new_out;
		else
			out2 = new_out;
		//cout = new_out;
	}


	// Return the minimum of the space in front of the two incoming
	// Conveyors.
	public override double SpaceFront()
	{
		return Math.Min(in1.SpaceFront(), in2.SpaceFront());
	}

	// If it is the Conveyor me's turn to allows it's Mobile object to
	// enter the merge, then return the space in the rear of the
	// outgoing Conveyor.  Else return -merge_space, thus the incoming
	// Conveyor will not advance incoming Mobile objects closer than
	// merge_space before the merge.
	public override double SpaceRear(Conveyor me)
	{
		/*
		if(!merging ||
		   (merge_one && me == in1) ||
		   (!merge_one && me == in2)  )
			return cout.SpaceRear(this);
		else
			return -merge_space;
		*/
		return Math.Min(out1.SpaceRear(this),out2.SpaceRear(this));
	}


	// Hand off the mobile object to the outgoing Conveyor with
	// residual speed residual_speed.  If both incoming Conveyors
	// have Mobile objects which are within merge_space of the
	// merge intersection, then alternate the incoming Conveyor
	// allowed to advance its Mobile object into the merge
	// intersection.
	public override void HandOff(Mobile mobile, double residual_speed)
	{
		merge_one = !merge_one;
		out1.HandOff(mobile, residual_speed);
	}


	// Set merging true if both incoming Conveyors have Mobile objects
	// ready to enter the Merge.  Set merge_one to true if in1's
	// Mobile object is closer than in2's.  Set merge_one false
	// otherwise.
	public override void Update()
	{
		merging = (in1.SpaceFront() <= merge_space) &&
				  (in2.SpaceFront() <= merge_space);

		if(!merging)
			merge_one = in1.SpaceFront() < in2.SpaceFront();
	}

	// A Merge is not painted
	public override void Draw(Graphics g){}
}
