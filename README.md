# dencli
>A reference front-end for the [DEnc](https://github.com/bloomtom/DEnc) library.

This application is a CLI front-end for the DEnc DASH encoder library. Multi-transcode your media files into the [MPEG DASH](https://en.wikipedia.org/wiki/Dynamic_Adaptive_Streaming_over_HTTP) format with a single command!


## Requirements
You'll need an ffmpeg, ffprobe and mp4box executable on the system you're running from. If you put these in your path variable then the defaults can be left.

You can get executables for your platform here:
 - [ffmpeg](https://ffmpeg.org/)
 - [mp4box](https://gpac.wp.imt.fr/downloads/)

You'll also need dotnet core if you want to compile from source.
A tutorial for installing dotnet using package management [can be found here](https://www.microsoft.com/net/learn/get-started-with-dotnet-tutorial). If you just want the binaries you can [find those here](https://www.microsoft.com/net/download/dotnet-core/2.1).

## Installation
Releases for 64 bit Windows and Linux are available on this repository. They are self-contained, and do not require the dotnet runtime to be installed.

If you want to build from source instead, the following commands should get you off the ground.

```
git clone https://github.com/bloomtom/dencli.git
cd dencli
dotnet build --configuration Release
```


## Command Options
The following is an output of the --help option.
```
dencli 0.3.1
MIT License - Copyright 2019 bloomtom
USAGE:
Simple usage:
  >  dotnet dencli.dll --input video.mkv
Complex usage:
  >  dotnet dencli.dll --delete truncate --input video.mkv --preset ultrafast

  -i, --input         Required. Input file to be encoded.

  -o, --output        The directory to write output files at. Defaults to the input file directory.

  --outfile           The base filename to use for the output files. Defaults to input filename.

  -d, --delete        (Default: none) Specify what should be done to the source on successful encode. Allows truncate or delete.

  -p, --preset        (Default: medium) Specify the ffmpeg preset.

  -q, --quality       (Default: medium) Specify the quality band to use. Allows potato, low, medium, high, ultra.

  --disablecopying    (Default: false) Disables automatic detection of copyable input streams.

  --disablecrush      (Default: false) Disables quality crushing. May lead to output files larger than the input.

  --framerate         (Default: 24) Specify the framerate to use.

  --keyinterval       (Default: 96) Specify the keyframe interval to use.

  --ffmpeg            (Default: ffmpeg) The path to ffmpeg.

  --ffprobe           (Default: ffprobe) The path to ffprobe.

  --mp4box            (Default: MP4Box) The path to MP4Box.

  --temp              Specify the temp directory. Defaults to current directory.

  --help              Display this help screen.

  --version           Display version information.
```

## Common Errors

##### Missing Requirements
You might see the message:
```
System.ComponentModel.Win32Exception (2): No such file or directory
   at System.Diagnostics.Process.StartCore(ProcessStartInfo startInfo)
   at System.Diagnostics.Process.Start()
```
Ensure ffmpeg, ffprobe, and mp4box are in your path or are specified in the option flags.
