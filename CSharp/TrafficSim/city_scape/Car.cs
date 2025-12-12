// Car class
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

public class Car: Mobile
{
	// constants to define the car geometry
	// length taken by a car on the road
	private const double INIT_LENGTH = 18;
	private const double INIT_WIDTH = 5;
	private const double INIT_MAX_SPEED = 5;

	private const double INIT_FRONT_X  =  5;
	private const double INIT_FRONT_Y  =  0;
	private const double INIT_REAR_X   = -5;
	private const double INIT_REAR_Y   =  0;

	private const double INIT_FLTX =  3;
	private const double INIT_FLTY =  3.5;
	private const double INIT_FRTX =  3;
	private const double INIT_FRTY = -3.5;

	private const double INIT_RLTX = -3;
	private const double INIT_RLTY =  3.5;
	private const double INIT_RRTX = -3;
	private const double INIT_RRTY = -3.5;

	private const double INIT_TIRE_WIDTH = 2;


	private double front_x;	// x coordinate of front of car
	private double front_y;	// y coordinate of front of car
	private double rear_x;	// x coordinate of rear of car
	private double rear_y;	// y coordinate of rear of car

	private double fltx, flty, frtx, frty;  // front tire coordinates
	private double rltx, rlty, rrtx, rrty;  // rear tire coordinates

	private double width;		// width of car
	private double tire_width;	// width of tires.

	public Car(double x, double y, Scene scene): base(x, y, INIT_LENGTH, INIT_MAX_SPEED, scene)
	{
		front_x = INIT_FRONT_X;
		front_y = INIT_FRONT_Y;
		rear_x  = INIT_REAR_X;
		rear_y  = INIT_REAR_Y;

		fltx = INIT_FLTX;
		flty = INIT_FLTY;
		frtx = INIT_FRTX;
		frty = INIT_FRTY;

		rltx = INIT_RLTX;
		rlty = INIT_RLTY;
		rrtx = INIT_RRTX;
		rrty = INIT_RRTY;

		width = INIT_WIDTH;
		tire_width = INIT_TIRE_WIDTH;
	}

	// Rotate the coordinate of the car such that the car points in
	// in the direction of angle.  The units of angle is Radians.
	public override void SetDirection(double angle)
	{
		base.SetDirection(angle);

		front_x  = RotateX(INIT_FRONT_X,  INIT_FRONT_Y,  angle);
		front_y  = RotateY(INIT_FRONT_X,  INIT_FRONT_Y,  angle);
		rear_x   = RotateX(INIT_REAR_X,   INIT_REAR_Y,   angle);
		rear_y   = RotateY(INIT_REAR_X,   INIT_REAR_Y,   angle);

		fltx = RotateX(INIT_FLTX, INIT_FLTY, angle);
		flty = RotateY(INIT_FLTX, INIT_FLTY, angle);
		frtx = RotateX(INIT_FRTX, INIT_FRTY, angle);
		frty = RotateY(INIT_FRTX, INIT_FRTY, angle);

		rltx = RotateX(INIT_RLTX, INIT_RLTY, angle);
		rlty = RotateY(INIT_RLTX, INIT_RLTY, angle);
		rrtx = RotateX(INIT_RRTX, INIT_RRTY, angle);
		rrty = RotateY(INIT_RRTX, INIT_RRTY, angle);
	}

	// Paint the car to the display using the rotated coordinates.
	public override void Draw(Graphics g)
	{
		Pen pen = new Pen(Color.Black, (float)tire_width);
		// Front tires
		g.DrawLine(pen, (float)(x+fltx), (float)(y+flty),
						(float)(x+frtx), (float)(y+frty));

		// Rear tires
		g.DrawLine(pen, (float)(x+rltx), (float)(y+rlty),
						(float)(x+rrtx), (float)(y+rrty));
		// Car
		pen = new Pen(Color.Blue, (float)width);
		g.DrawLine(pen, (float)(x+front_x), (float)(y+front_y),
						(float)(x+rear_x), (float)(y+rear_y));
	}
}
