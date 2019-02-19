
using System.Collections.Generic;

public static class CrossSceneInformation {
    // CrossSceneInformation ist eine statische Klasse, damit wird eine
    // globale Variable erzeugt mit allen Informationen die Zwischengespeichert
    // werden sollen. Die Anwendung wird durch diese Klasse dynamisiert.

    // Auswahl der Fakultät
    public static string faculty;

    // Auswahl des Studiengangs
    public static string direction;

    // Auswahl des Semesters
    public static string semester;

    // Auswahl der Vertiefung
    public static string extension;

    // Auswahl des Moduls
    public static string modul;

    // Video Name
    public static string video;

    // Jump Modulname
    public static string jump;

    // Informationen der Anzeigetafel
    public static List<string> anzeige = new List<string>();
}
