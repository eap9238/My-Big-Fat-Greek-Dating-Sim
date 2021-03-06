﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class ClockTime : MonoBehaviour {

    public Fungus.Flowchart myFlowchart;
    private Text myText;

    private int minute;
    private int hour;
    private int day;
    private int week;

    // Use this for initialization
    void Start ()
    {
        myText = gameObject.GetComponent<Text>();

        //myFlowchart.SetIntegerVariable("minute", myFlowchart.GetIntegerVariable("minute") + 5);

        minute = myFlowchart.GetIntegerVariable("minute");
        hour = myFlowchart.GetIntegerVariable("hour");
        day = myFlowchart.GetIntegerVariable("day");
        week = myFlowchart.GetIntegerVariable("week");

        FixTime();
    }

    private void Update()
    {

    }

    public void AddMinute (int min)
    {
        minute += min;

		//Debug.Log ("Time updated from " + (minute - 5) + " to " + minute);

        FixTime();
    }

	void ShiftRooms()
	{
		minute += 5;

		//Debug.Log ("Time updated from " + (minute - 5) + " to " + minute);

		FixTime();
	}

    void FixTime()
    {
        if (minute >= 60)
        {
            minute -= 60;
            hour += 1;
        }

        if (hour == 24)
        {
            hour = 0;
            day += 1;
        }

        if (day > 7)
        {
            day = 1;
            week += 1;
        }

        myText.text = hour.ToString("D2") + " : " + minute.ToString("D2") + System.Environment.NewLine + "Day " + day + " Week " + week;

        myFlowchart.SetIntegerVariable("minute", minute);
        myFlowchart.SetIntegerVariable("hour", hour);
        myFlowchart.SetIntegerVariable("day", day);
        myFlowchart.SetIntegerVariable("week", week);
    }

	public void NextMorning()
	{
		hour = 8;

		minute = 0;

		day++;

		FixTime ();
	}

}
