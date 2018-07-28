using CommandLine;
using DEnc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace dencli
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments<Options>(args)
                    .WithParsed(o => RunWithOptions(o))
                    .WithNotParsed(e => HandleParseError(e));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                Environment.Exit(4);
            }
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            if (errs.Count() == 1)
            {
                var e = errs.First();
                if (e.Tag == ErrorType.VersionRequestedError || e.Tag == ErrorType.HelpRequestedError)
                {
                    Environment.Exit(1);
                }
            }

            Console.Error.WriteLine("Failed to parse command:");
            Console.Error.Write(string.Join('\n', errs));

            Environment.Exit(2);
        }

        private static void RunWithOptions(Options options)
        {
            string workingDirectory = options.TempDir ?? Environment.CurrentDirectory;
            string outputPath = options.OutputPath ?? Path.GetDirectoryName(options.InputFile);
            string outputName = options.OutputFilename ?? Path.GetFileNameWithoutExtension(options.InputFile);

            if (!File.Exists(options.InputFile))
            {
                Console.Error.WriteLine("Input file doesn't exist.");
                return;
            }

            var outputCallback = new Action<string>((x) => { Console.WriteLine(x); });
            var errorCallback = new Action<string>((x) => { Console.Error.WriteLine(x); });

            var encoder = new Encoder(options.FFmpegPath, options.FFprobePath, options.BoxPath, outputCallback, errorCallback, workingDirectory)
            {
                DisableQualityCrushing = options.DisableCrushing,
                EnableStreamCopying = options.EnableStreamCopying
            };
            var qualities = Quality.GenerateDefaultQualities(options.Quality, options.Preset);

            var result = encoder.GenerateDash(options.InputFile, outputName, options.Framerate, options.KeyInterval, qualities, outputPath);

            if (result != null)
            {
                HandleDelete(options.DeleteBehavior, options.InputFile);

                Console.WriteLine("MPD Generated: " + result.DashFilePath);
                Console.WriteLine("Media files:");
                Console.Write(string.Join('\n', result.MediaFiles));

                Environment.Exit(0);
            }

            Console.Error.WriteLine("Failed to produce files. Exiting...");
            Environment.Exit(3);
        }

        private static void HandleDelete(DeleteMode d, string file)
        {
            switch (d)
            {
                case DeleteMode.truncate:
                    File.WriteAllBytes(file, new byte[0]);
                    break;
                case DeleteMode.delete:
                    File.Delete(file);
                    break;
                default:
                    break;
            }
        }
    }
}
