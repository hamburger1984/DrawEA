using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("DrawEA")]
[assembly: AssemblyDescription("a little tool to draw finite state machines")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("dev_HH")]
[assembly: AssemblyProduct("DrawEA")]
[assembly: AssemblyCopyright("Copyright © 2006-2008 Andreas K. (mailto:Hamburger1984@gmail.com)")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("43383b3e-869c-412a-ba03-41244133b88c")]

[assembly: AssemblyVersion("0.3.9.4")]
[assembly: AssemblyFileVersion("0.3.9.4")]

/*
 * 0.3.9.4 (24.07.2008) Andreas Krohn
 *  - Drawing speed increased
 *  - Switches from & to the selected state are editable now
 * 
 * 0.3.9.3 (09.07.2008) Andreas Krohn
 *  - Switch & state editing improved (movements are painted immediately)
 *  - Resharped ;-)
 *  - Examples included
 *  - About-dialog
 * 
 * 0.3.9.2 (12.11.2006) Andreas Krohn
 *  - Switches can be edited now (move the control points of the bezier curves)
 *  - Drawing the finite state machines was extracted into a separate control (StateUI)
 * 
 * 0.3.9.1 (08.11.2006) Andreas Krohn
 *  - active state is highlighted during testing
 *  - output is now in the right order (was reversed)
 *  - Saving NFAs is possible again.. dumb error.. was using sfd.FileName instead of sfd.Filter
 * 
 * 0.3.9.0 (07.11.2006) Andreas Krohn
 *  - simple testing tool added
 *      1. input accepted/not accepted
 *      2. ouput
 *      3. states hit to get to the final state
 *      (may run into infinite recursions using epsilon or any other case I didn't find/catch)
 * 
 * 0.3.1.2 (04.11.2006) Andreas Krohn
 *  - epsilon added
 * 
 * 0.3.1.1 (04.11.2006) Andreas Krohn
 *  - SafeFileDialog for DFA was using the file-extension "dea" (instead of "dfa")
 * 
 * 0.3.1.0 (04.11.2006) Andreas Krohn
 *  - Font & Size can be changed now
 * 
 * 0.3.0.0 (04.11.2006) Andreas Krohn
 *  - now also has a nfa-mode
 *  - validation improved & some error cases avoided
 * 
 * 0.2.1.0 (03.11.2006) Andreas Krohn
 *  - auf Wunsch einer besonderen Person gibt's das Programm jetzt auch in Deutsch 
 *      …wer's in weitere Sprachen übersetzen will - nehmt die "lang.resx" Datei im Ordner Resources als Vorlage
 * 
 * 0.2.0.0 (02.-03.11.2006) Andreas Krohn
 *  - Drawing of switches improved
 *  - Save & Load in files
 * 
 * 0.1.0.0 (01.-02.11.2006) Andreas Krohn
 *  Initial Version
 *      Usage:
 *      - Create a column for every member of the input alphabet
 *      - Edit states & switches in the table
 *      - Press "Build"
 *      - Use your Mouse to rearrange the states
 *      - Press "Export" to generate a file
 *      Known Issues:
 *      - switches and their labels could be arranged much better in some situations
 * 
 * 
 */

