using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertLocation
{
    class Program
    {
        static void Main(string[] args)
        {
            //string directory = @"D:\output_convert\";
            //string fileName = "ID_data_test_10.txt";
            ////string directory = @"D:\data_all_cleaned\";
            string gazetteer = "ID.txt";
            ////string fileName = "data_location_distinct.txt";
            //string fileName = "data_location.txt";
            ////string outputFile = "data_location_coordinate_ii.txt";
            //string outputFile = "data_location_distinct.txt";
            //string outputFile = "data_location_distinct_iii.txt";

            //string dirFileName = @"D:\data_location_provinsi.txt";
            //string dirOutput = @"E:\output_location\data_province\";

            //string dirLocation = @"E:\output_real\data_location.txt";
            //string dirRawLocation = @"E:\output_real\data_location\data_location_raw.txt";
            //string dirRawLocationProvince = @"E:\output_location\data_province\";
            //string dirOutput = @"E:\output_real\data_location\data_location_complete_replace_province.txt";

            //string directory = @"E:\output_location\data_complete\data_location_v2_stage3_clean_empty_line\";
            //string fileName = @"data_location_v2_cleaned.txt";
            //string outputFile = @"data_location_v2_cleaned_distinct_v2.txt";

            // write number distinct location
            string directory = @"D:\data_all_terbaru\03-13-2016\data_location_v2_cleaned.txt";
            string output = @"D:\data_location_distinct_number.txt";
            DataController.writeNumberDistinctLocation(directory, output);

            //// write distinct location
            //List<string> lines = DataController.getLinesFromFile(directory + fileName);
            //Console.WriteLine(lines.Count);

            //lines = lines.Distinct().ToList();
            //Console.WriteLine(lines.Count);

            //DataController.writeDistinctLocation(lines, directory, outputFile);

            //// get real location from raw province
            ////DataController.getLocationFromRawProvince(dirRawLocationProvince);
           
            //// replace province with location
            //DataController.replaceProvinceWithRealLocation(dirRawLocationProvince, dirLocation, dirOutput);

            //// save raw location each province
            //DataController.saveLocationRawProvince(dirLocation, dirRawLocation, dirRawLocationProvince);

            //// make dir province
            //DataController.createDirectory(dirFileName, dirOutput);

            //// read lines
            //List<string> lines = DataController.getLinesFromFile(directory + fileName);

            //// write the coordinate to the file
            //DataController.writeCoordinateLocation(lines, directory, @"data_all_cleaned\" + gazetteer, outputFile);

            //// split one line
            //string[] words = DataController.getSplitLine(lines[8], "\t");

            //foreach(string word in words)
            //{
            //    Console.WriteLine(word);
            //}
            //Console.WriteLine(lines.Count);
            //lines = lines.Distinct().ToList();
            //Console.WriteLine(lines.Count);

            //DataController.writeDistinctLocation(lines, directory, outputFile);

            //string placeName = "jakarta";
            //Coordinate coordinate = new Coordinate();
            //coordinate = DataController.getCoordinateFromPlaceName(placeName, directory + gazetteer);
            //Console.WriteLine(coordinate.lati);
            //Console.WriteLine(coordinate.longi);

            Console.WriteLine("Selesai!");
            Console.ReadLine();
        }
    }
}
