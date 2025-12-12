// TrainCar class

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

public class TrainCar: Mobile
{
	// constants to define the TrainCar geometry
	// Front and rear positions of the TrainCar
	private const double INIT_FRONT_X  =  11;
	private const double INIT_FRONT_Y  =  0;
	private const double INIT_REAR_X   = -11;
	private const double INIT_REAR_Y   =  0;


	// Length taken by a TrainCar on the track (more than its physical length)
	private const double INIT_LENGTH = 25;

	private const double INIT_WIDTH = 12;
	private const double INIT_MAX_SPEED = 5;

	private double front_x;	// x coordinate of front of TrainCar
	private double front_y;	// y coordinate of front of TrainCar
	private double rear_x;	// x coordinate of rear of TrainCar
	private double rear_y;	// y coordinate of rear of TrainCar

	private double width;		// width of TrainCar

	public TrainCar(double x, double y, Scene scene): base(x,y,INIT_LENGTH, INIT_MAX_SPEED, scene)
	{
		front_x = INIT_FRONT_X;
		front_y = INIT_FRONT_Y;
		rear_x  = INIT_REAR_X;
		rear_y  = INIT_REAR_Y;

		width = INIT_WIDTH;
	}


	// Rotate the coordinate of the TrainCar such that the TrainCar points in
	// in the direction of angle.  The units of angle is Radians.
	public override void SetDirection(double angle)
	{
		base.SetDirection(angle);

		front_x  = RotateX(INIT_FRONT_X,  INIT_FRONT_Y,  angle);
		front_y  = RotateY(INIT_FRONT_X,  INIT_FRONT_Y,  angle);
		rear_x   = RotateX(INIT_REAR_X,   INIT_REAR_Y,   angle);
		rear_y   = RotateY(INIT_REAR_X,   INIT_REAR_Y,   angle);

	}

	// Paint the TrainCar to the display using the rotated coordinates.
	public override void Draw(Graphics g)
	{
		// TrainCar
		Pen pen = new Pen(Color.SteelBlue, (float)width);
		g.DrawLine(pen, (float)(x+front_x), (float)(y+front_y),
			(float)(x+rear_x), (float)(y+rear_y));
	}
}
