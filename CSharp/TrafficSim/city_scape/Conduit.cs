using System;
using System.Drawing;
using System.Collections;

// Conduit class

// A Conduit is an object which conveys potentially mutilple
// mobile objects over a distance, i.e. a Road, or a Sidewalk.

public abstract class Conduit: Conveyor
{
	protected double in_x, in_y;	 // x and y coordinates of start of Conduit
	protected double out_x, out_y; // x and y coordinates of end of Conduit
	protected Conveyor cin;	 // The Conveyor handing off Mobile objects
					         // to this Conduit.
	protected Conveyor cout;        // The Conveyor to which this Conduit will
									// hand off Mobile objects.

	// The Mobile objects currently being conveyed by this Conduit.
	protected MobileList mobile_list = new MobileList();
	protected double speed_limit;     // The speed limit of this Conduit.
	protected double angle;           // angle in Radians of Conduit

	// used to prevent infinite recursion on space check
	// in case the conduit is part of a loop
	// see spaceFront and spaceRear
	protected bool checked_space;

	// Set angle in the direction that this Conduit heads.
	protected void SetAngle()
	{
		double x = out_x - in_x;
		double y = out_y - in_y;
		angle = Math.Atan2(y, x);
	}

	// Construct a new Conduit with incoming Conveyor new_in,
	// outgoing Conveyor new_out, speed limit new_speed_limit
	// at layer 0
	public Conduit(Conveyor new_in, Conveyor new_out,
					double new_speed_limit, Scene scene): base(scene)
	{
		cin = new_in;
		cout = new_out;

		// Initialize the coordinates of this Conduit based on
		// the Conveyors that it is connected to.

		in_x = cin.OutX();
		in_y = cin.OutY();
		out_x = cout.InX();
		out_y = cout.InY();

		SetAngle();

		speed_limit = new_speed_limit;

		// Add this Conduit to the incoming and outgoing Conveyors
		// of the Conveyors that this Conduit is connected to.

		cin.AddOut(this);
		cout.AddIn(this);

		checked_space = false;
	}

	// Construct a new Conduit with incoming Conveyor new_in, out
	// going Conveyor new_out, speed limit new_speed_limit at
	// layer new_layer
	public Conduit(Conveyor new_in, Conveyor new_out,
			double new_speed_limit, int new_layer, Scene scene): base(new_layer,scene)
	{
		cin = new_in;
		cout = new_out;

		// Initialize the coordinates of this Conduit based on
		// the Conveyors that it is connected to.

		in_x = cin.OutX();
		in_y = cin.OutY();
		out_x = cout.InX();
		out_y = cout.InY();

		SetAngle();

		speed_limit = new_speed_limit;

		// Add this Conduit to the incoming and outgoing Conveyors
		// of the Conveyors that this Conduit is connected to.

		cin.AddOut(this);
		cout.AddIn(this);

		checked_space = false;
	}

	// Construct a new Conduit with incoming coordinates
	// (new_in_x, new_in_y), outgoing coordinates
	// (new_out_x, new_out_y), speed limit new_speed_limit
	// at layer 0
	public Conduit(double new_in_x, double new_in_y,
			double new_out_x, double new_out_y,
			double new_speed_limit, Scene scene): base(scene)
	{
		// Initialize the coordinates of this Conduit.
		in_x = new_in_x;
		in_y = new_in_y;
		out_x = new_out_x;
		out_y = new_out_y;

		SetAngle();

		speed_limit = new_speed_limit;

		checked_space = false;
	}

	// Construct a new Conduit with incoming coordinates
	// (new_in_x, new_in_y), outgoing coordinates
	// (new_out_x, new_out_y), speed limit new_speed_limit
	// at layer new_layer
	public Conduit(double new_in_x, double new_in_y,
			double new_out_x, double new_out_y,
			double new_speed_limit, int new_layer, Scene scene): base(new_layer, scene)
	{
		// Initialize the coordinates of this Conduit.
		in_x = new_in_x;
		in_y = new_in_y;
		out_x = new_out_x;
		out_y = new_out_y;

		SetAngle();

		speed_limit = new_speed_limit;

		checked_space = false;
	}

	// Return the x and y coordinates of the incoming
	// and outgoing Conveyors.
	public override double InX() { return in_x; }
	public override double InY() { return in_y; }
	public override double OutX() { return out_x; }
	public override double OutY() { return out_y; }

	// Set the incoming Conveyor to new_in.  This is used when the
	// second constructor was used to create this Conduit, and the
	// constructor of another Conveyor is adding this Conduit to its
	// incoming or outgoing Conveyors.
	public override void AddIn(Conveyor new_in) { cin = new_in; }
	public override void AddOut(Conveyor new_out) { cout = new_out; }


	// Return the amount of space in front of the first Mobile in the
	// Conduit.  If there is no Mobile in this Conduit, return the
	// length of the conduit plus the space in the front of the
	// incoming Conveyor.
	public override double SpaceFront()
	{
		if(mobile_list.IsEmpty())
		{
			if(checked_space)
				return Double.MaxValue;
			else
			{
				checked_space = true;
				double space = Distance(in_x, in_y, out_x, out_y) + cin.SpaceFront();
				checked_space = false;
				return space;
			}
		}
		else
		{
			Mobile mobile = mobile_list.PeekFront();
			return Distance(mobile.GetX(), mobile.GetY(), out_x, out_y) - mobile.GetLength()/2;
		}
	}

	// Return the amout of space in back of the last Mobile in the
	// Conduit.  If there is no last Mobile in this Conduit, return
	// the length of the conduit plus the space in the rear of the
	// outgoing Conveyor.
	public override double SpaceRear(Conveyor me)
	{
		if(mobile_list.IsEmpty())
		{
			if(checked_space)
				return Double.MaxValue;
			else
			{
				checked_space = true;
				double space = Distance(in_x, in_y, out_x, out_y) + cout.SpaceRear(this);
				checked_space = false;
				return space;
			}
		}
	else
	{
		Mobile mobile = mobile_list.PeekRear();
		return Distance(in_x, in_y, mobile.GetX(), mobile.GetY()) - mobile.GetLength()/2;
	}
	}

	// Receive a Mobile handed off to this Conduit.  residual_speed
	// is the remaining distance that mobile will move in this update.
	// if the residual_speed is greater than the length of the Conduit,
	// then hand off the mobile to the outgoing Conveyor with a
	// residual speed of residual_speed less the Conduit's length.

	public override void HandOff(Mobile mobile, double residual_speed)
	{
		double length_of_conduit = Distance(in_x, in_y, out_x, out_y);

		if(residual_speed > length_of_conduit)
			cout.HandOff(mobile, residual_speed - length_of_conduit);
		else
		{
			// Set the layer so that we see the vehicles on top of the conduit
			// Choose +2 instead of +1 in case the conduit has some details (e.g. a SceneLine)
			// already painted in the +1 layer.
//*-*-*			mobile.SetLayer(layer+2);
			mobile.SetLayer(layer+2);
			mobile_list.InsertRear(mobile);
			mobile.SetPosition(in_x, in_y);
			mobile.SetDirection(angle);
			mobile.Move(residual_speed);
		}
	}

	// Update the Mobile object on the Conduit.  Move the Mobile along
	// the Conduit.  The speed of each Mobile is limited by the maximum
	// speed of the Mobile, the speed limit of the Conduit, and the
	// available space in front of each Mobile.
	public override void Update()
	{
		MobileList.MobileListIterator mobiles = mobile_list.GetIterator();
		Mobile front_mobile = null;

		while(mobiles.HasMoreElements())
		{
			Mobile mobile = mobiles.NextElement();

			if(mobile.Moved())
				continue;

			mobile.Moved(true);

			double speed = Math.Min(speed_limit, mobile.GetMaxSpeed());

			if(front_mobile == null)
			{
				// If there is no Mobile in front of this mobile, then
				// Check to see if it is necessary to hand off this mobile
				// to the next Conveyor.

				double distance_to_out = Distance(mobile.GetX(), mobile.GetY(), out_x, out_y);

				double space_in_front =
				distance_to_out - mobile.GetLength()/2 + cout.SpaceRear(this);

				// Determine the speed (distance per update) that this
				// mobile should be moved.

				speed = Math.Max(0, Math.Min(speed, space_in_front));

				if(speed >= distance_to_out)
				{
					cout.HandOff(mobile, speed - distance_to_out);
					mobile_list.RemoveFront();
					mobile = null;
				}
				else
					mobile.Move(speed);
			}
			else
			{
				double space_in_front =
				Distance(front_mobile.GetX(), front_mobile.GetY(), mobile.GetX(), mobile.GetY()) -
				front_mobile.GetLength()/2 - mobile.GetLength()/2;

				speed = Math.Min(speed, space_in_front);

				mobile.Move(speed);
			}

			front_mobile = mobile;
		}
	}
}
