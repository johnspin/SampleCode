using System;
using System.Drawing;

// AccidentDestination class

public class AccidentDestination: Conveyor
{
	private double x, y;

	public AccidentDestination(double new_x, double new_y, Scene scene): base(scene)
	{
		x = new_x;
		y = new_y;
	}

	public override double InX() { return x; }
	public override double InY() { return y; }
	public override double OutX() { return x; }
	public override double OutY() { return y; }

	public override void AddIn(Conveyor new_in){}
	public override void AddOut(Conveyor new_out){}

	public override double SpaceFront(){return 0;}


	// Return Double.MaxValue, the maximum double value.  Thus there is always
	// space for incoming Mobile objects.
	public override double SpaceRear(Conveyor me) {return Double.MaxValue;}


	// Delete the incoming Mobile object mobile from the sceneList.
	public override void HandOff(Mobile mobile, double residual_speed)
	{
		//Accident a = new Accident();
		scene.Remove(mobile);

		// this one //Accident.GetIntoAccident();
	}
	public override void Update(){}
	public override void Draw(Graphics g){}
}