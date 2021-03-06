﻿// FieldLog – .NET logging solution
// © Yves Goergen, Made in Germany
// Website: http://unclassified.software/source/fieldlog
//
// This library is free software: you can redistribute it and/or modify it under the terms of
// the GNU Lesser General Public License as published by the Free Software Foundation, version 3.
//
// This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along with this
// library. If not, see <http://www.gnu.org/licenses/>.

using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyProduct("FieldLog")]
[assembly: AssemblyTitle("FieldLog")]
[assembly: AssemblyDescription("FieldLog library")]
[assembly: AssemblyCopyright("© {copyright:2013} Yves Goergen, GNU LGPL v3")]
[assembly: AssemblyCompany("unclassified software development")]

// Assembly identity version. Must be a dotted-numeric version.
[assembly: AssemblyVersion("1.0")]

// Repeat for Win32 file version resource because the assembly version is expanded to 4 parts.
[assembly: AssemblyFileVersion("1.0")]

// Informational version string, used for the About dialog, error reports and the setup script.
// Can be any freely formatted string containing punctuation, letters and revision codes.
[assembly: AssemblyInformationalVersion("1.{dmin:2015}_{chash:6}{!:+}")]

#if NET20
#if DEBUG
[assembly: AssemblyConfiguration("Debug, NET20")]
#else
[assembly: AssemblyConfiguration("Release, NET20")]
#endif
#elif ASPNET
#if DEBUG
[assembly: AssemblyConfiguration("Debug, ASPNET40")]
#else
[assembly: AssemblyConfiguration("Release, ASPNET40")]
#endif
#else
#if DEBUG
[assembly: AssemblyConfiguration("Debug, NET40")]
#else
[assembly: AssemblyConfiguration("Release, NET40")]
#endif
#endif

// Other attributes
[assembly: ComVisible(false)]
