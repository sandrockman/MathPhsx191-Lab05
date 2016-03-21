using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        bool menuLoop = true;

        Vector3D utilityPole = new Vector3D();
        Vector3D vectorLeg1 = new Vector3D();
        Vector3D vectorLeg2 = new Vector3D();
        Vector3D vectorLeg3 = new Vector3D();
        Vector3D currentVector = new Vector3D();
        Vector3D pointOnPole = new Vector3D();

        double tempMag = 0, tempHead = 0, tempPitch = 0;

        do
        {
            //clears screen.
            Console.Clear();
            //prompt and read length, pitch and heading of utility pole
            Console.WriteLine("Please enter length, pitch and heading of " +
                "utility pole\nIn meters and degrees.");
            Console.Write("Length: ");
            tempMag = Convert.ToDouble(Console.ReadLine());
            Console.Write("Pitch: ");
            tempMag = Convert.ToDouble(Console.ReadLine());
            Console.Write("Heading: ");
            tempMag = Convert.ToDouble(Console.ReadLine());
            utilityPole.SetRectGivenMagHeadPitch(tempMag, D2R(tempHead), D2R(tempPitch));

            //Show utility Pole information
            Console.WriteLine("Utility Pole Vector: <" + utilityPole.XValue + ", " +
                utilityPole.YValue + ", " + utilityPole.ZValue + ">.");
            Console.WriteLine("Mag: " + utilityPole.GetMagnitude() + "; Heading: " +
                utilityPole.GetHeading() + "; Pitch: " + utilityPole.GetPitch());
            
            //prompt and read magnitude, pitch and heading of 
            //first leg in bee journey
            Console.WriteLine("Please enter magnitude, pitch and heading of " +
                "first leg in bee journey.\nIn meters and degrees.");
            Console.Write("Magnitude: ");
            tempMag = Convert.ToDouble(Console.ReadLine());
            Console.Write("Pitch: ");
            tempMag = Convert.ToDouble(Console.ReadLine());
            Console.Write("Heading: ");
            tempMag = Convert.ToDouble(Console.ReadLine());
            vectorLeg1.SetRectGivenMagHeadPitch(tempMag, D2R(tempHead), D2R(tempPitch));

            //Displaying the current vector, equal to the first vector.
            currentVector = vectorLeg1;
            Console.WriteLine("Current Vector: <"+ currentVector.XValue +", "+ 
                currentVector.YValue +", "+ currentVector.ZValue +">.");
            Console.WriteLine("Mag: " + currentVector.GetMagnitude() +"; Heading: " + 
                currentVector.GetHeading() + "; Pitch: " + currentVector.GetPitch());
            pointOnPole = currentVector ^ utilityPole;
            Console.WriteLine("Closest Point on pole: ( " + pointOnPole.XValue + ", " +
                            pointOnPole.YValue + ", " + pointOnPole.ZValue + " ).");
            
            //prompt and read magnitude, pitch and heading of 
            //second leg in bee journey
            Console.WriteLine("Please enter magnitude, pitch and heading of " +
                "second leg in bee journey.\nIn meters and degrees.");
            Console.Write("Magnitude: ");
            tempMag = Convert.ToDouble(Console.ReadLine());
            Console.Write("Pitch: ");
            tempMag = Convert.ToDouble(Console.ReadLine());
            Console.Write("Heading: ");
            tempMag = Convert.ToDouble(Console.ReadLine());
            vectorLeg2.SetRectGivenMagHeadPitch(tempMag, D2R(tempHead), D2R(tempPitch));

            //Displaying the current vector after adding the second vector.
            currentVector = currentVector + vectorLeg2;
            Console.WriteLine("Current Vector: <" + currentVector.XValue + ", " +
                currentVector.YValue + ", " + currentVector.ZValue + ">.");
            Console.WriteLine("Mag: " + currentVector.GetMagnitude() + "; Heading: " +
                currentVector.GetHeading() + "; Pitch: " + currentVector.GetPitch());
            pointOnPole = currentVector ^ utilityPole;
            Console.WriteLine("Closest Point on pole: ( " + pointOnPole.XValue + ", " +
                            pointOnPole.YValue + ", " + pointOnPole.ZValue + " ).");
            
            //prompt and read magnitude, pitch and heading of 
            //third and final leg of bee journey
            Console.WriteLine("Please enter magnitude, pitch and heading of " +
                "third leg in bee journey.\nIn meters and degrees.");
            Console.Write("Magnitude: ");
            tempMag = Convert.ToDouble(Console.ReadLine());
            Console.Write("Pitch: ");
            tempMag = Convert.ToDouble(Console.ReadLine());
            Console.Write("Heading: ");
            tempMag = Convert.ToDouble(Console.ReadLine());
            vectorLeg3.SetRectGivenMagHeadPitch(tempMag, D2R(tempHead), D2R(tempPitch));

            //Displaying the current vector after adding the third vector.
            currentVector = currentVector + vectorLeg3;
            Console.WriteLine("Current Vector: <" + currentVector.XValue + ", " +
                currentVector.YValue + ", " + currentVector.ZValue + ">.");
            Console.WriteLine("Mag: " + currentVector.GetMagnitude() + "; Heading: " +
                currentVector.GetHeading() + "; Pitch: " + currentVector.GetPitch());
            pointOnPole = currentVector ^ utilityPole;
            Console.WriteLine("Closest Point on pole: ( " + pointOnPole.XValue + ", " +
                            pointOnPole.YValue + ", " + pointOnPole.ZValue + " ).");

            pointOnPole = utilityPole - currentVector;
            Console.WriteLine("Shortest Distance to end of utility pole: <" + currentVector.XValue + ", " +
                currentVector.YValue + ", " + currentVector.ZValue + ">.");
            Console.WriteLine("Mag: " + currentVector.GetMagnitude() + "; Heading: " +
                currentVector.GetHeading() + "; Pitch: " + currentVector.GetPitch());
            pointOnPole = currentVector ^ utilityPole;

            //question to halt loop and ask to see if exiting.
            Console.Write("Done Computing? (Y or N)");
            string input = Console.ReadLine();
            input = input.ToUpper();
            if (input[0] == 'Y')
                menuLoop = false;
        } while (menuLoop);
    }
    /// <summary>
    /// converts degrees to radians
    /// </summary>
    /// <param name="input">degree input to be converted to radians</param>
    /// <returns>double</returns>
    public static double D2R(double input)
    {
        return input * Math.PI / 180;
    }
}