using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace math191_lab01
{
    class Program
    {
        /// <summary>
        /// Menu Loop to choose comparison actions of program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //used to exit loop.
            bool menuLoop = true;
            do
            {
                //clear screen and present options to 
                Console.Clear();
                Console.Write("All units of measurement are in meters.\n" +
                    "0 - Sphere Overlap Check\n" + 
                    "1 - 2D Axially Aligned Bounding Boxes Overlap Check\n" +
                    "2 - 3D Axially Aligned Bounding Boxes Overlap Check\n" +
                    "3 - Line Segment Intersection\n" +
                    "Enter a numeral for the appropriate comparison: ");
                int checkAction = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch(checkAction)
                {
                    case 0://0 - Sphere Overlap Check
                        overlapOfSpheres();
                        break;
                    case 1://2D AABB
                        overlapBoundingBoxes2D();
                        break;
                    case 2://3D AABB
                        overlapBoundingBoxes3D();
                        break;
                    case 3://Line Segments
                        IntersectLineSegments();
                        break;
                    default:
                        Console.WriteLine("Improper choice entry. Try again.");
                        break;
                }
                //Asks to continue comparisons and takes data.
                Console.Write("\nWould you like to make another comparison?" +
                    "(Y or N): ");
                string checkLoop = Console.ReadLine();
                checkLoop = checkLoop.ToUpper();
                //reacts to data accordingly
                if(checkLoop[0] != 'Y')
                {
                    menuLoop = false;
                }
            } while (menuLoop);
            Console.Write("Goodbye.");
        }

        /// <summary>
        /// Collects data for for points which are groups as the endpoints for
        /// two line segments and are then checked to see if they intersect.
        /// Must first check to see if the LINES intersect, and then if the
        /// line SEGMENTS overlap. Uses Pythagorean Theorem, standard line form
        /// Cramer's Rule, and several comparisons.
        /// </summary>
        /// <pre>No two endpoints per line segment are equivalent 
        /// (ex. (0,0) & (0,0))</pre>
        static void IntersectLineSegments()
        {
            //set up x and y variables for line segment points on line 1
            double seg1x1 = 0, seg1y1 = 0, seg1x2 = 0, seg1y2 = 0;
            //set up x and y variables for line segment points on line 2
            double seg2x1 = 0, seg2y1 = 0, seg2x2 = 0, seg2y2 = 0;

            //Retrieve the x and y coordinates for the 1st point of segment 1
            Console.Write("Please enter x coord of 1st point of segment 1: ");
            seg1x1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter y coord of 1st point of segment 1: ");
            seg1y1 = Convert.ToDouble(Console.ReadLine());

            //Retrieve the x and y coordinates for the 2nd point of segment 1
            Console.Write("Please enter x coord of 2nd point of segment 1: ");
            seg1x2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter y coord of 2nd point of segment 1: ");
            seg1y2 = Convert.ToDouble(Console.ReadLine());

            //Retrieve the x and y coordinates for the 1st point of segment 2
            Console.Write(
                "\nPlease enter x coord of 1st point of segment 2: ");
            seg2x1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter y coord of 1st point of segment 2: ");
            seg2y1 = Convert.ToDouble(Console.ReadLine());

            //Retrieve the x and y coordinates for the 2nd point of segment 2
            Console.Write("Please enter x coord of 2nd point of segment 2: ");
            seg2x2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter y coord of 2nd point of segment 2: ");
            seg2y2 = Convert.ToDouble(Console.ReadLine());

            //Ax + By = C
            //(y1 - y2)x + (-x1 + x2)y = (x2 * y1) - (x1 * y2)
            //A = (y1 - y2)             D = A for seg2
            //B = (-x1 + x2)            E = B for seg2
            //C = (x2 * y1) - (x1 * y2) F = C for seg2

            //Use points for line segment 1 to create
            //standard line form equation, Ax + By = C
            double paramA = seg1y1 - seg1y2;
            double paramB = seg1x2 - seg1x1;
            double paramC = (seg1x2 * seg1y1) - (seg1x1 * seg1y2);
            //print out the equation for Ax + By = C
            Console.Write("\nThe equation for line segment 1 is: ");
            Console.WriteLine("({0:F2})x + ({1:F2})y = {2:F3}",
                paramA, paramB, paramC);
            
            //Use points for line segment 2 to create
            //standard line form equation, Dx + Ey = F
            double paramD = seg2y1 - seg2y2;
            double paramE = seg2x2 - seg2x1;
            double paramF = (seg2x2 * seg2y1) - (seg2x1 * seg2y2);
            //print out the equation for Dx + Ey = F
            Console.Write("\nThe equation for line segment 2 is: ");
            Console.WriteLine("({0:F2})x + ({1:F2})y = {2:F3}",
                paramD, paramE, paramF);

            //Using Cramer's Rule, and extrapolating to the simplest form,
            //we can see the x and y intercepts
            //x = (CE - FB)/(AE - DB)
            //y = (AF - DC)/(AE - DB)

            //top half of equation for x intercept.
            double CEminusFB = (paramC * paramE) - (paramF * paramB);
            //top half of equation for y intercept.
            double AFminusDC = (paramA * paramF) - (paramD * paramC);

            //Find bottom half for the equations to see if it equals zero.
            double AEminusDB = (paramA * paramE) - (paramD * paramB);
            //if the bottom half of the equation is not zero, continue,
            //otherwise print out the statement why not.
            if (AEminusDB == 0)
            {
                Console.WriteLine("The bottom half of the equations is zero.");
                //check if the segments are part of the same line by seeing if
                //at least one point is zero.
                if (CEminusFB == 0)
                {
                    Console.WriteLine("The segments are of the same line.");
                    //Now to see if the segments overlap. 
                    //Using code made for later section. Code is not optimized
                    //for this section, so using multiple boolean values.
                    //for the x values...
                    bool checkX1 = true, checkX2 = true;
                    //and for the y values.
                    bool checkY1 = true, checkY2 = true;
                    //check x & y for first endpoint
                    checkLineSegmentIntercept
                        (ref checkX1, seg2x1, seg2x2, seg1x1);
                    checkLineSegmentIntercept
                        (ref checkY1, seg2y1, seg2y2, seg1y1);
                    //check x & y for second endpoint
                    checkLineSegmentIntercept
                        (ref checkX2, seg2x1, seg2x2, seg1x2);
                    checkLineSegmentIntercept
                        (ref checkY2, seg2y1, seg2y2, seg1y2);
                    //Print out whether or not the line segments overlap
                    //check is within the line. Needs to check if both
                    //x and y of an end point are true at the same time
                    Console.WriteLine("The line segments {0}overlap.",
                        ((checkX1 && checkY1) || (checkX2 && checkY2)) ? 
                        "" : "Do Not ");
                }
                else
                {
                    Console.WriteLine("The two segments are distinct.");
                }
                //Console.WriteLine("The line segments are either parallel " +
                  //   "or the same line.\n This means there are either zero " +
                    // "points of intersection or infinite.");
            }
            else
            {
                //find x and y intercepts for the two lines and print result
                double xIntercept = CEminusFB / AEminusDB;
                double yIntercept = AFminusDC / AEminusDB;
                Console.WriteLine("The point of intercept is ({0:F2} , " +
                    "{1:F2}).", xIntercept, yIntercept);

                //boolean value initialized to check if 
                bool isIntersecting = true;
                //Check to see if the X-intercept is in proper parameters
                //Check on line segment 1
                checkLineSegmentIntercept
                    (ref isIntersecting, seg1x1, seg1x2, xIntercept);
                //Check on line segment 2 if no false value has been found
                if(isIntersecting)
                    checkLineSegmentIntercept
                        (ref isIntersecting, seg2x1, seg2x2, xIntercept);

                //Check to see if the Y-intercept is in proper parameters
                //Check on line segment 1if no false value has been found
                if(isIntersecting)
                    checkLineSegmentIntercept
                        (ref isIntersecting, seg1x1, seg1x2, xIntercept);
                //Check on line segment 2 if no false value has been found
                if(isIntersecting)
                    checkLineSegmentIntercept
                        (ref isIntersecting, seg2x1, seg2x2, xIntercept);

                //if isIntersecting is still true, then the two lines intersect
                //Print the outcome
                Console.WriteLine("The two line segments {0}intersect",
                    (isIntersecting ? "" : "Do Not "));
                

            }//end if-else to check line segment intersect
        }

        /// <summary>
        /// Checks to see if the intersectValue is within value1 and value2
        /// parameters. Use only with x values or y values of the same line
        /// segments at once
        /// </summary>
        /// <param name="isIntersecting">bool reference to use</param>
        /// <param name="value1">1st side of x or y for 1 line segment</param>
        /// <param name="value2">2nd side of x or y for 1 line segment</param>
        /// <param name="intersectValue">value</param>
        static void checkLineSegmentIntercept(ref bool isIntersecting, 
            double value1, double value2, double intersectValue)
        {
            //check orientation of the two values
            //then check if the intersect value is outside that orientation.
            //if so set to false
            if (value1 < value2)
            {//IV is less than low value(1) or greater than high value(2)
                if (value1 > intersectValue || intersectValue > value2)
                {
                    isIntersecting = false;
                }
            }
            else// if (value1 >= value2)
            {//IV is less than low value(2) or greater than high value(1)
                if (value1 < intersectValue || intersectValue < value2)
                {
                    isIntersecting = false;
                }
            }
        }  

        static void overlapBoundingBoxes2D()
        {
            //declare the characteristic points for box 1
            double box1x1 = 0, box1y1 = 0, box1x2 = 0, box1y2 = 0;
            //declare the characteristic points for box 2
            double box2x1 = 0, box2y1 = 0, box2x2 = 0, box2y2 = 0;

            //collect the x and y coordinates for box 1's characteristic points
            Console.Write
                ("Please enter x coord of lower-left point of box 1: ");
            box1x1 = Convert.ToDouble(Console.ReadLine());
            Console.Write
                ("Please enter y coord of lower-left point of box 1: ");
            box1y1 = Convert.ToDouble(Console.ReadLine());
            Console.Write
                ("Please enter x coord of upper-right point of box 1: ");
            box1x2 = Convert.ToDouble(Console.ReadLine());
            Console.Write
                ("Please enter y coord of upper-right point of box 1: ");
            box1y2 = Convert.ToDouble(Console.ReadLine());
            
            //collect the x and y coordinates for box 2's characteristic points
            Console.Write
                ("\nPlease enter x coord of lower-left point of box 2: ");
            box2x1 = Convert.ToDouble(Console.ReadLine());
            Console.Write
                ("Please enter y coord of lower-left point of box 2: ");
            box2y1 = Convert.ToDouble(Console.ReadLine());
            Console.Write
                ("Please enter x coord of upper-right point of box 2: ");
            box2x2 = Convert.ToDouble(Console.ReadLine());
            Console.Write
                ("Please enter y coord of upper-right point of box 2: ");
            box2y2 = Convert.ToDouble(Console.ReadLine());

            //calculate midpoint ranges for box 1 (stand-ins for center) 
            double box1MidpointX = MidpointCalc(box1x1, box1x2);
            double box1MidpointY = MidpointCalc(box1y1, box1y2);

            //calculate midpoint ranges for box 2 (stand-ins for center) 
            double box2MidpointX = MidpointCalc(box2x1, box2x2);
            double box2MidpointY = MidpointCalc(box2y1, box2y2);

            //collect half side distances for both boxes' x value
            double box1HalfX = BoxHalfSide(box1x1, box1x2);
            double box2HalfX = BoxHalfSide(box2x1, box2x2);
            //collect the distance between the midpoints, axially for x
            double xDistance = MidDistances(box1MidpointX, box2MidpointX);
            //find distance of half sides for x combined
            double xHalfSum = box1HalfX + box2HalfX;
            //Print data to screen.
            Console.WriteLine("\nThe distance between x midpoints is: {0:f2}.",
                                xDistance);
            Console.WriteLine("The distance the sum of half of " +
                                "each x side is: {0:f2}.", xHalfSum);
            //if they overlap, then continue with other values, else stop.
            if (BoxOverlap(xDistance, xHalfSum))
            {
                Console.WriteLine("\nX sides overlap.\n" +
                    "X midpoint distance is less than or equal to " + 
                    "the sum of half-sides for x.");
                //collect half side distances for both boxes' y value 
                double box1HalfY = BoxHalfSide(box1y1, box1y2);
                double box2HalfY = BoxHalfSide(box2y1, box2y2);
                //collect the distance between the midpoints, axially for y
                double yDistance = MidDistances(box1MidpointY, box2MidpointY);
                //find distance of half sides for y combined
                double yHalfSum = box1HalfY + box2HalfY;
                //print data to screen
                Console.WriteLine("\nThe distance between y midpoints is: " +
                                "{0:f2}.", yDistance);
                Console.WriteLine("The distance the sum of half of each " +
                                "y side is: {0:f2}.", yHalfSum);
                //if they overlap, then they overlap, else stop.
                if (BoxOverlap(yDistance, yHalfSum))
                    Console.WriteLine("\nDOES OVERLAP.\n" +
                        "Y midpoint distance is less than or equal to " +
                        "the sum of half-sides for y.");
                else
                    Console.WriteLine("\nDOES NOT OVERLAP.\n" +
                        "Y midpoint distance is greater than " +
                        "the sum of half-sides for y.");

            }//end y compare
            else
                Console.WriteLine("\nDOES NOT OVERLAP.\n" + 
                    "X midpoint distance is greater than " + 
                    "the sum of half-sides for x.");
        }//end overlapBoundingBoxes2D

        static void overlapBoundingBoxes3D()
        {
            //declare the characteristic points for box 1
            double box1x1 = 0, box1y1 = 0, box1z1 = 0; 
            double box1x2 = 0, box1y2 = 0, box1z2 = 0;
            //declare the characteristic points for box 2
            double box2x1 = 0, box2y1 = 0, box2z1 = 0;
            double box2x2 = 0, box2y2 = 0, box2z2 = 0;
            
            //collect x, y, and data for 1st point of AABB 1.
            Console.Write("Please enter x coord of lower-left point of box 1: ");
            box1x1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter y coord of lower-left point of box 1: ");
            box1y1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter z coord of lower-left point of box 1: ");
            box1z1 = Convert.ToDouble(Console.ReadLine());
            //collect x, y, and data for 2nd point of AABB 1.
            Console.Write("Please enter x coord of upper-right point of box 1: ");
            box1x2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter y coord of upper-right point of box 1: ");
            box1y2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter z coord of upper-right point of box 1: ");
            box1z2 = Convert.ToDouble(Console.ReadLine());

            //collect x, y, and data for 1st point of AABB 2.
            Console.Write("\nPlease enter x coord of lower-left point of box 2: ");
            box2x1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter y coord of lower-left point of box 2: ");
            box2y1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter z coord of lower-left point of box 2: ");
            box2z1 = Convert.ToDouble(Console.ReadLine());
            //collect x, y, and data for 2nd point of AABB 2.
            Console.Write("Please enter x coord of upper-right point of box 2: ");
            box2x2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter y coord of upper-right point of box 2: ");
            box2y2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter z coord of upper-right point of box 2: ");
            box2z2 = Convert.ToDouble(Console.ReadLine());

            //calculate midpoint ranges for box 1 (stand-ins for center)
            double box1MidpointX = MidpointCalc(box1x1, box1x2);
            double box1MidpointY = MidpointCalc(box1y1, box1y2);
            double box1MidpointZ = MidpointCalc(box1z1, box1z2);

            //calculate midpoint ranges for box 2 (stand-ins for center)
            double box2MidpointX = MidpointCalc(box2x1, box2x2);
            double box2MidpointY = MidpointCalc(box2y1, box2y2);
            double box2MidpointZ = MidpointCalc(box2z1, box2z2);

            //collect half side distances for both boxes' x value
            double box1HalfX = BoxHalfSide(box1x1, box1x2);
            double box2HalfX = BoxHalfSide(box2x1, box2x2);
            //collect the distance between the midpoints, axially for x
            double xDistance = MidDistances(box1MidpointX, box2MidpointX);
            //find distance of half sides for x combined
            double xHalfSum = box1HalfX + box2HalfX;

            Console.WriteLine("\nThe distance between x " +
                                "midpoints is: {0:f2}.", xDistance);
            Console.WriteLine("The distance the sum of half of each " +
                                "x side is: {0:f2}.", xHalfSum);
            //if they overlap, then continue with other values, else stop.
            if (BoxOverlap(xDistance, xHalfSum))
            {
                Console.WriteLine("\nX sides overlap.\n" +
                    "X midpoint distance is less than or equal to " +
                    "the sum of half-sides for x.");
                //collect half side distances for both boxes' y value
                double box1HalfY = BoxHalfSide(box1y1, box1y2);
                double box2HalfY = BoxHalfSide(box2y1, box2y2);
                //collect the distance between the midpoints, axially for y
                double yDistance = MidDistances(box1MidpointY, box2MidpointY);
                //find distance of half sides for y combined
                double yHalfSum = box1HalfY + box2HalfY;

                Console.WriteLine("\nThe distance between y " +
                                "midpoints is: {0:f2}.", yDistance);
                Console.WriteLine("The distance the sum of half of each " +
                                "y side is: {0:f2}.", yHalfSum);
                //if they overlap, then continue with other value, else stop.
                if (BoxOverlap(yDistance, yHalfSum))
                {
                    Console.WriteLine("\nY Sides Overlap.\n" +
                        "Y midpoint distance is less than or equal to " +
                        "the sum of half-sides for y.");
                    //collect half side distances for both boxes' z value
                    double box1HalfZ = BoxHalfSide(box1z1, box1z2);
                    double box2HalfZ = BoxHalfSide(box2z1, box2z2);
                    //collect the distance between the midpoints, axially for z
                    double zDistance = 
                        MidDistances(box1MidpointZ, box2MidpointZ);
                    //find distance of half sides for y combined
                    double zHalfSum = box1HalfZ + box2HalfZ;

                    Console.WriteLine("\nThe distance between z " +
                                    "midpoints is: {0:f2}.", zDistance);
                    Console.WriteLine("The distance the sum of half of each " +
                                    "z side is: {0:f2}.", zHalfSum);
                    //if they overlap, then all axes overlap, else stop.
                    if (BoxOverlap(zDistance, zHalfSum))
                    {
                        //all three axes overlap. Huzzah
                        Console.WriteLine("\nZ Sides Overlap.\n" +
                            "\nBOXES OVERLAP.\n" +
                            "Z midpoint distance is less than or equal to " +
                            "the sum of half-sides for Z.");
                    }
                    else
                        Console.WriteLine("\nDOES NOT OVERLAP.\n" +
                            "Z midpoint distance is greater than " +
                            "the sum of half-sides for z.");
                }//end z overlap check
                else
                    Console.WriteLine("\nDOES NOT OVERLAP.\n" +
                        "Y midpoint distance is greater than " +
                        "the sum of half-sides for y.");

            }//end y overlap check
            else
                Console.WriteLine("\nDOES NOT OVERLAP.\n" +
                    "X midpoint distance is greater than " +
                    "the sum of half-sides for x.");

        }//end overlapBoundingBoxes3D

        /// <summary>
        /// returns a midpoint for either x, y, or z coord on an AABB
        /// </summary>
        /// <param name="point1">1st appropriate coord portion</param>
        /// <param name="point2">2nd appropriate coord portion</param>
        /// <returns>number between the two presented</returns>
        static double MidpointCalc(double point1, double point2)
        {
            return (point1 + point2)/2;
        }

        /// <summary>
        /// finds the distance between the two midpoint coordinate
        /// parts given (x's, y's, or z's) and finds the distance between
        /// the two, then returns that found value.
        /// </summary>
        /// <param name="midpoint1">middle of coord part for 1st box</param>
        /// <param name="midpoint2">middle of coord part for 2nd box</param>
        /// <returns>distance between points, in absolute form.</returns>
        static double MidDistances(double midpoint1, double midpoint2)
        {
            return Math.Abs(midpoint1 - midpoint2);
        }

        /// <summary>
        /// finds the length of half the side of a box parameter (x, y, or z)
        /// then returns the finding
        /// </summary>
        /// <param name="point1">endpoint for box coord. part</param>
        /// <param name="point2">opposite endpoint for box coord. part</param>
        /// <returns>half the side of a box (radius in one direction)</returns>
        static double BoxHalfSide(double point1, double point2)
        {
            return Math.Abs(point1 - point2)/2;
        }

        /// <summary>
        /// simple boolean check to see if the boxes overlap on 1 specific axis
        /// can be redone for later 
        /// </summary>
        /// <param name="distance">distance between midpoints on 1 axis</param>
        /// <param name="HalfSum">sum of two box radii on 1 axis</param>
        /// <returns>true if boxes overlap on axis, else false</returns>
        static bool BoxOverlap(double distance, double HalfSum)
        {
            if (distance < HalfSum)
                return true;
            else
                return false;
        }

        static void overlapOfSpheres()
        {
            //declare center coords. and radius for sphere 1
            double x1 = 0, y1 = 0, z1 = 0, r1 = 0;
            //declare center coords. and radius for sphere 2
            double x2 = 0, y2 = 0, z2 = 0, r2 = 0;

            //collect center coords. and radius for first sphere
            Console.Write("Please enter x coord of first sphere: ");
            x1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter y coord of first sphere: ");
            y1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter z coord of first sphere: ");
            z1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter radius of first sphere: ");
            r1 = Convert.ToDouble(Console.ReadLine());

            //collect center coords. and radius for second sphere
            Console.Write("Please enter x coord of second sphere: ");
            x2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter y coord of second sphere: ");
            y2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter z coord of second sphere: ");
            z2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Please enter radius of second sphere: ");
            r2 = Convert.ToDouble(Console.ReadLine());

            //calculate and print to screen the distance between the 
            //center points of the two spheres.
            Console.Write("The square of the distance " +
                            "between the center points is: ");
            double distance = calcSphereOverlap(x1, y1, z1, x2, y2, z2);
            Console.WriteLine("{0:f2}", distance);
            //calculate the sum of the radii and print to the screen.
            double radSum = ((r1 + r2) * (r1 + r2));
            Console.WriteLine("The square of the sum of the " +
                            "radii is: {0:f2}", radSum);
            
            //if the distance between the spheres is greater than the sum
            //of the radii, then it doesn't overlap. Else they do.
            if (distance > radSum)
                Console.WriteLine("The spheres don't overlap since " +
                    "distance is greater than sum of the radii.");
            else
                Console.WriteLine("The spheres overlap since distance is " +
                            "less than or equal than the sum of the radii.");
        }

        /// <summary>
        /// finds the square of the distance for the two sphere and returns it.
        /// </summary>
        /// <param name="x1">x coord for sphere 1</param>
        /// <param name="y1">y coord for sphere 1</param>
        /// <param name="z1">z coord for sphere 1</param>
        /// <param name="x2">x coord for sphere 2</param>
        /// <param name="y2">y coord for sphere 2</param>
        /// <param name="z2">z coord for sphere 2</param>
        /// <returns>sqare of the distance between 
        /// the two center points</returns>
        static double calcSphereOverlap(double x1, double y1, double z1,
                                double x2, double y2, double z2)
        {
            return (Math.Pow((x1 - x2),2) + 
                            Math.Pow((y1 - y2),2) + 
                            Math.Pow((z1 - z2),2));
        }
    }
}
