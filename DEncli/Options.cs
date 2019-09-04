using CommandLine;
using CommandLine.Text;
using DEnc;
using System;
using System.Collections.Generic;
using System.Text;

namespace dencli
{
    public enum DeleteMode
    {
        none,
        truncate,
        delete
    }

    public class Options
    {
        [Usage(ApplicationAlias = ">  dotnet dencli.dll")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Simple usage", new Options { InputFile = "video.mkv" });
                yield return new Example("Complex usage", new Options { InputFile = "video.mkv", DeleteBehavior = DeleteMode.truncate, Preset = "ultrafast", Quality = DefaultQuality.potato });
            }
        }

        [Option('i', "input", Required = true, HelpText = "Input file to be encoded.")]
        public string InputFile { get; set; }

        [Option('o', "output", Default = null, HelpText = "The directory to write output files at. Defaults to the input file directory.")]
        public string OutputPath { get; set; }

        [Option("outfile", Default = null, HelpText = "The base filename to use for the output files. Defaults to input filename.")]
        public string OutputFilename { get; set; }

        [Option('d', "delete", Default = DeleteMode.none, HelpText = "Specify what should be done to the source on successful encode. Allows truncate or delete.")]
        public DeleteMode DeleteBehavior { get; set; }

        [Option('p', "preset", Default = "medium", HelpText = "Specify the ffmpeg preset.")]
        public string Preset { get; set; }

        [Option('q', "quality", Default = DefaultQuality.medium, HelpText = "Specify the quality band to use. Allows potato, low, medium, high, ultra.")]
        public DefaultQuality Quality { get; set; }

        [Option("disablecopying", Default = false, HelpText = "Disables automatic detection of copyable input streams.")]
        public bool DisableStreamCopying { get; set; }

        [Option("disablecrush", Default = false, HelpText = "Disables quality crushing. May lead to output files larger than the input.")]
        public bool DisableCrushing { get; set; }

        [Option("framerate", Default = 24, HelpText = "Specify the framerate to use.")]
        public int Framerate { get; set; }

        [Option("keyinterval", Default = 96, HelpText = "Specify the keyframe interval to use.")]
        public int KeyInterval { get; set; }

        [Option("ffmpeg", Default = "ffmpeg", HelpText = "The path to ffmpeg.")]
        public string FFmpegPath { get; set; }

        [Option("ffprobe", Default = "ffprobe", HelpText = "The path to ffprobe.")]
        public string FFprobePath { get; set; }

        [Option("mp4box", Default = "MP4Box", HelpText = "The path to MP4Box.")]
        public string BoxPath { get; set; }

        [Option("temp", Default = null, HelpText = "Specify the temp directory. Defaults to current directory.")]
        public string TempDir { get; set; }
    }
}