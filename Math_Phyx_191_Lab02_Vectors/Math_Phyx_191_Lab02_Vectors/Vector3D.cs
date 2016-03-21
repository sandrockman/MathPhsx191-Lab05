using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * @author Victor Haskins
 * class Vector3D stores Vector information that can be retrieved and printed
 * in rectangular, angular, and Euler methods
 */


    public class Vector3D
    {
        public double XValue { get; private set; }
        public double YValue { get; private set; }
        public double ZValue { get; private set; }
        public double WValue { get; private set; }
        
        /// <summary>
        /// Default constructor for the Vector3D class to be modified later
        /// </summary>
        public Vector3D()
        {
            XValue = 0;
            YValue = 0;
            ZValue = 0;
            WValue = 1;
        }

        /// <summary>
        /// takes 2D rectangular coordinates for saving into Vector3D
        /// </summary>
        /// <param name="newX">x value to be saved</param>
        /// <param name="newY">y value to be saved</param>
        public void SetRectGivenRect(double newX, double newY)
        {
            XValue = newX;
            YValue = newY;
        }

        /// <summary>
        /// takes rectangular coordinates for saving into Vector3D
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <param name="newZ"></param>
        public void SetRectGivenRect(double newX, double newY, double newZ)
        {
            XValue = newX;
            YValue = newY;
            ZValue = newZ;
        }

        /// <summary>
        /// Takes the magnitude and heading, converts it into
        /// rectangular coordinates for saving.
        /// </summary>
        /// <param name="magnitude"></param>
        /// <param name="heading"></param>
        public void SetRectGivenPolar(double magnitude, double heading)
        {
            XValue = magnitude * Math.Cos(heading);
            YValue = magnitude * Math.Sin(heading);
        }

        /// <summary>
        /// Takes the magnitude, heading and pitch, converts it into
        /// rectangular coordinates for saving.
        /// </summary>
        /// <param name="magnitude"></param>
        /// <param name="heading"></param>
        /// <param name="pitch"></param>
        public void SetRectGivenMagHeadPitch(double magnitude, 
            double heading, double pitch)
        {
            XValue = magnitude * Math.Cos(pitch) * Math.Cos(heading);
            YValue = magnitude * Math.Cos(pitch) * Math.Sin(heading);
            ZValue = magnitude * Math.Sin(pitch);
        }

        /// <summary>
        /// Prints the rectangular coordinates
        /// </summary>
        public void PrintRect()
        {
            Console.WriteLine("Rectangular Vector Coordinates: " +
                "< {0:F2} , {1:F2} , {2:F2} >", XValue, YValue, ZValue);
        }

        /// <summary>
        /// Prints the Magnitude, Heading and Pitch to the screen
        /// </summary>
        public void PrintMagHeadPitch()
        {
            Console.WriteLine("Magnitude: {0:F5}", GetMagnitude());
            if (GetMagnitude() == 0)
            {
                Console.WriteLine("Because magnitude is zero, " +
                    "produced zero vector.");
            }
            else
            {
                if (GetMagnitude("twoD") == 0)
                {
                    Console.WriteLine("Because 2D, xy magnitude is zero, " +
                    "would produce error.");
                }
                else
                {
                    Console.WriteLine("Heading: {0:F5} degrees", GetHeading());
                }
                Console.WriteLine("Pitch: {0:F5} degrees", GetPitch());
            }
        }

        /// <summary>
        /// prints the Euler angles
        /// </summary>
        public void PrintAngles()
        {
            Console.WriteLine("Euler Angles");
            if (GetMagnitude() != 0)
            {
                Console.WriteLine("Alpha: {0:F5}", GetAlpha());
                Console.WriteLine("Beta: {0:F5}", GetBeta());
                Console.WriteLine("Gamma: {0:F5}", GetGamma());
            }
            else
            {
                Console.WriteLine("Magnitude is zero, so answers are invalid.");
                Console.WriteLine("Should return:\nAlpha: 0");
                Console.WriteLine("Beta: 0");
                Console.WriteLine("Gamma: 0");
            }
        }

        /// <summary>
        /// returns the Magnitude, essentially the distance of the line
        /// from the origin point to the adjusted endpoint, showing the vector
        /// </summary>
        /// <returns>magnitude of vector</returns>
        public double GetMagnitude()
        {
            return Math.Sqrt(Math.Pow(XValue,2) + 
                             Math.Pow(YValue,2) + 
                             Math.Pow(ZValue,2));
        }
        
        /// <summary>
        /// special case code to overload GetMagnitude. This will automatically
        /// return only the magnitude on the XY plane. Error checking if
        /// someone tries to break it.
        /// </summary>
        /// <param name="twoD"></param>
        /// <returns>magnitude of XY plane of vector in most cases</returns>
        public double GetMagnitude(string twoD)
        {
            //in case of error, will redirect to full magnitude, but overloading
            //should render this moot.
            if (String.IsNullOrEmpty(twoD))
                return GetMagnitude();
            else
                return Math.Sqrt(Math.Pow(XValue, 2) +
                                 Math.Pow(YValue, 2));
        }
        /// <summary>
        /// calculates and returns the Pitch, Z angle off of the XY plane
        /// </summary>
        /// <returns>Pitch</returns>
        public double GetPitch()
        {
            //returns zero if magnitude results in a zero vector
            if (GetMagnitude() == 0)
                return 0;
            else
                return Math.Asin(ZValue / GetMagnitude()) * 180 / Math.PI;
        }

        /// <summary>
        /// calculates and returns the Heading, the angle on an XY plane
        /// that is off of a designated x positive origin rotating towards a
        /// positive y origin and continues.
        /// </summary>
        /// <returns>Heading</returns>
        public double GetHeading()
        {
            //returns zero if 2D magnitude results in a zero vector
            if (GetMagnitude("twoD") == 0)
                return 0;
            else
            {
                if (YValue < 0)//angle should appear in quadrant 3 or 4 sub. angle from 360
                    return 360 - Math.Abs(Math.Acos(XValue / GetMagnitude("twoD")) * 180 / Math.PI);
                else//quadrant 1 or two
                    return Math.Acos(XValue / GetMagnitude("twoD")) * 180 / Math.PI;
            }
        }

        /// <summary>
        /// returns the alpha angle for a vector
        /// </summary>
        /// <returns>alpha angle</returns>
        public double GetAlpha()
        {
            //returns zero if magnitude results in a zero vector
            if (GetMagnitude() == 0)
                return 0;
            else
                return Math.Acos(XValue / GetMagnitude()) * 180 / Math.PI;
        }

        /// <summary>
        /// Returns the Beta angle for a vector
        /// </summary>
        /// <returns>Beta angle in degrees</returns>
        public double GetBeta()
        {
            //returns zero if magnitude results in a zero vector
            if (GetMagnitude() == 0)
                return 0;
            else
                return Math.Acos(YValue / GetMagnitude()) * 180 / Math.PI;
        }

        /// <summary>
        /// Returns the Gamma Value
        /// </summary>
        /// <returns>gamma angle in degrees</returns>
        public double GetGamma()
        {
            //returns zero if magnitude results in a zero vector
            if (GetMagnitude() == 0)
                return 0;
            else
                return Math.Acos(ZValue / GetMagnitude()) * 180 / Math.PI;
        }

        /// <summary>
        /// returns the XValue
        /// </summary>
        /// <returns>XValue</returns>
        public double GetX()
        {
            return XValue;
        }

        /// <summary>
        /// returns the YValue
        /// </summary>
        /// <returns>YValue</returns>
        public double GetY()
        {
            return YValue;
        }

        /// <summary>
        /// returns the ZValue
        /// </summary>
        /// <returns>ZValue</returns>
        public double GetZ()
        {
            return ZValue;
        }
    }
