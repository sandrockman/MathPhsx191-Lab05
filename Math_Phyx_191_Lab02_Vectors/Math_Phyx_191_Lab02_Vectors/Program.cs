using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * @author Victor Haskins
 * class Program loops through menu to read vector data and print out variants
 * of the vector data to be used in future programs.
 */


    class Program
    {
        /// <summary>
        /// Reads in user input and calls functions to store and print vector
        /// data.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool menuLoop = true;
            int vectorType = 0;
            int inputType = 0;
            int angleType = 0;

            Vector3D vector = new Vector3D();
            do
            {
                //clears screen.
                Console.Clear();
                //vector type to find.
                Console.Write("[2] : 2D Vector\n[3] : 3D Vector\n" +
                            "Please Enter Vector Type: ");
                vectorType = Convert.ToInt32(Console.ReadLine());
                //rectangular or MHP inputs
                Console.Write("\n[1] : Rectangular Inputs" + 
                            "\n[2] : Angular Inputs\n" +
                            "Please Enter Input Type: ");
                inputType = Convert.ToInt32(Console.ReadLine());
                //if entering MHP inputs, pointing out if answers are being
                //entered in degrees or radians.
                if (inputType == 2)
                {
                    Console.Write("\n[1] : Degree Angles\n[2] : Radian Angles\n" +
                                "Please Enter Input Type: ");
                    angleType = Convert.ToInt32(Console.ReadLine());
                }
                //parses if calling the function used to enter rectangular or MHP data
                //if entering MHP data, also passing whether using degrees or radians
                switch(inputType)
                {
                    case 1:
                        SetRectWithRect(vector, vectorType);
                        break;
                    case 2:
                        SetRectWithAngle(vector, vectorType, angleType);
                        break;
                    default:
                        Console.Write("Improper input parameter(s)");
                        break;

                }

                //prints out data depending on whether it was a 2D or 3D vector
                switch(vectorType)
                {
                    case 2:
                        Console.WriteLine("2D Vector Info:");
                        Console.WriteLine("\nRectangular Info:");
                        vector.PrintRect();
                        Console.WriteLine("\nMagnitude and Heading Info");
                        vector.PrintMagHeadPitch();
                        Console.WriteLine("\nEuler Angular Info:");
                        vector.PrintAngles();
                        break;
                    case 3:
                        Console.WriteLine("3D Vector Info:");
                        Console.WriteLine("\nRectangular Info:");
                        vector.PrintRect();
                        Console.WriteLine("\nMagnitude, Heading, and Pitch Info");
                        vector.PrintMagHeadPitch();
                        Console.WriteLine("\nEuler Angular Info:");
                        vector.PrintAngles();
                        break;
                    default:
                        break;
                }

                //question to halt loop and ask to see if exiting.
                Console.Write("Done Computing? (Y or N)");
                string input = Console.ReadLine();
                input = input.ToUpper();
                if (input[0] == 'Y')
                    menuLoop = false;
            } while(menuLoop);
        }

        /// <summary>
        /// using rectangular inputs, sets the rectangular variables in the 
        /// vector class by calling the right functions
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="vectorType"></param>
        public static void SetRectWithRect(Vector3D vector, int vectorType)
        {

            //declare and initialize
            double newX = 0;
            double newY = 0;
            double newZ = 0;
            //read in data
            Console.Write("Enter x coord: ");
            newX = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter y coord: ");
            newY = Convert.ToDouble(Console.ReadLine());
            //read in z data if a 3D vector and set data in vector class
            //accordingly.
            if(vectorType == 3)
            {
                Console.Write("Enter z coord: ");
                newZ = Convert.ToDouble(Console.ReadLine());
                vector.SetRectGivenRect(newX, newY, newZ);
            }
            else
            {
                vector.SetRectGivenRect(newX, newY);
            }
        }

        /// <summary>
        /// using angular inputs, sets the rectangular variables in the 
        /// vector class by calling the right functions
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="vectorType"></param>
        /// <param name="angleType"></param>
        public static void SetRectWithAngle(Vector3D vector, int vectorType, int angleType)
        {
            double magnitude = 0;
            double heading = 0;
            double pitch = 0;

            Console.Write("Enter Magnitude: ");
            magnitude = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter Heading Angle (xy plane): ");
            heading = Convert.ToDouble(Console.ReadLine());
            //if angle type being inputted is degrees, convert to radians
            if (angleType == 1)
                heading = ConvertToRadians(heading);

            //read pitch if it is a 3D vector and set the data accordingly.
            if(vectorType == 3)
            {
                Console.Write("Enter Pitch Angle (Z): ");
                pitch = Convert.ToDouble(Console.ReadLine());
                //if angle type being inputted is degrees, convert to radians
                if (angleType == 1)
                    pitch = ConvertToRadians(pitch);
                vector.SetRectGivenMagHeadPitch(magnitude, heading, pitch);
            }
            else
            {
                vector.SetRectGivenPolar(magnitude, heading);
            }
        }
        /// <summary>
        /// takes input and converts it to radians if originally entered as degrees.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double ConvertToRadians(double input)
        {
            return input * Math.PI / 180;
        }
        
    }