using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConvertLocation
{
    class DataController
    {
        public static List<string> getLinesFromFile(string directory)
        {
            List<string> lines = new List<string>(); 
            string line = "";

            // Read the file line by line
            System.IO.StreamReader file = new System.IO.StreamReader(directory);
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line.ToLower());
            }

            return lines;
        }

        public static Coordinate getCoordinateFromPlaceName(string place, string directory)
        {
            Coordinate coordinate = new Coordinate();
            coordinate.lati = "";
            coordinate.longi = "";
            string line = "";
            bool found = false;

            // Read the file line by line
            System.IO.StreamReader file = new System.IO.StreamReader(directory);
            while ((line = file.ReadLine()) != null)
            {
                string[] column = getSplitLine(line, "\t");
                //string[] placeNames = getSplitLine(column[3], ",");
                //foreach(string placeName in placeNames)
                //{
                    if(!found && column[1].ToLower().Equals(place))
                    {
                        coordinate.lati = column[4];
                        coordinate.longi = column[5];
                        found = true;
                    }
                //}
            }

            if(!found)
            {
                System.IO.StreamReader file2 = new System.IO.StreamReader(directory);
                while ((line = file2.ReadLine()) != null)
                {
                    string[] column = getSplitLine(line, "\t");
                    string[] placeNames = getSplitLine(column[3], ",");
                    foreach(string placeName in placeNames)
                    {
                        if (!found && placeName.ToLower().Equals(place))
                        {
                            coordinate.lati = column[4];
                            coordinate.longi = column[5];
                            found = true;
                        }
                    }
                }
            }

            return coordinate;
        }

        public static string[] getSplitLine(string line, string spliter)
        {
            string[] words = line.Split(new string[] {spliter}, StringSplitOptions.RemoveEmptyEntries);

            return words;
        }

        public static void writeDistinctLocation(List<String> lines, string directory, string outputFile)
        {
            foreach (string line in lines)
            {
                //Console.WriteLine(line);
                File.AppendAllText(directory + outputFile, line + Environment.NewLine);
            }
        }

        public static void writeCoordinateLocation(List<string> placeNames, string directory, string gazetteer, string outputFile)
        {
            foreach(string placeName in placeNames)
            {
                Console.WriteLine(placeName);
                Coordinate coordinate = new Coordinate();
                coordinate = getCoordinateFromPlaceName(placeName, directory + gazetteer);
                File.AppendAllText(directory + outputFile, coordinate.lati + "\t" + coordinate.longi + Environment.NewLine);
            }
        }

        public static void createDirectory(string dirFileName, string dirOutput)
        {
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(dirFileName);
            while ((line = file.ReadLine()) != null)
            {
                System.IO.Directory.CreateDirectory(dirOutput + line);
            }
        }

        public static void saveLocationRawProvince(string dirLocation, string dirRawLocation, string dirRawLocationProvince)
        {
            // for each province
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(@"D:\data_location_provinsi.txt");
            while ((line = file.ReadLine()) != null)
            {
                // for each line location
                string location = "";
                int counter = 0;
                System.IO.StreamReader file2 = new System.IO.StreamReader(dirLocation);
                while ((location = file2.ReadLine()) != null)
                {
                    location = location.ToLower();
                    counter++;
                    if (line.Equals(location))
                    {
                        string rawLocation = File.ReadLines(dirRawLocation).Skip(counter - 1).Take(1).First();
                        File.AppendAllText(dirRawLocationProvince + line + @"\data_location_raw.txt", counter + "\t" + rawLocation + Environment.NewLine);
                    }
                }
            }
        }

        public static void getLocationFromRawProvince(string dirRawLocationProvince)
        {
            // for each province
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(@"D:\data_location_provinsi.txt");
            while ((line = file.ReadLine()) != null)
            {
                // for each raw location in province
                string location = "";
                System.IO.StreamReader file2 = new System.IO.StreamReader(dirRawLocationProvince + line + @"\data_location_raw.txt");
                while ((location = file2.ReadLine()) != null)
                {
                    string[] lineSplit = location.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
            
                    // remove time
                    string text = "";
                    //Regex regex = new Regex(@"(?<=.*, ).*?(?= \|)");
                    Regex regex = new Regex(@".*(?=,.* \|)");
                    Match match = regex.Match(lineSplit[1]);
                    if (match.Success)
                    {
                        text = match.Value;

                        // get location before province
                        text = Regex.Replace(text, @".*,\s+", string.Empty);

                        // check if result is "The Jakarta Post", "thejakartapost.com", "The Associated Press"
                        if (text.ToLower().Equals("the jakarta post") || text.ToLower().Equals("thejakartapost.com") ||
                            text.ToLower().Equals("the associated press") || text.ToLower().Equals("associated press"))
                        {
                            text = line;
                        }
                    }

                    // store the result on file
                    File.AppendAllText(dirRawLocationProvince + line + @"\data_location_real_v2.txt", lineSplit[0] + "\t" + text + Environment.NewLine);
                }
            }
        }

        public static void replaceProvinceWithRealLocation(string dirRawLocationProvince, string dirLocation, string dirOutput)
        {
            // read all line location
            string[] arrLine = File.ReadAllLines(dirLocation);

            // for each province
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(@"D:\data_location_provinsi.txt");
            while ((line = file.ReadLine()) != null)
            {
                // for each location in province
                string location = "";
                System.IO.StreamReader file2 = new System.IO.StreamReader(dirRawLocationProvince + line + @"\data_location_real_v2.txt");
                while ((location = file2.ReadLine()) != null)
                {
                    string[] lineSplit = location.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

                    // replace location
                    int lineNumber = Convert.ToInt32(lineSplit[0]);
                    arrLine[lineNumber - 1] = lineSplit[1];
                }
            }

            // save new data location in new file
            File.WriteAllLines(dirOutput, arrLine);
        }
    }

    class Coordinate
    {
        public string lati { get; set; }
        public string longi { get; set; }
    }
}
