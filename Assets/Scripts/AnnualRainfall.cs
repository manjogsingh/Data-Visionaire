using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class AnnualRainfall:MonoBehaviour
{
    public string y2000,y2001,y2002,y2003,y2004,y2005,y2006,y2007,y2008,y2009,y2010,y2011,y2012,y2013,y2014,y2015;
	public Dictionary<string,string> year=new Dictionary<string,string>();
	
	void Start()
	{
		set();
	}
	void Update()
	{
		year["2000"]=y2000;
		year["2001"]=y2001;
		year["2002"]=y2002;
		year["2003"]=y2003;
		year["2004"]=y2004;
		year["2005"]=y2005;
		year["2006"]=y2006;
		year["2007"]=y2007;
		year["2008"]=y2008;
		year["2009"]=y2009;
		year["2010"]=y2010;
		year["2011"]=y2011;
		year["2012"]=y2012;
		year["2013"]=y2013;
		year["2014"]=y2014;
		year["2015"]=y2015;
	}
	void set(){
		year.Clear();
		year.Add("2000",y2000);
		year.Add("2001",y2001);
		year.Add("2002",y2002);
		year.Add("2003",y2003);
		year.Add("2004",y2004);
		year.Add("2005",y2005);
		year.Add("2006",y2006);
		year.Add("2007",y2007);
		year.Add("2008",y2008);
		year.Add("2009",y2009);
		year.Add("2010",y2010);
		year.Add("2011",y2011);
		year.Add("2012",y2012);
		year.Add("2013",y2013);
		year.Add("2014",y2014);
		year.Add("2015",y2015);
	}
}