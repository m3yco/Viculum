using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CrossSceneInformation {
    // Auswahl der Fakultät
    public static string faculty
    {
        get
        {
            return faculty;
        }
        set
        {
            faculty = value;
        }
    }

    // Auswahl des Studiengangs
    public static string direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
        }
    }

    // Auswahl des Semesters
    public static string semester
    {
        get
        {
            return semester;
        }
        set
        {
            semester = value;
        }
    }

    // Auswahl des Moduls
    public static string modul
    {
        get
        {
            return modul;
        }
        set
        {
            modul = value;
        }
    }
}
