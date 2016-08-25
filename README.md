# ioRPC

`.NET 4.5+` [![Build status](https://ci.appveyor.com/api/projects/status/3okbwy9pncwgvs3a?svg=true)](https://ci.appveyor.com/project/donovansolms/iorpc) `Mono` [![Build Status](https://travis-ci.org/ProjectLimitless/ioRPC.svg?branch=master)](https://travis-ci.org/ProjectLimitless/ioRPC)

[![Coverity status](https://scan.coverity.com/projects/9992/badge.svg)](https://scan.coverity.com/projects/projectlimitless-iorpc)

A minimalistic cross-platform remote procedure call library that works over the standard input/output of processes.

## When to use ioRPC?

When you require Inter-process communication (IPC) on .NET across Windows, Linux and macOS for controlling a subprocess. Pipes would be easier, but is unsupported on Mono.

## Usage

See the `Samples` folder. Contains a basic 'Server' and subprocess 'Client' that implements a basic 'Add' function.

---
*A part of Project Limitless*

[![Project Limitless](https://www.donovansolms.com/downloads/projectlimitless.jpg)](https://www.projectlimitless.io)

[https://www.projectlimitless.io](https://www.projectlimitless.io)

