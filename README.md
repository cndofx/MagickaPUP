# MagickaPUP

A program to pack and unpack XNB files for the videogame Magicka. Uses JSON files as output text format for easy user editing and as intermediate communication format between MagickaPUP and MagickCow.

## Changes

- Updated from .NET Framework 4.8 to .NET 9
- Replaced `System.Drawing.Imaging.Bitmap` with `SixLabors.ImageSharp.Image`
- Removed references to DirectX and Forms (looking for a way to conditionally support only on windows)
