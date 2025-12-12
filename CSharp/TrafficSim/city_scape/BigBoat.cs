// BigBoat class

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

public class BigBoat: Mobile {
	// constants to define the BigBoat geometry
	// Front and rear positions of the BigBoat
	private const double INIT_FRONT_X  =  16;
	private const double INIT_FRONT_Y  =  0;
	private const double INIT_REAR_X   = -9;
	private const double INIT_REAR_Y   =  0;


	// Length taken by a BigBoat on the River (more than its physical length)
	private const double INIT_LENGTH = 40;

	private const double INIT_FRONT_WIDTH = 16;
	private const double INIT_MAX_SPEED = 5;
	private const double INIT_STACK_WIDTH = 8;

	private const double INIT_LStX = 2;
	private const double INIT_LStY = 0.5;
	private const double INIT_RStX = 2;
	private const double INIT_RStY = -0.5;

	private double front_x;	// x coordinate of front of BigBoat
	private double front_y;	// y coordinate of front of BigBoat
	private double rear_x;	// x coordinate of rear of BigBoat
	private double rear_y;	// y coordinate of rear of BigBoat

	private double lStX;
	private double lStY;
	private double rStX;
	private double rStY;

	private double width_front;		// width of BigBoat front
	private double width_stack;		// width of BigBoat smoke stack

	public BigBoat(double x, double y, Scene scene): base(x,y,INIT_LENGTH, INIT_MAX_SPEED, scene) {
		front_x = INIT_FRONT_X;
		front_y = INIT_FRONT_Y;
		rear_x  = INIT_REAR_X;
		rear_y  = INIT_REAR_Y;

		lStX = INIT_LStX;
		lStY = INIT_LStY;
		rStX = INIT_RStX;
		rStY = INIT_RStY;

		width_front = INIT_FRONT_WIDTH;
		width_stack = INIT_STACK_WIDTH;
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

		lStX = RotateX(INIT_LStX, INIT_LStY, angle);
		lStY = RotateY(INIT_LStX, INIT_LStY, angle);
		rStX = RotateX(INIT_RStX, INIT_RStY, angle);
		rStY = RotateY(INIT_RStX, INIT_RStY, angle);
	}

	// Paint the BigBoat to the display using the rotated coordinates.
	public override void Draw(Graphics g)
	{
		//pen.StartCap = LineCap.Triangle;
		//pen.EndCap = LineCap.Round;

		// Hull
		/*Pen pen = new Pen(Color.Brown, (float)width_front);
		pen.StartCap = LineCap.Triangle;
		g.DrawLine(pen, (float)(x+front_x), (float)(y+front_y),
						(float)x, (float)y);
		pen = new Pen(Color.Brown, (float)width_back);
		pen.EndCap = LineCap.Round;
		g.DrawLine(pen, (float)x, (float)y,
			(float)(x+rear_x), (float)(y+rear_y));*/

		float scaleFactor = 4.0f;
		float xFactor= (float)((rear_x-front_x)/scaleFactor), yFactor=(float)((rear_y-front_y)/scaleFactor);

		Pen pen = new Pen(Color.Brown, (float)width_front);
		pen.StartCap = LineCap.Triangle;
		pen.EndCap = LineCap.Round;
		for (int i=0; i<(int)scaleFactor; i++)	{
			g.DrawLine(pen, (float)(x-rear_x+xFactor*i), (float)(y-rear_y+yFactor*i),
				(float)( x-rear_x+xFactor*(i+1) ), (float)( y-rear_y+yFactor*(i+1) ) );
			pen.Width--;
//			if (pen.Color ==  Color.Black)
//				pen.Color = Color.White;
//			else
//				pen.Color = Color.Black;
		}

		//smoke stack
		pen = new Pen(Color.Black, (float)width_stack);
		pen.StartCap = LineCap.Round;
		pen.EndCap = LineCap.Round;
		g.DrawLine(pen, (float)(x+lStX), (float)(y+lStY),
			(float)(x+rStX), (float)(y+rStY));
	}
}
