/*
    Alec Pagliarussi
    ID: 101196746
    Daniel Lee
    ID: 100517557
    Edward Entecott
    ID: 101190443
 */

using System;
using System.IO;

namespace ScantronProject {
    class Program {
        static int calculateScore (string key, string stuAnswer, int[] questCounter) {
            // calculates the score and add if correct add 1 to respective question counter
            int score = 0;
            for (int i = 0; i < stuAnswer.Length; i++) {
                if (stuAnswer[i] == 'X') {
                    // X is no + or -
                } else if (stuAnswer[i] == key[i]) {
                    score += 4;
                    questCounter[i] = questCounter[i] + 1;
                } else {
                    score -= 1;
                }
            }
            return score;
        }
        static string[] parseLine (string line) {
            string[] param = { " ", "\t" };
            string[] pair = line.Split (param, StringSplitOptions.RemoveEmptyEntries);
            return pair;
        }
        static bool scanFile (string path) {
            string line;
            string[] splitLine;
            int numStudents = 0;
            int[] questCounter;
            string key;
            string output = "";
            // Instantiate stream reader
            StreamReader sr;
            try {
                sr = new StreamReader (path);
                line = sr.ReadLine ();
                key = line;
                questCounter = new int[key.Length];
                // Read line -> parse line -> calculate score -> add id and score to string
                while (line != null) {
                    line = sr.ReadLine ();
                    splitLine = parseLine (line);
                    if (splitLine[0] == "0") break;
                    numStudents++;
                    output += $"{splitLine[0]}\t|\t{calculateScore (key, splitLine[1], questCounter)}\n";
                }
            } catch (Exception e) {
                Console.WriteLine (e);
                return false;
            }
            sr.Close ();
            // Output report
            Console.WriteLine ("ID\t|\tSCORE");
            Console.WriteLine (output);
            Console.WriteLine ("Total number of students: {0}", numStudents);
            Console.WriteLine ("Number of correct responses per question: ");
            // Accepts any number of questions (not fixed to 20)
            for (int j = 0; j < questCounter.Length / 10; j++) {
                Console.Write ("Question:\t");
                for (int i = 0 + (j * 10); i < questCounter.Length && i < j * 10 + 10; i++) {
                    Console.Write ("{0}\t", i + 1);
                }
                Console.Write ("\n#Correct:\t");
                for (int i = 0 + (j * 10); i < questCounter.Length && i < j * 10 + 10; i++) {
                    Console.Write ("{0}\t", questCounter[i]);
                }
                Console.WriteLine ("\n");
            }

            return true;
        }
        static void Main (string[] args) {
            scanFile ("exam.txt");
            Console.ReadKey ();
        }
    }
}