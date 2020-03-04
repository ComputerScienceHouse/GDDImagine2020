# How to use glipglop:
It is as simple as:
```cs
DeviceManager manager = new DeviceManager();
```
Yep that's it. 

## Adding functions to the different devices
Just use that manager and call AddPressed or AddReleased.
Let's say we have two functions defined:
```cs
static void Pressed()
{
    Console.WriteLine("Pressed");
}
static void Released()
{
    Console.WriteLine("Released");
}
```
Using that device manager we will call AddPressed, passing in that function as a "PressedDel" (The pressed delegate) as well as the device we want to assign that delegate to and piece on that device:
```cs
manager.AddPressed(new PressedDel(Pressed), "D0", "P0");
```

"D0" symbolizes the device that you want to assign a PressedDel. <br>
"P0" symbolizes the component on said device you want to assign that PressedDel. <br>
After this is done and the device is plugged in everytime you press one of the buttons, it will call the function(s) you assigned to the PressedDel. <br>
Also don't worry about having the device plugged in or not, it will start in a state where it is scanning all of your COM ports (USB ports) and anytime one is connected, it will create the device associated with it.   If you have any questions about that feel free to ask.

## Stopping the reading of input from the device:
Call the function:
```cs
manager.StopReading();
```
This will rejoin the thread it was running on to your program and not read the input anymore from the box. <br>
To start reading again just call:
```cs
manager.StartReading();
```
And the reading of input will resume.