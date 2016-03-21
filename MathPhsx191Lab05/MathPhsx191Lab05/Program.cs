using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * @author Victor Haskins
 * class Program using Forward Euler method for tracking, check distance of
 * box movement as friction works against it
 */

class Program
{
    const double TIMESTEP = 0.10; //seconds
    const double GRAVITY = 9.8; //meters per seconds squared

    static void Main(string[] args)
    {
        BoxForce();
        RocketForce();
    }

    static void BoxForce()
    {
        double frictionCoefficient = 0;
        double initSpeed = 0;

        double velocity;
        double friction; //will be reverse acceleration.
        double position = 0;
        double time = 0;

        Console.WriteLine("Changing force with friction...");
        Console.Write("Enter the coefficient of friction: ");
        frictionCoefficient = Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter the initial speed of box: ");
        initSpeed = Convert.ToDouble(Console.ReadLine());

        friction = -1 * frictionCoefficient * GRAVITY;
        velocity = initSpeed;

        while(velocity > 0)
        {
            position += velocity * TIMESTEP;
            velocity += friction * TIMESTEP;
            time += TIMESTEP;
        }

        Console.WriteLine("Final Position: " + position + "m, Time elapsed: " +
                            time + "s.");
    }

    static void RocketForce()
    {
        //initialize variables as per lab instructions
        double time = 0;      //seconds
        double mass = 0.0742; //kg
        double oneOverMass = 1 / mass;
        double coefOfWindResist = 0.02;
        double thrustMag = 10;   //Newtons, magnitude
        double heading = 23 * Math.PI / 180;
        double pitch = 62 * Math.PI / 180;

        Vector3D position = new Vector3D();
        position.SetRectGivenRect(0, 0, 0.2);

        Vector3D thrustVector = new Vector3D();
        thrustVector.SetRectGivenMagHeadPitch(thrustMag, heading, pitch);

        Vector3D velocity = new Vector3D();
        velocity.SetRectGivenRect(0, 0, 0);

        Vector3D thrustAccel = new Vector3D();
        thrustAccel = oneOverMass & thrustVector;
        Vector3D gravityAccel = new Vector3D();
        gravityAccel.SetRectGivenRect(0, 0, -GRAVITY);

        Vector3D acceleration = gravityAccel + thrustAccel;
        //apply thrust
        while(time <= 1.0)
        {
            position = position + (TIMESTEP & velocity);
            velocity = velocity + (TIMESTEP & acceleration);
            acceleration = (gravityAccel + thrustAccel) - (coefOfWindResist & velocity);
            Console.Write("Time" + time + "; ");
            position.PrintRect();
            time += TIMESTEP;
        }

        while (position.GetZ() > 0)
        {
            position = position + (TIMESTEP & velocity);
            velocity = velocity + (TIMESTEP & acceleration);
            acceleration = gravityAccel - (coefOfWindResist & velocity);
            time += TIMESTEP;
        }
        Console.WriteLine("Final Position:");
        position.PrintRect();
        Console.WriteLine("Time Elapsed" + time);
    }
}
