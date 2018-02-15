using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuRotation : MonoBehaviour {

	public int[] year = { 2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015 };
	public int index = 0;
	public Text currentYear, previousYear, nextYear;

	void Start () {
		
		currentYear.text = year[index].ToString ();
		nextYear.text = year[index + 1].ToString ();
		previousYear.text = (year.Length - 1).ToString ();
	}
	public void MenuLeft () {
		currentYear.text = year[++index].ToString ();
		previousYear.text = year[index - 1].ToString ();
		if (index + 1 >= year.Length) {
			nextYear.text = year[0].ToString ();
		} else {
			nextYear.text = (index + 1).ToString ();
		}

	}
	public void MenuRight () {
		currentYear.text = year[--index].ToString ();
		previousYear.text = year[index + 1].ToString ();
		if (index - 1 < 0) {
			previousYear.text = year[year.Length - 1].ToString ();
		} else {
			previousYear.text = (index - 1).ToString ();
		}
	}
}