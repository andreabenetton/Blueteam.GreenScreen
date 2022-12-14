# Blueteam.GreenScreen

GreenScreen is a Energy Saving Overlay blazor component that allows your monitor to consume less energy when idle

Usage: 
```
<GreenScreen Timeout="new TimeOnly(0,0,15)" 
             ScreenActivated="@MethodToBeCalled()"/>
					
```
The ScreenActivated event allows the application using the component, for example, to save a draft of the current activity or to close the work session in an orderly manner. 

---
[![GNU Lesser General Public License](https://github.com/andreabenetton/Blueteam.GreenScreen/blob/main/lgplv3-147x51.png?raw=true)](https://www.gnu.org/licenses/lgpl-3.0.en.html)
The project source code is licensed under a [GNU Lesser General Public License v3.0 or later](https://www.gnu.org/licenses/lgpl-3.0.txt).

BlueTeam normally releases its sources under the GNU Affero General Public License v3.0 or later. This project is an exception for the public utility it could have.

---
[![Creative Commons License](https://github.com//andreabenetton/Blueteam.GreenScreen/blob/main/CC-BY-SA-88x31.png?raw=true)](http://creativecommons.org/licenses/by-sa/4.0/)
The project documentation is licensed under a [Creative Commons Attribution-ShareAlike 4.0 International License](http://creativecommons.org/licenses/by-sa/4.0/).