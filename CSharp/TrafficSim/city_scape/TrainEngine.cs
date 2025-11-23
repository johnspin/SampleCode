// TrainEngine class

using System;
using System.Drawing;
using System.Drawing.Drawing2D;


public class TrainEngine: Mobile 
{
	// constants to define the TrainEngine geometry
	// Front and rear positions of the TrainEngine
	private const double INIT_FRONT_X  =  9;
	private const double INIT_FRONT_Y  =  0;
	private const double INIT_REAR_X   = -11;
	private const double INIT_REAR_Y   =  0;


	// Length taken by a TrainEngine on the track (more than its physical length)
	private const double INIT_LENGTH = 32;

	private const double INIT_FRONT_WIDTH = 16;
	private const double INIT_MAX_SPEED = 3;
	private const double INIT_STACK1_WIDTH = 8;
	private const double INIT_STACK2_WIDTH = 5;

	private const double INIT_LSt1X = -2;
	private const double INIT_LSt1Y = 0.5;
	private const double INIT_RSt1X = -2;
	private const double INIT_RSt1Y = -0.5;

	private const double INIT_LSt2X = 9;
	private const double INIT_LSt2Y = 0.5;
	private const double INIT_RSt2X = 9;
	private const double INIT_RSt2Y = -0.5;

	private double front_x;	// x coordinate of front of TrainEngine
	private double front_y;	// y coordinate of front of TrainEngine
	private double rear_x;	// x coordinate of rear of TrainEngine
	private double rear_y;	// y coordinate of rear of TrainEngine

	private double lSt1X;
	private double lSt1Y;
	private double rSt1X;
	private double rSt1Y;

	private double lSt2X;
	private double lSt2Y;
	private double rSt2X;
	private double rSt2Y;

	private double width_front;		// width of TrainEngine front
	private double width_stack1;		// width of TrainEngine smoke stack
	private double width_stack2;

	public TrainEngine(double x, double y, Scene scene): base(x,y,INIT_LENGTH, 
		INIT_MAX_SPEED, scene) 
	{
		front_x = INIT_FRONT_X;
		front_y = INIT_FRONT_Y;
		rear_x  = INIT_REAR_X;
		rear_y  = INIT_REAR_Y;

		lSt1X = INIT_LSt1X;
		lSt1Y = INIT_LSt1Y;
		rSt1X = INIT_RSt1X;
		rSt1Y = INIT_RSt1Y;

		lSt2X = INIT_LSt2X;
		lSt2Y = INIT_LSt2Y;
		rSt2X = INIT_RSt2X;
		rSt2Y = INIT_RSt2Y;		

		width_front = INIT_FRONT_WIDTH;
		width_stack1 = INIT_STACK1_WIDTH;
		width_stack2 = INIT_STACK2_WIDTH;
	}

	// Rotate the coordinate of the BigBoat such that the BigBoat points in
	// in the direction of angle.  The units of angle is Radians.
	public override void SetDirection(double angle)
	{
		base.SetDirection(angle);

		front_x = RotateX(INIT_FRONT_X,  INIT_FRONT_Y,  angle);
		front_y = RotateY(INIT_FRONT_X,  INIT_FRONT_Y,  angle);
		rear_x  = RotateX(INIT_REAR_X,   INIT_REAR_Y,   angle);
		rear_y  = RotateY(INIT_REAR_X,   INIT_REAR_Y,   angle);

		lSt1X = RotateX(INIT_LSt1X, INIT_LSt1Y, angle);
		lSt1Y = RotateY(INIT_LSt1X, INIT_LSt1Y, angle);
		rSt1X = RotateX(INIT_RSt1X, INIT_RSt1Y, angle);
		rSt1Y = RotateY(INIT_RSt1X, INIT_RSt1Y, angle);

		lSt2X = RotateX(INIT_LSt2X, INIT_LSt2Y, angle);
		lSt2Y = RotateY(INIT_LSt2X, INIT_LSt2Y, angle);
		rSt2X = RotateX(INIT_RSt2X, INIT_RSt2Y, angle);
		rSt2Y = RotateY(INIT_RSt2X, INIT_RSt2Y, angle);
	}

	// Paint the TrainEngine to the display using the rotated coordinates.
	public override void Draw(Graphics g)
	{
		float scaleFactor = 5.0f;
		float xFactor= (float)((rear_x-front_x)/scaleFactor), 
			yFactor=(float)((rear_y-front_y)/scaleFactor);

		Pen pen = new Pen(Color.Gray, (float)width_front);
		pen.StartCap = LineCap.Triangle;
		pen.EndCap = LineCap.Square;
		for (int i=0; i<(int)scaleFactor; i++)	
		{
			g.DrawLine(pen, (float)(x-rear_x+xFactor*i), (float)(y-rear_y+yFactor*i),
				(float)( x-rear_x+xFactor*(i+1) ), (float)( y-rear_y+yFactor*(i+1) ) );
			pen.Width--;
		}

		//smoke stack
		pen = new Pen(Color.Black, (float)width_stack1);
		pen.StartCap = LineCap.Round;
		pen.EndCap = LineCap.Round;
		g.DrawLine(pen, (float)(x+lSt1X), (float)(y+lSt1Y),
			(float)(x+rSt1X), (float)(y+rSt1Y));

		//smoke stack
		pen = new Pen(Color.Black, (float)width_stack2-3);
		pen.StartCap = LineCap.Round;
		pen.EndCap = LineCap.Round;
		g.DrawLine(pen, (float)(x+lSt2X), (float)(y+lSt2Y),
			(float)(x+rSt2X), (float)(y+rSt2Y));
	}
}