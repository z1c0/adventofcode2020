using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("START");
            Part1();
            Part2();
            System.Console.WriteLine("DONE");
        }

        private static void Part1()
        {
            var passportDataList = ReadInput();
            var validCount = passportDataList.Count(p => p.IsValid());
            Console.WriteLine($"{validCount} of {passportDataList.Count} are valid");
        }
        private static void Part2()
        {
            var passportDataList = ReadInput();
            var validCount = passportDataList.Count(p => p.IsValidEx());
            Console.WriteLine($"{validCount} of {passportDataList.Count} are valid (ex)");
        }

        private static List<PassportData> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var passportDataList = new List<PassportData>();
            PassportData passportData = null;
            foreach (var line in lines)
            {
                if (passportData == null)
                {
                    passportData = new PassportData();
                    passportDataList.Add(passportData);
                }
                if (string.IsNullOrEmpty(line))
                {
                    passportData = null;
                }
                else 
                {
                    var parts = line.Split(" ");
                    foreach (var part in parts)
                    {
                        var tokens = part.Split(":");
                        switch (tokens[0])
                        {
                            case "ecl": 
                                passportData.EyeColor = tokens[1];
                                break;
                            case "pid": 
                                passportData.PassportId = tokens[1];
                                break;
                            case "eyr": 
                                passportData.ExpirationYear = tokens[1];
                                break;
                            case "hcl": 
                                passportData.HairColor = tokens[1];
                                break;
                            case "byr": 
                                passportData.BirthYear = tokens[1];
                                break;
                            case "iyr": 
                                passportData.IssueYear = tokens[1];
                                break;
                            case "cid": 
                                passportData.CountryId = tokens[1];
                                break;
                            case "hgt": 
                                passportData.Height = tokens[1];
                                break;
                        }
                    }
                }
            }
            return passportDataList;
        }
    }
}