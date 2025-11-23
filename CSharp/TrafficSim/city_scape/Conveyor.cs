// Conveyor class

public abstract class Conveyor: SceneElement
{
    // Construct a Conveyor object with layer value 0.
    public Conveyor(Scene scene): base(scene){}

    // Construct a Conveyor object with layer value new_layer.
		public Conveyor(int new_layer, Scene scene): base(new_layer, scene){}

    // Return the x and y coordinates of the start of the Conveyor
    public abstract double InX();
    public abstract double InY();


    // Return the x and y coordinates of the end of the Conveyor.
    // Same as the start coordinates if the Conveyor is 1 dimensional.
    public abstract double OutX();
    public abstract double OutY();


    // Add an incoming Conveyor: a Conveyor that will hand off
    // Mobile objects to this Conveyor.
    public abstract void AddIn(Conveyor new_in);


    // Add an outgoing Conveyor: a Conveyor that will receive Mobile
    // objects that are handed off by this Conveyor.
    public abstract void AddOut(Conveyor new_out);


    // The space in front of this Conveyor:  The distance between the
    // front most car and the end of the Conveyor.
    public abstract double SpaceFront();



    // The space in the rear of the Conveyor:  The distance bewteen
    // the rear most car and the start of the Conveyor.  This method
    // is used to determine if there is enough space to hand off a
    // Mobile object to this Conveyor.  Returning a value less than
    // the actual space can be used to prevent incoming Conveyors
    // from handing off a Mobile object to this Coveyor.  This can be
    // used to hold up traffic such as with a StopLight.  The Conveyor
    // _in_ passes itself to identify to this Conveyor which incoming
    // Conveyor is requesting to see if there is enough space.  This is
    // used in merging situations: when a choice of which Conveyor to
    // grant space to needs to be made.  Thus, this method allows in a
    // very general way the ability to control incoming traffic.
    public abstract double SpaceRear(Conveyor cin);


    // Hand off mobile to this Conveyor.  residual_speed is the
    // remaining speed (distance per update) that this mobile will
    // travel after being handed off.
    public abstract void HandOff(Mobile mobile, double residual_speed);
}
