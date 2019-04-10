﻿// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see licence.txt in the main folder

using System;
using System.Linq;
using System.Security.Principal;
using NLog;
using NLog.Fluent;

namespace AAEmu.Commons.Utils
{
    public static class CliUtil
    {
        private const string TitlePrefix = "ARCHEAGE SERVER : ";
        private static Logger _log = LogManager.GetCurrentClassLogger();

        private static readonly string[] Logo = new string[]
        {
            @"    ___                                 ___                      ",
            @"   / _ \             __                / _ \                     ",
            @"  / /_\ \  __  _  __/\ \  __    ___   / /_\ \    ___     ___     ",
            @" /\ \__\ \/\ \/ \/ __)\ \/  \  / __ \/\ \__\ \  / __ \  / __ \   ",
            @" \ \ \_/\ \ \  //\ \_\ \  /\ \/\  __/\ \ \_/\ \/\ \_\ \/\  __/   ",
            @"  \ \_\\ \_\ \_\\ \___) \_\ \_\ \____\\ \_\\ \_\ \__/\ \ \____\  ",
            @"   \/_/ \/_/\/_/ \/__/ \/_/\/_/\/___/  \/_/ \/_/\/__/_\ \/____/  ",
            @"                                                    /\___\       ",
            @"                                                    \/___/       ",
        };

        private static readonly string[] Credits = new string[]
        {
            @"by the AAEmu Project",
        };

        /// <summary>
        /// Writes logo and credits to Console.
        /// </summary>
        /// <param name="color">Color of the logo.</param>
        public static void WriteHeader(string consoleTitle, ConsoleColor color)
        {
            Console.Title = TitlePrefix + consoleTitle;

	        WriteSeperator();

	        Console.ForegroundColor = color;
            WriteLinesCentered(Logo);

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            WriteLinesCentered(Credits);

            Console.ResetColor();

	        WriteSeperator();
        }

        /// <summary>
        /// Writes seperator in form of 80 underscores to Console.
        /// </summary>
        public static void WriteSeperator()
        {
            Console.WriteLine("".PadLeft(Console.WindowWidth, '_'));
        }

        /// <summary>
        /// Writes lines to Console, centering them as a group.
        /// </summary>
        /// <param name="lines"></param>
        private static void WriteLinesCentered(string[] lines)
        {
            var longestLine = lines.Max(a => a.Length);
            foreach (var line in lines)
            {
                WriteLineCentered(line, longestLine);
            }
        }

        /// <summary>
        /// Writes line to Console, centering it either with the string's
        /// length or the given length as reference.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="referenceLength">Set to greater than 0, to use it as reference length, to align a text group.</param>
        private static void WriteLineCentered(string line, int referenceLength = -1)
        {
            if (referenceLength < 0)
            {
                referenceLength = line.Length;
            }

            Console.WriteLine(line.PadLeft(line.Length + Console.WindowWidth / 2 - referenceLength / 2));
        }

        public static void LoadingTitle()
        {
            if (!Console.Title.StartsWith("* "))
            {
                Console.Title = "* " + Console.Title;
            }
        }

        public static void RunningTitle()
        {
            Console.Title = Console.Title.TrimStart('*', ' ');
        }

        /// <summary>
        /// Waits for the return key, and closes the application afterwards.
        /// </summary>
        /// <param name="exitCode"></param>
        /// <param name="wait"></param>
        public static void Exit(int exitCode, bool wait = true)
        {
            if (wait)
            {
                _log.Info("Press Enter to exit.");
                Console.ReadLine();
            }
            _log.Info("Exiting...");
            Environment.Exit(exitCode);
        }

        /// <summary>
        /// Returns whether the application runs with admin rights or not.
        /// </summary>
        public static bool CheckAdmin()
        {
            var id = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(id);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}