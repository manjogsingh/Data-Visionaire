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
		previousYear.text = year[(year.Length - 1)].ToString ();
	}
	public void MenuLeft () {
		index++;

		if (index == 0) {
			currentYear.text = year[index].ToString ();
			nextYear.text = year[index + 1].ToString ();
			previousYear.text = year[year.Length - 1].ToString ();
		} else if (index == year.Length - 1) {
			currentYear.text = year[index].ToString ();
			previousYear.text = year[index - 1].ToString ();
			nextYear.text = year[0].ToString ();
		} else if (index == year.Length) {
			index = 0;
			currentYear.text = year[index].ToString ();
			nextYear.text = year[index + 1].ToString ();
			previousYear.text = year[year.Length - 1].ToString ();
		} else if (index == -1) {
			index = year.Length - 1;
			currentYear.text = year[index].ToString ();
			nextYear.text = year[0].ToString ();
			previousYear.text = year[index - 1].ToString ();
		} else {
			currentYear.text = year[index].ToString ();
			nextYear.text = year[index + 1].ToString ();
			previousYear.text = year[index - 1].ToString ();
		}
	}
	public void MenuRight () {
		index--;

		if (index == 0) {
			currentYear.text = year[index].ToString ();
			nextYear.text = year[index + 1].ToString ();
			previousYear.text = year[year.Length - 1].ToString ();
		} else if (index == year.Length - 1) {
			currentYear.text = year[index].ToString ();
			previousYear.text = year[index - 1].ToString ();
			nextYear.text = year[0].ToString ();
		} else if (index == year.Length) {
			index = 0;
			currentYear.text = year[index].ToString ();
			nextYear.text = year[index + 1].ToString ();
			previousYear.text = year[year.Length - 1].ToString ();
		} else if (index == -1) {
			index = year.Length - 1;
			currentYear.text = year[index].ToString ();
			nextYear.text = year[0].ToString ();
			previousYear.text = year[index - 1].ToString ();
		} else {
			currentYear.text = year[index].ToString ();
			nextYear.text = year[index + 1].ToString ();
			previousYear.text = year[index - 1].ToString ();
		}
	}
}