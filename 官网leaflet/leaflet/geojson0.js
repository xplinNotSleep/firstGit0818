/*定义点要素的geojson对象*/
var geojsonFeature0=
{
	"type":"Feature",
	"properties":
	{
		"popupContent": "This is a point!"
	},
	"geometry":
	{
		"type":"Point",
		"coordinates": [-104.99204, 39.75621]
	}
};
var geojsonFeature1=
{
	"type":"Feature",
	"properties":
	{
		"popupContent": "This is another point!"
	},
	"geometry":
	{
		"type":"Point",
		"coordinates": [-104.998, 39.733]
	}
};

var geojsonFeature2=
{
	"type":"Feature",
	"properties":
	{
		"popupContent": "This is a point with buffer!"
	},
	"geometry":
	{
		"type":"Point",
		"coordinates": [-105.0423, 39.748]
	}
};

/*定义多边形要素*/
var geojsonPolygon1=
{
	"type":"Feature",
	"properties":
	{
		"popupContent": "This is a polygon!",
		"style":
		{
			weight:5,
			color:"#999",
			opacity:1,
			fillColor: "#B0DE5C",
			fillOpacity: 0.8
		}
	},
	"geometry":
	{
		"type":"Polygon",
		"coordinates": 
		[
			[
				[-105.00432014465332, 39.74732195489861],
				[-105.00715255737305, 39.75620006835170],
				[-105.00921249389647, 39.74468219277038],
				[-105.00432014465332, 39.71732195489861]
			]
		]
		
	}
};

/*定义点集要素*/
var geojsonPtCol1=
{
    "type":"FeatureCollection",
	"features":
	[
		{
			"type":"Feature",
			"properties":
			{
				"popupContent": "sPoint!",
			},
			"geometry":
			{
				"type":"Point",
				"coordinates": 
					[-104.9923, 39.761]
			}
		},
		{
			"type":"Feature",
			"properties":
			{
				"popupContent": "ePoint!",
			},
			"geometry":
			{
				"type":"Point",
				"coordinates": 
					[-104.9823, 39.754]
				
			}
		},
		
		
	]	

};


/*定义线段要素*/
var geojsonPoline1=
{
    "type":"Feature",
	"geometry":
	{
		"type":"LineString",
		"coordinates":
		[[-104.98,39.74],[-104.91,39.78]]
	},
	"properties":
	{
		"popupContent":"定义的线段",
		
	}

};